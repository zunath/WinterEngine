using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Library.Managers;
using WinterEngine.Network;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Listeners;
using Xceed.Wpf.Toolkit;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataAccess;
using System.Linq;

namespace WinterEngine.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private bool _isRunning;
        private OpenFileDialog _openFile;
        private List<ContentPackage> _contentPackages;
        private BackgroundWorker _masterServerThread;
        private BackgroundWorker _gameListenerThread;
        private BindingList<string> _connectedUsernames;

        private readonly IModuleManager _moduleManager;
        private readonly IGenericRepository<ContentPackage> _contentPackageRepository;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the game server and master client are running.
        /// </summary>
        private bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        /// <summary>
        /// Gets or sets the thread responsible for managing the master server client.
        /// </summary>
        private BackgroundWorker MasterServerThread
        {
            get { return _masterServerThread; }
            set { _masterServerThread = value; }
        }

        /// <summary>
        /// Gets or sets the thread responsible for managing the game listener.
        /// </summary>
        private BackgroundWorker GameListenerThread
        {
            get { return _gameListenerThread; }
            set { _gameListenerThread = value; }
        }

        /// <summary>
        /// Gets or sets the open file dialog.
        /// </summary>
        private OpenFileDialog OpenFile
        {
            get { return _openFile; }
            set { _openFile = value; }
        }

        /// <summary>
        /// Gets or sets the list of content packages used by the active module.
        /// </summary>
        private List<ContentPackage> ContentPackageList
        {
            get { return _contentPackages; }
            set { _contentPackages = value; }
        }

        /// <summary>
        /// Gets the usernames currently connected to the server.
        /// </summary>
        protected BindingList<string> ConnectedUsernames
        {
            get { return _connectedUsernames; }
            set { _connectedUsernames = value; }
        }

        #endregion

        #region Constructors

        public MainWindow(IModuleManager moduleManager, IGenericRepository<ContentPackage> contentPackage)
        {
            if (moduleManager == null) throw new ArgumentNullException("moduleManager");
            _moduleManager = moduleManager;

            if (contentPackage == null) throw new ArgumentNullException("contentPackage");
            _contentPackageRepository = contentPackage;

            InitializeComponent();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Handles setting default values to controls on window load,
        /// instantiating objects, and other initialization actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            GameListenerThread = new BackgroundWorker();
            GameListenerThread.WorkerSupportsCancellation = true;
            GameListenerThread.WorkerReportsProgress = false;
            GameListenerThread.DoWork += GameServerThread_DoWork;
            GameListenerThread.RunWorkerCompleted += GameListenerThread_RunWorkerCompleted;

            MasterServerThread = new BackgroundWorker();
            MasterServerThread.WorkerSupportsCancellation = true;
            MasterServerThread.WorkerReportsProgress = false;
            MasterServerThread.DoWork += MasterServerThread_DoWork;
            MasterServerThread.RunWorkerCompleted += MasterServerThread_RunWorkerCompleted;

            OpenFile = new OpenFileDialog();
            InitializeOpenFileDialog();

            numericPort.DefaultValue = GameServerConfiguration.DefaultGamePort;
            numericPort.Text = Convert.ToString(numericPort.DefaultValue);

            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.DefaultValue);
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.DefaultValue);

            ConnectedUsernames = new BindingList<string>();
            listBoxPlayers.ItemsSource = ConnectedUsernames;

            UpdateExternalIPAddress();

            /*
            using (PlayerCharacterRepository repo = new PlayerCharacterRepository())
            {
                UserProfile profile = new UserProfile
                {
                    UserName = "z"
                };

                PlayerCharacter character = new PlayerCharacter
                {
                    Age = 22,
                    Biography = "test bio",
                    FirstName = "Zunath",
                    LastName = "Zintuachi",
                    IsGameMaster = false
                };

                repo.SerializePlayerCharacterFile(character, profile);
            }
            */
        }

        /// <summary>
        /// Prompts user to select a valid .WMOD file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.ShowDialog();
        }

        /// <summary>
        /// Handles sending a message to all players currently connected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Starts or stops all client and server connections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (IsRunning)
            {
                ToggleServerStatusMode(false);
                buttonStartStop.Content = "Start Server";
            }
            else
            {
                ToggleServerStatusMode(true);

                if (!GameListenerThread.IsBusy)
                {
                    GameListenerThread.RunWorkerAsync(BuildServerDetails());
                }

                if (!MasterServerThread.IsBusy)
                {
                    MasterServerThread.RunWorkerAsync(BuildServerDetails());
                }

                buttonStartStop.Content = "Shutdown";
            }
        }

        /// <summary>
        /// Handles banning a user's account from this server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBanAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Handles booting a user from this server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBootPlayer_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the OpenFile components
        /// </summary>
        private void InitializeOpenFileDialog()
        {
            FileExtensionFactory fileExtensionFactory = new FileExtensionFactory();
            OpenFile.Filter = fileExtensionFactory.BuildModuleFileFilter();

            OpenFile.AddExtension = true;
            OpenFile.Title = "Open Module";
            OpenFile.Multiselect = false;

            OpenFile.FileOk += LoadModule;
        }

        /// <summary>
        /// Loads the selected file's data into the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadModule(object sender, CancelEventArgs e)
        {
            string path = OpenFile.FileName;
            textBoxModuleFileName.Text = Path.GetFileNameWithoutExtension(path);
            _moduleManager.OpenModule(path);
            ContentPackageList = _contentPackageRepository.GetAll().ToList();
        }

        /// <summary>
        /// Toggles whether the server is currently running, enabling or disabling the controls as necessary.
        /// </summary>
        /// <param name="serverStarted">Set to true if the server is started. Set to false if the server is stopped.</param>
        private void ToggleServerStatusMode(bool serverStarted)
        {
            IsRunning = serverStarted;
            buttonBrowse.IsEnabled = !serverStarted;
            numericPort.IsEnabled = !serverStarted;

            textBoxServerMessage.IsEnabled = serverStarted;
            buttonBanAccount.IsEnabled = serverStarted;
            buttonBootPlayer.IsEnabled = serverStarted;
        }

        /// <summary>
        /// Handles updating the external IP address content on a separate thread.
        /// </summary>
        private void UpdateExternalIPAddress()
        {
            BackgroundWorker ipAddressCheckerWorker = new BackgroundWorker();
            ipAddressCheckerWorker.DoWork += delegate
            {
                WebServiceClientUtility netUtility = new WebServiceClientUtility();

                string externalIPAddress = netUtility.GetExternalIPAddress();
                labelIPAddress.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => { labelIPAddress.Content = externalIPAddress; }));

            };
            ipAddressCheckerWorker.RunWorkerAsync();
        }


        /// <summary>
        /// Builds a ServerDetails object based on data put into the form's fields.
        /// </summary>
        /// <returns></returns>
        private WinterServer BuildServerDetails()
        {
            if (Object.ReferenceEquals(listBoxGameType.SelectedItem, null))
            {
                listBoxGameType.SelectedIndex = 0;
            }

            if (Object.ReferenceEquals(comboBoxPVPType.SelectedItem, null))
            {
                comboBoxPVPType.SelectedIndex = 0;
            }

            WinterServer server = new WinterServer
                    {
                        ServerName = textBoxServerName.Text,
                        ServerMaxLevel = Convert.ToByte(numericMaxLevel.Value),
                        ServerMaxPlayers = Convert.ToByte(numericMaxPlayers.Value),
                        ServerPort = (ushort)numericPort.Value,
                        ServerDescription = textBoxDescription.Text,
                        ServerAnnouncement = textBoxAnnouncement.Text,
                        GameTypeID = (GameTypeEnum)listBoxGameType.SelectedItem,
                        PVPTypeID = (PVPTypeEnum)comboBoxPVPType.SelectedItem,
                        IsAutoDownloadEnabled = (bool)checkBoxAllowFileAutoDownload.IsChecked,
                        IsCharacterDeletionEnabled = (bool)checkBoxAllowCharacterDeletion.IsChecked
                    };

            return server;
        }

        #endregion

        #region GUI Methods

        /// <summary>
        /// Defaults the text of the MaxPlayers numeric integer to the value of the control.
        /// This is a workaround for a bug in the extended WPF controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetDefaultValues_MaxPlayers(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.Value);
        }

        /// <summary>
        /// Defaults the text of the MaxLevel numeric integer to the value of the control.
        /// This is a workaround for a bug in the extended WPF controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetDefaultValues_MaxLevel(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.Value);
        }

        /// <summary>
        /// Defaults the text of the Port numeric integer to the value of the control.
        /// This is a workaround for a bug in the extended WPF controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetDefaultValues_Port(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericPort.Text = Convert.ToString(numericPort.Value);
        }

        /// <summary>
        /// Adds or removes usernames from the list of players.
        /// </summary>
        /// <param name="updatedUsernameList"></param>
        private void UpdateUsersList(List<string> updatedUsernameList)
        {
            for (int index = ConnectedUsernames.Count - 1; index >= 0; index--)
            {
                if (!updatedUsernameList.Contains(ConnectedUsernames[index]))
                {
                    ConnectedUsernames.RemoveAt(index);
                }
            }

            foreach (string username in updatedUsernameList)
            {
                if (!ConnectedUsernames.Contains(username))
                {
                    ConnectedUsernames.Add(username);
                }
            }
        }

        #endregion

        #region Game server thread

        private void GameServerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WinterServer serverCopy = e.Argument as WinterServer;
                GameNetworkListener gameServer = new GameNetworkListener(serverCopy.ServerPort, ContentPackageList);
                gameServer.OnLogMessage += gameServer_OnLogMessageReceived;

                while (IsRunning)
                {
                    this.Dispatcher.Invoke(DispatcherPriority.Normal,
                        new Action(() => { serverCopy = BuildServerDetails(); }));

                    gameServer.Process(serverCopy);

                    this.Dispatcher.Invoke(DispatcherPriority.Normal,
                        new Action(() => { UpdateUsersList(gameServer.ConnectedUsernames); }));
                }

                gameServer.Shutdown();
            }
            catch
            {
                throw;
            }
        }

        private void GameListenerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ConnectedUsernames.Clear();

            if (!Object.ReferenceEquals(e.Error, null))
            {
                throw e.Error;
            }
        }

        private void gameServer_OnLogMessageReceived(object sender, NetworkLogMessageEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() => { txtLog.Text += e.Message + "\n"; }));
        }

        #endregion

        #region Master client thread

        private void MasterServerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MasterServerClient masterClient = new MasterServerClient();
                WinterServer serverCopy = e.Argument as WinterServer;

                while (IsRunning)
                {
                    this.Dispatcher.Invoke(DispatcherPriority.Normal,
                        new Action(() => { serverCopy = BuildServerDetails(); }));
                    masterClient.Process(serverCopy);

                }
            }
            catch
            {
                throw;
            }
        }

        private void MasterServerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Object.ReferenceEquals(e.Error, null))
            {
                throw e.Error;
            }
        }


        #endregion
    }
}
