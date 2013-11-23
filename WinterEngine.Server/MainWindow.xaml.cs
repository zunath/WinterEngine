using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Linq.Expressions;
using Microsoft.Win32;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.XMLObjects;
using WinterEngine.Library.Managers;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Listeners;
using Xceed.Wpf.Toolkit;
using WinterEngine.DataTransferObjects.ViewModels;

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

        #region Event Handling

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new ServerViewModel();
            WebUtility = new WebServiceClientUtility();
            
            MasterServerDispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 30),
                DispatcherPriority.Normal,
                new EventHandler(SendServerDetailsToMasterServer),
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
            textBoxServerStatus.DataContext = ViewModel;
            labelIPAddress.DataContext = ViewModel;
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.ShowDialog();
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ServerStatus == ServerStatusEnum.Running)
            {
                ToggleServerStatusMode(false);
                buttonStartStop.Content = "Start Server";
                GameServerListener.OnLogMessage -= gameServer_OnLogMessageReceived;
                GameServerListener.Shutdown();

                GameServerDispatcherTimer.Stop();
                MasterServerDispatcherTimer.Stop();

                ViewModel.ServerStatusMessage = "Not started...";
            }
            else if(ViewModel.ServerStatus == ServerStatusEnum.Stopped)
            {
                StartServerAsync();
            }
        }

        private void buttonSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void buttonBanAccount_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.QueuedBanUserList = (List<string>)listBoxPlayers.SelectedItems;
        }

        private void buttonBootPlayer_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.QueuedBootUserList = (List<string>)listBoxPlayers.SelectedItems;
        }

        #endregion

        #region Methods

        private async void StartServerAsync()
        {
            bool success = false;
            ViewModel.ServerStatusMessage = "Starting server...";
            ViewModel.ServerStatus = ServerStatusEnum.Starting;

            await Task.Factory.StartNew(() => 
            {
                success = WebUtility.IsMasterServerAlive();
            });

            if (success)
            {
                WinterServer serverDetails = BuildServerDetails();
                buttonStartStop.Content = "Shutdown";
                SendServerDetailsAsync(serverDetails);
                GameServerListener = new GameNetworkListener(serverDetails.ServerPort, ViewModel.ContentPackageList);
                GameServerListener.OnLogMessage += gameServer_OnLogMessageReceived;

                GameServerDispatcherTimer.Start();
                MasterServerDispatcherTimer.Start();
                ViewModel.ServerStatusMessage = "Running...";
                ToggleServerStatusMode(true);
            }
            else
            {
                ViewModel.ServerStatusMessage = "Unable to connect to master server...";
            }
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
            string externalIPAddress = "";
            await Task.Factory.StartNew(() =>
            {
                WebServiceClientUtility netUtility = new WebServiceClientUtility();
                externalIPAddress = netUtility.GetExternalIPAddress();
            });

            ViewModel.ServerIPAddress = externalIPAddress;
            labelIPAddress.Content = ViewModel.ServerIPAddress;
        }


        /// <summary>
        /// Builds a ServerDetails object based on data put into the form's fields.
        /// </summary>
        /// <returns></returns>
        private WinterServer BuildServerDetails()
        {
            if(listBoxGameType.SelectedItem == null)
            {
                listBoxGameType.SelectedIndex = 0;
            }

            if(comboBoxPVPType.SelectedItem == null)
            {
                comboBoxPVPType.SelectedIndex = 0;
            }

            WinterServer server = new WinterServer
            {
                ServerName = ViewModel.ServerSettings.ServerName,
                ServerMaxLevel = (byte)ViewModel.ServerSettings.MaxLevel,
                ServerMaxPlayers = (byte)ViewModel.ServerSettings.MaxPlayers,
                ServerPort = (ushort)ViewModel.ServerSettings.PortNumber,
                ServerDescription = ViewModel.ServerSettings.ServerDescription,
                ServerAnnouncement = ViewModel.ServerSettings.ServerAnnouncement,
                GameTypeID = ViewModel.ServerSettings.GameType,
                PVPTypeID = ViewModel.ServerSettings.PVPSetting,
                IsAutoDownloadEnabled = ViewModel.ServerSettings.AllowFileAutoDownload,
                IsCharacterDeletionEnabled = ViewModel.ServerSettings.AllowCharacterDeletion,
                QueuedServerMessage = ViewModel.QueuedServerMessage,
                BanUserList = ViewModel.QueuedBanUserList,
                BootUserList = ViewModel.QueuedBootUserList
            };
            ViewModel.QueuedServerMessage = "";
            ViewModel.QueuedBanUserList.Clear();
            ViewModel.QueuedBootUserList.Clear();

            return server;
        }

        private void LoadSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ServerSettingsXML));

            if (File.Exists(FilePaths.ServerSettingsPath))
            {
                using (StreamReader reader = new StreamReader(FilePaths.ServerSettingsPath))
                {
                    ViewModel.ServerSettings = (ServerSettingsXML)serializer.Deserialize(reader);
                }
            }
            else
            {
                // Create a file with initial values.
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerSettingsXML));
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.ASCII };
                XmlWriter writer = XmlWriter.Create(stringWriter, settings);
                serializer.Serialize(writer, ViewModel.ServerSettings);
                string xmlOutput = stringWriter.ToString();

                File.WriteAllText(FilePaths.ServerSettingsPath, xmlOutput);
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

        private void UpdateUsersList(List<string> updatedUsernameList)
        {
            ViewModel.ConnectedUsernames = new BindingList<string>(updatedUsernameList);
        }

        #endregion

        #region Game server methods

        private void ProcessGameServer(object sender, EventArgs e)
        {
            if (ViewModel.ServerStatus == ServerStatusEnum.Running)
            {
                GameServerListener.Process(BuildServerDetails());

                this.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => { UpdateUsersList(GameServerListener.ConnectedUsernames); }));
            }
        }

        private void gameServer_OnLogMessageReceived(object sender, NetworkLogMessageEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() => { txtLog.Text += e.Message + "\n"; }));
        }

        #endregion

        #region Master client methods

        private void SendServerDetailsToMasterServer(object sender, EventArgs e)
        {
            if (ViewModel.ServerStatus == ServerStatusEnum.Running)
            {
                WebUtility.SendServerDetails(BuildServerDetails());
            }
        }

        private async void SendServerDetailsAsync(WinterServer serverDetails)
        {
            await Task.Factory.StartNew(() =>
            {
                WebUtility.SendServerDetails(serverDetails);
            });
        }

        #endregion
    }
}
