using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class AutoDownloaderStatusEventArgs : EventArgs
    {
        private int _currentFilePercentComplete;

        public int TotalPercentComplete { get; set; }
        public int CurrentFilePercentComplete 
        {
            get
            {
                return _currentFilePercentComplete;
            }
            set
            {
                if (value > 100)
                {
                    value = 100;
                }
                _currentFilePercentComplete = value;
            }
        }
        public string CurrentFileName { get; set; }

    }
}
