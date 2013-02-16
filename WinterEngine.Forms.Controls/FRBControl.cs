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

namespace WinterEngine.Forms.Controls
{
    public partial class FRBControl : UserControl
    {
        private ToolsetEditor GameInstance;
        private Timer GameTimer;
        private bool mWasTimerEnabled;

        public FRBControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();

            // create a game instance and pass the game timer, which starts when Initialization is complete
            GameInstance = new ToolsetEditor(() =>
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


        // todo: something's not quite right here, if camera is pixel perfect, logo should never change size but it does. why?
        private void UpdateViewportResolution()
        {
            FlatRedBallServices.GraphicsOptions.SuspendDeviceReset();
            FlatRedBallServices.GraphicsOptions.SetResolution(Viewport.Width, Viewport.Height);
            SpriteManager.Camera.UsePixelCoordinates();
            FlatRedBallServices.GraphicsOptions.ResumeDeviceReset();
            FlatRedBallServices.GraphicsOptions.ResetDevice();
        }


        protected override void Dispose(bool disposing)
        {
            if (!Object.ReferenceEquals(GameTimer, null))
            {
                GameTimer.Stop();
            }

            if (!Object.ReferenceEquals(GameInstance, null))
            {
                GameInstance.Dispose();
            }
              
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        // pause game engine while resizing or Draw() calls will blow up
        public void ResizeBegin(object sender, EventArgs e)
        {
            mWasTimerEnabled = GameTimer.Enabled;
            GameTimer.Enabled = false;
        }

        // resume game timer state and update resolution after resize
        public void ResizeEnd(object sender, EventArgs e)
        {
            UpdateViewportResolution();
            GameTimer.Enabled = mWasTimerEnabled;
        }
    }
}
