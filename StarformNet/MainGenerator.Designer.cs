namespace DLS.StarformNET
{
    partial class MainGenerator
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
            this._descriptionBox = new System.Windows.Forms.TextBox();
            this._regenButton = new System.Windows.Forms.Button();
            this._mainTabs = new System.Windows.Forms.TabControl();
            this._systemDetailsTab = new System.Windows.Forms.TabPage();
            this._systemMapTab = new System.Windows.Forms.TabPage();
            this._nameLabel = new System.Windows.Forms.Label();
            this._systemNameBox = new System.Windows.Forms.TextBox();
            this._systemInfo = new DLS.StarformNET.Display.SystemInfoGroup();
            this._systemMap = new DLS.StarformNET.Display.SystemMap();
            this._orbitMap = new DLS.StarformNET.Display.SystemOrbitMap();
            this._mainTabs.SuspendLayout();
            this._systemDetailsTab.SuspendLayout();
            this._systemMapTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._orbitMap)).BeginInit();
            this.SuspendLayout();
            // 
            // _descriptionBox
            // 
            this._descriptionBox.Location = new System.Drawing.Point(626, 223);
            this._descriptionBox.Multiline = true;
            this._descriptionBox.Name = "_descriptionBox";
            this._descriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._descriptionBox.Size = new System.Drawing.Size(237, 211);
            this._descriptionBox.TabIndex = 1;
            // 
            // _regenButton
            // 
            this._regenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._regenButton.Location = new System.Drawing.Point(12, 707);
            this._regenButton.Name = "_regenButton";
            this._regenButton.Size = new System.Drawing.Size(117, 23);
            this._regenButton.TabIndex = 2;
            this._regenButton.Text = "Regenerate";
            this._regenButton.UseVisualStyleBackColor = true;
            this._regenButton.Click += new System.EventHandler(this._regenButton_Click);
            // 
            // _mainTabs
            // 
            this._mainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainTabs.Controls.Add(this._systemDetailsTab);
            this._mainTabs.Controls.Add(this._systemMapTab);
            this._mainTabs.Location = new System.Drawing.Point(12, 34);
            this._mainTabs.Name = "_mainTabs";
            this._mainTabs.SelectedIndex = 0;
            this._mainTabs.Size = new System.Drawing.Size(1141, 671);
            this._mainTabs.TabIndex = 3;
            // 
            // _systemDetailsTab
            // 
            this._systemDetailsTab.Controls.Add(this._systemInfo);
            this._systemDetailsTab.Controls.Add(this._systemMap);
            this._systemDetailsTab.Controls.Add(this._descriptionBox);
            this._systemDetailsTab.Location = new System.Drawing.Point(4, 22);
            this._systemDetailsTab.Name = "_systemDetailsTab";
            this._systemDetailsTab.Padding = new System.Windows.Forms.Padding(3);
            this._systemDetailsTab.Size = new System.Drawing.Size(1133, 645);
            this._systemDetailsTab.TabIndex = 0;
            this._systemDetailsTab.Text = "System Details";
            this._systemDetailsTab.UseVisualStyleBackColor = true;
            // 
            // _systemMapTab
            // 
            this._systemMapTab.Controls.Add(this._orbitMap);
            this._systemMapTab.Location = new System.Drawing.Point(4, 22);
            this._systemMapTab.Name = "_systemMapTab";
            this._systemMapTab.Padding = new System.Windows.Forms.Padding(3);
            this._systemMapTab.Size = new System.Drawing.Size(1133, 645);
            this._systemMapTab.TabIndex = 1;
            this._systemMapTab.Text = "Orbit Map";
            this._systemMapTab.UseVisualStyleBackColor = true;
            // 
            // _nameLabel
            // 
            this._nameLabel.AutoSize = true;
            this._nameLabel.Location = new System.Drawing.Point(13, 11);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(75, 13);
            this._nameLabel.TabIndex = 4;
            this._nameLabel.Text = "System Name:";
            // 
            // _systemNameBox
            // 
            this._systemNameBox.Location = new System.Drawing.Point(94, 8);
            this._systemNameBox.Name = "_systemNameBox";
            this._systemNameBox.Size = new System.Drawing.Size(796, 20);
            this._systemNameBox.TabIndex = 5;
            // 
            // _systemInfo
            // 
            this._systemInfo.Location = new System.Drawing.Point(8, 6);
            this._systemInfo.Name = "_systemInfo";
            this._systemInfo.Size = new System.Drawing.Size(236, 153);
            this._systemInfo.TabIndex = 2;
            this._systemInfo.TabStop = false;
            this._systemInfo.Text = "System Info";
            // 
            // _systemMap
            // 
            this._systemMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._systemMap.BackColor = System.Drawing.Color.Black;
            this._systemMap.Location = new System.Drawing.Point(250, 6);
            this._systemMap.Name = "_systemMap";
            this._systemMap.PlanetPadding = 0;
            this._systemMap.Size = new System.Drawing.Size(877, 153);
            this._systemMap.TabIndex = 0;
            this._systemMap.TabStop = false;
            // 
            // _orbitMap
            // 
            this._orbitMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._orbitMap.BackColor = System.Drawing.Color.Black;
            this._orbitMap.Location = new System.Drawing.Point(7, 7);
            this._orbitMap.Name = "_orbitMap";
            this._orbitMap.Size = new System.Drawing.Size(1120, 632);
            this._orbitMap.TabIndex = 0;
            this._orbitMap.TabStop = false;
            // 
            // MainGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 738);
            this.Controls.Add(this._systemNameBox);
            this.Controls.Add(this._nameLabel);
            this.Controls.Add(this._mainTabs);
            this.Controls.Add(this._regenButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainGenerator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Solar System Generator";
            this._mainTabs.ResumeLayout(false);
            this._systemDetailsTab.ResumeLayout(false);
            this._systemDetailsTab.PerformLayout();
            this._systemMapTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._orbitMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Display.SystemMap _systemMap;
        private System.Windows.Forms.TextBox _descriptionBox;
        private System.Windows.Forms.Button _regenButton;
        private System.Windows.Forms.TabControl _mainTabs;
        private System.Windows.Forms.TabPage _systemDetailsTab;
        private System.Windows.Forms.TabPage _systemMapTab;
        private System.Windows.Forms.Label _nameLabel;
        private System.Windows.Forms.TextBox _systemNameBox;
        private Display.SystemInfoGroup _systemInfo;
        private Display.SystemOrbitMap _orbitMap;
    }
}

