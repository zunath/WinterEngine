using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Network;
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
        private ClientServer Server { get; set; }

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
            Server = new ClientServer();
            Server.OnServerStart += Server_OnServerStart;
            Server.OnServerShutdown += Server_OnServerShutdown;

            OpenFile = new OpenFileDialog();
            InitializeOpenFileDialog();

            comboBoxPVPType.SelectedIndex = 0;
            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.DefaultValue);
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.DefaultValue);
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


        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (Server.IsServerRunning)
            {
                Server.Shutdown();
            }
            else
            {
                ServerDetails details = 
                    new ServerDetails { Name = textBoxServerName.Text, 
                                        MaxLevel = Convert.ToByte(numericMaxLevel.Value)
                                      };
                Server.Start(details);
            }
        }

        private void buttonBanAccount_Click(object sender, RoutedEventArgs e)
        {

        }

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

        #endregion

    }
}
