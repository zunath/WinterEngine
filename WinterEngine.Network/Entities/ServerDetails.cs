using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Lidgren.Network;
using WinterEngine.Network.Entities;

namespace WinterEngine.Network.Entities
{
    public class ServerDetails
    {
        #region Properties
        public string ServerName { get; set; }
        public string ServerDescription { get; set; }
        public byte ServerMaxPlayers { get; set; }
        public byte ServerMaxLevel { get; set; }
        public float Ping { get; set; }
        public ConnectionAddress Connection { get; set; }
        public DateTime LastPacketReceived { get; set; }
        public int Port { get; set; }

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
