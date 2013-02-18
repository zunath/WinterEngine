using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Content.Scene;
using Microsoft.Xna.Framework;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using FlatRedBall.Content.Scene;
using Microsoft.Xna.Framework;
#endif

namespace WinterEngine.Client.Screens
{

    
    public partial class SplashScreen
    {
        #region Enums

        enum LogicState
        {
            Uninitialized,
            FadingIn,
            Showing,
            FadingOut
        }

        #endregion

        double mLastStateChange = 0;
        LogicState mCurrentLogicState = LogicState.Uninitialized;
        Color mOldBackgroundColor;
        CameraSave mOldCameraSetup;
        TimeSpan mOldTimeSpan;
		public void CustomInitialize()
		{
            mOldCameraSetup = CameraSave.FromCamera(SpriteManager.Camera);
            mOldBackgroundColor = SpriteManager.Camera.BackgroundColor;
            SpriteManager.Camera.UsePixelCoordinates();
            SpriteManager.Camera.BackgroundColor = Color.Black;
            BackStackBehavior = FlatRedBall.Utilities.BackStackBehavior.Ignore;
            CurrentState = VariableState.Transparent;

            mOldTimeSpan = FlatRedBallServices.Game.TargetElapsedTime;
            // Go to 10 fps to make loading go faster
            FlatRedBallServices.Game.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100); ;

		}

		public void CustomActivity(bool firstTimeCalled)
		{
            if (firstTimeCalled)
            {
                StartAsyncLoad(NextScreen);
            }

            LogicStateActivity();

		}

		public void CustomDestroy()
		{

            FlatRedBallServices.Game.TargetElapsedTime = mOldTimeSpan;
            mOldCameraSetup.SetCamera(SpriteManager.Camera);
            SpriteManager.Camera.BackgroundColor = mOldBackgroundColor;
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void LogicStateActivity()
        {
            const double FadeTime = 0.5;
            const double ShowTime = 1.8;

            /////////////////////EARLY OUT////////////////////////
            // This guaranteees a few frames have passed before we start logic
            if (this.ActivityCallCount < 3)
            {
                return;
            }
            ///////////////////END EARLY OUT/////////////////////

            switch (mCurrentLogicState)
            {
                case LogicState.Uninitialized:
                    InterpolateToState(VariableState.Opaque, FadeTime);
                    mCurrentLogicState = LogicState.FadingIn;
                    break;
                case LogicState.FadingIn:
                    if (CurrentState == VariableState.Opaque)
                    {
                        mCurrentLogicState = LogicState.Showing;
                        mLastStateChange = TimeManager.CurrentTime;
                    }
                    break;
                case LogicState.Showing:
                    if (TimeManager.SecondsSince(mLastStateChange) > ShowTime)
                    {
                        InterpolateToState(VariableState.Transparent, FadeTime);
                        mCurrentLogicState = LogicState.FadingOut;
                    }
                    break;
                case LogicState.FadingOut:

                    if (CurrentState == VariableState.Transparent && AsyncLoadingState == FlatRedBall.Screens.AsyncLoadingState.Done)
                    {
                        IsActivityFinished = true;
                        // NextScreen should be set through the Glue UI
                    }
                    break;
            }
        }
	}
}
