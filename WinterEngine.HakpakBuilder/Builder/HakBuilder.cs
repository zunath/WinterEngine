using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using Ionic.Zlib;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Enumerations;
using WinterEngine.Library;

namespace WinterEngine.Hakpak.Builder
{
    public partial class HakBuilder : Form
    {
        #region Fields

        private string _saveFilePath;

        #endregion

        #region Properties

        private string SaveFilePath
        {
            get { return _saveFilePath; }
            set { _saveFilePath = value; }
        }

        #endregion

        public HakBuilder()
        {
            InitializeComponent();

            // Set filters for the Save/Open dialog boxes
            FileExtensionFactory factory = new FileExtensionFactory();
            string extension = factory.GetFileExtension(FileType.UncompiledHakpak);
            saveFileDialogSaveAs.Filter = "Uncompiled Hakpak (*" + extension + ")|*" + extension;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            FileExtensionFactory extensions = new FileExtensionFactory();
            string modelFileExtension = extensions.GetFileExtension(FileType.Model);
            string textureFileExtension = extensions.GetFileExtension(FileType.Texture);

            openFileDialogBuilder.Filter = "Model Files (*" + modelFileExtension + ")|*" + modelFileExtension + "|" + 
                                    "Texture Files (*" + textureFileExtension + ")|*" + textureFileExtension;
            openFileDialogBuilder.ShowDialog();

            // At least one file was selected
            if (!Object.ReferenceEquals(openFileDialogBuilder.FileNames, null))
            {
                // Loop through the list of file names and add them to the list box
                foreach(var currentFile in openFileDialogBuilder.FileNames)
                {
                    // Does the file exist?
                    if (File.Exists(currentFile))
                    {
                        // Is the file in the list already?
                        if (!DoesFileExistInListBox(currentFile))
                        {
                            // Add the file to the list box
                            listBoxResources.Items.Add(currentFile);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Builds each file in the list using the XNA content pipeline.
        /// The background worker handles the processing and updates the progress 
        /// bar as each file is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBuild_Click(object sender, EventArgs e)
        {
            FileExtensionFactory extensions = new FileExtensionFactory();
            string fileExtension = extensions.GetFileExtension(FileType.Hakpak);
            saveFileDialog.Filter = "Hakpak Files (*" + fileExtension + ")|*" + fileExtension;
            saveFileDialog.ShowDialog();

            if (!Object.ReferenceEquals(saveFileDialog.FileName, null))
            {
                // Disable all controls while building is in progress.
                buttonAddFiles.Enabled = false;
                buttonBuild.Enabled = false;
                buttonRemoveFiles.Enabled = false;
                textBoxDescription.Enabled = false;
                textBoxName.Enabled = false;
                listBoxResources.Enabled = false;

                // Start the build process on a separate thread so that the GUI does not lock up during
                // heavy processing.
                backgroundWorkerProcess.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Simple search to see if a file name matches an item already in the list box.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool DoesFileExistInListBox(string fileName)
        {
            foreach (string currentItem in listBoxResources.Items)
            {
                if (currentItem == fileName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Remove all selected files from the list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveFiles_Click(object sender, EventArgs e)
        {
            // Removals must be in reverse order.
            for (int current = listBoxResources.SelectedItems.Count - 1; current >= 0; current--)
            {
                // Remove the item at the correct index
                listBoxResources.Items.RemoveAt(listBoxResources.SelectedIndices[current]);
            }
        }

        private void backgroundWorkerProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            // Report progress throughout the process so that the GUI thread gets updated.
            backgroundWorkerProcess.ReportProgress(0);

            // If there's an existing hakpak file, delete it
            if (File.Exists(saveFileDialog.FileName))
            {
                File.Delete(saveFileDialog.FileName);
            }
                
            ContentBuilder builder = new ContentBuilder();
            string destinationDirectory = new DirectoryInfo(saveFileDialog.FileName).Parent.FullName;

            // Add each file in the list to the builder
            foreach (string file in listBoxResources.Items)
            {
                // Take the file's base name and remove the extension. When building gets done, the .xnb extension will be applied
                builder.Add(file, Path.GetFileNameWithoutExtension(new DirectoryInfo(file).Name), null, null);
            }

            backgroundWorkerProcess.ReportProgress(30);
            // Perform the build, capturing any error information
            string buildError = builder.Build();

            backgroundWorkerProcess.ReportProgress(70);

            if (string.IsNullOrEmpty(buildError))
            {
                // Create a new zip file disguised as a custom engine file at the location specified by user
                using (ZipFile zipFile = new ZipFile(saveFileDialog.FileName))
                {
                    // Disable compression
                    zipFile.CompressionLevel = CompressionLevel.None;
                    // Retrieve the compiled files from memory 
                    string tempPath = builder.OutputDirectory;
                    string xnaFileExtension = new FileExtensionFactory().GetFileExtension(FileType.XNACompiledFile);
                    string[] files = Directory.GetFiles(tempPath, "*" + xnaFileExtension);

                    // Loop through the file list and add them to the zip file
                    foreach (string file in files)
                    {
                        // Note that "" means add the file to the root of the archive
                        zipFile.AddFile(file, "");
                    }

                    // Save the zip file to disk.
                    zipFile.Save();
                    backgroundWorkerProcess.ReportProgress(100);
                }
            }
            else
            {
                // If the build failed, display an error message.
                MessageBox.Show(buildError, "Error");
            }
            
        }

        private void backgroundWorkerProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarBuild.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// After the build process is complete, re-enable all buttons and controls.
        /// Also reset the list of resource paths.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonAddFiles.Enabled = true;
            buttonBuild.Enabled = true;
            buttonRemoveFiles.Enabled = true;
            textBoxDescription.Enabled = true;
            textBoxName.Enabled = true;
            listBoxResources.Enabled = true;
        }

        /// <summary>
        /// Unload resources from form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxDescription.Text = "";
            SaveFilePath = "";
            listBoxResources.Items.Clear();
        }

        /// <summary>
        /// Link back to the build method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemBuild_Click(object sender, EventArgs e)
        {
            buttonBuild.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // No save file path set or no valid file at destination - use the Save As method
            if (String.IsNullOrEmpty(SaveFilePath) || !File.Exists(SaveFilePath))
            {
                saveAsToolStripMenuItem.PerformClick();
            }
            else
            {
                SaveUncompiledHakpak();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Display the save as pop up menu and set the selected path to the SaveFilePath property
            saveFileDialogSaveAs.ShowDialog();

            if (!String.IsNullOrEmpty(saveFileDialogSaveAs.FileName))
            {
                SaveFilePath = saveFileDialogSaveAs.FileName;
                SaveUncompiledHakpak();
            }
        }
        /// <summary>
        /// Saves an uncompiled hakpak to the location set by the SaveFilePath property.
        /// If property is not set, this method will do nothing.
        /// </summary>
        private void SaveUncompiledHakpak()
        {
            if (!String.IsNullOrEmpty(SaveFilePath))
            {
                File.Delete(SaveFilePath);

                using (ZipFile file = new ZipFile(SaveFilePath))
                {
                    file.CompressionLevel = CompressionLevel.None;

                    foreach (string listFile in listBoxResources.Items)
                    {
                        file.AddFile(listFile, "");
                    }

                    file.Save();
                }
            }
        }
    }
}
