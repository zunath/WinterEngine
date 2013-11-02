using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.XMLObjects;
using WinterEngine.Library.Managers;

namespace WinterEngine.ContentPackageCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        IGameResourceManager _gameResourceManager;
        #endregion

        #region Properties

        private ContentPackageXML Model { get; set; }
        private OpenFileDialog OpenFilePrompt { get; set; }
        private SaveFileDialog SaveFilePrompt { get; set; }
        private FileExtensionFactory ExtensionFactory { get; set; }

        #endregion

        #region Constructors

        public MainWindow(IGameResourceManager gameResourceManager)
        {
            if (gameResourceManager == null) throw new ArgumentNullException("gameResourceManager");
            _gameResourceManager = gameResourceManager;

            InitializeComponent();
        }

        #endregion

        #region Menu Item Methods

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            if (!Model.IsModified || !PromptToSaveChanges())
            {
                Application.Current.Shutdown();
            }
        }

        private void miSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveContentPackage(true);
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            SaveContentPackage(false);
        }

        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFilePrompt.Filter = ExtensionFactory.BuildContentPackageFileFilter();
            OpenFilePrompt.Multiselect = false;
            if ((bool)OpenFilePrompt.ShowDialog())
            {
                if(Path.GetExtension(OpenFilePrompt.FileName) != ExtensionFactory.GetFileExtension(FileTypeEnum.ContentPackage))
                {
                    MessageBox.Show("Invalid content package file.", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (File.Exists(OpenFilePrompt.FileName))
                {
                    if (!Model.IsModified || !PromptToSaveChanges())
                    {
                        LoadContentPackage(OpenFilePrompt.FileName);
                    }
                }
            }
        }

        private void miNew_Click(object sender, RoutedEventArgs e)
        {
            if (!Model.IsModified || !PromptToSaveChanges())
            {
                txtDescription.Text = "";
                txtName.Text = "";
                Model.ResourceList.Clear();
            }
        }

        #endregion

        #region Button Methods

        private void btnAddFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFilePrompt.Filter = ExtensionFactory.BuildContentPackageResourceFileFilter();
            OpenFilePrompt.Multiselect = true;

            if ((bool)OpenFilePrompt.ShowDialog())
            {
                foreach (string path in OpenFilePrompt.FileNames)
                {
                    string file = Path.GetFileName(path);
                    ContentPackageResourceXML resource = Model.ResourceList.SingleOrDefault(x => x.FileName == file);
                    if (resource == null)
                    {
                        resource = new ContentPackageResourceXML
                        {
                            FilePath = path,
                            FileName = file,
                            ResourceType = ContentPackageResourceTypeEnum.None,
                            IsInPackage = false
                        };
                        Model.ResourceList.Add(resource);
                    }
                }

                if (OpenFilePrompt.FileNames.Count() > 0)
                {
                    Model.IsModified = true;
                }
            }
        }

        private void btnRemoveFiles_Click(object sender, RoutedEventArgs e)
        {
            List<ContentPackageResourceXML> resourceList = lstAddedResources.SelectedItems.Cast<ContentPackageResourceXML>().ToList();

            foreach (ContentPackageResourceXML resource in resourceList)
            {
                ContentPackageResourceXML resourceToRemove = Model.ResourceList.SingleOrDefault(x => x.FileName == resource.FileName);
                Model.ResourceList.Remove(resourceToRemove);
            }

            if (resourceList.Count > 0)
            {
                Model.IsModified = true;
            }
        }

        private void resourceTypeButtons_Checked(object sender, RoutedEventArgs e)
        {
            List<ContentPackageResourceXML> selectedResources = lstAddedResources.SelectedItems.Cast<ContentPackageResourceXML>().ToList();
            foreach (ContentPackageResourceXML resource in selectedResources)
            {
                if ((bool)rdoBGM.IsChecked) resource.ResourceType = ContentPackageResourceTypeEnum.BGM;
                else if ((bool)rdoCharacter.IsChecked) resource.ResourceType = ContentPackageResourceTypeEnum.Character;
                else if ((bool)rdoPlaceable.IsChecked) resource.ResourceType = ContentPackageResourceTypeEnum.Placeable;
                else if ((bool)rdoSoundEffect.IsChecked) resource.ResourceType = ContentPackageResourceTypeEnum.SoundEffect;
                else if ((bool)rdoTileset.IsChecked) resource.ResourceType = ContentPackageResourceTypeEnum.Tileset;
            }
        }

        #endregion

        #region Object List Methods

        private void lstAddedResources_Selected(object sender, RoutedEventArgs e)
        {
            rdoBGM.IsEnabled = false;
            rdoSoundEffect.IsEnabled = false;
            rdoCharacter.IsEnabled = false;
            rdoPlaceable.IsEnabled = false;
            rdoTileset.IsEnabled = false;
            ContentPackageResourceXML resource = lstAddedResources.SelectedItem as ContentPackageResourceXML;
            if (resource == null || lstAddedResources.SelectedItems.Count > 1) return;
            
            string extension = Path.GetExtension(resource.FileName).ToLower();

            if (extension == ".png")
            {
                rdoCharacter.IsEnabled = true;
                rdoPlaceable.IsEnabled = true;
                rdoTileset.IsEnabled = true;
            }
            else if (extension == ".mp3")
            {
                rdoBGM.IsEnabled = true;
            }
            else if (extension == ".wav")
            {
                rdoSoundEffect.IsEnabled = true;
            }


            switch (resource.ResourceType)
            {
                case ContentPackageResourceTypeEnum.BGM:
                    rdoBGM.IsChecked = true;
                    break;
                case ContentPackageResourceTypeEnum.Character:
                    rdoCharacter.IsChecked = true;
                    break;
                case ContentPackageResourceTypeEnum.Placeable:
                    rdoPlaceable.IsChecked = true;
                    break;
                case ContentPackageResourceTypeEnum.SoundEffect:
                    rdoSoundEffect.IsChecked = true;
                    break;
                case ContentPackageResourceTypeEnum.Tileset:
                    rdoTileset.IsChecked = true;
                    break;
                default:
                    rdoBGM.IsChecked = false;
                    rdoCharacter.IsChecked = false;
                    rdoPlaceable.IsChecked = false;
                    rdoSoundEffect.IsChecked = false;
                    rdoTileset.IsChecked = false;
                    break;
            }


        }

        private void SaveContentPackage(bool forceLocationSelection)
        {
            if (!ValidateResourceEntries()) return;

            if (forceLocationSelection || String.IsNullOrWhiteSpace(Model.FilePath))
            {
                SaveFilePrompt.Filter = ExtensionFactory.BuildContentPackageFileFilter();
                if ((bool)SaveFilePrompt.ShowDialog())
                {
                    Model.FilePath = SaveFilePrompt.FileName;
                }
            }

            if (!String.IsNullOrWhiteSpace(Model.FilePath))
            {
                try
                {
                    Model.Description = txtDescription.Text;
                    Model.Name = txtName.Text;

                    XmlSerializer serializer = new XmlSerializer(typeof(ContentPackageXML));
                    StringWriter stringWriter = new StringWriter();
                    XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.ASCII};
                    XmlWriter writer = XmlWriter.Create(stringWriter, settings);
                    serializer.Serialize(writer, Model);
                    string manifestXML = stringWriter.ToString();

                    File.WriteAllText("./Manifest.xml", manifestXML);

                    using (ZipFile zipFile = new ZipFile(Model.FilePath))
                    {
                        zipFile.CompressionLevel = CompressionLevel.None;
                        
                        // Update the Manifest.xml file.
                        if (zipFile["Manifest.xml"] != null)
                        {
                            zipFile.RemoveEntry("Manifest.xml");
                        }
                        zipFile.AddFile("./Manifest.xml", "");

                        List<ContentPackageResourceXML> resourceList = lstAddedResources.Items.Cast<ContentPackageResourceXML>().ToList();
                        
                        // Remove files which are no longer used.
                        for(int index = zipFile.Entries.Count; index > 0; index--)
                        {
                            ZipEntry entry = zipFile[index-1];
                            if (entry.FileName != "Manifest.xml")
                            {
                                if (!resourceList.Exists(x => x.FileName == entry.FileName))
                                {
                                    zipFile.RemoveEntry(entry);
                                }
                            }
                        }
                        
                        // Upsert new/modified files
                        foreach (ContentPackageResourceXML resource in resourceList)
                        {
                            ZipEntry entry = zipFile[resource.FileName];

                            if(entry == null)
                            {
                                zipFile.AddFile(resource.FilePath, "");
                            }
                            else
                            {
                                // Modified file isn't in the package. Remove existing and replace with new version.
                                if (!resource.IsInPackage && File.Exists(resource.FilePath))
                                {
                                    zipFile.RemoveEntry(entry);
                                    zipFile.AddFile(resource.FilePath, "");
                                }
                            }
                        }

                        zipFile.Save();

                        // Mark all as "unmodified"
                        resourceList.ForEach(a => a.IsInPackage = true);

                        // Clean up
                        File.Delete("./Manifest.xml");
                        Model.IsModified = false;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private void LoadContentPackage(string filePath)
        {
            try
            {
                Model = _gameResourceManager.DeserializeContentPackageFile(filePath);
                foreach (ContentPackageResourceXML resource in Model.ResourceList)
                {
                    resource.IsInPackage = true;
                }

                lstAddedResources.DataContext = Model.ResourceList;
                txtDescription.Text = Model.Description;
                txtName.Text = Model.Name;

                Model.FilePath = filePath;
            }
            catch
            {
                throw;
            }
        }

        private bool ValidateResourceEntries()
        {
            bool success = false;

            // Check for invalid resource types
            List<ContentPackageResourceXML> invalidResourceTypeList = Model.ResourceList.Where(x => x.ResourceType == ContentPackageResourceTypeEnum.None).ToList();

            if (invalidResourceTypeList.Count() <= 0)
            {
                success = true;
            }
            else
            {
                MessageBox.Show("Please ensure you have specified a resource type for all resources.", "Invalid Resource Types", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return success;
        }

        #endregion

        #region Form Methods

        private void Window_Initialized(object sender, EventArgs e)
        {
            Model = new ContentPackageXML();
            ExtensionFactory = new FileExtensionFactory();
            SaveFilePrompt = new SaveFileDialog();
            OpenFilePrompt = new OpenFileDialog();

            lstAddedResources.DataContext = Model.ResourceList;
        }

        private bool PromptToSaveChanges()
        {
            bool userCanceled = true;

            if (Model.IsModified)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save changes to this content package?",
                    "Unsaved Changes Pending", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    userCanceled = false;
                    Model.IsModified = false;
                    SaveContentPackage(false);
                }
                else if (result == MessageBoxResult.No)
                {
                    userCanceled = false;
                    Model.IsModified = false;
                }
            }

            return userCanceled;
        }

        #endregion


    }
}
