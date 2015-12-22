namespace Plugin_Modder_UI
{
    partial class UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.RefreshList = new System.Windows.Forms.Button();
            this.OpenPluginFolder = new System.Windows.Forms.Button();
            this.activePluginsBox = new System.Windows.Forms.CheckedListBox();
            this.RunSE = new System.Windows.Forms.Button();
            this.BinLocationTextBox = new System.Windows.Forms.TextBox();
            this.InstallButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.RefreshList);
            this.panel1.Controls.Add(this.OpenPluginFolder);
            this.panel1.Controls.Add(this.activePluginsBox);
            this.panel1.Controls.Add(this.RunSE);
            this.panel1.Controls.Add(this.BinLocationTextBox);
            this.panel1.Controls.Add(this.InstallButton);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 338);
            this.panel1.TabIndex = 0;
            // 
            // RefreshList
            // 
            this.RefreshList.Location = new System.Drawing.Point(164, 310);
            this.RefreshList.Name = "RefreshList";
            this.RefreshList.Size = new System.Drawing.Size(167, 23);
            this.RefreshList.TabIndex = 5;
            this.RefreshList.Text = "Refresh List";
            this.RefreshList.UseVisualStyleBackColor = true;
            this.RefreshList.Click += new System.EventHandler(this.RefreshList_Click);
            // 
            // OpenPluginFolder
            // 
            this.OpenPluginFolder.Location = new System.Drawing.Point(4, 310);
            this.OpenPluginFolder.Name = "OpenPluginFolder";
            this.OpenPluginFolder.Size = new System.Drawing.Size(153, 23);
            this.OpenPluginFolder.TabIndex = 4;
            this.OpenPluginFolder.Text = "Open In Explorer";
            this.OpenPluginFolder.UseVisualStyleBackColor = true;
            this.OpenPluginFolder.Click += new System.EventHandler(this.OpenPluginFolder_Click);
            // 
            // activePluginsBox
            // 
            this.activePluginsBox.FormattingEnabled = true;
            this.activePluginsBox.Location = new System.Drawing.Point(4, 59);
            this.activePluginsBox.Name = "activePluginsBox";
            this.activePluginsBox.Size = new System.Drawing.Size(471, 244);
            this.activePluginsBox.TabIndex = 3;
            this.activePluginsBox.SelectedIndexChanged += new System.EventHandler(this.activePluginsBox_SelectedIndexChanged);
            // 
            // RunSE
            // 
            this.RunSE.Location = new System.Drawing.Point(163, 30);
            this.RunSE.Name = "RunSE";
            this.RunSE.Size = new System.Drawing.Size(168, 23);
            this.RunSE.TabIndex = 2;
            this.RunSE.Text = "Run SpaceEngineers";
            this.RunSE.UseVisualStyleBackColor = true;
            this.RunSE.Click += new System.EventHandler(this.RunSE_Click);
            // 
            // BinLocationTextBox
            // 
            this.BinLocationTextBox.Location = new System.Drawing.Point(4, 4);
            this.BinLocationTextBox.Name = "BinLocationTextBox";
            this.BinLocationTextBox.Size = new System.Drawing.Size(471, 20);
            this.BinLocationTextBox.TabIndex = 1;
            this.BinLocationTextBox.Text = "Enter in SE Bin64 Here";
            this.BinLocationTextBox.TextChanged += new System.EventHandler(this.BinLocationTextBox_TextChanged);
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(4, 30);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(153, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "Install SEMultiPlugin";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(502, 362);
            this.Controls.Add(this.panel1);
            this.Name = "UI";
            this.Text = "Plugin Modder UI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox BinLocationTextBox;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button OpenPluginFolder;
        private System.Windows.Forms.CheckedListBox activePluginsBox;
        private System.Windows.Forms.Button RunSE;
        private System.Windows.Forms.Button RefreshList;
    }
}

