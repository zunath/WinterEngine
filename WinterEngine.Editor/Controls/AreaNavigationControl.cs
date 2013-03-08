using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Editor.ExtendedEventArgs;
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.Controls
{
    public partial class AreaNavigationControl : UserControl
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public AreaNavigationControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<CameraButtonPressEventArgs> OnCameraButtonPress;
        public event EventHandler<ObjectRotationButtonPressEventArgs> OnObjectRotationButtonPress;
        public event EventHandler OnCameraButtonReleased;
        public event EventHandler OnObjectRotationButtonReleased;

        #endregion

        #region Event Handling


        private void buttonCameraLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonPress, null))
            {
                OnCameraButtonPress(this, new CameraButtonPressEventArgs(CameraMovementTypeEnum.Left));
            }
        }

        private void buttonCameraRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonPress, null))
            {
                OnCameraButtonPress(this, new CameraButtonPressEventArgs(CameraMovementTypeEnum.Right));
            }
        }

        private void buttonCameraUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonPress, null))
            {
                OnCameraButtonPress(this, new CameraButtonPressEventArgs(CameraMovementTypeEnum.Up));
            }
        }

        private void buttonCameraDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonPress, null))
            {
                OnCameraButtonPress(this, new CameraButtonPressEventArgs(CameraMovementTypeEnum.Down));
            }
        }

        private void buttonRotateClockwise_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonPress, null))
            {
                OnObjectRotationButtonPress(this, new ObjectRotationButtonPressEventArgs(ObjectRotationTypeEnum.Clockwise));
            }
        }

        private void buttonRotateCounterclockwise_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonPress, null))
            {
                OnObjectRotationButtonPress(this, new ObjectRotationButtonPressEventArgs(ObjectRotationTypeEnum.CounterClockwise));
            }
        }

        private void buttonCamera_MouseUp(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnCameraButtonReleased, null))
            {
                OnCameraButtonReleased(this, new EventArgs());
            }
        }

        private void buttonRotateObject_MouseUp(object sender, MouseEventArgs e)
        {
            if (!Object.ReferenceEquals(OnObjectRotationButtonReleased, null))
            {
                OnObjectRotationButtonReleased(this, new EventArgs());
            }
        }

        #endregion


        #region Methods

        #endregion
    }
}
