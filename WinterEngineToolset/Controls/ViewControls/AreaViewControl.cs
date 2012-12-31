using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.Controls.XnaControls.Shared;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class AreaViewControl : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private ModelViewerControl _modelViewer;
        private ContentBuilder _contentBuilder;
        private ContentManager _contentManager;

        public AreaViewControl()
        {
            InitializeComponent();
            AddXNAViewerControl();

            _contentBuilder = new ContentBuilder();
            _contentManager = new ContentManager(_modelViewer.Services, _contentBuilder.OutputDirectory);

            this.Load += LoadContent;
        }

        private void AddXNAViewerControl()
        {
            _modelViewer = new ModelViewerControl();
            _modelViewer.Dock = DockStyle.Fill;
            panelObjectViewer.Controls.Add(_modelViewer);
        }

        private void LoadContent(object sender, EventArgs e)
        {
            LoadModel("C:\\Users\\Tyler\\documents\\visual studio 2010\\Projects\\WinterEngine\\Content\\Models\\Cats.fbx");
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

    }
}
