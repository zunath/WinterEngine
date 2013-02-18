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
using FlatRedBall.Localization;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
#endif

namespace WinterEngine.Client.Screens
{
	public partial class Demo
	{
        Form form;
        CustomDrawableBatch batch;

		void CustomInitialize()
		{
            batch = new CustomDrawableBatch();
            SpriteManager.AddDrawableBatch(batch);
            SpriteManager.AddPositionedObject(batch);
		}

		void CustomActivity(bool firstTimeCalled)
		{
            InputManager.Keyboard.ControlPositionedObject(batch);
            float rotationSpeed = MathHelper.PiOver2 * TimeManager.SecondDifference;
            //if (InputManager.Keyboard.KeyDown(Keys.A)) batch.RotationZ += rotationSpeed;
            //if (InputManager.Keyboard.KeyDown(Keys.D)) batch.RotationZ -= rotationSpeed;
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
