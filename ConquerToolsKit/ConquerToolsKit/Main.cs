using System;
using System.IO;
using System.Windows.Forms;
using static ConquerToolsKit.ConquerDatFile;

namespace ConquerToolsKit
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
            ConquerToolsHelper.CTools = new ConquerTools();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cbxDatFileType.DataSource = Enum.GetNames(typeof(DatFileType));
        }

        private void SelectFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lblSelectedDatFile.Text = "Selected File: " + selectFile.FileName;
        }

        private void BtnDecryptDat_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Encrypted Conquer Dat File|*.dat";
            string filenameOutput = Path.ChangeExtension(selectFile.FileName, "txt");
            Enum.TryParse(cbxDatFileType.SelectedItem.ToString(), out DatFileType datFileType);
            if (datFileType == DatFileType.AUTODETECT)
            {
                ConquerToolsHelper.CTools.AutoDetectionDecrypt(selectFile.FileName, filenameOutput);
            }
            else
            {
                ConquerToolsHelper.CTools.CustomDecrypt(selectFile.FileName, filenameOutput, datFileType);
            }
        }

        private void BtnEncryptDat_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Decrypted Conquer Dat File|*.txt";
            string filenameOutput = Path.ChangeExtension(selectFile.FileName, "dat");
            Enum.TryParse(cbxDatFileType.SelectedItem.ToString(), out DatFileType datFileType);
            if (datFileType == DatFileType.AUTODETECT)
            {
                ConquerToolsHelper.CTools.AutoDetectionDecrypt(selectFile.FileName, filenameOutput);
            }
            else
            {
                ConquerToolsHelper.CTools.CustomDecrypt(selectFile.FileName, filenameOutput, datFileType);
            }
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Encrypted Conquer Dat File|*.dat";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                string filenameOutput = Path.ChangeExtension(selectFile.FileName, "txt");
                Enum.TryParse(cbxDatFileType.SelectedItem.ToString(), out DatFileType datFileType);
                if (datFileType == DatFileType.AUTODETECT)
                {
                    ConquerToolsHelper.CTools.AutoDetectionDecrypt(selectFile.FileName, filenameOutput);
                } else
                {
                    ConquerToolsHelper.CTools.CustomDecrypt(selectFile.FileName, filenameOutput, datFileType);
                }
                if (File.Exists(filenameOutput))
                {
                    string[] lines = File.ReadAllLines(filenameOutput);
                    ConquerToolsHelper.CTools.GenerateTable(lines, dgvAdvanced, ConquerToolsHelper.CTools.SelectedDatFile, tglRawMode.Checked);
                }
            }
        }

        private void MainIcon_Click(object sender, EventArgs e)
        {
            tabTools.SelectedIndex = tabTools.TabCount - 1;
        }
    }
}
