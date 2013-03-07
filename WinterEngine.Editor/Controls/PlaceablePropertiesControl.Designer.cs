namespace WinterEngine.Editor.Controls
{
    partial class PlaceablePropertiesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageItemDetails = new System.Windows.Forms.TabPage();
            this.checkBoxHasInventory = new System.Windows.Forms.CheckBox();
            this.checkBoxUseable = new System.Windows.Forms.CheckBox();
            this.tagTextBoxPlaceable = new WinterEngine.Forms.Controls.Standard.TagTextBox();
            this.resrefTextBoxPlaceable = new WinterEngine.Forms.Controls.Standard.ResrefTextBox();
            this.nameTextBoxPlaceable = new WinterEngine.Forms.Controls.Standard.NameTextBox();
            this.labelItemDetailsHeader = new System.Windows.Forms.Label();
            this.labelItemResref = new System.Windows.Forms.Label();
            this.labelItemTag = new System.Windows.Forms.Label();
            this.labelItemName = new System.Windows.Forms.Label();
            this.tabPageEvents = new System.Windows.Forms.TabPage();
            this.buttonEditOnUserDefined = new System.Windows.Forms.Button();
            this.comboBoxOnUserDefined = new System.Windows.Forms.ComboBox();
            this.labelOnUserDefined = new System.Windows.Forms.Label();
            this.buttonEditOnUsed = new System.Windows.Forms.Button();
            this.comboBoxOnUsed = new System.Windows.Forms.ComboBox();
            this.labelOnUsed = new System.Windows.Forms.Label();
            this.buttonEditOnUnLock = new System.Windows.Forms.Button();
            this.comboBoxOnUnLock = new System.Windows.Forms.ComboBox();
            this.labelOnUnLock = new System.Windows.Forms.Label();
            this.buttonEditOnSpellCastAt = new System.Windows.Forms.Button();
            this.comboBoxOnSpellCastAt = new System.Windows.Forms.ComboBox();
            this.labelOnSpellCastAt = new System.Windows.Forms.Label();
            this.buttonEditOnOpen = new System.Windows.Forms.Button();
            this.comboBoxOnOpen = new System.Windows.Forms.ComboBox();
            this.labelOnOpen = new System.Windows.Forms.Label();
            this.buttonEditOnPhysicalAttacked = new System.Windows.Forms.Button();
            this.comboBoxOnPhysicalAttacked = new System.Windows.Forms.ComboBox();
            this.labelOnPhysicalAttacked = new System.Windows.Forms.Label();
            this.buttonEditOnLock = new System.Windows.Forms.Button();
            this.comboBoxOnLock = new System.Windows.Forms.ComboBox();
            this.labelOnLock = new System.Windows.Forms.Label();
            this.buttonEditOnDisturbed = new System.Windows.Forms.Button();
            this.comboBoxOnDisturbed = new System.Windows.Forms.ComboBox();
            this.labelOnDisturbed = new System.Windows.Forms.Label();
            this.buttonEditOnHeartbeat = new System.Windows.Forms.Button();
            this.comboBoxOnHeartbeat = new System.Windows.Forms.ComboBox();
            this.labelOnHeartbeat = new System.Windows.Forms.Label();
            this.buttonEditOnDeath = new System.Windows.Forms.Button();
            this.comboBoxOnDeath = new System.Windows.Forms.ComboBox();
            this.labelOnDeath = new System.Windows.Forms.Label();
            this.buttonEditOnDamaged = new System.Windows.Forms.Button();
            this.comboBoxOnDamaged = new System.Windows.Forms.ComboBox();
            this.labelOnDamaged = new System.Windows.Forms.Label();
            this.buttonEditOnClose = new System.Windows.Forms.Button();
            this.comboBoxOnClose = new System.Windows.Forms.ComboBox();
            this.labelOnClose = new System.Windows.Forms.Label();
            this.buttonEditOnClick = new System.Windows.Forms.Button();
            this.comboBoxOnClick = new System.Windows.Forms.ComboBox();
            this.labelOnClick = new System.Windows.Forms.Label();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.labelPlaceableDescription = new System.Windows.Forms.Label();
            this.textBoxPlaceableDescription = new System.Windows.Forms.TextBox();
            this.tabPageComments = new System.Windows.Forms.TabPage();
            this.labelPlaceableComments = new System.Windows.Forms.Label();
            this.textBoxPlaceableComments = new System.Windows.Forms.TextBox();
            this.buttonDiscardChanges = new System.Windows.Forms.Button();
            this.buttonApplyChanges = new System.Windows.Forms.Button();
            this.tabControlProperties.SuspendLayout();
            this.tabPageItemDetails.SuspendLayout();
            this.tabPageEvents.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.tabPageComments.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperties.Controls.Add(this.tabPageItemDetails);
            this.tabControlProperties.Controls.Add(this.tabPageEvents);
            this.tabControlProperties.Controls.Add(this.tabPageDescription);
            this.tabControlProperties.Controls.Add(this.tabPageComments);
            this.tabControlProperties.Enabled = false;
            this.tabControlProperties.Location = new System.Drawing.Point(3, 3);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(308, 417);
            this.tabControlProperties.TabIndex = 3;
            // 
            // tabPageItemDetails
            // 
            this.tabPageItemDetails.Controls.Add(this.checkBoxHasInventory);
            this.tabPageItemDetails.Controls.Add(this.checkBoxUseable);
            this.tabPageItemDetails.Controls.Add(this.tagTextBoxPlaceable);
            this.tabPageItemDetails.Controls.Add(this.resrefTextBoxPlaceable);
            this.tabPageItemDetails.Controls.Add(this.nameTextBoxPlaceable);
            this.tabPageItemDetails.Controls.Add(this.labelItemDetailsHeader);
            this.tabPageItemDetails.Controls.Add(this.labelItemResref);
            this.tabPageItemDetails.Controls.Add(this.labelItemTag);
            this.tabPageItemDetails.Controls.Add(this.labelItemName);
            this.tabPageItemDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageItemDetails.Name = "tabPageItemDetails";
            this.tabPageItemDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItemDetails.Size = new System.Drawing.Size(300, 391);
            this.tabPageItemDetails.TabIndex = 1;
            this.tabPageItemDetails.Text = "Details";
            this.tabPageItemDetails.UseVisualStyleBackColor = true;
            // 
            // checkBoxHasInventory
            // 
            this.checkBoxHasInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxHasInventory.AutoSize = true;
            this.checkBoxHasInventory.Location = new System.Drawing.Point(170, 180);
            this.checkBoxHasInventory.Name = "checkBoxHasInventory";
            this.checkBoxHasInventory.Size = new System.Drawing.Size(92, 17);
            this.checkBoxHasInventory.TabIndex = 12;
            this.checkBoxHasInventory.Text = "Has Inventory";
            this.checkBoxHasInventory.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseable
            // 
            this.checkBoxUseable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseable.AutoSize = true;
            this.checkBoxUseable.Location = new System.Drawing.Point(170, 157);
            this.checkBoxUseable.Name = "checkBoxUseable";
            this.checkBoxUseable.Size = new System.Drawing.Size(65, 17);
            this.checkBoxUseable.TabIndex = 11;
            this.checkBoxUseable.Text = "Useable";
            this.checkBoxUseable.UseVisualStyleBackColor = true;
            // 
            // tagTextBoxPlaceable
            // 
            this.tagTextBoxPlaceable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagTextBoxPlaceable.IsValid = true;
            this.tagTextBoxPlaceable.Location = new System.Drawing.Point(81, 86);
            this.tagTextBoxPlaceable.Name = "tagTextBoxPlaceable";
            this.tagTextBoxPlaceable.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.tagTextBoxPlaceable.Size = new System.Drawing.Size(216, 28);
            this.tagTextBoxPlaceable.TabIndex = 10;
            this.tagTextBoxPlaceable.TagText = "";
            // 
            // resrefTextBoxPlaceable
            // 
            this.resrefTextBoxPlaceable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resrefTextBoxPlaceable.Enabled = false;
            this.resrefTextBoxPlaceable.IsValid = false;
            this.resrefTextBoxPlaceable.Location = new System.Drawing.Point(81, 112);
            this.resrefTextBoxPlaceable.Name = "resrefTextBoxPlaceable";
            this.resrefTextBoxPlaceable.ResourceType = WinterEngine.DataTransferObjects.Enumerations.ResourceTypeEnum.Area;
            this.resrefTextBoxPlaceable.ResrefText = "";
            this.resrefTextBoxPlaceable.Size = new System.Drawing.Size(216, 28);
            this.resrefTextBoxPlaceable.TabIndex = 9;
            // 
            // nameTextBoxPlaceable
            // 
            this.nameTextBoxPlaceable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBoxPlaceable.IsValid = true;
            this.nameTextBoxPlaceable.Location = new System.Drawing.Point(81, 58);
            this.nameTextBoxPlaceable.Name = "nameTextBoxPlaceable";
            this.nameTextBoxPlaceable.NameText = "";
            this.nameTextBoxPlaceable.Size = new System.Drawing.Size(216, 28);
            this.nameTextBoxPlaceable.TabIndex = 8;
            // 
            // labelItemDetailsHeader
            // 
            this.labelItemDetailsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelItemDetailsHeader.AutoSize = true;
            this.labelItemDetailsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemDetailsHeader.Location = new System.Drawing.Point(47, 16);
            this.labelItemDetailsHeader.Name = "labelItemDetailsHeader";
            this.labelItemDetailsHeader.Size = new System.Drawing.Size(224, 31);
            this.labelItemDetailsHeader.TabIndex = 7;
            this.labelItemDetailsHeader.Text = "Placeable Details";
            // 
            // labelItemResref
            // 
            this.labelItemResref.AutoSize = true;
            this.labelItemResref.Location = new System.Drawing.Point(6, 123);
            this.labelItemResref.Name = "labelItemResref";
            this.labelItemResref.Size = new System.Drawing.Size(41, 13);
            this.labelItemResref.TabIndex = 3;
            this.labelItemResref.Text = "Resref:";
            // 
            // labelItemTag
            // 
            this.labelItemTag.AutoSize = true;
            this.labelItemTag.Location = new System.Drawing.Point(6, 95);
            this.labelItemTag.Name = "labelItemTag";
            this.labelItemTag.Size = new System.Drawing.Size(29, 13);
            this.labelItemTag.TabIndex = 2;
            this.labelItemTag.Text = "Tag:";
            // 
            // labelItemName
            // 
            this.labelItemName.AutoSize = true;
            this.labelItemName.Location = new System.Drawing.Point(6, 68);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(38, 13);
            this.labelItemName.TabIndex = 1;
            this.labelItemName.Text = "Name:";
            // 
            // tabPageEvents
            // 
            this.tabPageEvents.Controls.Add(this.buttonEditOnUserDefined);
            this.tabPageEvents.Controls.Add(this.comboBoxOnUserDefined);
            this.tabPageEvents.Controls.Add(this.labelOnUserDefined);
            this.tabPageEvents.Controls.Add(this.buttonEditOnUsed);
            this.tabPageEvents.Controls.Add(this.comboBoxOnUsed);
            this.tabPageEvents.Controls.Add(this.labelOnUsed);
            this.tabPageEvents.Controls.Add(this.buttonEditOnUnLock);
            this.tabPageEvents.Controls.Add(this.comboBoxOnUnLock);
            this.tabPageEvents.Controls.Add(this.labelOnUnLock);
            this.tabPageEvents.Controls.Add(this.buttonEditOnSpellCastAt);
            this.tabPageEvents.Controls.Add(this.comboBoxOnSpellCastAt);
            this.tabPageEvents.Controls.Add(this.labelOnSpellCastAt);
            this.tabPageEvents.Controls.Add(this.buttonEditOnOpen);
            this.tabPageEvents.Controls.Add(this.comboBoxOnOpen);
            this.tabPageEvents.Controls.Add(this.labelOnOpen);
            this.tabPageEvents.Controls.Add(this.buttonEditOnPhysicalAttacked);
            this.tabPageEvents.Controls.Add(this.comboBoxOnPhysicalAttacked);
            this.tabPageEvents.Controls.Add(this.labelOnPhysicalAttacked);
            this.tabPageEvents.Controls.Add(this.buttonEditOnLock);
            this.tabPageEvents.Controls.Add(this.comboBoxOnLock);
            this.tabPageEvents.Controls.Add(this.labelOnLock);
            this.tabPageEvents.Controls.Add(this.buttonEditOnDisturbed);
            this.tabPageEvents.Controls.Add(this.comboBoxOnDisturbed);
            this.tabPageEvents.Controls.Add(this.labelOnDisturbed);
            this.tabPageEvents.Controls.Add(this.buttonEditOnHeartbeat);
            this.tabPageEvents.Controls.Add(this.comboBoxOnHeartbeat);
            this.tabPageEvents.Controls.Add(this.labelOnHeartbeat);
            this.tabPageEvents.Controls.Add(this.buttonEditOnDeath);
            this.tabPageEvents.Controls.Add(this.comboBoxOnDeath);
            this.tabPageEvents.Controls.Add(this.labelOnDeath);
            this.tabPageEvents.Controls.Add(this.buttonEditOnDamaged);
            this.tabPageEvents.Controls.Add(this.comboBoxOnDamaged);
            this.tabPageEvents.Controls.Add(this.labelOnDamaged);
            this.tabPageEvents.Controls.Add(this.buttonEditOnClose);
            this.tabPageEvents.Controls.Add(this.comboBoxOnClose);
            this.tabPageEvents.Controls.Add(this.labelOnClose);
            this.tabPageEvents.Controls.Add(this.buttonEditOnClick);
            this.tabPageEvents.Controls.Add(this.comboBoxOnClick);
            this.tabPageEvents.Controls.Add(this.labelOnClick);
            this.tabPageEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Size = new System.Drawing.Size(300, 391);
            this.tabPageEvents.TabIndex = 4;
            this.tabPageEvents.Text = "Events";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            // 
            // buttonEditOnUserDefined
            // 
            this.buttonEditOnUserDefined.Location = new System.Drawing.Point(317, 343);
            this.buttonEditOnUserDefined.Name = "buttonEditOnUserDefined";
            this.buttonEditOnUserDefined.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnUserDefined.TabIndex = 38;
            this.buttonEditOnUserDefined.Text = "Edit";
            this.buttonEditOnUserDefined.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnUserDefined
            // 
            this.comboBoxOnUserDefined.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnUserDefined.FormattingEnabled = true;
            this.comboBoxOnUserDefined.Location = new System.Drawing.Point(116, 345);
            this.comboBoxOnUserDefined.Name = "comboBoxOnUserDefined";
            this.comboBoxOnUserDefined.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnUserDefined.TabIndex = 37;
            // 
            // labelOnUserDefined
            // 
            this.labelOnUserDefined.AutoSize = true;
            this.labelOnUserDefined.Location = new System.Drawing.Point(7, 348);
            this.labelOnUserDefined.Name = "labelOnUserDefined";
            this.labelOnUserDefined.Size = new System.Drawing.Size(80, 13);
            this.labelOnUserDefined.TabIndex = 36;
            this.labelOnUserDefined.Text = "OnUserDefined";
            // 
            // buttonEditOnUsed
            // 
            this.buttonEditOnUsed.Location = new System.Drawing.Point(317, 317);
            this.buttonEditOnUsed.Name = "buttonEditOnUsed";
            this.buttonEditOnUsed.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnUsed.TabIndex = 35;
            this.buttonEditOnUsed.Text = "Edit";
            this.buttonEditOnUsed.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnUsed
            // 
            this.comboBoxOnUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnUsed.FormattingEnabled = true;
            this.comboBoxOnUsed.Location = new System.Drawing.Point(116, 319);
            this.comboBoxOnUsed.Name = "comboBoxOnUsed";
            this.comboBoxOnUsed.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnUsed.TabIndex = 34;
            // 
            // labelOnUsed
            // 
            this.labelOnUsed.AutoSize = true;
            this.labelOnUsed.Location = new System.Drawing.Point(7, 322);
            this.labelOnUsed.Name = "labelOnUsed";
            this.labelOnUsed.Size = new System.Drawing.Size(46, 13);
            this.labelOnUsed.TabIndex = 33;
            this.labelOnUsed.Text = "OnUsed";
            // 
            // buttonEditOnUnLock
            // 
            this.buttonEditOnUnLock.Location = new System.Drawing.Point(317, 293);
            this.buttonEditOnUnLock.Name = "buttonEditOnUnLock";
            this.buttonEditOnUnLock.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnUnLock.TabIndex = 32;
            this.buttonEditOnUnLock.Text = "Edit";
            this.buttonEditOnUnLock.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnUnLock
            // 
            this.comboBoxOnUnLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnUnLock.FormattingEnabled = true;
            this.comboBoxOnUnLock.Location = new System.Drawing.Point(116, 295);
            this.comboBoxOnUnLock.Name = "comboBoxOnUnLock";
            this.comboBoxOnUnLock.Size = new System.Drawing.Size(182, 21);
            this.comboBoxOnUnLock.TabIndex = 31;
            // 
            // labelOnUnLock
            // 
            this.labelOnUnLock.AutoSize = true;
            this.labelOnUnLock.Location = new System.Drawing.Point(7, 298);
            this.labelOnUnLock.Name = "labelOnUnLock";
            this.labelOnUnLock.Size = new System.Drawing.Size(59, 13);
            this.labelOnUnLock.TabIndex = 30;
            this.labelOnUnLock.Text = "OnUnLock";
            // 
            // buttonEditOnSpellCastAt
            // 
            this.buttonEditOnSpellCastAt.Location = new System.Drawing.Point(317, 268);
            this.buttonEditOnSpellCastAt.Name = "buttonEditOnSpellCastAt";
            this.buttonEditOnSpellCastAt.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnSpellCastAt.TabIndex = 29;
            this.buttonEditOnSpellCastAt.Text = "Edit";
            this.buttonEditOnSpellCastAt.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnSpellCastAt
            // 
            this.comboBoxOnSpellCastAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnSpellCastAt.FormattingEnabled = true;
            this.comboBoxOnSpellCastAt.Location = new System.Drawing.Point(116, 270);
            this.comboBoxOnSpellCastAt.Name = "comboBoxOnSpellCastAt";
            this.comboBoxOnSpellCastAt.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnSpellCastAt.TabIndex = 28;
            // 
            // labelOnSpellCastAt
            // 
            this.labelOnSpellCastAt.AutoSize = true;
            this.labelOnSpellCastAt.Location = new System.Drawing.Point(7, 273);
            this.labelOnSpellCastAt.Name = "labelOnSpellCastAt";
            this.labelOnSpellCastAt.Size = new System.Drawing.Size(75, 13);
            this.labelOnSpellCastAt.TabIndex = 27;
            this.labelOnSpellCastAt.Text = "OnSpellCastAt";
            // 
            // buttonEditOnOpen
            // 
            this.buttonEditOnOpen.Location = new System.Drawing.Point(317, 240);
            this.buttonEditOnOpen.Name = "buttonEditOnOpen";
            this.buttonEditOnOpen.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnOpen.TabIndex = 26;
            this.buttonEditOnOpen.Text = "Edit";
            this.buttonEditOnOpen.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnOpen
            // 
            this.comboBoxOnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnOpen.FormattingEnabled = true;
            this.comboBoxOnOpen.Location = new System.Drawing.Point(116, 242);
            this.comboBoxOnOpen.Name = "comboBoxOnOpen";
            this.comboBoxOnOpen.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnOpen.TabIndex = 25;
            // 
            // labelOnOpen
            // 
            this.labelOnOpen.AutoSize = true;
            this.labelOnOpen.Location = new System.Drawing.Point(7, 245);
            this.labelOnOpen.Name = "labelOnOpen";
            this.labelOnOpen.Size = new System.Drawing.Size(47, 13);
            this.labelOnOpen.TabIndex = 24;
            this.labelOnOpen.Text = "OnOpen";
            // 
            // buttonEditOnPhysicalAttacked
            // 
            this.buttonEditOnPhysicalAttacked.Location = new System.Drawing.Point(317, 214);
            this.buttonEditOnPhysicalAttacked.Name = "buttonEditOnPhysicalAttacked";
            this.buttonEditOnPhysicalAttacked.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnPhysicalAttacked.TabIndex = 23;
            this.buttonEditOnPhysicalAttacked.Text = "Edit";
            this.buttonEditOnPhysicalAttacked.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnPhysicalAttacked
            // 
            this.comboBoxOnPhysicalAttacked.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnPhysicalAttacked.FormattingEnabled = true;
            this.comboBoxOnPhysicalAttacked.Location = new System.Drawing.Point(116, 216);
            this.comboBoxOnPhysicalAttacked.Name = "comboBoxOnPhysicalAttacked";
            this.comboBoxOnPhysicalAttacked.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnPhysicalAttacked.TabIndex = 22;
            // 
            // labelOnPhysicalAttacked
            // 
            this.labelOnPhysicalAttacked.AutoSize = true;
            this.labelOnPhysicalAttacked.Location = new System.Drawing.Point(7, 219);
            this.labelOnPhysicalAttacked.Name = "labelOnPhysicalAttacked";
            this.labelOnPhysicalAttacked.Size = new System.Drawing.Size(103, 13);
            this.labelOnPhysicalAttacked.TabIndex = 21;
            this.labelOnPhysicalAttacked.Text = "OnPhysicalAttacked";
            // 
            // buttonEditOnLock
            // 
            this.buttonEditOnLock.Location = new System.Drawing.Point(317, 188);
            this.buttonEditOnLock.Name = "buttonEditOnLock";
            this.buttonEditOnLock.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnLock.TabIndex = 20;
            this.buttonEditOnLock.Text = "Edit";
            this.buttonEditOnLock.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnLock
            // 
            this.comboBoxOnLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnLock.FormattingEnabled = true;
            this.comboBoxOnLock.Location = new System.Drawing.Point(116, 190);
            this.comboBoxOnLock.Name = "comboBoxOnLock";
            this.comboBoxOnLock.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnLock.TabIndex = 19;
            // 
            // labelOnLock
            // 
            this.labelOnLock.AutoSize = true;
            this.labelOnLock.Location = new System.Drawing.Point(7, 193);
            this.labelOnLock.Name = "labelOnLock";
            this.labelOnLock.Size = new System.Drawing.Size(45, 13);
            this.labelOnLock.TabIndex = 18;
            this.labelOnLock.Text = "OnLock";
            // 
            // buttonEditOnDisturbed
            // 
            this.buttonEditOnDisturbed.Location = new System.Drawing.Point(317, 163);
            this.buttonEditOnDisturbed.Name = "buttonEditOnDisturbed";
            this.buttonEditOnDisturbed.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnDisturbed.TabIndex = 17;
            this.buttonEditOnDisturbed.Text = "Edit";
            this.buttonEditOnDisturbed.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnDisturbed
            // 
            this.comboBoxOnDisturbed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnDisturbed.FormattingEnabled = true;
            this.comboBoxOnDisturbed.Location = new System.Drawing.Point(116, 165);
            this.comboBoxOnDisturbed.Name = "comboBoxOnDisturbed";
            this.comboBoxOnDisturbed.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnDisturbed.TabIndex = 16;
            // 
            // labelOnDisturbed
            // 
            this.labelOnDisturbed.AutoSize = true;
            this.labelOnDisturbed.Location = new System.Drawing.Point(7, 168);
            this.labelOnDisturbed.Name = "labelOnDisturbed";
            this.labelOnDisturbed.Size = new System.Drawing.Size(66, 13);
            this.labelOnDisturbed.TabIndex = 15;
            this.labelOnDisturbed.Text = "OnDisturbed";
            // 
            // buttonEditOnHeartbeat
            // 
            this.buttonEditOnHeartbeat.Location = new System.Drawing.Point(317, 137);
            this.buttonEditOnHeartbeat.Name = "buttonEditOnHeartbeat";
            this.buttonEditOnHeartbeat.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnHeartbeat.TabIndex = 14;
            this.buttonEditOnHeartbeat.Text = "Edit";
            this.buttonEditOnHeartbeat.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnHeartbeat
            // 
            this.comboBoxOnHeartbeat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnHeartbeat.FormattingEnabled = true;
            this.comboBoxOnHeartbeat.Location = new System.Drawing.Point(116, 139);
            this.comboBoxOnHeartbeat.Name = "comboBoxOnHeartbeat";
            this.comboBoxOnHeartbeat.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnHeartbeat.TabIndex = 13;
            // 
            // labelOnHeartbeat
            // 
            this.labelOnHeartbeat.AutoSize = true;
            this.labelOnHeartbeat.Location = new System.Drawing.Point(7, 142);
            this.labelOnHeartbeat.Name = "labelOnHeartbeat";
            this.labelOnHeartbeat.Size = new System.Drawing.Size(68, 13);
            this.labelOnHeartbeat.TabIndex = 12;
            this.labelOnHeartbeat.Text = "OnHeartbeat";
            // 
            // buttonEditOnDeath
            // 
            this.buttonEditOnDeath.Location = new System.Drawing.Point(317, 109);
            this.buttonEditOnDeath.Name = "buttonEditOnDeath";
            this.buttonEditOnDeath.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnDeath.TabIndex = 11;
            this.buttonEditOnDeath.Text = "Edit";
            this.buttonEditOnDeath.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnDeath
            // 
            this.comboBoxOnDeath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnDeath.FormattingEnabled = true;
            this.comboBoxOnDeath.Location = new System.Drawing.Point(116, 111);
            this.comboBoxOnDeath.Name = "comboBoxOnDeath";
            this.comboBoxOnDeath.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnDeath.TabIndex = 10;
            // 
            // labelOnDeath
            // 
            this.labelOnDeath.AutoSize = true;
            this.labelOnDeath.Location = new System.Drawing.Point(7, 114);
            this.labelOnDeath.Name = "labelOnDeath";
            this.labelOnDeath.Size = new System.Drawing.Size(50, 13);
            this.labelOnDeath.TabIndex = 9;
            this.labelOnDeath.Text = "OnDeath";
            // 
            // buttonEditOnDamaged
            // 
            this.buttonEditOnDamaged.Location = new System.Drawing.Point(317, 80);
            this.buttonEditOnDamaged.Name = "buttonEditOnDamaged";
            this.buttonEditOnDamaged.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnDamaged.TabIndex = 8;
            this.buttonEditOnDamaged.Text = "Edit";
            this.buttonEditOnDamaged.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnDamaged
            // 
            this.comboBoxOnDamaged.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnDamaged.FormattingEnabled = true;
            this.comboBoxOnDamaged.Location = new System.Drawing.Point(116, 82);
            this.comboBoxOnDamaged.Name = "comboBoxOnDamaged";
            this.comboBoxOnDamaged.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnDamaged.TabIndex = 7;
            // 
            // labelOnDamaged
            // 
            this.labelOnDamaged.AutoSize = true;
            this.labelOnDamaged.Location = new System.Drawing.Point(7, 85);
            this.labelOnDamaged.Name = "labelOnDamaged";
            this.labelOnDamaged.Size = new System.Drawing.Size(67, 13);
            this.labelOnDamaged.TabIndex = 6;
            this.labelOnDamaged.Text = "OnDamaged";
            // 
            // buttonEditOnClose
            // 
            this.buttonEditOnClose.Location = new System.Drawing.Point(317, 50);
            this.buttonEditOnClose.Name = "buttonEditOnClose";
            this.buttonEditOnClose.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnClose.TabIndex = 5;
            this.buttonEditOnClose.Text = "Edit";
            this.buttonEditOnClose.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnClose
            // 
            this.comboBoxOnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnClose.FormattingEnabled = true;
            this.comboBoxOnClose.Location = new System.Drawing.Point(116, 52);
            this.comboBoxOnClose.Name = "comboBoxOnClose";
            this.comboBoxOnClose.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnClose.TabIndex = 4;
            // 
            // labelOnClose
            // 
            this.labelOnClose.AutoSize = true;
            this.labelOnClose.Location = new System.Drawing.Point(7, 55);
            this.labelOnClose.Name = "labelOnClose";
            this.labelOnClose.Size = new System.Drawing.Size(47, 13);
            this.labelOnClose.TabIndex = 3;
            this.labelOnClose.Text = "OnClose";
            // 
            // buttonEditOnClick
            // 
            this.buttonEditOnClick.Location = new System.Drawing.Point(317, 22);
            this.buttonEditOnClick.Name = "buttonEditOnClick";
            this.buttonEditOnClick.Size = new System.Drawing.Size(47, 23);
            this.buttonEditOnClick.TabIndex = 2;
            this.buttonEditOnClick.Text = "Edit";
            this.buttonEditOnClick.UseVisualStyleBackColor = true;
            // 
            // comboBoxOnClick
            // 
            this.comboBoxOnClick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOnClick.FormattingEnabled = true;
            this.comboBoxOnClick.Location = new System.Drawing.Point(116, 24);
            this.comboBoxOnClick.Name = "comboBoxOnClick";
            this.comboBoxOnClick.Size = new System.Drawing.Size(181, 21);
            this.comboBoxOnClick.TabIndex = 1;
            // 
            // labelOnClick
            // 
            this.labelOnClick.AutoSize = true;
            this.labelOnClick.Location = new System.Drawing.Point(7, 27);
            this.labelOnClick.Name = "labelOnClick";
            this.labelOnClick.Size = new System.Drawing.Size(44, 13);
            this.labelOnClick.TabIndex = 0;
            this.labelOnClick.Text = "OnClick";
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.Controls.Add(this.labelPlaceableDescription);
            this.tabPageDescription.Controls.Add(this.textBoxPlaceableDescription);
            this.tabPageDescription.Location = new System.Drawing.Point(4, 22);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(300, 391);
            this.tabPageDescription.TabIndex = 2;
            this.tabPageDescription.Text = "Description";
            this.tabPageDescription.UseVisualStyleBackColor = true;
            // 
            // labelPlaceableDescription
            // 
            this.labelPlaceableDescription.AutoSize = true;
            this.labelPlaceableDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlaceableDescription.Location = new System.Drawing.Point(15, 16);
            this.labelPlaceableDescription.Name = "labelPlaceableDescription";
            this.labelPlaceableDescription.Size = new System.Drawing.Size(277, 31);
            this.labelPlaceableDescription.TabIndex = 12;
            this.labelPlaceableDescription.Text = "Placeable Description";
            // 
            // textBoxPlaceableDescription
            // 
            this.textBoxPlaceableDescription.Location = new System.Drawing.Point(3, 74);
            this.textBoxPlaceableDescription.MaxLength = 4000;
            this.textBoxPlaceableDescription.Multiline = true;
            this.textBoxPlaceableDescription.Name = "textBoxPlaceableDescription";
            this.textBoxPlaceableDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxPlaceableDescription.Size = new System.Drawing.Size(291, 317);
            this.textBoxPlaceableDescription.TabIndex = 11;
            // 
            // tabPageComments
            // 
            this.tabPageComments.Controls.Add(this.labelPlaceableComments);
            this.tabPageComments.Controls.Add(this.textBoxPlaceableComments);
            this.tabPageComments.Location = new System.Drawing.Point(4, 22);
            this.tabPageComments.Name = "tabPageComments";
            this.tabPageComments.Size = new System.Drawing.Size(300, 391);
            this.tabPageComments.TabIndex = 3;
            this.tabPageComments.Text = "Comments";
            this.tabPageComments.UseVisualStyleBackColor = true;
            // 
            // labelPlaceableComments
            // 
            this.labelPlaceableComments.AutoSize = true;
            this.labelPlaceableComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlaceableComments.Location = new System.Drawing.Point(15, 16);
            this.labelPlaceableComments.Name = "labelPlaceableComments";
            this.labelPlaceableComments.Size = new System.Drawing.Size(271, 31);
            this.labelPlaceableComments.TabIndex = 14;
            this.labelPlaceableComments.Text = "Placeable Comments";
            // 
            // textBoxPlaceableComments
            // 
            this.textBoxPlaceableComments.Location = new System.Drawing.Point(3, 74);
            this.textBoxPlaceableComments.MaxLength = 4000;
            this.textBoxPlaceableComments.Multiline = true;
            this.textBoxPlaceableComments.Name = "textBoxPlaceableComments";
            this.textBoxPlaceableComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxPlaceableComments.Size = new System.Drawing.Size(291, 317);
            this.textBoxPlaceableComments.TabIndex = 13;
            // 
            // buttonDiscardChanges
            // 
            this.buttonDiscardChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonDiscardChanges.Enabled = false;
            this.buttonDiscardChanges.Location = new System.Drawing.Point(151, 426);
            this.buttonDiscardChanges.Name = "buttonDiscardChanges";
            this.buttonDiscardChanges.Size = new System.Drawing.Size(102, 23);
            this.buttonDiscardChanges.TabIndex = 13;
            this.buttonDiscardChanges.Text = "Discard Changes";
            this.buttonDiscardChanges.UseVisualStyleBackColor = true;
            this.buttonDiscardChanges.Click += new System.EventHandler(this.buttonDiscardChanges_Click);
            // 
            // buttonApplyChanges
            // 
            this.buttonApplyChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonApplyChanges.Enabled = false;
            this.buttonApplyChanges.Location = new System.Drawing.Point(35, 426);
            this.buttonApplyChanges.Name = "buttonApplyChanges";
            this.buttonApplyChanges.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyChanges.TabIndex = 12;
            this.buttonApplyChanges.Text = "Apply Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            this.buttonApplyChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // PlaceablePropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDiscardChanges);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "PlaceablePropertiesControl";
            this.Size = new System.Drawing.Size(308, 452);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageItemDetails.ResumeLayout(false);
            this.tabPageItemDetails.PerformLayout();
            this.tabPageEvents.ResumeLayout(false);
            this.tabPageEvents.PerformLayout();
            this.tabPageDescription.ResumeLayout(false);
            this.tabPageDescription.PerformLayout();
            this.tabPageComments.ResumeLayout(false);
            this.tabPageComments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageItemDetails;
        private System.Windows.Forms.Label labelItemDetailsHeader;
        private System.Windows.Forms.Label labelItemResref;
        private System.Windows.Forms.Label labelItemTag;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.TabPage tabPageEvents;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.TabPage tabPageComments;
        private System.Windows.Forms.Button buttonDiscardChanges;
        private System.Windows.Forms.Button buttonApplyChanges;
        private System.Windows.Forms.Label labelPlaceableComments;
        private System.Windows.Forms.TextBox textBoxPlaceableComments;
        private System.Windows.Forms.Label labelPlaceableDescription;
        private System.Windows.Forms.TextBox textBoxPlaceableDescription;
        private WinterEngine.Forms.Controls.Standard.NameTextBox nameTextBoxPlaceable;
        private WinterEngine.Forms.Controls.Standard.TagTextBox tagTextBoxPlaceable;
        private WinterEngine.Forms.Controls.Standard.ResrefTextBox resrefTextBoxPlaceable;
        private System.Windows.Forms.CheckBox checkBoxHasInventory;
        private System.Windows.Forms.CheckBox checkBoxUseable;
        private System.Windows.Forms.Button buttonEditOnPhysicalAttacked;
        private System.Windows.Forms.ComboBox comboBoxOnPhysicalAttacked;
        private System.Windows.Forms.Label labelOnPhysicalAttacked;
        private System.Windows.Forms.Button buttonEditOnLock;
        private System.Windows.Forms.ComboBox comboBoxOnLock;
        private System.Windows.Forms.Label labelOnLock;
        private System.Windows.Forms.Button buttonEditOnDisturbed;
        private System.Windows.Forms.ComboBox comboBoxOnDisturbed;
        private System.Windows.Forms.Label labelOnDisturbed;
        private System.Windows.Forms.Button buttonEditOnHeartbeat;
        private System.Windows.Forms.ComboBox comboBoxOnHeartbeat;
        private System.Windows.Forms.Label labelOnHeartbeat;
        private System.Windows.Forms.Button buttonEditOnDeath;
        private System.Windows.Forms.ComboBox comboBoxOnDeath;
        private System.Windows.Forms.Label labelOnDeath;
        private System.Windows.Forms.Button buttonEditOnDamaged;
        private System.Windows.Forms.ComboBox comboBoxOnDamaged;
        private System.Windows.Forms.Label labelOnDamaged;
        private System.Windows.Forms.Button buttonEditOnClose;
        private System.Windows.Forms.ComboBox comboBoxOnClose;
        private System.Windows.Forms.Label labelOnClose;
        private System.Windows.Forms.Button buttonEditOnClick;
        private System.Windows.Forms.ComboBox comboBoxOnClick;
        private System.Windows.Forms.Label labelOnClick;
        private System.Windows.Forms.Button buttonEditOnUserDefined;
        private System.Windows.Forms.ComboBox comboBoxOnUserDefined;
        private System.Windows.Forms.Label labelOnUserDefined;
        private System.Windows.Forms.Button buttonEditOnUsed;
        private System.Windows.Forms.ComboBox comboBoxOnUsed;
        private System.Windows.Forms.Label labelOnUsed;
        private System.Windows.Forms.Button buttonEditOnUnLock;
        private System.Windows.Forms.ComboBox comboBoxOnUnLock;
        private System.Windows.Forms.Label labelOnUnLock;
        private System.Windows.Forms.Button buttonEditOnSpellCastAt;
        private System.Windows.Forms.ComboBox comboBoxOnSpellCastAt;
        private System.Windows.Forms.Label labelOnSpellCastAt;
        private System.Windows.Forms.Button buttonEditOnOpen;
        private System.Windows.Forms.ComboBox comboBoxOnOpen;
        private System.Windows.Forms.Label labelOnOpen;
    }
}
