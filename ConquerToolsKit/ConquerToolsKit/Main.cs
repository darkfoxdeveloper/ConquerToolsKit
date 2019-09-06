using System;
using System.IO;
using System.Windows.Forms;

namespace ConquerToolsKit
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        private ConquerTools ctools;
        public Main()
        {
            InitializeComponent();
            ctools = new ConquerTools();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Encrypted Itemtype File|*.dat";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ctools.ItemtypeDecrypt(selectFile.FileName, Path.ChangeExtension(selectFile.FileName, "txt"));
            }
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Decrypted Itemtype File|*.txt";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ctools.ItemtypeEncrypt(selectFile.FileName, Path.ChangeExtension(selectFile.FileName, "dat"));
            }
        }

        private void SelectFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lblSelectedFile.Text = "Selected File: " + selectFile.FileName;
        }
    }
}
