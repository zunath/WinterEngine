using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using WinterEngine.Game.Entities;
using WinterEngine.Game.Screens;
using FlatRedBall.Gui;
using FlatRedBall.Math;
using Microsoft.Xna.Framework;
namespace WinterEngine.Game.Entities
{
	public partial class GuiBaseEntity
	{
		void OnClick (FlatRedBall.Gui.IWindow callingWindow)
        {
            if (!_webView.IsLoading)
            {

                float x = GuiManager.Cursor.WorldXAt(this.Z, this.LayerProvidedByContainer);
                float y = GuiManager.Cursor.WorldYAt(this.Z, this.LayerProvidedByContainer);
                int screenX = 0;
                int screenY = 0;

                MathFunctions.AbsoluteToWindow(x, y, 0.0f, ref screenX, ref screenY, SpriteManager.Camera);

                float xedge = SpriteManager.Camera.XEdge;
                float yedge = SpriteManager.Camera.YEdge;

                /*
                float cursorWorldCoordinateX = GuiManager.Cursor.WorldXAt(0.0f);
                float cursorWorldCoordinateY = GuiManager.Cursor.WorldYAt(0.0f);
                int cursorScreenCoordinateX = 0;
                int cursorScreenCoordinateY = 0;


                cursorScreenCoordinateX = GuiManager.Cursor.ScreenX;
                cursorScreenCoordinateY = GuiManager.Cursor.ScreenY;

                //MathFunctions.AbsoluteToWindow(cursorWorldCoordinateX, cursorWorldCoordinateY, 0.0f, ref cursorScreenCoordinateX, ref cursorScreenCoordinateY, SpriteManager.Camera);

                float windowWorldCoordinateX = callingWindow.WorldUnitX;
                float windowWorldCoordinateY = callingWindow.WorldUnitY;
                int windowScreenCoordinateX = 0;
                int windowScreenCoordinateY = 0;

                MathFunctions.AbsoluteToWindow(windowWorldCoordinateX, windowWorldCoordinateY, 0.0f, ref windowScreenCoordinateX, ref windowScreenCoordinateY, SpriteManager.Camera);


                int awesomiumX = windowScreenCoordinateX - cursorScreenCoordinateX;
                int awesomiumY = windowScreenCoordinateY - cursorScreenCoordinateY;

                */



                Console.Write("");

            }
        }

	}
}
