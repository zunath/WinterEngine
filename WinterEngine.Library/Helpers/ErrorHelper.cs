using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinterEngine.Library.Helpers
{
    public static class ErrorHelper
    {
        public static void ShowErrorDialog(string message, Exception ex)
        {
            string finalMessage = message + "\n\n";
            message += ex.Message + "\n\n";

            if(!Object.ReferenceEquals(ex.InnerException, null))
                message += ex.InnerException.Message + "\n";

            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
}
