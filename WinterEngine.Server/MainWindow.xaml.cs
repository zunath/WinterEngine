using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Network;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Servers;

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
            NetworkUtility netUtility = new NetworkUtility();

            GameServer = new ClientServer();
            GameServer.OnServerStart += Server_OnServerStart;
            GameServer.OnServerShutdown += Server_OnServerShutdown;

            MasterClient = new LobbyClient();

            OpenFile = new OpenFileDialog();
            InitializeOpenFileDialog();

            comboBoxPVPType.SelectedIndex = 0;
            listBoxGameType.SelectedIndex = 0;

            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.DefaultValue);
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.DefaultValue);

            labelIPAddress.Content = netUtility.GetExternalIPAddress();
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

        

        #endregion

    }
}
