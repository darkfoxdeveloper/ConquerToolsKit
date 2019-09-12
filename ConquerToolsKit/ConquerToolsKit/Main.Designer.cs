namespace ConquerToolsKit
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabTools = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblRawMode = new MetroFramework.Controls.MetroLabel();
            this.tglRawMode = new MetroFramework.Controls.MetroToggle();
            this.dgvAdvanced = new System.Windows.Forms.DataGridView();
            this.lblSelectedDatFile = new MetroFramework.Controls.MetroLabel();
            this.btnOpenFile = new MetroFramework.Controls.MetroButton();
            this.cbxDatFileType = new MetroFramework.Controls.MetroComboBox();
            this.btnEncryptDat = new MetroFramework.Controls.MetroButton();
            this.btnDecryptDat = new MetroFramework.Controls.MetroButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblAbout = new MetroFramework.Controls.MetroLabel();
            this.mainPanel = new MetroFramework.Controls.MetroPanel();
            this.selectFile = new System.Windows.Forms.OpenFileDialog();
            this.mainIcon = new System.Windows.Forms.PictureBox();
            this.tabTools.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tabPage1);
            this.tabTools.Controls.Add(this.tabPage2);
            this.tabTools.Location = new System.Drawing.Point(3, 3);
            this.tabTools.Name = "tabTools";
            this.tabTools.SelectedIndex = 0;
            this.tabTools.Size = new System.Drawing.Size(794, 382);
            this.tabTools.TabIndex = 0;
            this.tabTools.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblRawMode);
            this.tabPage1.Controls.Add(this.tglRawMode);
            this.tabPage1.Controls.Add(this.dgvAdvanced);
            this.tabPage1.Controls.Add(this.lblSelectedDatFile);
            this.tabPage1.Controls.Add(this.btnOpenFile);
            this.tabPage1.Controls.Add(this.cbxDatFileType);
            this.tabPage1.Controls.Add(this.btnEncryptDat);
            this.tabPage1.Controls.Add(this.btnDecryptDat);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(786, 340);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Editor";
            // 
            // lblRawMode
            // 
            this.lblRawMode.AutoSize = true;
            this.lblRawMode.Location = new System.Drawing.Point(595, 65);
            this.lblRawMode.Name = "lblRawMode";
            this.lblRawMode.Size = new System.Drawing.Size(87, 19);
            this.lblRawMode.TabIndex = 9;
            this.lblRawMode.Text = "Show in RAW";
            // 
            // tglRawMode
            // 
            this.tglRawMode.AutoSize = true;
            this.tglRawMode.Location = new System.Drawing.Point(687, 66);
            this.tglRawMode.Name = "tglRawMode";
            this.tglRawMode.Size = new System.Drawing.Size(80, 17);
            this.tglRawMode.TabIndex = 8;
            this.tglRawMode.Text = "Off";
            this.tglRawMode.UseSelectable = true;
            // 
            // dgvAdvanced
            // 
            this.dgvAdvanced.AllowUserToAddRows = false;
            this.dgvAdvanced.AllowUserToOrderColumns = true;
            this.dgvAdvanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdvanced.Location = new System.Drawing.Point(15, 90);
            this.dgvAdvanced.Name = "dgvAdvanced";
            this.dgvAdvanced.Size = new System.Drawing.Size(755, 233);
            this.dgvAdvanced.TabIndex = 7;
            // 
            // lblSelectedDatFile
            // 
            this.lblSelectedDatFile.AutoSize = true;
            this.lblSelectedDatFile.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedDatFile.Location = new System.Drawing.Point(15, 45);
            this.lblSelectedDatFile.Name = "lblSelectedDatFile";
            this.lblSelectedDatFile.Size = new System.Drawing.Size(95, 19);
            this.lblSelectedDatFile.TabIndex = 6;
            this.lblSelectedDatFile.Text = "Selected File: -";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(15, 15);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseSelectable = true;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // cbxDatFileType
            // 
            this.cbxDatFileType.FormattingEnabled = true;
            this.cbxDatFileType.ItemHeight = 23;
            this.cbxDatFileType.Location = new System.Drawing.Point(290, 13);
            this.cbxDatFileType.Name = "cbxDatFileType";
            this.cbxDatFileType.Size = new System.Drawing.Size(121, 29);
            this.cbxDatFileType.TabIndex = 4;
            this.cbxDatFileType.UseSelectable = true;
            // 
            // btnEncryptDat
            // 
            this.btnEncryptDat.Location = new System.Drawing.Point(200, 15);
            this.btnEncryptDat.Name = "btnEncryptDat";
            this.btnEncryptDat.Size = new System.Drawing.Size(75, 23);
            this.btnEncryptDat.TabIndex = 3;
            this.btnEncryptDat.Text = "Encrypt";
            this.btnEncryptDat.UseSelectable = true;
            this.btnEncryptDat.Click += new System.EventHandler(this.BtnEncryptDat_Click);
            // 
            // btnDecryptDat
            // 
            this.btnDecryptDat.Location = new System.Drawing.Point(105, 15);
            this.btnDecryptDat.Name = "btnDecryptDat";
            this.btnDecryptDat.Size = new System.Drawing.Size(75, 23);
            this.btnDecryptDat.TabIndex = 2;
            this.btnDecryptDat.Text = "Decrypt";
            this.btnDecryptDat.UseSelectable = true;
            this.btnDecryptDat.Click += new System.EventHandler(this.BtnDecryptDat_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblAbout);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(786, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Location = new System.Drawing.Point(15, 15);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(243, 19);
            this.lblAbout.TabIndex = 0;
            this.lblAbout.Text = "This program is under developing now...";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.tabTools);
            this.mainPanel.HorizontalScrollbarBarColor = true;
            this.mainPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.mainPanel.HorizontalScrollbarSize = 10;
            this.mainPanel.Location = new System.Drawing.Point(0, 63);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(800, 388);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.VerticalScrollbarBarColor = true;
            this.mainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.mainPanel.VerticalScrollbarSize = 10;
            // 
            // selectFile
            // 
            this.selectFile.FileOk += new System.ComponentModel.CancelEventHandler(this.SelectFile_FileOk);
            // 
            // mainIcon
            // 
            this.mainIcon.Image = global::ConquerToolsKit.Properties.Resources.conquer_icon;
            this.mainIcon.Location = new System.Drawing.Point(725, 10);
            this.mainIcon.Name = "mainIcon";
            this.mainIcon.Size = new System.Drawing.Size(68, 47);
            this.mainIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainIcon.TabIndex = 2;
            this.mainIcon.TabStop = false;
            this.mainIcon.Click += new System.EventHandler(this.MainIcon_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainIcon);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Resizable = false;
            this.Text = "ConquerToolsKit";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabTools.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdvanced)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl tabTools;
        private MetroFramework.Controls.MetroPanel mainPanel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog selectFile;
        private MetroFramework.Controls.MetroLabel lblAbout;
        private System.Windows.Forms.PictureBox mainIcon;
        private System.Windows.Forms.TabPage tabPage1;
        private MetroFramework.Controls.MetroButton btnEncryptDat;
        private MetroFramework.Controls.MetroButton btnDecryptDat;
        private MetroFramework.Controls.MetroComboBox cbxDatFileType;
        private MetroFramework.Controls.MetroButton btnOpenFile;
        private MetroFramework.Controls.MetroLabel lblSelectedDatFile;
        private System.Windows.Forms.DataGridView dgvAdvanced;
        private MetroFramework.Controls.MetroToggle tglRawMode;
        private MetroFramework.Controls.MetroLabel lblRawMode;
    }
}

