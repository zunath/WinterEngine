using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.IO;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network
{
    public enum AgentRole
    {
        Client,
        Server
    }

    public class NetworkAgent
    {
        private NetPeer mPeer;
        private NetPeerConfiguration mConfig;
        private AgentRole mRole;
        private int port = 5121;
        private NetOutgoingMessage mOutgoingMessage;
        private List<NetIncomingMessage> mIncomingMessages;
        
        public List<NetConnection> Connections
        {
            get
            {
                return mPeer.Connections;
            }
        }

        /// <summary>
        /// Customize appIdentifier. Note: Client and server appIdentifier must be the same.
        /// </summary>
        public NetworkAgent(AgentRole role, string tag, int customPort)
        {
            mRole = role;
            mConfig = new NetPeerConfiguration(tag);
            port = customPort;

            Initialize();
        }

        private void Initialize()
        {
            if (mRole == AgentRole.Server)
            {
                mConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
                mConfig.Port = port;
                //Casts the NetPeer to a NetServer
                mPeer = new NetServer(mConfig);
            }
            if (mRole == AgentRole.Client)
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
            if (mRole == AgentRole.Client)
            {
                mPeer.Connect(ip, port);
            }
            else
            {
                throw new SystemException("Attempted to connect as server. Only clients should connect.");
            }
        }

        /// <summary>
        /// Closes the NetPeer
        /// </summary>
        public void Shutdown()
        {
            mPeer.Shutdown("Closing connection.");
        }

        /// <summary>
        /// Writes a packet's properties to an outgoing message.
        /// </summary>
        /// <param name="packet"></param>
        public void WriteMessage(Packet packet)
        {
            // The first byte is always the packet type.
            mOutgoingMessage.Write((byte)packet.PacketType);
            mOutgoingMessage.WriteAllProperties(packet);
        }

        /// <summary>
        /// Sends off mOutgoingMessage and then clears it for the next send.
        /// Defaults to UnreliableSequenced for fast transfer which guarantees that older messages
        /// won't be processed after new messages. If IsGuaranteed is true it uses ReliableSequenced
        /// which is safer but much slower.
        /// </summary>
        public void SendMessage(NetConnection recipient)
        {
            SendMessage(recipient, false);
        }
        public void SendMessage(NetConnection recipient, bool IsGuaranteed)
        {
            NetDeliveryMethod method = IsGuaranteed ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.UnreliableSequenced;
            mPeer.SendMessage(mOutgoingMessage, recipient, method);
            mOutgoingMessage = mPeer.CreateMessage();
        }

        /// <summary>
        /// Reads every message in the queue and returns a list of data messages.
        /// Other message types just write a Console note.
        /// This should be called every update by the Game Screen
        /// The Game Screen should implement the actual handling of messages.
        /// </summary>
        /// <returns></returns>
        public List<NetIncomingMessage> CheckForMessages()
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
                        if (mRole == AgentRole.Server)
                            output += incomingMessage.ReadString() + "\n";
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)incomingMessage.ReadByte();
                        if (mRole == AgentRole.Server)
                            output += "Status Message: " + incomingMessage.ReadString() + "\n";

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
            if (mRole == AgentRole.Server)
            {
                StreamWriter textOut = new StreamWriter(new FileStream("log.txt", FileMode.Append, FileAccess.Write));
                textOut.Write(output);
                textOut.Close();
            }
            return mIncomingMessages;
        }
    }
}
