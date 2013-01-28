using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class ItemPropertiesControl : UserControl
    {
        #region Fields

        private ObjectViewer3D _itemModel;
        private ObjectViewer2D _itemIcon;
        private Item _backupItem;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the backup item which is used to revert changes.
        /// </summary>
        public Item BackupItem
        {
            get { return _backupItem; }
            set { _backupItem = value; }
        }

        /// <summary>
        /// Gets or sets the item icon 
        /// </summary>
        private ObjectViewer2D ItemIcon
        {
            get { return _itemIcon; }
            set { _itemIcon = value; }
        }

        /// <summary>
        /// Gets or sets the item model
        /// </summary>
        private ObjectViewer3D ItemModel
        {
            get { return _itemModel; }
            set { _itemModel = value; }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnSaveItem;

        #endregion

        #region Event Handling

        /// <summary>
        /// Handles populating the list boxes with selectable graphic resources.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemPropertiesControl_Load(object sender, EventArgs e)
        {
            PopulateControls();
        }

        /// <summary>
        /// Handles loading an icon and model in the respective viewer panels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAppearance_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemIcon.LoadGraphic(listBoxAppearance.SelectedItem as GraphicResource);   
        }

        /// <summary>
        /// Handles updating an item's entry in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChangesItemDetails_Click(object sender, EventArgs e)
        {
            if (nameTextBoxItem.IsValid && tagTextBoxItem.IsValid)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    Item item = new Item();
                    item.Comment = textBoxItemComments.Text;
                    item.Description = textBoxItemDescription.Text;
                    item.Name = nameTextBoxItem.NameText;
                    item.Tag = tagTextBoxItem.TagText;
                    item.Resref = resrefTextBoxItem.ResrefText;
                    item.Price = (int)numericUpDownPrice.Value;
                    item.Weight = (int)numericUpDownWeight.Value;
                    item.ResourceCategoryID = BackupItem.ResourceCategoryID;

                    repo.Update(resrefTextBoxItem.ResrefText, item);
                    BackupItem = item;

                    GameObjectEventArgs eventArgs = new GameObjectEventArgs();
                    eventArgs.GameObject = item;
                    OnSaveItem(this, eventArgs);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid name and tag.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!nameTextBoxItem.IsValid)
                {
                    nameTextBoxItem.Focus();
                }
                else
                {
                    tagTextBoxItem.Focus();
                }
            }
        }

        /// <summary>
        /// Handles reverting all input fields to the backup item's values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiscardChangesItemDetails_Click(object sender, EventArgs e)
        {
            textBoxItemComments.Text = BackupItem.Comment;
            textBoxItemDescription.Text = BackupItem.Description;
            nameTextBoxItem.NameText = BackupItem.Name;
            resrefTextBoxItem.ResrefText = BackupItem.Resref;
            tagTextBoxItem.TagText = BackupItem.Tag;
            numericUpDownPrice.Value = BackupItem.Price;
            numericUpDownWeight.Value = BackupItem.Weight;

        }

        #endregion

        #region Constructors

        public ItemPropertiesControl()
        {
            InitializeComponent();

            // Designer in VS2010 has issues with custom controls.
            // Manually add the 3D object viewer when the program runs.
            _itemModel = new ObjectViewer3D();
            _itemModel.Dock = DockStyle.Fill;
            panelItemModelViewer.Controls.Add(_itemModel);

            _itemIcon = new ObjectViewer2D();
            _itemIcon.Dock = DockStyle.Fill;
            panelItemIconViewer.Controls.Add(_itemIcon);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates all controls with graphic resource options.
        /// </summary>
        public void PopulateControls()
        {
            using (GraphicResourceRepository repo = new GraphicResourceRepository())
            {
                List<GraphicResource> iconList = repo.GetAll2DGraphics(ResourceTypeEnum.Item);
                List<GraphicResource> modelList = repo.GetAll3DGraphics(ResourceTypeEnum.Item);

                foreach (GraphicResource resource in iconList)
                {
                    resource.TemporaryDisplayName = resource.ResourceFileName;
                    listBoxAppearance.Items.Add(resource);
                }

                foreach (GraphicResource resource in modelList)
                {
                    resource.TemporaryDisplayName = resource.ResourceFileName;
                    listBoxAppearance.Items.Add(resource);
                }
            }
        }

        /// <summary>
        /// Handles enabling controls for item manipulation when an item is loaded.
        /// </summary>
        /// <param name="item"></param>
        public void LoadItem(Item item)
        {
            BackupItem = item;

            // Re-enable controls
            tabControlProperties.Enabled = true;
            buttonApplyChangesItemDetails.Enabled = true;
            buttonDiscardChangesItemDetails.Enabled = true;

            // Load data into controls
            nameTextBoxItem.NameText = item.Name;
            tagTextBoxItem.TagText = item.Tag;
            resrefTextBoxItem.ResrefText = item.Resref;

            textBoxItemComments.Text = item.Comment;
            textBoxItemDescription.Text = item.Description;
            numericUpDownPrice.Value = item.Price;
            numericUpDownWeight.Value = item.Weight;
        }

        

        #endregion




    }
}
