﻿using System;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class GraphicView : UserControl
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public GraphicView()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        public void RefreshControls()
        {
            graphicPropertiesControl.RefreshControls();
        }

        public void UnloadControls()
        {
            graphicPropertiesControl.UnloadControls();
        }

        #endregion
    }
}
