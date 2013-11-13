using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class NetworkLogMessageEventArgs : EventArgs
    {
        private string _message;
        private bool _isDateTimeApplied;

        public string Message 
        {
            get
            {
                return _message;
            }
            set
            {
                string finalMessage = value;

                if (!_isDateTimeApplied)
                {
                    finalMessage = DateTime.Now + " " + finalMessage;
                    _isDateTimeApplied = true;
                }
                _message = finalMessage;
            }
        }
    }
}
