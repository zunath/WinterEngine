using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls.Shared;
using System;

namespace WinterEngine.Toolset.Controls.XnaControls
{
    public partial class ObjectViewer3D : UserControl
    {
        #region Variables

        private ModelViewerControl _modelViewer;
        private ContentBuilder _contentBuilder;
        private ContentManager _contentManager;

        #endregion

        #region Properties

        #endregion

        #region Overrides
        public ObjectViewer3D()
        {
            InitializeComponent();
            AddXNAViewerControl();

            _contentBuilder = new ContentBuilder();
            _contentManager = new ContentManager(_modelViewer.Services, _contentBuilder.OutputDirectory);

            this.Load += LoadContent;
        }

        private void LoadContent(object sender, EventArgs e)
        {
            LoadModel("C:\\Users\\Tyler\\Desktop\\Debugging\\Cats.fbx");
        }

        #endregion

        #region Methods


        private void AddXNAViewerControl()
        {
            _modelViewer = new ModelViewerControl();
            _modelViewer.Dock = DockStyle.Fill;
            panelObjectViewer.Controls.Add(_modelViewer);
        }


        void LoadModel(string fileName)
        {
            Cursor = Cursors.WaitCursor;

            // Unload any existing model.
            _modelViewer.Model = null;
            _contentManager.Unload();

            // Tell the ContentBuilder what to build.
            _contentBuilder.Clear();
            _contentBuilder.Add(fileName, "Model", null, "ModelProcessor");

            // Build this new model data.
            string buildError = _contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                // If the build succeeded, use the ContentManager to
                // load the temporary .xnb file that we just created.

                _modelViewer.Model = _contentManager.Load<Model>("Model");
            }
            else
            {
                // If the build failed, display an error message.
                MessageBox.Show(buildError, "Error");
            }

            Cursor = Cursors.Arrow;
        }

        #endregion

    }
}
