﻿using System;
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
using WinterEngine.Library;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.HakpakBuilder.Builder;
using System.Xml;

namespace WinterEngine.Hakpak.Builder
{
    public partial class HakBuilder : Form
    {
        #region Fields

        private string _saveFilePath;
        private bool _uncompiledArchiveOpened;
        private string _openFilePath;

        #endregion

        #region Properties

        private string SaveFilePath
        {
            get { return _saveFilePath; }
            set { _saveFilePath = value; }
        }

        private bool UncompiledArchivedOpened
        {
            get { return _uncompiledArchiveOpened; }
            set { _uncompiledArchiveOpened = value; }
        }

        private string OpenFilePath
        {
            get { return _openFilePath; }
            set { _openFilePath = value; }
        }

        #endregion

        #region Constructors

        public HakBuilder()
        {
            InitializeComponent();
            InitializeFileDialogFilters();
            UncompiledArchivedOpened = false;

            resourceTypeControl.OnResourceChanged += resourceTypeControl_OnResourceChanged;
        }

        #endregion

        #region Event Handlers
        
        /// <summary>
        /// Handles disposing of the form when the exit button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Handles prompting user to add resource files to the hakpak
        /// and processes them accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            FileExtensionFactory extensions = new FileExtensionFactory();
            string textureFileExtension = extensions.GetFileExtension(FileTypeEnum.SpriteSheet);
            string soundFileExtension = extensions.GetFileExtension(FileTypeEnum.Sound);
            string musicFileExtension = extensions.GetFileExtension(FileTypeEnum.Music);

            openFileDialogBuilder.ShowDialog();

