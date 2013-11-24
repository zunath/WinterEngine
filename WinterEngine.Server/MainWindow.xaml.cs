using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.ViewModels;
using WinterEngine.DataTransferObjects.XMLObjects;
using WinterEngine.Library.Managers;
using WinterEngine.Library.Utility;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Listeners;
using Xceed.Wpf.Toolkit;

namespace WinterEngine.Server
{
    public partial class MainWindow : Window
    {
        #region Properties

        protected ServerViewModel ViewModel { get; set; }
        private WebServiceClientUtility WebUtility { get; set; }
        private OpenFileDialog OpenFile { get; set; }
        private DispatcherTimer MasterServerDispatcherTimer { get; set; }
        private DispatcherTimer GameServerDispatcherTimer { get; set; }
        private GameNetworkListener GameServerListener { get; set; }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<GameNetworkListenerProcessEventArgs> OnProcessingCycleStart;

        #endregion

        #region Event Handling

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new ServerViewModel();
            WebUtility = new WebServiceClientUtility();

            MasterServerDispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 2),
                DispatcherPriority.Normal,
                new EventHandler(SendServerDetailsToMasterServerAsync),
                Dispatcher.CurrentDispatcher);
            MasterServerDispatcherTimer.Stop();

            GameServerDispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 5),
                DispatcherPriority.Normal,
                new EventHandler(ProcessGameServer),
                Dispatcher.CurrentDispatcher);
            GameServerDispatcherTimer.Stop();

            OpenFile = new OpenFileDialog();
            InitializeOpenFileDialog();

            SetViewModelBindings();
            LoadSettings();
            UpdateExternalIPAddressAsync();
        }

        private void SetViewModelBindings()
        {
            textBoxServerName.DataContext = ViewModel;
            numericPort.DataContext = ViewModel;
            comboBoxPVPType.DataContext = ViewModel;
            listBoxPlayers.DataContext = ViewModel;
            listBoxGameType.DataContext = ViewModel;
            numericMaxPlayers.DataContext = ViewModel;
            numericMaxLevel.DataContext = ViewModel;
            checkBoxAllowCharacterDeletion.DataContext = ViewModel;
            checkBoxAllowFileAutoDownload.DataContext = ViewModel;
            textBoxPlayerPassword.DataContext = ViewModel;
            textBoxGMPassword.DataContext = ViewModel;
            textBoxServerMessage.DataContext = ViewModel;
            textBoxDescription.DataContext = ViewModel;
            textBoxAnnouncement.DataContext = ViewModel;
            listBoxBlacklist.DataContext = ViewModel;
            labelIPAddress.DataContext = ViewModel;
            textBoxServerStatus.DataContext = ViewModel;
            textBoxModuleFileName.DataContext = ViewModel;
            listBoxLog.DataContext = ViewModel;
            textBoxBlacklistUsername.DataContext = ViewModel;
        }

        private void BindGameServerEvents()
        {
            this.OnProcessingCycleStart += GameServerListener.ProcessCycleBegin;
            GameServerListener.OnProcessingCycleComplete += this.GameServerCycleCompleted;
        }

        private void UnbindGameServerEvents()
        {
            this.OnProcessingCycleStart -= GameServerListener.ProcessCycleBegin;
            GameServerListener.OnProcessingCycleComplete -= this.GameServerCycleCompleted;
        }


        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.ShowDialog();
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ServerMessage = textBoxServerMessage.Text;
            textBoxServerMessage.Text = "";
        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ServerStatus == ServerStatusEnum.Running)
            {
                StopServer();
            }
            else if(ViewModel.ServerStatus == ServerStatusEnum.Stopped)
            {
                StartServerAsync();
            }
        }

        private void buttonSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            XMLUtility.SerializeObjectToFile<ServerSettingsXML>(ViewModel.ServerSettings, FilePaths.ServerSettingsPath);
        }

        private void buttonBanAccount_Click(object sender, RoutedEventArgs e)
        {
            List<string> newBannedUsers = listBoxPlayers.SelectedItems.OfType<string>().ToList();
            ViewModel.QueuedBootUserList.AddRange(newBannedUsers);

            foreach (string username in newBannedUsers)
            {
                if(!ViewModel.ServerSettings.BannedUserAccounts.Contains(username))
                {
                    ViewModel.ServerSettings.BannedUserAccounts.Add(username);
                }
            }

            XMLUtility.SerializeObjectToFile<ServerSettingsXML>(ViewModel.ServerSettings, FilePaths.ServerSettingsPath);
        }

        private void buttonBootPlayer_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.QueuedBootUserList.AddRange(listBoxPlayers.SelectedItems.OfType<string>().ToList());
        }

        #endregion

        #region Methods

        private async void StartServerAsync()
        {
            bool success = false;
            ViewModel.LogMessages.Insert(0, "Starting server..");
            ViewModel.ServerStatusMessage = "Starting server...";
            ViewModel.ServerStatus = ServerStatusEnum.Starting;

            await TaskEx.Run(() => 
            {
                success = WebUtility.IsMasterServerAlive();
            });

            if (success)
            {
                buttonStartStop.Content = "Shutdown";
                SendServerDetailsAsync();
                GameServerListener = new GameNetworkListener(ViewModel.ServerSettings.PortNumber, ViewModel.ContentPackageList);
                BindGameServerEvents();

                GameServerDispatcherTimer.Start();
                MasterServerDispatcherTimer.Start();
                ViewModel.ServerStatusMessage = "Running...";
                ToggleServerStatusMode(true);
                ViewModel.LogMessages.Insert(0, "Server started up successfully! Running...");
            }
            else
            {
                ViewModel.ServerStatusMessage = "Unable to connect to master server...";
            }
        }

        private void StopServer()
        {
            ToggleServerStatusMode(false);
            buttonStartStop.Content = "Start Server";
            GameServerListener.Shutdown();

            GameServerDispatcherTimer.Stop();
            MasterServerDispatcherTimer.Stop();
            SendServerDetailsAsync();

            ViewModel.ServerStatusMessage = "Not started...";
            ViewModel.LogMessages.Insert(0, "Server stopped.");
        }

        private void InitializeOpenFileDialog()
        {
            FileExtensionFactory fileExtensionFactory = new FileExtensionFactory();
            OpenFile.Filter = fileExtensionFactory.BuildModuleFileFilter();

            OpenFile.AddExtension = true;
            OpenFile.Title = "Open Module";
            OpenFile.Multiselect = false;

            OpenFile.FileOk += LoadModule;
        }

        private void LoadModule(object sender, CancelEventArgs e)
        {
            string path = OpenFile.FileName;
            ViewModel.ModuleFileName = Path.GetFileNameWithoutExtension(path);

            ModuleManager manager = new ModuleManager();
            manager.OpenModule(path);

            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                ViewModel.ContentPackageList = repo.GetAll();
            }

            buttonStartStop.IsEnabled = true;
            buttonSendMessage.IsEnabled = true;
        }

        private void ToggleServerStatusMode(bool serverStarted)
        {
            if (serverStarted)
            {
                ViewModel.ServerStatus = ServerStatusEnum.Running;
            }
            else
            {
                ViewModel.ServerStatus = ServerStatusEnum.Stopped;
            }

            buttonBrowse.IsEnabled = !serverStarted;
            numericPort.IsEnabled = !serverStarted;

            textBoxServerMessage.IsEnabled = serverStarted;
            buttonBanAccount.IsEnabled = serverStarted;
            buttonBootPlayer.IsEnabled = serverStarted;
        }

        private async void UpdateExternalIPAddressAsync()
        {
            string externalIPAddress = "Unknown";
            await TaskEx.Run(() =>
            {
                externalIPAddress = WebUtility.GetExternalIPAddress();
            });

            ViewModel.ServerIPAddress = externalIPAddress;
        }

        private void LoadSettings()
        {
            if (File.Exists(FilePaths.ServerSettingsPath))
            {
                ViewModel.ServerSettings = XMLUtility.DeserializeFile<ServerSettingsXML>(FilePaths.ServerSettingsPath);
            }
            else
            {
                XMLUtility.SerializeObjectToFile<ServerSettingsXML>(ViewModel.ServerSettings, FilePaths.ServerSettingsPath);
            }
        }

        #endregion

        #region GUI Methods

        private void SetDefaultValues_MaxPlayers(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.Value);
        }

        private void SetDefaultValues_MaxLevel(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.Value);
        }

        private void SetDefaultValues_Port(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericPort.Text = Convert.ToString(numericPort.Value);
        }

        #endregion

        #region Game server methods

        private void ProcessGameServer(object sender, EventArgs e)
        {
            if (ViewModel.ServerStatus == ServerStatusEnum.Running)
            {
                if (OnProcessingCycleStart != null)
                {
                    // Send data from server application to game network thread.
                    GameNetworkListenerProcessEventArgs updatedDataEventArgs = new GameNetworkListenerProcessEventArgs
                    {
                        ServerIPAddress = ViewModel.ServerIPAddress,
                        ServerPort = ViewModel.ServerSettings.PortNumber,
                        BanUserList = ViewModel.ServerSettings.BannedUserAccounts.ToList(),
                        BootUserList = ViewModel.QueuedBootUserList,
                        ServerMessage = ViewModel.ServerMessage,
                        ServerAnnouncement = ViewModel.ServerSettings.Announcement,
                        ServerName = ViewModel.ServerSettings.Name,
                    };

                    OnProcessingCycleStart(this, updatedDataEventArgs);
                }

                GameServerListener.Process();
            }
        }

        private void GameServerCycleCompleted(object sender, GameNetworkListenerProcessEventArgs e)
        {
            RefreshLogMessages(e.LogMessages);
            RefreshPlayerList(e.PlayerList);
        }

        private void RefreshLogMessages(List<string> messages)
        {
            foreach (string message in messages)
            {
                ViewModel.LogMessages.Insert(0, "(" + DateTime.Now + ") " + message);
            }
        }

        private void RefreshPlayerList(List<string> updatedUsernames)
        {
            // Usernames to remove
            List<string> usernames = ViewModel.ConnectedUsernames.Where(x => !updatedUsernames.Contains(x)).ToList();
            foreach (string username in usernames)
            {
                ViewModel.ConnectedUsernames.Remove(username);
            }
            // Usernames to add
            usernames = updatedUsernames.Where(x => !ViewModel.ConnectedUsernames.Contains(x)).ToList();

            foreach (string username in usernames)
            {
                ViewModel.ConnectedUsernames.Add(username);
            }
        }

        private void buttonAddToBlacklist_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ViewModel.BlackListUserName))
            {
                if (!ViewModel.ServerSettings.BannedUserAccounts.Contains(ViewModel.BlackListUserName))
                {
                    ViewModel.ServerSettings.BannedUserAccounts.Add(ViewModel.BlackListUserName);
                    XMLUtility.SerializeObjectToFile<ServerSettingsXML>(ViewModel.ServerSettings, FilePaths.ServerSettingsPath);
                }
            }
            ViewModel.BlackListUserName = "";
        }

        private void buttonRemoveSelectedBlacklist_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxBlacklist.SelectedItems != null && listBoxBlacklist.SelectedItems.Count > 0)
            {
                List<string> itemsToRemove = listBoxBlacklist.SelectedItems.OfType<string>().ToList();

                foreach (string username in itemsToRemove)
                {
                    ViewModel.ServerSettings.BannedUserAccounts.Remove(username);
                }

                XMLUtility.SerializeObjectToFile<ServerSettingsXML>(ViewModel.ServerSettings, FilePaths.ServerSettingsPath);
            }

        }

        #endregion

        #region Master client methods

        private async void SendServerDetailsToMasterServerAsync(object sender, EventArgs e)
        {
            await TaskEx.Run(() =>
            {
                if (ViewModel.ServerStatus == ServerStatusEnum.Running)
                {
                    WebUtility.SendServerDetails(ViewModel);
                }
            });
        }

        private async void SendServerDetailsAsync()
        {
            await TaskEx.Run(() =>
            {
                WebUtility.SendServerDetails(ViewModel);
            });
        }

        #endregion

    }
}
