using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinterEngine.Editor.Controls
{
    public partial class ObjectBar : UserControl, IEditorControl
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public ObjectBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Events / Delegates

        public event EventHandler OnObjectSelected;

        #endregion

        #region Event Handling

        #endregion

        #region Methods

        public void XNAInitialize()
        {
        }

        public void XNAUpdate()
        {
        }

        #endregion
    }
}
