﻿using System;
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
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.XMLObjects;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace WinterEngine.Server
{
    public partial class MainWindow : Window
    {
        #region Properties

        private ServerSettingsXML ServerSettings { get; set; }
        private ServerStatusEnum ServerStatus { get; set; }
        private WebServiceClientUtility WebUtility { get; set; }
        private OpenFileDialog OpenFile { get; set; }
        private List<ContentPackage> ContentPackageList { get; set; }
        protected BindingList<string> ConnectedUsernames { get; set; }
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

        /// <summary>
        /// Handles setting default values to controls on window load,
        /// instantiating objects, and other initialization actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            WebUtility = new WebServiceClientUtility();
            ServerStatus = ServerStatusEnum.Stopped;
            LoadSettings();

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

            numericPort.DefaultValue = GameServerConfiguration.DefaultGamePort;
            numericPort.Text = Convert.ToString(numericPort.DefaultValue);

            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.DefaultValue);
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.DefaultValue);

            ConnectedUsernames = new BindingList<string>();
            listBoxPlayers.ItemsSource = ConnectedUsernames;

            UpdateExternalIPAddress();
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
            if (ServerStatus == ServerStatusEnum.Running)
            {
                ToggleServerStatusMode(false);
                buttonStartStop.Content = "Start Server";
                GameServerListener.OnLogMessage -= gameServer_OnLogMessageReceived;
                GameServerListener.Shutdown();

                GameServerDispatcherTimer.Stop();
                MasterServerDispatcherTimer.Stop();

                textBoxServerStatus.Text = "Not started...";
            }
            else if(ServerStatus == ServerStatusEnum.Stopped)
            {
                StartServer();
            }
        }

        private void buttonSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            ServerSettings.AllowCharacterDeletion = (bool)checkBoxAllowCharacterDeletion.IsChecked;
            ServerSettings.AllowFileAutoDownload = (bool)checkBoxAllowFileAutoDownload.IsChecked;
            ServerSettings.GMPassword = textBoxDMPassword.Text;
            ServerSettings.MaxLevel = (int)numericMaxLevel.Value;
            ServerSettings.MaxPlayers = (int)numericMaxPlayers.Value;
            ServerSettings.PlayerPassword = textBoxPlayerPassword.Text;
            ServerSettings.PortNumber = (int)numericPort.Value;
            ServerSettings.PVPSetting = (PVPTypeEnum)comboBoxPVPType.SelectedValue;
            ServerSettings.ServerAnnouncement = textBoxAnnouncement.Text;
            ServerSettings.ServerDescription = textBoxDescription.Text;
            ServerSettings.ServerName = textBoxServerName.Text;

            SaveSettings();
        }

        private void buttonBanAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonBootPlayer_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Methods

        private async void StartServer()
        {
            bool success = false;
            textBoxServerStatus.Text = "Starting server...";
            ServerStatus = ServerStatusEnum.Starting;

            await Task.Factory.StartNew(() => 
            {
                success = WebUtility.IsMasterServerAlive();
            });

            if (success)
            {
                WinterServer serverDetails = BuildServerDetails();
                buttonStartStop.Content = "Shutdown";
                SendServerDetailsAsync(serverDetails);
                GameServerListener = new GameNetworkListener(serverDetails.ServerPort, ContentPackageList);
                GameServerListener.OnLogMessage += gameServer_OnLogMessageReceived;

                GameServerDispatcherTimer.Start();
                MasterServerDispatcherTimer.Start();
                textBoxServerStatus.Text = "Running...";
                ToggleServerStatusMode(true);
            }
            else
            {
                textBoxServerStatus.Text = "Unable to connect to master server...";
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
            textBoxModuleFileName.Text = Path.GetFileNameWithoutExtension(path);

            ModuleManager manager = new ModuleManager();
            manager.OpenModule(path);

            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                ContentPackageList = repo.GetAll();
            }
        }

        private void ToggleServerStatusMode(bool serverStarted)
        {
            if (serverStarted)
            {
                ServerStatus = ServerStatusEnum.Running;
            }
            else
            {
                ServerStatus = ServerStatusEnum.Stopped;
            }

            buttonBrowse.IsEnabled = !serverStarted;
            numericPort.IsEnabled = !serverStarted;

            textBoxServerMessage.IsEnabled = serverStarted;
            buttonBanAccount.IsEnabled = serverStarted;
            buttonBootPlayer.IsEnabled = serverStarted;
        }

        private async void UpdateExternalIPAddress()
        {
            string externalIPAddress = "";
            await Task.Factory.StartNew(() =>
            {
                WebServiceClientUtility netUtility = new WebServiceClientUtility();
                externalIPAddress = netUtility.GetExternalIPAddress();
            });

            labelIPAddress.Content = externalIPAddress;
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

        private void LoadSettings()
        {
            ServerSettings = new ServerSettingsXML();
            XmlSerializer serializer = new XmlSerializer(typeof(ServerSettingsXML));

            if (File.Exists(FilePaths.ServerSettingsPath))
            {
                using (StreamReader reader = new StreamReader(FilePaths.ServerSettingsPath))
                {
                    ServerSettings = (ServerSettingsXML)serializer.Deserialize(reader);
                }
            }
            else
            {
                // Create a file with initial values.
                SaveSettings();
            }

            textBoxServerName.Text = ServerSettings.ServerName;
            numericMaxLevel.Value = ServerSettings.MaxLevel;
            numericMaxPlayers.Value = ServerSettings.MaxPlayers;
            numericPort.Value = ServerSettings.PortNumber;
            textBoxDescription.Text = ServerSettings.ServerDescription;
            textBoxAnnouncement.Text = ServerSettings.ServerAnnouncement;
            comboBoxPVPType.SelectedValue = ServerSettings.PVPSetting;
            checkBoxAllowCharacterDeletion.IsChecked = ServerSettings.AllowCharacterDeletion;
            checkBoxAllowFileAutoDownload.IsChecked = ServerSettings.AllowFileAutoDownload;
            textBoxDMPassword.Text = ServerSettings.GMPassword;
            textBoxPlayerPassword.Text = ServerSettings.PlayerPassword;
        }

        private void SaveSettings()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerSettingsXML));
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.ASCII };
                XmlWriter writer = XmlWriter.Create(stringWriter, settings);
                serializer.Serialize(writer, ServerSettings);
                string xmlOutput = stringWriter.ToString();

                File.WriteAllText(FilePaths.ServerSettingsPath, xmlOutput);
            }
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

        #region Game server methods

        private void ProcessGameServer(object sender, EventArgs e)
        {
            if (ServerStatus == ServerStatusEnum.Running)
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
            if (ServerStatus == ServerStatusEnum.Running)
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
