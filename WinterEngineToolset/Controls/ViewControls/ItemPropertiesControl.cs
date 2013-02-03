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
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class ItemPropertiesControl : UserControl
    {
        #region Fields

        private XNAControl _itemIcon;
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
        private XNAControl ItemIcon
        {
            get { return _itemIcon; }
            set { _itemIcon = value; }
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
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates all controls with graphic resource options.
        /// </summary>
        public void PopulateControls()
        {
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
