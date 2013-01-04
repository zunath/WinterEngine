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
        public HakBuilder()
        {
            InitializeComponent();
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

            openFileDialog.Filter = "Model Files (*" + modelFileExtension + ")|*" + modelFileExtension + "|" + 
                                    "Texture Files (*" + textureFileExtension + ")|*" + textureFileExtension;
            openFileDialog.ShowDialog();

            // At least one file was selected
            if (!Object.ReferenceEquals(openFileDialog.FileNames, null))
            {
                // Loop through the list of file names and add them to the list box
                foreach(var currentFile in openFileDialog.FileNames)
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

            // Must have selected a valid name
            if (!Object.ReferenceEquals(saveFileDialog.FileName, null))
            {
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

                // Perform the build, capturing any error information
                string buildError = builder.Build();

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
                    }
                }
                else
                {
                    // If the build failed, display an error message.
                    MessageBox.Show(buildError, "Error");
                }
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

    }
}
