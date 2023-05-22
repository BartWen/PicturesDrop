using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PicturesDrop
{
    public partial class Main : Form
    {

        private OpenFileDialog openFile;
        private string _sourceFile;

        public Main()
        {
            InitializeComponent();
            GetFilePath();
            PathFile();
            DeleteBtnVisible();
        }
        private void DeleteBtnVisible()
        {
            if (pictureBox.Image != null)

                btnDelete.Visible = true;
            else
                btnDelete.Visible = false;

        }
        private void GetFilePath()

        {
            if (File.Exists(PathFile()))
            {
                _sourceFile = File.ReadAllText(PathFile());
                if (_sourceFile == string.Empty)
                {
                    return;
                }
                this.pictureBox.Image = Image.FromFile(_sourceFile);
            }
        }
        private string PathFile()
        {
            var pathFile = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\UserPath.txt";

            if (!File.Exists(pathFile))
                File.Create(pathFile);

            return pathFile;

        }
        private void btnAddPhoto_Click_1(object sender, EventArgs e)
        {
            openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox.Image = Image.FromFile(openFile.FileName);
            }

            var sourceFile = openFile.ToString();

            _sourceFile = sourceFile.Remove(0, 56);

            File.WriteAllText(PathFile(), _sourceFile);

            DeleteBtnVisible();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            File.WriteAllText(PathFile(), string.Empty);

            this.pictureBox.Image = null;

            DeleteBtnVisible();
        }
    }

}
