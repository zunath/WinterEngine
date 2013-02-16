using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework.Graphics;
using FlatRedBall;
using Microsoft.Xna.Framework;

namespace WinterEngine.Client
{
    public class CustomDrawableBatch : PositionedObject, IDrawableBatch
    {
        #region Fields

        #region XML Docs
        /// <summary>
        /// The effect used to draw
        /// </summary>
        #endregion
        private BasicEffect mEffect;

        #region XML Docs
        /// <summary>
        /// The vertices used to draw the shape
        /// </summary>
        #endregion
        private VertexPositionColor[] mVertices;

        #region XML Docs
        /// <summary>
        /// The indices to draw the shape
        /// </summary>
        #endregion
        private short[] mIndices;

        #endregion

        #region Properties

        #region XML Docs
        /// <summary>
        /// Here we tell the engine if we want this batch
        /// updated every frame.  Since we have no updating to
        /// do though, we will set this to false
        /// </summary>
        #endregion
        public bool UpdateEveryFrame
        {
            get { return false; }
        }

        #endregion

        #region Constructor / Initialization

        #region XML Docs
        /// <summary>
        /// Create and initialize all assets
        /// </summary>
        #endregion
        public CustomDrawableBatch()
            : base()
        {
            // Create the effect
            mEffect = new BasicEffect(
                FlatRedBallServices.GraphicsDevice);
            mEffect.VertexColorEnabled = true;

            // Create the vertices
            mVertices = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(-1f,-1.732f / 3f,0f), Color.Red),
                new VertexPositionColor(new Vector3(1f,-1.732f / 3f,0f), Color.Green),
                new VertexPositionColor(new Vector3(0f,1.732f * 2f / 3f,0f), Color.Blue)
            };

            // Create the indices
            mIndices = new short[] { 0, 1, 2 };
        }

        #endregion

        #region Methods

        #region XML Docs
        /// <summary>
        /// Custom drawing technique - sets graphics states and
        /// draws the custom shape
        /// </summary>
        /// <param name="camera">The currently drawing camera</param>
        #endregion
        public void Draw(Camera camera)
        {
            // Set graphics states
            FlatRedBallServices.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Have the current camera set our current view/projection variables
            camera.SetDeviceViewAndProjection(mEffect, false);

            // Here we get the positioned object's transformation (position / rotation)
            mEffect.World = base.TransformationMatrix;

            // Start the effect

            foreach (EffectPass pass in mEffect.CurrentTechnique.Passes)
            {
                // Start each pass

                pass.Apply();

                // Draw the shape
                FlatRedBallServices.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    mVertices, 0, mVertices.Length,
                    mIndices, 0, mIndices.Length / 3);
            }
        }

        #region XML Docs
        /// <summary>
        /// Here we update our batch - but this batch doesn't
        /// need to be updated
        /// </summary>
        #endregion
        public void Update()
        {
        }

        #region XML Docs
        /// <summary>
        /// Here we destroy all assets that need destroying.
        /// In this case, all our assets will be destroyed
        /// automatically upon quitting
        /// </summary>
        #endregion
        public void Destroy()
        {
        }

        #endregion
    }
}
