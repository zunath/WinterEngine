using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WinterEngine.Network.Configuration;

namespace WinterEngine.Network.Servers
{
    public class FileTransferListener
    {
        #region Fields

        private TcpListener _fileListener;
        private bool _isListenerRunning;
        private TcpClient _clientSocket;
        private BackgroundWorker _listenerThread;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file listener
        /// </summary>
        private TcpListener FileListener
        {
            get { return _fileListener; }
            set { _fileListener = value; }
        }

        /// <summary>
        /// Gets or sets whether the file transfer listener is running
        /// </summary>
        private bool IsListenerRunning
        {
            get { return _isListenerRunning; }
            set { _isListenerRunning = value; }
        }

        /// <summary>
        /// Gets or sets the listener thread.
        /// </summary>
        private BackgroundWorker ListenerThread
        {
            get { return _listenerThread; }
            set { _listenerThread = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new file transfer listener using the default client-server port.
        /// </summary>
        public FileTransferListener(string ipAddress)
        {
            IPAddress address = Dns.GetHostAddresses(ipAddress)[0];
            FileListener = new TcpListener(address, ClientServerConfiguration.DefaultPort);
            ConstructorInitialize();
        }

        /// <summary>
        /// Constructs a new file transfer listener using a custom client-server port.
        /// </summary>
        /// <param name="customPort">The custom port to use.</param>
        public FileTransferListener(string ipAddress, int customPort)
        {
            IPAddress address = Dns.GetHostAddresses(ipAddress)[0];
            FileListener = new TcpListener(address, customPort);
            ConstructorInitialize();
        }

        /// <summary>
        /// Handles generic initialization for all constructors.
        /// </summary>
        private void ConstructorInitialize()
        {
            ListenerThread = new BackgroundWorker();
            ListenerThread.DoWork += Listen;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Begins listening for file transfer requests.
        /// </summary>
        public void StartListening()
        {
            try
            {
                IsListenerRunning = true;
                FileListener.Start();
                ListenerThread.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to start listening (FileTransferListener: StartListening() )", ex);
            }
        }

        /// <summary>
        /// Handles listening for new connections.
        /// Called from the ListenerThread and runs asynchronously in the background.
        /// </summary>
        private void Listen(object sender, EventArgs e)
        {
            while (IsListenerRunning)
            {
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Stops listening for file transfer requests.
        /// </summary>
        public void StopListening()
        {
            try
            {
                IsListenerRunning = false;
                FileListener.Stop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
