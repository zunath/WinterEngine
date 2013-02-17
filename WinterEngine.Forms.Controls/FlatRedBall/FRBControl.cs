using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlatRedBall;
using WinterEngine.Toolset.Editor;
using Timer = System.Timers.Timer;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Forms.Controls.FlatRedBall
{
    // Adapted from profexorgeek's FlatRedBall XNA 4.0 in Windows Form example:
    // http://www.gluevault.com/projects/tech-demo/52-flatredball-xna-40-windows-form
    public partial class FRBControl : UserControl
    {
        #region Fields

        private ToolsetEditorGame _gameInstance;
        private Timer _gameTime;
        private bool _wasTimerEnabled;
        #endregion

        #region Properties

        private ToolsetEditorGame GameInstance
        {
            get { return _gameInstance; }
            set { _gameInstance = value; }
        }

        private Timer GameTimer
        {
            get { return _gameTime; }
            set { _gameTime = value; }
        }

        private bool WasTimerEnabled
        {
            get { return _wasTimerEnabled; }
            set { _wasTimerEnabled = value; }
        }

        #endregion

        #region Constructors

        public FRBControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            // create a game instance and pass the game timer, which starts when Initialization is complete
            GameInstance = new ToolsetEditorGame(() =>
            {
                GameTimer = new Timer(15);
                GameTimer.Elapsed += (sender, e) =>
                {
                    try
                    {
                        GameInstance.Tick();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to tick, game not set up? " + ex.Message);
                    }
                };
                GameTimer.Start();
            });

            // get a handle from the GameInstance to use as a Form and stuff it in the Viewport Panel we created
            Form form = Form.FromHandle(GameInstance.Window.Handle) as Form;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.Parent = Viewport;
            Viewport.Controls.Add(form);
            form.Visible = true;

            UpdateViewportResolution();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Attaches this control's resize methods to the form's resize events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FRBControl_Load(object sender, EventArgs e)
        {
            Form form = FindForm();
            form.ResizeBegin += ResizeBegin;
            form.ResizeEnd += ResizeEnd;
        }

        #endregion

        #region Methods

        // todo: something's not quite right here, if camera is pixel perfect, logo should never change size but it does. why?
        private void UpdateViewportResolution()
        {
            FlatRedBallServices.GraphicsOptions.SuspendDeviceReset();
            FlatRedBallServices.GraphicsOptions.SetResolution(Viewport.Width, Viewport.Height);
            SpriteManager.Camera.UsePixelCoordinates();
            FlatRedBallServices.GraphicsOptions.ResumeDeviceReset();
            FlatRedBallServices.GraphicsOptions.ResetDevice();
        }

        // pause game engine while resizing or Draw() calls will blow up
        private void ResizeBegin(object sender, EventArgs e)
        {
            WasTimerEnabled = GameTimer.Enabled;
            GameTimer.Enabled = false;
        }

        // resume game timer state and update resolution after resize
        private void ResizeEnd(object sender, EventArgs e)
        {
            UpdateViewportResolution();
            GameTimer.Enabled = WasTimerEnabled;
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            GameTimer.Stop();
            GameInstance.Dispose();
             
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}
