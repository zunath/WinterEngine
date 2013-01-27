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
using WinterEngine.Library;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.Library.Helpers;
using Ionic.Zip;
using System.IO;

namespace WinterEngine.Toolset.Controls.XnaControls
{
    public partial class ObjectViewer3D : UserControl
    {
        #region Variables

        private ModelViewerControl _modelViewer;
        private ContentManager _contentManager;
        private GraphicResource _graphicResource;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the graphic resource of this object viewer.
        /// </summary>
        public GraphicResource Resource
        {
            get { return _graphicResource; }
            set { _graphicResource = value; }
        }

        #endregion

        #region Constructors
        
        public ObjectViewer3D()
        {
            InitializeComponent();
            AddXNAViewerControl();

            _contentManager = new ContentManager(_modelViewer.Services);
        }

        #endregion


        #region Methods


        private void AddXNAViewerControl()
        {
            _modelViewer = new ModelViewerControl();
            _modelViewer.Dock = DockStyle.Fill;
            panelObjectViewer.Controls.Add(_modelViewer);
        }

        /// <summary>
        /// Loads a pre-built model into the panel.
        /// </summary>
        public void LoadModel()
        {
            string resourcePath = "";

            try
            {
                Cursor = Cursors.WaitCursor;

                // Unload any existing model.
                _modelViewer.Model = null;
                _contentManager.Unload();

                using (ZipFile zipFile = new ZipFile(Resource.ResourcePackagePath))
                {
                    resourcePath = "./" + Resource.ResourceFileName;

                    ZipEntry entry = zipFile[Resource.ResourceFileName];
                    entry.Extract();
                    _modelViewer.Model = _contentManager.Load<Model>("./" + Path.GetFileNameWithoutExtension(entry.FileName));
                    File.Delete("./" + entry.FileName);
                }

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                if (File.Exists(resourcePath))
                {
                    File.Delete(resourcePath);
                }

                ErrorHelper.ShowErrorDialog("Error loading graphic.", ex);
            }
        }

        #endregion

    }
}