            // At least one file was selected
            if (!Object.ReferenceEquals(openFileDialogBuilder.FileNames, null))
            {
                // Loop through the list of file names and add them to the list box
                foreach (string currentFile in openFileDialogBuilder.FileNames)
                {
                    HakResource resource = new HakResource(currentFile, HakResourceTypeEnum.Tileset);
                    
                    // Does the file exist?
                    if (File.Exists(currentFile))
                    {
                        // Is the file in the list already?
                        if (!DoesFileExistInListBox(resource))
                        {
                            // Add the file to the list box
                            listBoxResources.Items.Add(resource);
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
            // Must have at least one resource to build
            if (listBoxResources.Items.Count <= 0)
            {
                MessageBox.Show("Please select files to build by pressing the \"Add File(s)...\" button", "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FileExtensionFactory extensions = new FileExtensionFactory();
            string fileExtension = extensions.GetFileExtension(FileTypeEnum.Hakpak);
            saveFileDialog.Filter = "Hakpak Files (*" + fileExtension + ")|*" + fileExtension;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
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
                int index = listBoxResources.SelectedIndices[current];
                listBoxResources.Items.RemoveAt(index);
            }
        }

        /// <summary>
        /// Handles the heavy-lifting of compiling files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            ContentBuilder builder = new ContentBuilder();
            List<HakResource> fileList = new List<HakResource>();
            string destinationDirectory = new DirectoryInfo(saveFileDialog.FileName).Parent.FullName;
            DirectoryInfo tempDirectoryInfo = Directory.CreateDirectory(destinationDirectory + "\\temp");
            string manifestPath = "";

            // Report progress throughout the process so that the GUI thread gets updated.
            backgroundWorkerProcess.ReportProgress(0);

            // If there's an existing hakpak file, delete it
            if (File.Exists(saveFileDialog.FileName))
            {
                File.Delete(saveFileDialog.FileName);
            }

            // All of the files are contained inside of an archive (an uncompiled hakpak)
            if (UncompiledArchivedOpened)
            {
                using (ZipFile zipFile = new ZipFile(OpenFilePath))
                {
                    // Extract files to the temporary directory
                    zipFile.ExtractAll(tempDirectoryInfo.FullName);

                    // Add each file path to the list
                    foreach (string current in Directory.GetFiles(tempDirectoryInfo.FullName))
                    {
                        HakResource resource = new HakResource(current, HakResourceTypeEnum.Tileset);
                        fileList.Add(resource);
                    }
                }
            }
            // Otherwise, all of the files have different file paths
            else
            {
                // Put the path names listed in the list box into the file list
                foreach (HakResource current in listBoxResources.Items)
                {
                    fileList.Add(current);
                }
            }


            Dictionary<HakResource, HakResource> modifiedFileNameDictionary = GenerateUniqueFileNameList(fileList);

            // Add each file in the list to the builder
            foreach (HakResource file in fileList)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(file.ResourcePath);
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(dirInfo.Name);
                string extension = Path.GetExtension(file.ResourcePath);
                string processorType = GetProcessorType(extension);

                // Only add to the builder if a processor type is found for the material.
                if (!String.IsNullOrEmpty(processorType))
                {
                    // Take the file's base name and remove the extension. When building gets done, the .xnb extension will be applied.
                    builder.Add(file.ResourcePath, modifiedFileNameDictionary[file].ResourcePath, null, processorType);
                }
                // If no processor type was found, alert the user
                else
                {
                    MessageBox.Show("Error: Invalid file type\n\n" + file, "Error Building", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            backgroundWorkerProcess.ReportProgress(30);

            // Perform the build, capturing any error information
            string buildError = builder.Build();

            // Remove the temporary directory, if available
            tempDirectoryInfo.Delete(true);

            backgroundWorkerProcess.ReportProgress(70);

            if (string.IsNullOrEmpty(buildError))
            {
                // Create a new zip file disguised as a custom engine file at the location specified by user
                using (ZipFile zipFile = new ZipFile(saveFileDialog.FileName))
                {
                    FileExtensionFactory fileExtensionFactory = new FileExtensionFactory();
                    zipFile.CompressionLevel = CompressionLevel.None;
                    string tempPath = builder.OutputDirectory;
                    string xnaFileExtension = fileExtensionFactory.GetFileExtension(FileTypeEnum.XNACompiledFile);
                    string[] files = Directory.GetFiles(tempPath, "*" + xnaFileExtension);
                    List<HakResource> hakResourceList = new List<HakResource>();
                    manifestPath = CreateManifestFile(builder.OutputDirectory, fileList);
                    
                    foreach (string file in files)
                    {
                        // Note that "" means add the file to the root of the archive
                        zipFile.AddFile(file, "");
                    }

                    // Add the manifest file
                    zipFile.AddFile(manifestPath, "");

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

        /// <summary>
        /// Updates the main form with the progress of the hakpak processor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            MessageBox.Show("Build complete!", "Build Complete", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            OpenFilePath = "";
            listBoxResources.Items.Clear();
            UncompiledArchivedOpened = false;
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

        /// <summary>
        /// Handles saving the uncompiled hakpak.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles saving the uncompiled hakpak, prompting user to select the
        /// location to save the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Handles prompting user to open an uncompiled hakpak and then
        /// opens it into the editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileExtensionFactory extensions = new FileExtensionFactory();
            string uncompiledHakpakFileExtension = extensions.GetFileExtension(FileTypeEnum.UncompiledHakpak);

            openFileDialogBuilder.Filter = "Uncompiled Hakpak (*" + uncompiledHakpakFileExtension + ")|*" + uncompiledHakpakFileExtension;

            if (openFileDialogBuilder.ShowDialog() == DialogResult.OK)
            {
                // Clear out existing data
                listBoxResources.Items.Clear();
                textBoxDescription.Text = "";
                textBoxName.Text = "";
                UncompiledArchivedOpened = true;
                OpenFilePath = openFileDialogBuilder.FileName;

                using (ZipFile zipFile = ZipFile.Read(openFileDialogBuilder.FileName))
                {
                    foreach (ZipEntry current in zipFile)
                    {
                        listBoxResources.Items.Add(current.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// When a resource is selected, load its data into the controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numberSelected = listBoxResources.SelectedItems.Count;
            
            if (numberSelected > 1)
            {
                resourceTypeControl.Enabled = false;
            }
            else
            {
                HakResource resource = listBoxResources.SelectedItem as HakResource;
                resourceTypeControl.Enabled = false;

                if (!Object.ReferenceEquals(resource, null))
                {
                    resourceTypeControl.Enabled = true;
                    resourceTypeControl.ChangeResourceType(resource.ResourceType);
                    
                    // To-Do: Load graphic file in window
                }
            }

            
        }

        /// <summary>
        /// Handles changing the resource type on a selected hak resource when the OnResourceChanged event is raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resourceTypeControl_OnResourceChanged(object sender, ResourceTypeChangedEventArgs e)
        {
            HakResource resource = listBoxResources.SelectedItem as HakResource;
            resource.ResourceType = e.ResourceType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Simple search to see if a resource file path matches an item already in the list box.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool DoesFileExistInListBox(HakResource resource)
        {
            bool exists = false;

            foreach (HakResource currentItem in listBoxResources.Items)
            {
                if (currentItem.ResourcePath == resource.ResourcePath)
                {
                    exists = true;
                }
            }

            return exists;
        }

        /// <summary>
        /// Returns the string used by the content builder to determine the type of processing
        /// it needs to perform.
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        private string GetProcessorType(string fileExtension)
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            FileTypeEnum fileType = factory.GetFileType(fileExtension);
            string processorType = "NONE";


            switch (fileType)
            {
                case FileTypeEnum.Music:
                    processorType = "SongContent";
                    break;
                case FileTypeEnum.Sound:
                    processorType = "SoundEffectProcessor";
                    break;
                case FileTypeEnum.SpriteSheet:
                    processorType = "TextureProcessor";
                    break;
                default:
                    processorType = null;
                    break;
            }

            return processorType;
        }

        /// <summary>
        /// Saves an uncompiled hakpak to the location set by the SaveFilePath property.
        /// If property is not set, this method will do nothing.
        /// </summary>
        private void SaveUncompiledHakpak()
        {
            if (!String.IsNullOrEmpty(SaveFilePath))
            {
                // Delete the existing uncompiled hakpak at the specified location, if any
                File.Delete(SaveFilePath);

                // Create a new zip file at the specified location
                using (ZipFile file = new ZipFile(SaveFilePath))
                {
                    // Set compression level to none - this is in order to speed up processing
                    // in-game and in the toolset
                    file.CompressionLevel = CompressionLevel.None;

                    // Loop through each file in the selected items list and add them to the zip file
                    foreach (string listFile in listBoxResources.Items)
                    {
                        // Note that the "" means put all files in the root of the archive.
                        file.AddFile(listFile, "");
                    }

                    file.Save();
                }
            }
        }

        /// <summary>
        /// Initializes the filters on the Open/Save file dialog boxes.
        /// </summary>
        private void InitializeFileDialogFilters()
        {
            FileExtensionFactory extensions = new FileExtensionFactory();
            string spriteSheetFileExtension = extensions.GetFileExtension(FileTypeEnum.SpriteSheet);
            string soundFileExtension = extensions.GetFileExtension(FileTypeEnum.Sound);
            string musicFileExtension = extensions.GetFileExtension(FileTypeEnum.Music);
            string uncompiledHakpakFileExtension = extensions.GetFileExtension(FileTypeEnum.UncompiledHakpak);

            openFileDialogBuilder.Filter = "All Available Types|*" + spriteSheetFileExtension + ";*" + soundFileExtension + "|" +
                                      "Sprite Sheet Files|*" + spriteSheetFileExtension + "|" +
                                      "Audio Files|*" + soundFileExtension;

            saveFileDialogSaveAs.Filter = "Uncompiled Hakpak (*" + uncompiledHakpakFileExtension + ")|*" + uncompiledHakpakFileExtension;

        }

        /// <summary>
        /// Loops through the list of selected resources and appends unique ID numbers to the end
        /// of the file name. This is done because once compiled, all files have the .xnb extension and may
        /// cause name collisions.
        /// Returns a dictionary of the modified file names. The key is the full path name, including extension.
        /// </summary>
        /// <returns></returns>
        private Dictionary<HakResource, HakResource> GenerateUniqueFileNameList(List<HakResource> fileList)
        {
            List<HakResource> modifiedFileNameList = new List<HakResource>();
            Dictionary<HakResource, HakResource> dictionaryFileNames = new Dictionary<HakResource, HakResource>();

            // Generate unique ID numbers, if necessary.
            foreach (HakResource current in fileList)
            {
                string pureFileName = Path.GetFileNameWithoutExtension(new DirectoryInfo(current.ResourcePath).Name);
                string modFileName = pureFileName;

                // Append a unique ID number to the end of the file's name if it already exists in either list
                int index = 0;
                while (fileList.Exists(x => x.ResourcePath == modFileName) || modifiedFileNameList.Exists(x => x.ResourcePath == modFileName))
                {
                    index++;
                    modFileName = pureFileName + index;
                }

                // The file now has a unique name. Add it to the modified list.
                HakResource modFileResource = new HakResource(modFileName, current.ResourceType);
                modifiedFileNameList.Add(modFileResource);

                // And add it to the dictionary we will be returning once completed.
                dictionaryFileNames.Add(current, modFileResource);
            }

            // We're finished - return the modified file list
            return dictionaryFileNames;
        }

        /// <summary>
        /// Builds a manifest file containing details about each individual resource.
        /// Then returns the path to the file.
        /// </summary>
        private string CreateManifestFile(string directory, List<HakResource> resourceList)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            string fileName = "Manifest.xml";
            string path = directory + "/" + fileName;
            int index = 1;

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("HakpakResources");

                foreach (HakResource resource in resourceList)
                {
                    writer.WriteStartElement("Resource");

                    writer.WriteElementString("ID", Convert.ToString(index));
                    writer.WriteElementString("Name", resource.ResourceName);
                    writer.WriteElementString("Path", resource.ResourcePath);
                    writer.WriteElementString("Type", resource.ResourceType.ToString());

                    writer.WriteEndElement();

                    index++;
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return path;
        }

        #endregion
    }
}
