using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            // Generate new list with all current data from DataGridView
            Dictionary<uint, DatFileLine> rowValuesGenerated = new Dictionary<uint, DatFileLine>();
            StringBuilder rowValues = new StringBuilder();
            DatFileConfig config = ConquerToolsHelper.CTools.SelectedDatFile.GetCurrentConfig();
            foreach (DataGridViewRow row in dgvAdvanced.Rows)
            {
                rowValuesGenerated.Add((uint)row.Index, new DatFileLine() { LineAttribute = new Dictionary<string, string>() });
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string cellValue = cell.Value.ToString();
                    rowValuesGenerated[(uint)row.Index].LineAttribute.Add("#" + cell.ColumnIndex, cellValue);
                    rowValues.Append(cellValue);
                    if (cell.ColumnIndex < row.Cells.Count)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (char value in config.Separators)
                        {
                            builder.Append(value);
                        }
                        string sep = builder.ToString();
                        rowValues.Append(sep);
                    }
                }
                rowValues.Append('\n');
            }
            ConquerToolsHelper.CTools.SelectedDatFile.CurrentFileContent = rowValuesGenerated;
            ConquerToolsHelper.CTools.SelectedDatFile.CurrentRAWFileContent = rowValues.ToString().Split('\n');
            ConquerToolsHelper.CTools.SaveDat();
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
                }
                else
                {
                    ConquerToolsHelper.CTools.CustomDecrypt(selectFile.FileName, filenameOutput, datFileType);
                }
                if (ConquerToolsHelper.CTools.SelectedDatFile.CurrentFileContent.Count > 0)
                {
                    ConquerToolsHelper.CTools.GenerateTable(dgvAdvanced, tglRawMode.Checked);
                }
            }
        }

        private void MainIcon_Click(object sender, EventArgs e)
        {
            tabTools.SelectedIndex = tabTools.TabCount - 1;
        }
    }
}
