using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Helpers;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    public partial class NewModuleEntry : Form
    {
        #region Fields
        
        #endregion

        #region Properties
        
        #endregion

        #region Constructors
        public NewModuleEntry()
        {
            InitializeComponent();
        }
        #endregion

        #region Delegates / Events

        public event EventHandler<ModuleCreationEventArgs> OnModuleCreationSuccess;

        #endregion

        #region Methods
        
        /// <summary>
        /// Handles validation of user input one last time before creating the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            ModuleCreationEventArgs eventArgs = new ModuleCreationEventArgs();

            if (nameTextBoxEntry.IsValid && tagTextBoxEntry.IsValid)
            {
                try
                {
                    WinterModuleFactory moduleFactory = new WinterModuleFactory(nameTextBoxEntry.NameText, tagTextBoxEntry.TagText);
                    moduleFactory.CreateModule();
                    eventArgs.ModuleFactory = moduleFactory;

                    // Pass the temporary directory's path via event.
                    OnModuleCreationSuccess(this, eventArgs);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error adding new module. (Method: buttonOK_Click)", ex);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        

    }
}
