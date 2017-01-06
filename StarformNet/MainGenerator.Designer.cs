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
            this._regenButton = new System.Windows.Forms.Button();
            this._nameLabel = new System.Windows.Forms.Label();
            this._systemNameBox = new System.Windows.Forms.TextBox();
            this._systemDetailsTab = new System.Windows.Forms.TabPage();
            this._planetSelectorLabel = new System.Windows.Forms.Label();
            this._planetSelector = new System.Windows.Forms.ComboBox();
            this._mainTabs = new System.Windows.Forms.TabControl();
            this._zoomInButton = new System.Windows.Forms.Button();
            this._zoomOutButton = new System.Windows.Forms.Button();
            this._orbitMap = new DLS.StarformNET.Display.SystemOrbitMap();
            this._planetInfoGroup = new DLS.StarformNET.Display.PlanetInfoGroup();
            this._systemInfo = new DLS.StarformNET.Display.SystemInfoGroup();
            this._systemMap = new DLS.StarformNET.Display.SystemMap();
            this._systemDetailsTab.SuspendLayout();
            this._mainTabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._orbitMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).BeginInit();
            this.SuspendLayout();
            // 
            // _regenButton
            // 
            this._regenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._regenButton.Location = new System.Drawing.Point(12, 795);
            this._regenButton.Name = "_regenButton";
            this._regenButton.Size = new System.Drawing.Size(117, 23);
            this._regenButton.TabIndex = 2;
            this._regenButton.Text = "Regenerate";
            this._regenButton.UseVisualStyleBackColor = true;
            this._regenButton.Click += new System.EventHandler(this._regenButton_Click);
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
            // _systemDetailsTab
            // 
            this._systemDetailsTab.Controls.Add(this._zoomOutButton);
            this._systemDetailsTab.Controls.Add(this._zoomInButton);
            this._systemDetailsTab.Controls.Add(this._planetSelectorLabel);
            this._systemDetailsTab.Controls.Add(this._planetSelector);
            this._systemDetailsTab.Controls.Add(this._orbitMap);
            this._systemDetailsTab.Controls.Add(this._planetInfoGroup);
            this._systemDetailsTab.Controls.Add(this._systemInfo);
            this._systemDetailsTab.Controls.Add(this._systemMap);
            this._systemDetailsTab.Location = new System.Drawing.Point(4, 22);
            this._systemDetailsTab.Name = "_systemDetailsTab";
            this._systemDetailsTab.Padding = new System.Windows.Forms.Padding(3);
            this._systemDetailsTab.Size = new System.Drawing.Size(1112, 729);
            this._systemDetailsTab.TabIndex = 0;
            this._systemDetailsTab.Text = "System Details";
            this._systemDetailsTab.UseVisualStyleBackColor = true;
            // 
            // _planetSelectorLabel
            // 
            this._planetSelectorLabel.AutoSize = true;
            this._planetSelectorLabel.Location = new System.Drawing.Point(7, 130);
            this._planetSelectorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._planetSelectorLabel.Name = "_planetSelectorLabel";
            this._planetSelectorLabel.Size = new System.Drawing.Size(45, 13);
            this._planetSelectorLabel.TabIndex = 6;
            this._planetSelectorLabel.Text = "Planets:";
            // 
            // _planetSelector
            // 
            this._planetSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._planetSelector.FormattingEnabled = true;
            this._planetSelector.Location = new System.Drawing.Point(56, 127);
            this._planetSelector.Margin = new System.Windows.Forms.Padding(2);
            this._planetSelector.Name = "_planetSelector";
            this._planetSelector.Size = new System.Drawing.Size(372, 21);
            this._planetSelector.TabIndex = 5;
            this._planetSelector.SelectionChangeCommitted += new System.EventHandler(this._planetSelector_Click);
            // 
            // _mainTabs
            // 
            this._mainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainTabs.Controls.Add(this._systemDetailsTab);
            this._mainTabs.Location = new System.Drawing.Point(12, 34);
            this._mainTabs.Multiline = true;
            this._mainTabs.Name = "_mainTabs";
            this._mainTabs.SelectedIndex = 0;
            this._mainTabs.Size = new System.Drawing.Size(1120, 755);
            this._mainTabs.TabIndex = 3;
            // 
            // _zoomInButton
            // 
            this._zoomInButton.Location = new System.Drawing.Point(1083, 671);
            this._zoomInButton.Name = "_zoomInButton";
            this._zoomInButton.Size = new System.Drawing.Size(23, 23);
            this._zoomInButton.TabIndex = 7;
            this._zoomInButton.Text = "+";
            this._zoomInButton.UseVisualStyleBackColor = true;
            this._zoomInButton.Click += new System.EventHandler(this._zoomInButton_Click);
            // 
            // _zoomOutButton
            // 
            this._zoomOutButton.Location = new System.Drawing.Point(1083, 700);
            this._zoomOutButton.Name = "_zoomOutButton";
            this._zoomOutButton.Size = new System.Drawing.Size(23, 23);
            this._zoomOutButton.TabIndex = 8;
            this._zoomOutButton.Text = "-";
            this._zoomOutButton.UseVisualStyleBackColor = true;
            this._zoomOutButton.Click += new System.EventHandler(this._zoomOutButton_Click);
            // 
            // _orbitMap
            // 
            this._orbitMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._orbitMap.BackColor = System.Drawing.Color.Black;
            this._orbitMap.Location = new System.Drawing.Point(432, 127);
            this._orbitMap.Name = "_orbitMap";
            this._orbitMap.Size = new System.Drawing.Size(674, 596);
            this._orbitMap.TabIndex = 4;
            this._orbitMap.TabStop = false;
            // 
            // _planetInfoGroup
            // 
            this._planetInfoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._planetInfoGroup.Location = new System.Drawing.Point(8, 153);
            this._planetInfoGroup.Margin = new System.Windows.Forms.Padding(2);
            this._planetInfoGroup.Name = "_planetInfoGroup";
            this._planetInfoGroup.Padding = new System.Windows.Forms.Padding(2);
            this._planetInfoGroup.Size = new System.Drawing.Size(418, 573);
            this._planetInfoGroup.TabIndex = 3;
            this._planetInfoGroup.TabStop = false;
            this._planetInfoGroup.Text = "Planet Details";
            // 
            // _systemInfo
            // 
            this._systemInfo.Location = new System.Drawing.Point(8, 6);
            this._systemInfo.Name = "_systemInfo";
            this._systemInfo.Size = new System.Drawing.Size(418, 110);
            this._systemInfo.TabIndex = 2;
            this._systemInfo.TabStop = false;
            this._systemInfo.Text = "System Details";
            // 
            // _systemMap
            // 
            this._systemMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._systemMap.BackColor = System.Drawing.Color.Black;
            this._systemMap.Location = new System.Drawing.Point(432, 12);
            this._systemMap.Name = "_systemMap";
            this._systemMap.PlanetPadding = 0;
            this._systemMap.Size = new System.Drawing.Size(674, 103);
            this._systemMap.TabIndex = 0;
            this._systemMap.TabStop = false;
            // 
            // MainGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 830);
            this.Controls.Add(this._systemNameBox);
            this.Controls.Add(this._nameLabel);
            this.Controls.Add(this._mainTabs);
            this.Controls.Add(this._regenButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainGenerator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Solar System Generator";
            this._systemDetailsTab.ResumeLayout(false);
            this._systemDetailsTab.PerformLayout();
            this._mainTabs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._orbitMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _regenButton;
        private System.Windows.Forms.Label _nameLabel;
        private System.Windows.Forms.TextBox _systemNameBox;
        private System.Windows.Forms.TabPage _systemDetailsTab;
        private System.Windows.Forms.Label _planetSelectorLabel;
        private System.Windows.Forms.ComboBox _planetSelector;
        private Display.SystemOrbitMap _orbitMap;
        private Display.PlanetInfoGroup _planetInfoGroup;
        private Display.SystemInfoGroup _systemInfo;
        private Display.SystemMap _systemMap;
        private System.Windows.Forms.TabControl _mainTabs;
        private System.Windows.Forms.Button _zoomOutButton;
        private System.Windows.Forms.Button _zoomInButton;
    }
}

