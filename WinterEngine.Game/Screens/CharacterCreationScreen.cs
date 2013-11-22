using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace WinterEngine.Game.Screens
{
	public partial class CharacterCreationScreen
	{

		private void CustomInitialize()
        {
            CharacterCreationUIEntityInstance.OnChangeScreen += base.ChangeScreen;


		}

        private void CustomActivity(bool firstTimeCalled)
		{


		}

        private void CustomDestroy()
		{
            CharacterCreationUIEntityInstance.OnChangeScreen -= base.ChangeScreen;

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
