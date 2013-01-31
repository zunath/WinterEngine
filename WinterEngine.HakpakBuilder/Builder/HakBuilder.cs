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
using WinterEngine.Library;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GUI;

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
            string modelFileExtension = extensions.GetFileExtension(FileTypeEnum.Model);
            string textureFileExtension = extensions.GetFileExtension(FileTypeEnum.Texture);
            string soundFileExtension = extensions.GetFileExtension(FileTypeEnum.Sound);
            string musicFileExtension = extensions.GetFileExtension(FileTypeEnum.Music);

            openFileDialogBuilder.Filter = "All Available Types|*" + textureFileExtension + ";*" + modelFileExtension + ";*" + soundFileExtension + "|" +
                                      "Texture Files|*" + textureFileExtension + "|" +
                                      "Model Files|*" + modelFileExtension + "|" +
                                      "Audio Files|*" + soundFileExtension;

            openFileDialogBuilder.ShowDialog();

            // At least one file was selected
            if (!Object.ReferenceEquals(openFileDialogBuilder.FileNames, null))
            {
                // Loop through the list of file names and add them to the list box
                foreach (string currentFile in openFileDialogBuilder.FileNames)
                {
                    // Does the file exist?
                    if (File.Exists(currentFile))
                    {
                        HakpakResource resource = new HakpakResource { FilePath = currentFile, IsItem = false, Is2D = false, ItemPartType = ItemPartEnum.None };
                        // Is the file in the list already?
                        if (!DoesFileExistInListBox(resource))
                        {
                            // Add the file to the list box
                            listBoxResources.Items.Add(resource);
                        }
                    }
                }

                RepopulateLinkToListBox();
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

            if (!ValidateItemLinks())
            {
                MessageBox.Show("Please ensure that all items are properly linked.", "Missing Item Link", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    textBoxResourceName.Enabled = false;
                    checkBoxIsItem.Enabled = false;
                    comboBoxItemPartType.Enabled = false;
                    listBoxLinkTo.Enabled = false;

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

                // Remove the link to this item that's being removed.
                HakpakResource resource = listBoxResources.Items[index] as HakpakResource;
                resource.LinkedResource.LinkedResource = null;

                // Remove the item at the correct index
                listBoxResources.Items.RemoveAt(index);
            }

            // Clear GUI
            textBoxResourceName.Text = String.Empty;
            comboBoxItemPartType.SelectedIndex = 0;
            checkBoxIsItem.Checked = false;

        }

        /// <summary>
        /// Handles the heavy-lifting of compiling files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            ContentBuilder builder = new ContentBuilder();
            List<string> fileList = new List<string>();
            string destinationDirectory = new DirectoryInfo(saveFileDialog.FileName).Parent.FullName;
            DirectoryInfo tempDirectoryInfo = Directory.CreateDirectory(destinationDirectory + "\\temp");

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
                        fileList.Add(current);
                    }
                }
            }
            // Otherwise, all of the files have different file paths
            else
            {
                // Put the path names listed in the list box into the file list
                foreach (string current in listBoxResources.Items)
                {
                    fileList.Add(current);
                }
            }


            Dictionary<string, string> modifiedFileNameDictionary = GenerateUniqueFileNameList(fileList);

            // Add each file in the list to the builder
            foreach (string file in fileList)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(file);
                string fileNameNoExtension = Path.GetFileNameWithoutExtension(dirInfo.Name);
                string extension = Path.GetExtension(file);
                string processorType = GetProcessorType(extension);

                // Only add to the builder if a processor type is found for the material.
                if (!String.IsNullOrEmpty(processorType))
                {
                    // Take the file's base name and remove the extension. When building gets done, the .xnb extension will be applied.
                    builder.Add(file, modifiedFileNameDictionary[file], null, processorType);
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
                    // Disable compression
                    zipFile.CompressionLevel = CompressionLevel.None;
                    // Retrieve the compiled files from memory 
                    string tempPath = builder.OutputDirectory;
                    string xnaFileExtension = new FileExtensionFactory().GetFileExtension(FileTypeEnum.XNACompiledFile);
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
            textBoxResourceName.Enabled = true;
            checkBoxIsItem.Enabled = true;
            comboBoxItemPartType.Enabled = true;
            listBoxLinkTo.Enabled = true;

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
        /// Handles loading all of the item part types to the resource type combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HakBuilder_Load(object sender, EventArgs e)
        {
            foreach (ItemPartEnum itemPart in Enum.GetValues(typeof(ItemPartEnum)))
            {
                comboBoxItemPartType.Items.Add(itemPart);
            }
            comboBoxItemPartType.SelectedItem = comboBoxItemPartType.Items[0];
        }

        /// <summary>
        /// Handles enabling or disabling the Item option controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxIsItem_CheckedChanged(object sender, EventArgs e)
        {
            HakpakResource resource = listBoxResources.SelectedItem as HakpakResource;

            if (!Object.ReferenceEquals(resource, null))
            {
                resource.IsItem = checkBoxIsItem.Checked;

                comboBoxItemPartType.Enabled = checkBoxIsItem.Checked;

                if (resource.ItemPartType != ItemPartEnum.None && checkBoxIsItem.Checked)
                {
                    listBoxLinkTo.Enabled = true;
                }
                else
                {
                    listBoxLinkTo.Enabled = false;
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
            HakpakResource resource = listBoxResources.SelectedItem as HakpakResource;

            if (!Object.ReferenceEquals(resource, null))
            {
                textBoxResourceName.Text = resource.ResourceName;
                checkBoxIsItem.Checked = resource.IsItem;
                
                comboBoxItemPartType.SelectedItem = resource.ItemPartType;

                textBoxResourceName.Enabled = true;
                checkBoxIsItem.Enabled = true;
            }
            else
            {
                textBoxResourceName.Enabled = false;
                checkBoxIsItem.Checked = false;
                checkBoxIsItem.Enabled = false;
                listBoxLinkTo.Enabled = false;
                comboBoxItemPartType.Enabled = false;
            }

            RepopulateLinkToListBox();
        }

        /// <summary>
        /// Update the resource object's name as the text field changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxResourceName_TextChanged(object sender, EventArgs e)
        {
            HakpakResource resource = listBoxResources.SelectedItem as HakpakResource;

            if (!Object.ReferenceEquals(resource, null))
            {
                resource.ResourceName = textBoxResourceName.Text;
            }
        }

        /// <summary>
        /// Handles updating the selected hakpak resource's ItemPartType property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItemPartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HakpakResource resource = listBoxResources.SelectedItem as HakpakResource;

            if (!Object.ReferenceEquals(resource, null))
            {
                resource.ItemPartType = (ItemPartEnum)comboBoxItemPartType.SelectedIndex + 1;

                if (!Object.ReferenceEquals(resource.LinkedResource, null))
                {
                    resource.LinkedResource.ItemPartType = (ItemPartEnum)comboBoxItemPartType.SelectedIndex + 1;
                }

                // If resource has no 3D model, it cannot be linked.
                if (resource.ItemPartType == ItemPartEnum.None)
                {
                    if (!Object.ReferenceEquals(resource.LinkedResource, null))
                    {
                        resource.LinkedResource.LinkedResource = null;
                    }
                     
                    resource.LinkedResource = null;
                    listBoxLinkTo.Enabled = false;
                }
                else
                {
                    listBoxLinkTo.Enabled = true;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles repopulating the link to list box based on 
        /// the currently selected item in the resources list box.
        /// </summary>
        private void RepopulateLinkToListBox()
        {
            listBoxLinkTo.Items.Clear();

            listBoxLinkTo.Items.Add(new HakpakResource { ResourceName = "No Link" });

            if (!Object.ReferenceEquals(listBoxResources.SelectedItem, null))
            {
                FileExtensionFactory factory = new FileExtensionFactory();
                HakpakResource selectedResource = listBoxResources.SelectedItem as HakpakResource;

                foreach (HakpakResource resource in listBoxResources.Items)
                {
                    // If a resource is already linked, it will not appear in the list.
                    if ((selectedResource != resource && Object.ReferenceEquals(resource.LinkedResource, null))
                        || resource.LinkedResource == selectedResource)
                    {
                        // If a texture is selected, display only model files in the package.
                        if (selectedResource.FileExtension == factory.GetFileExtension(FileTypeEnum.Texture)
                            && resource.FileExtension == factory.GetFileExtension(FileTypeEnum.Model))
                        {
                            listBoxLinkTo.Items.Add(resource);
                        }

                        // If a model is selected, display only texture files in the package.
                        else if (selectedResource.FileExtension == factory.GetFileExtension(FileTypeEnum.Model)
                            && resource.FileExtension == factory.GetFileExtension(FileTypeEnum.Texture))
                        {
                            listBoxLinkTo.Items.Add(resource);
                         
                        }

                        // Select the linked resource
                        if (resource.LinkedResource == selectedResource)
                        {
                            listBoxLinkTo.SelectedItem = resource;
                        }

                    }
                }

                // Resource has no linked resource - select the "No Link" option by default
                if (Object.ReferenceEquals(selectedResource.LinkedResource, null))
                {
                    listBoxLinkTo.SelectedIndex = 0;
                }

            }
        }

        /// <summary>
        /// Returns true if all item resources are valid (properly linked).
        /// Returns false if an item resource is not valid.
        /// </summary>
        /// <returns></returns>
        private bool ValidateItemLinks()
        {
            foreach (HakpakResource resource in listBoxResources.Items)
            {
                if (resource.IsItem)
                {
                    if (resource.ItemPartType != ItemPartEnum.None)
                    {
                        if (Object.ReferenceEquals(resource.LinkedResource, null))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Simple search to see if a file name matches an item already in the list box.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool DoesFileExistInListBox(HakpakResource resource)
        {
            foreach (HakpakResource currentItem in listBoxResources.Items)
            {
                if (currentItem.FilePath == resource.FilePath)
                    return true;
            }

            return false;
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
                case FileTypeEnum.Model:
                    processorType = "ModelProcessor";
                    break;
                case FileTypeEnum.Texture:
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
            string modelFileExtension = extensions.GetFileExtension(FileTypeEnum.Model);
            string textureFileExtension = extensions.GetFileExtension(FileTypeEnum.Texture);
            string soundFileExtension = extensions.GetFileExtension(FileTypeEnum.Sound);
            string musicFileExtension = extensions.GetFileExtension(FileTypeEnum.Music);
            string uncompiledHakpakFileExtension = extensions.GetFileExtension(FileTypeEnum.UncompiledHakpak);

            openFileDialogBuilder.Filter = "All Available Types|*" + textureFileExtension + ";*" + modelFileExtension + ";*" + soundFileExtension + "|" +
                                      "Texture Files|*" + textureFileExtension + "|" +
                                      "Model Files|*" + modelFileExtension + "|" +
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
        private Dictionary<string, string> GenerateUniqueFileNameList(List<string> fileList)
        {
            List<string> modifiedFileNameList = new List<string>();
            Dictionary<string, string> dictionaryFileNames = new Dictionary<string, string>();

            // Generate unique ID numbers, if necessary.
            foreach (string current in fileList)
            {
                string pureFileName = Path.GetFileNameWithoutExtension(new DirectoryInfo(current).Name);
                string modFileName = pureFileName;

                // Append a unique ID number to the end of the file's name if it already exists in either list
                int index = 0;
                while (fileList.Exists(x => x == modFileName) || modifiedFileNameList.Exists(x => x == modFileName))
                {
                    index++;
                    modFileName = pureFileName + index;
                }

                // The file now has a unique name. Add it to the modified list.
                modifiedFileNameList.Add(modFileName);

                // And add it to the dictionary we will be returning once completed.
                dictionaryFileNames.Add(current, modFileName);
            }

            // We're finished - return the modified file list
            return dictionaryFileNames;
        }

        #endregion

        /// <summary>
        /// Link the selected resource with the selected linked object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxLinkTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            HakpakResource resource = listBoxResources.SelectedItem as HakpakResource;
            HakpakResource linkedResource = listBoxLinkTo.SelectedItem as HakpakResource;

            if (!Object.ReferenceEquals(resource, null) && !Object.ReferenceEquals(linkedResource, null))
            {
                // Unlink previous resource
                if (!Object.ReferenceEquals(resource.LinkedResource, null))
                {
                    resource.LinkedResource.LinkedResource = null;
                }

                // 0 represents the "No Link" option.
                if (listBoxLinkTo.SelectedIndex != 0)
                {
                    linkedResource.ItemPartType = resource.ItemPartType;
                    linkedResource.IsItem = resource.IsItem;

                    resource.LinkedResource = linkedResource;
                    linkedResource.LinkedResource = resource;
                }
                // "No Link" option was selected. Mark linked resources to null.
                else
                {
                    if (!Object.ReferenceEquals(resource.LinkedResource, null))
                    {
                        resource.LinkedResource.LinkedResource = null;
                        resource.LinkedResource = null;
                    }
                }
            }
        }
    }
}
