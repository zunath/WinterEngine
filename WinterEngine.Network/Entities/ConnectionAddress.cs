﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network.Entities
{
    public class ConnectionAddress
    {
        #region Properties

        public IPAddress IP { get; set; }
        public int Port { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a unique hash for this object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            // Reference: http://stackoverflow.com/questions/892618/create-a-hashcode-of-two-numbers
            int hashPrime = 23 * 31;
            int ipHash = hashPrime + IP.GetHashCode();
            int portHash = hashPrime + Port.GetHashCode();

            return ipHash + portHash;
        }

        /// <summary>
        /// Returns true if two ConnectionAddress objects are the same.
        /// Returns false if they are not the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ConnectionAddress comparedObject = obj as ConnectionAddress;

            if (Object.ReferenceEquals(comparedObject, null))
            {
                return false;
            }
            else if (this.GetHashCode() == comparedObject.GetHashCode())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion
    }
}
