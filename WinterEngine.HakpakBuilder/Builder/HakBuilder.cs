using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Library.Enumerations;
using WinterEngine.Library.Helpers;

namespace WinterEngine.Hakpak.Builder
{
    public partial class HakBuilder : Form
    {
        public HakBuilder()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            WinterFileExtensions extensions = new WinterFileExtensions();
            string modelFileExtension = extensions.getFileExtension(FileType.Model);
            string textureFileExtension = extensions.getFileExtension(FileType.Texture);

            openFileDialog.Filter = "Model Files (*" + modelFileExtension + ")|*" + modelFileExtension + "|" + 
                                    "Texture Files (*" + textureFileExtension + ")|*" + textureFileExtension;
            openFileDialog.ShowDialog();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {

        }
    }
}
