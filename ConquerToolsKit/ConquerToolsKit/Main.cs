using System;
using System.IO;
using System.Windows.Forms;
using static ConquerToolsKit.DatCrypto;

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
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.COMMON);
            cbxEncryptionKey.DataSource = Enum.GetNames(typeof(DatCrypto.EncryptionKey));
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Encrypted Conquer Itemtype File|*.dat";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ctools.ItemtypeDecrypt(selectFile.FileName, Path.ChangeExtension(selectFile.FileName, "txt"));
            }
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Decrypted Conquer Itemtype File|*.txt";
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

        private void BtnDecryptDat_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Encrypted Conquer Dat File|*.dat";
            ctools.AutoDetectionEncrypt(ctools.SelectedFile, Path.ChangeExtension(selectFile.FileName, "txt"));
        }

        private void BtnEncryptDat_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Decrypted Conquer Dat File|*.txt";
            ctools.AutoDetectionEncrypt(ctools.SelectedFile, Path.ChangeExtension(selectFile.FileName, "dat"));
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Encrypted Conquer Dat File|*.dat";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ctools.SelectedFile = selectFile.FileName;
                DatCrypto dc = new DatCrypto(ctools.SelectedFile);
                cbxEncryptionKey.SelectedItem = dc.DetectedEncryptionKey.ToString();
                lblSelectedDatFile.Text = "Selected File: " + selectFile.FileName;
            } else
            {
                ctools.SelectedFile = "";
            }
        }
    }
}
