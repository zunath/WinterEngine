using System.Windows.Forms;
using WinterEngine.Toolset.ExtendedEventArgs;
using System;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class CreatureView : UserControl, IViewControls
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public CreatureView()
        {
            InitializeComponent();

            // Subscribe to the OnOpenObject event in the tree category control area
            treeCategoryControlCreature.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            // Subscribe to the OnSaveObject event in the creature view control.
            creatureViewControl.OnSaveCreature += new EventHandler<GameObjectEventArgs>(SaveObject);
        }

        #endregion

        #region Methods

        public void RefreshControls()
        {
            treeCategoryControlCreature.RefreshTreeView();
        }

        public void UnloadControls()
        {
            treeCategoryControlCreature.UnloadTreeView();
        }


        /// <summary>
        /// Handles loading child controls using the object passed by the tree category control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadObject(object sender, GameObjectEventArgs e)
        {
            creatureViewControl.LoadCreature(e.GameObject as Creature);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active creature.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveObject(object sender, GameObjectEventArgs e)
        {
            treeCategoryControlCreature.ActiveGameObject = e.GameObject;
            treeCategoryControlCreature.RefreshNodeNames();
        }

        #endregion

    }
}
