﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Network;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Servers;
using Xceed.Wpf.Toolkit;

namespace WinterEngine.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private OpenFileDialog _openFile;
        private BackgroundWorker _gameWorker;
        private ServerDetails _serverDetails;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the client-server server
        /// </summary>
        private ClientServer GameServer { get; set; }

        /// <summary>
        /// Gets or sets the master server client
        /// </summary>
        private LobbyClient MasterClient { get; set; }

        /// <summary>
        /// Gets or sets the open file dialog.
        /// </summary>
        private OpenFileDialog OpenFile
        {
            get { return _openFile; }
            set {_openFile = value;}
        }

        /// <summary>
        /// Handles tracking the game state. This includes player interaction,
        /// game logic, etc.
        /// </summary>
        private BackgroundWorker GameWorker
        {
            get { return _gameWorker; }
            set { _gameWorker = value; }
        }

        /// <summary>
        /// Gets or sets the server details
        /// </summary>
        private ServerDetails Details
        {
            get { return _serverDetails; }
            set { _serverDetails = value; }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles setting default values to controls on window load,
        /// instantiating objects, and other initialization actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            GameServer = new ClientServer();
            GameServer.OnServerStart += Server_OnServerStart;
            GameServer.OnServerShutdown += Server_OnServerShutdown;

            Details = new ServerDetails();

            MasterClient = new LobbyClient();

            OpenFile = new OpenFileDialog();
            InitializeOpenFileDialog();

            comboBoxPVPType.SelectedIndex = 0;
            listBoxGameType.SelectedIndex = 0;

            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.DefaultValue);
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.DefaultValue);

            UpdateExternalIPAddress();
        }

        private void Server_OnServerShutdown(object sender, EventArgs e)
        {
            buttonStartStop.Content = "Start Server";
        }

        private void Server_OnServerStart(object sender, EventArgs e)
        {
            buttonStartStop.Content = "Shutdown";
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
            if (GameServer.IsServerRunning)
            {
                ToggleControls(true);
                GameServer.Shutdown();
                MasterClient.Shutdown();
            }
            else
            {
                ToggleControls(false);
                ServerDetails details = 
                    new ServerDetails { Name = textBoxServerName.Text, 
                                        MaxLevel = Convert.ToByte(numericMaxLevel.Value),
                                        MaxPlayers = Convert.ToByte(numericMaxPlayers.Value)
                                      };
                GameServer.Start();
                MasterClient.Start(details);
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
            FileExtensionFactory winterExtensions = new FileExtensionFactory();
            string fileExtension = winterExtensions.GetFileExtension(FileTypeEnum.Module);
            OpenFile.Filter = "Winter Module Files (*" + fileExtension + ") | " + "*" + fileExtension;

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
            textBoxModuleFileName.Text = Path.GetFileNameWithoutExtension(OpenFile.SafeFileName);
        }

        /// <summary>
        /// Toggles controls to be enabled or disabled.
        /// </summary>
        /// <param name="enabled">Set to true to enable controls. Set to false to disable them.</param>
        private void ToggleControls(bool enabled)
        {
            buttonBrowse.IsEnabled = enabled;

            textBoxServerMessage.IsEnabled = !enabled;
            buttonBanAccount.IsEnabled = !enabled;
            buttonBootPlayer.IsEnabled = !enabled;
        }

        /// <summary>
        /// Handles updating the external IP address content on a separate thread.
        /// </summary>
        private void UpdateExternalIPAddress()
        {
            BackgroundWorker ipAddressCheckerWorker = new BackgroundWorker();
            ipAddressCheckerWorker.DoWork += delegate
            {
                WebServiceUtility netUtility = new WebServiceUtility();

                string externalIPAddress = netUtility.GetExternalIPAddress();
                labelIPAddress.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => { labelIPAddress.Content = externalIPAddress; }));

            };
            ipAddressCheckerWorker.RunWorkerAsync();
        }

        #endregion

        #region GUI Methods
        // These methods are used to fix bugs with the extended WPF controls.
        // If a user doesn't enter any value for certain controls and the controls lose focus,
        // the value of the control is updated but the text is NOT.
        // A manual fix is applied here so that users aren't confused.

        private void SetDefaultValues_MaxPlayers(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.Value);
        }

        private void SetDefaultValues_MaxLevel(object sender, RoutedEventArgs e)
        {
            IntegerUpDown control = e.Source as IntegerUpDown;
            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.Value);


            _serverDetails.Name = textBoxServerName.Text;
            _serverDetails.MaxLevel = Convert.ToByte(numericMaxLevel.Value);
            _serverDetails.MaxPlayers = Convert.ToByte(numericMaxPlayers.Value);

            MasterClient._serverDetails = this._serverDetails;
        }

        #endregion

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WebServiceUtility utility = new WebServiceUtility();

            System.Windows.MessageBox.Show(utility.SendServerDetails(Details));

        }


    }
}
