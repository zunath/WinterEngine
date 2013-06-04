using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lidgren.Network;
using ProtoBuf;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network
{
    public class NetworkAgent
    {
        #region Fields

        private NetPeer mPeer;
        private NetPeerConfiguration mConfig;
        private AgentRoleEnum mRole;
        private int port;
        private NetOutgoingMessage mOutgoingMessage;
        private List<NetIncomingMessage> mIncomingMessages;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a list of connections in use by the agent.
        /// </summary>
        public List<NetConnection> Connections
        {
            get
            {
                return mPeer.Connections;
            }
        }

        public NetPeerStatus Status
        {
            get
            {
                return mPeer.Status;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Customize appIdentifier. Note: Client and server appIdentifier must be the same.
        /// </summary>
        public NetworkAgent(AgentRoleEnum role, string tag, int customPort)
        {
            

            mRole = role;
            mConfig = new NetPeerConfiguration(tag);
            port = customPort;

            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (mRole == AgentRoleEnum.Server)
            {
                mConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
                mConfig.Port = port;
                //Casts the NetPeer to a NetServer
                mPeer = new NetServer(mConfig);
            }

            else if (mRole == AgentRoleEnum.Client)
            {
                mConfig.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
                //Casts the NetPeer to a NetClient
                mPeer = new NetClient(mConfig);
            }

            mPeer.Start();
            
            mIncomingMessages = new List<NetIncomingMessage>();
            mOutgoingMessage = mPeer.CreateMessage();
        }

        /// <summary>
        /// Connects to a server. Throws an exception if you attempt to call Connect as a Server.
        /// </summary>
        public void Connect(string ip)
        {
            if (mRole == AgentRoleEnum.Client)
            {
                mPeer.Connect(ip, port);
            }
            else
            {
                throw new SystemException("Attempted to connect as server. Only clients should connect.");
            }
        }

        /// <summary>
        /// Disconnects from all connections
        /// </summary>
        public void Disconnect()
        {
            foreach (NetConnection connection in Connections)
            {
                connection.Disconnect("Disconnecting");
            }
        }

        /// <summary>
        /// Writes a packet's properties to an outgoing message.
        /// </summary>
        /// <param name="packet"></param>
        public void WriteMessage(PacketBase packet)
        {
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize<PacketBase>(stream, packet); // Protobuf serialization
            mOutgoingMessage.Write(stream.ToArray());
        }

        /// <summary>
        /// Sends the outgoing message and clears it for the next send.
        /// </summary>
        /// <param name="recipient">The recipient of the packet</param>
        /// <param name="method">The delivery method</param>
        public void SendMessage(NetConnection recipient, NetDeliveryMethod method)
        {
            mPeer.SendMessage(mOutgoingMessage, recipient, method);
            mOutgoingMessage = mPeer.CreateMessage();   
        }

        /// <summary>
        /// Reads every message in the queue and returns a list of data messages.
        /// Other message types just write a Console note.
        /// </summary>
        /// <returns></returns>
        private List<NetIncomingMessage> CheckForMessages()
        {
            mIncomingMessages.Clear();
            NetIncomingMessage incomingMessage;
            string output = "";

            while ((incomingMessage = mPeer.ReadMessage()) != null)
            {
                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        mPeer.SendDiscoveryResponse(null, incomingMessage.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        if (mRole == AgentRoleEnum.Server)
                            output += incomingMessage.ReadString() + "\n";
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)incomingMessage.ReadByte();
                        if (mRole == AgentRoleEnum.Server)
                        {
                            output += "Status Message: " + incomingMessage.ReadString() + "\n";
                        }
                        if (status == NetConnectionStatus.Connected)
                        {
                            //PLAYER CONNECTED
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        mIncomingMessages.Add(incomingMessage);
                        break;
                    default:
                        // unknown message type
                        break;
                }
            }
            if (mRole == AgentRoleEnum.Server)
            {
                StreamWriter textOut = new StreamWriter(new FileStream("log.txt", FileMode.Append, FileAccess.Write));
                textOut.Write(output);
                textOut.Close();
            }
            return mIncomingMessages;
        }

        /// <summary>
        /// Checks for queued messages and deserializes them into packets.
        /// </summary>
        /// <returns></returns>
        public List<PacketBase> CheckForPackets()
        {
            List<NetIncomingMessage> messages = CheckForMessages();
            List<PacketBase> packets = new List<PacketBase>();
            MemoryStream stream = new MemoryStream();
            PacketBase currentPacket;

            foreach (NetIncomingMessage currentMessage in messages)
            {
                stream = new MemoryStream(currentMessage.ReadBytes(currentMessage.LengthBytes));
                currentPacket = Serializer.Deserialize<PacketBase>(stream); // Protobuf deserialization
                currentPacket.SenderConnection = currentMessage.SenderConnection;

                packets.Add(currentPacket);
            }

            return packets;
        }
        #endregion

    }
}
