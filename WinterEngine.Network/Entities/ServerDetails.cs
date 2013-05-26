using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Network.Entities;

namespace WinterEngine.Network.Entities
{
    [DataContract]
    public class ServerDetails
    {
        #region Fields

        private ConnectionAddress _address;

        #endregion

        #region Properties

        [DataMember]
        public string ServerName { get; set; }
        [DataMember]
        public string ServerDescription { get; set; }
        [DataMember]
        public byte ServerMaxPlayers { get; set; }
        [DataMember]
        public byte ServerMaxLevel { get; set; }
        public float Ping { get; set; }
        [DataMember]
        public ConnectionAddress Connection
        {
            get
            {
                if (_address == null)
                {
                    _address = new ConnectionAddress();
                }
                return _address;
            }
            set
            {
                if (_address == null)
                {
                    _address = new ConnectionAddress();
                }
                _address = value;
            }
        }
        public DateTime LastPacketReceived { get; set; }
        [DataMember]
        public ushort ServerPort { get; set; }
        [DataMember]
        public PVPTypeEnum PVPTypeID { get; set; }
        [DataMember]
        public GameTypeEnum GameTypeID { get; set; }

        #endregion

        #region Constructors

        public ServerDetails()
        {
            Connection = new ConnectionAddress();
            LastPacketReceived = DateTime.UtcNow;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Compares the connection of two ServerDetails objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ServerDetails comparedObject = obj as ServerDetails;

            if (Object.ReferenceEquals(comparedObject, null))
            {
                return false;
            }
            else
            {
                return Connection.Equals(comparedObject.Connection);
            }
        }

        /// <summary>
        /// Returns the hash code of the connection.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Connection.GetHashCode();
        }

        /// <summary>
        /// Override to display the server details in list boxes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ServerName;
        }

        #endregion
    }
}
