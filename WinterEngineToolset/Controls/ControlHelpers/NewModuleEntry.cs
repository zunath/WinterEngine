﻿using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.Factories;
using WinterEngine.Toolset.Helpers;
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
        private bool Validation()
        {
            errorProvider.Clear();

            string nameText = nameTextBoxEntry.NameText;
            string tagText = tagTextBoxEntry.TagText;
            Regex tagRegex = new Regex("^[a-zA-Z0-9_]*$");
            bool succeed = true;

            // No character restrictions on the name field. It just can't be blank
            if (nameText == "")
            {
                errorProvider.SetError(nameTextBoxEntry, "Invalid Name");
                succeed = false;
            }

            if (!tagRegex.IsMatch(tagText) || tagText == "")
            {
                errorProvider.SetError(tagTextBoxEntry, "Invalid Tag");
                succeed = false;
            }

            return succeed;
        }

        /// <summary>
        /// Handles validation of user input one last time before creating the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool succeed = Validation();
            ModuleCreationEventArgs eventArgs = new ModuleCreationEventArgs();

            if (succeed)
            {
                try
                {
                    // Create the directory using the WinterFileHelper
                    WinterFileHelper fileHelper = new WinterFileHelper();
                    eventArgs.TemporaryPathDirectory = fileHelper.CreateTemporaryDirectory();

                    // The module repository will handle creating the database file, linking to it,
                    // and initializing the tables.
                    using (ModuleRepository repo = new ModuleRepository())
                    {
                        repo.CreateNewModule(eventArgs.TemporaryPathDirectory, "TempDB");
                    }

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

        /// <summary>
        /// Handles validation for the page when the control loses focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBoxEntry_Leave(object sender, EventArgs e)
        {
            Validation();
        }

        /// <summary>
        /// Handles validation for the page when control loses focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tagTextBoxEntry_Leave(object sender, EventArgs e)
        {
            Validation();
        }

        #endregion

    }
}
