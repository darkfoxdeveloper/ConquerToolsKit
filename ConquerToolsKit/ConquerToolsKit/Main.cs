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
            selectFile.Filter = "Itemtype File|*.dat";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ctools.ItemtypeDecrypt(File.ReadAllBytes(selectFile.FileName), Path.ChangeExtension(selectFile.FileName, ".txt"));
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            selectFile.Filter = "Itemtype File|*.dat";
            DialogResult dres = selectFile.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ctools.ItemtypeEncrypt(File.ReadAllBytes(selectFile.FileName), Path.ChangeExtension(selectFile.FileName, ".dat"));
            }
        }
    }
}
