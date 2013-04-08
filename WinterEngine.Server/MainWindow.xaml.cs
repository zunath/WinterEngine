using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Win32;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Network;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using WinterEngine.Network.ExtendedEventArgs;
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
            GameServer = new ClientServer();
            GameServer.OnServerStart += Server_OnServerStart;
            GameServer.OnServerShutdown += Server_OnServerShutdown;

            MasterClient = new LobbyClient();

            OpenFile = new OpenFileDialog();
            InitializeOpenFileDialog();

            numericPort.DefaultValue = ClientServerConfiguration.DefaultPort;
            numericPort.Text = Convert.ToString(numericPort.DefaultValue);

            numericMaxLevel.Text = Convert.ToString(numericMaxLevel.DefaultValue);
            numericMaxPlayers.Text = Convert.ToString(numericMaxPlayers.DefaultValue);

            UpdateExternalIPAddress();
            MasterClient.OnServerPropertiesChanged += MasterClient_OnServerPropertiesChanged;
        }

        private void MasterClient_OnServerPropertiesChanged(object sender, EventArgs e)
        {
            MasterClient.ServerInformation = BuildServerDetails();
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
                ToggleServerStatusMode(false);
                GameServer.Shutdown();
                MasterClient.Shutdown();
            }
            else
            {
                ToggleServerStatusMode(true);
                GameServer.Start();
                MasterClient.Start(BuildServerDetails());
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
        /// Toggles whether the server is currently running, enabling or disabling the controls as necessary.
        /// </summary>
        /// <param name="serverStarted">Set to true if the server is started. Set to false if the server is stopped.</param>
        private void ToggleServerStatusMode(bool serverStarted)
        {
            buttonBrowse.IsEnabled = !serverStarted;

            textBoxServerMessage.IsEnabled = serverStarted;
            buttonBanAccount.IsEnabled = serverStarted;
            buttonBootPlayer.IsEnabled = serverStarted;

            numericPort.IsEnabled = !serverStarted;
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


        /// <summary>
        /// Builds a ServerDetails object based on data put into the form's fields.
        /// </summary>
        /// <returns></returns>
        private ServerDetails BuildServerDetails()
        {
            if(Object.ReferenceEquals(listBoxGameType.SelectedItem, null))
            {
                listBoxGameType.SelectedIndex = 0;
            }

            if (Object.ReferenceEquals(comboBoxPVPType.SelectedItem, null))
            {
                comboBoxPVPType.SelectedIndex = 0;
            }

            ServerDetails details =
                    new ServerDetails
                    {
                        ServerName = textBoxServerName.Text,
                        ServerMaxLevel = Convert.ToByte(numericMaxLevel.Value),
                        ServerMaxPlayers = Convert.ToByte(numericMaxPlayers.Value),
                        ServerPort = (ushort)numericPort.Value,
                        ServerDescription = textBoxDescription.Text,
                        GameType = (GameTypeEnum)listBoxGameType.SelectedItem,
                        PVPType = (PVPTypeEnum)comboBoxPVPType.SelectedItem
                    };

            return details;
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
        /// Calls RaiseServerDetailsChangedEvent when called for any text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (!Object.ReferenceEquals(MasterClient, null))
            {
                MasterClient_OnServerPropertiesChanged(sender, new EventArgs());
            }
        }

        /// <summary>
        /// Calls RaiseServerDetailsChangedEvent when called for any combo box or list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Object.ReferenceEquals(MasterClient, null))
            {
                MasterClient_OnServerPropertiesChanged(sender, new EventArgs());
            }
        }

        /// <summary>
        /// Calls RaiseServerDetailsChangedEvent when called for any numeric integer up/down control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericIntegerChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!Object.ReferenceEquals(MasterClient, null))
            {
                MasterClient_OnServerPropertiesChanged(sender, new EventArgs());
            }
        }

        #endregion


    }
}
