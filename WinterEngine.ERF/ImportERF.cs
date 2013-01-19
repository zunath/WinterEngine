using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Helpers;

namespace WinterEngine.ERF
{
    public partial class ImportERF : Form
    {
        #region Fields

        #endregion

        #region Properties
        #endregion

        #region Constructors

        public ImportERF()
        {
            InitializeComponent();
            InitializeFilters();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the filters for the openFileDialog 
        /// </summary>
        private void InitializeFilters()
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            string erfFileExtension = factory.GetFileExtension(FileTypeEnum.Erf);

            openFileDialog.Filter = "Encapsulated Resource Files|*" + erfFileExtension; 
        }

        /// <summary>
        /// Call this to attempt an ERF import. It will ask
        /// user for an ERF file to import. If the ERF has conflicting resources,
        /// this form will appear. Otherwise, all of the resources will be imported
        /// to the module.
        /// </summary>
        public void AttemptImport()
        {
            string tempDirectory = "";
            try
            {
                WinterFileHelper fileHelper = new WinterFileHelper();
                tempDirectory = fileHelper.CreateTemporaryDirectory();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (ZipFile zipFile = new ZipFile())
                    {
                        zipFile.ExtractAll(tempDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception ex)
            {
                // Remove the temporary directory, if it exists.
                if (Directory.Exists(tempDirectory))
                {
                    Directory.Delete(tempDirectory, true);
                }
                
                ErrorHelper.ShowErrorDialog("Error importing encapsulated resource file", ex);
            }
        }

        #endregion


        #region Event handling

        /// <summary>
        /// Handles selecting all items in the list box when user clicks the "Select All" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            // Disable the visibility so that the control won't try to redraw itself
            // while selecting all objects. This speeds up the process.
            listBoxResources.Visible = false;

            for (int index = 0; index < listBoxResources.Items.Count; index++)
            {
                listBoxResources.SetSelected(index, true);
            }

            // Make it visible again.
            listBoxResources.Visible = true;
        }

        /// <summary>
        /// Handles de-selecting all items in the list box when user clicks the "Select None" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectNone_Click(object sender, EventArgs e)
        {
            listBoxResources.Visible = false;

            for (int index = 0; index < listBoxResources.Items.Count; index++)
            {
                listBoxResources.SetSelected(index, false);
            }

            listBoxResources.Visible = true;
        }

        /// <summary>
        /// Handles exiting the import ERF form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }
}
