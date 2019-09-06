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
            this.lblSelectedFile = new MetroFramework.Controls.MetroLabel();
            this.btnEncrypt = new MetroFramework.Controls.MetroButton();
            this.btnDecrypt = new MetroFramework.Controls.MetroButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnEncryptDat = new MetroFramework.Controls.MetroButton();
            this.btnDecryptDat = new MetroFramework.Controls.MetroButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblAbout = new MetroFramework.Controls.MetroLabel();
            this.mainPanel = new MetroFramework.Controls.MetroPanel();
            this.selectFile = new System.Windows.Forms.OpenFileDialog();
            this.mainIcon = new System.Windows.Forms.PictureBox();
            this.tabTools.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tabPage1);
            this.tabTools.Controls.Add(this.tabPage2);
            this.tabTools.Controls.Add(this.tabPage3);
            this.tabTools.Location = new System.Drawing.Point(3, 3);
            this.tabTools.Name = "tabTools";
            this.tabTools.SelectedIndex = 1;
            this.tabTools.Size = new System.Drawing.Size(794, 382);
            this.tabTools.TabIndex = 0;
            this.tabTools.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblSelectedFile);
            this.tabPage1.Controls.Add(this.btnEncrypt);
            this.tabPage1.Controls.Add(this.btnDecrypt);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(786, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Itemtype";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedFile.Location = new System.Drawing.Point(15, 45);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(95, 19);
            this.lblSelectedFile.TabIndex = 2;
            this.lblSelectedFile.Text = "Selected File: -";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(105, 15);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseSelectable = true;
            this.btnEncrypt.Click += new System.EventHandler(this.BtnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(15, 15);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 0;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseSelectable = true;
            this.btnDecrypt.Click += new System.EventHandler(this.BtnDecrypt_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnEncryptDat);
            this.tabPage2.Controls.Add(this.btnDecryptDat);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(786, 340);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Encrypt/Decrypt";
            // 
            // btnEncryptDat
            // 
            this.btnEncryptDat.Location = new System.Drawing.Point(105, 15);
            this.btnEncryptDat.Name = "btnEncryptDat";
            this.btnEncryptDat.Size = new System.Drawing.Size(75, 23);
            this.btnEncryptDat.TabIndex = 3;
            this.btnEncryptDat.Text = "Encrypt";
            this.btnEncryptDat.UseSelectable = true;
            // 
            // btnDecryptDat
            // 
            this.btnDecryptDat.Location = new System.Drawing.Point(15, 15);
            this.btnDecryptDat.Name = "btnDecryptDat";
            this.btnDecryptDat.Size = new System.Drawing.Size(75, 23);
            this.btnDecryptDat.TabIndex = 2;
            this.btnDecryptDat.Text = "Decrypt";
            this.btnDecryptDat.UseSelectable = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblAbout);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(786, 340);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "About";
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl tabTools;
        private System.Windows.Forms.TabPage tabPage1;
        private MetroFramework.Controls.MetroPanel mainPanel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.OpenFileDialog selectFile;
        private MetroFramework.Controls.MetroButton btnDecrypt;
        private MetroFramework.Controls.MetroButton btnEncrypt;
        private MetroFramework.Controls.MetroLabel lblSelectedFile;
        private MetroFramework.Controls.MetroLabel lblAbout;
        private System.Windows.Forms.PictureBox mainIcon;
        private System.Windows.Forms.TabPage tabPage2;
        private MetroFramework.Controls.MetroButton btnEncryptDat;
        private MetroFramework.Controls.MetroButton btnDecryptDat;
    }
}

