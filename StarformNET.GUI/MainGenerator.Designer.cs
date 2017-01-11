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
            this._systemDetailsTab = new System.Windows.Forms.TabPage();
            this._zoomOutButton = new System.Windows.Forms.Button();
            this._zoomInButton = new System.Windows.Forms.Button();
            this._planetSelectorLabel = new System.Windows.Forms.Label();
            this._planetSelector = new System.Windows.Forms.ComboBox();
            this._mainTabs = new System.Windows.Forms.TabControl();
            this._regenAllButton = new System.Windows.Forms.Button();
            this._systemListBox = new System.Windows.Forms.ListBox();
            this._systemListLabel = new System.Windows.Forms.Label();
            this._genOptionsGroup = new System.Windows.Forms.GroupBox();
            this._countSelector = new System.Windows.Forms.NumericUpDown();
            this._countLabel = new System.Windows.Forms.Label();
            this._seedLabel = new System.Windows.Forms.Label();
            this._seedSelector = new System.Windows.Forms.NumericUpDown();
            this._eccentricityLabel = new System.Windows.Forms.Label();
            this._eccentricitySelector = new System.Windows.Forms.NumericUpDown();
            this._orbitMap = new DLS.StarformNET.Display.SystemOrbitMap();
            this._planetInfoGroup = new DLS.StarformNET.Display.PlanetInfoGroup();
            this._systemInfo = new DLS.StarformNET.Display.SystemInfoGroup();
            this._systemMap = new DLS.StarformNET.Display.SystemMap();
            this._dustDensityLabel = new System.Windows.Forms.Label();
            this._dustDensitySelector = new System.Windows.Forms.NumericUpDown();
            this._gasRatioLabel = new System.Windows.Forms.Label();
            this._gasRatioSelector = new System.Windows.Forms.NumericUpDown();
            this._systemDetailsTab.SuspendLayout();
            this._mainTabs.SuspendLayout();
            this._genOptionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._countSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._seedSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._eccentricitySelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._orbitMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dustDensitySelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gasRatioSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // _regenButton
            // 
            this._regenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._regenButton.Location = new System.Drawing.Point(1108, 785);
            this._regenButton.Name = "_regenButton";
            this._regenButton.Size = new System.Drawing.Size(117, 23);
            this._regenButton.TabIndex = 2;
            this._regenButton.Text = "Regenerate System";
            this._regenButton.UseVisualStyleBackColor = true;
            this._regenButton.Click += new System.EventHandler(this._regenButton_Click);
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
            this._systemDetailsTab.Size = new System.Drawing.Size(1209, 741);
            this._systemDetailsTab.TabIndex = 0;
            this._systemDetailsTab.Text = "System Details";
            this._systemDetailsTab.UseVisualStyleBackColor = true;
            // 
            // _zoomOutButton
            // 
            this._zoomOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._zoomOutButton.Location = new System.Drawing.Point(1180, 712);
            this._zoomOutButton.Name = "_zoomOutButton";
            this._zoomOutButton.Size = new System.Drawing.Size(23, 23);
            this._zoomOutButton.TabIndex = 8;
            this._zoomOutButton.Text = "-";
            this._zoomOutButton.UseVisualStyleBackColor = true;
            this._zoomOutButton.Click += new System.EventHandler(this._zoomOutButton_Click);
            // 
            // _zoomInButton
            // 
            this._zoomInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._zoomInButton.Location = new System.Drawing.Point(1180, 683);
            this._zoomInButton.Name = "_zoomInButton";
            this._zoomInButton.Size = new System.Drawing.Size(23, 23);
            this._zoomInButton.TabIndex = 7;
            this._zoomInButton.Text = "+";
            this._zoomInButton.UseVisualStyleBackColor = true;
            this._zoomInButton.Click += new System.EventHandler(this._zoomInButton_Click);
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
            this._mainTabs.Location = new System.Drawing.Point(12, 12);
            this._mainTabs.Multiline = true;
            this._mainTabs.Name = "_mainTabs";
            this._mainTabs.SelectedIndex = 0;
            this._mainTabs.Size = new System.Drawing.Size(1217, 767);
            this._mainTabs.TabIndex = 3;
            // 
            // _regenAllButton
            // 
            this._regenAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._regenAllButton.Location = new System.Drawing.Point(1108, 807);
            this._regenAllButton.Name = "_regenAllButton";
            this._regenAllButton.Size = new System.Drawing.Size(117, 23);
            this._regenAllButton.TabIndex = 6;
            this._regenAllButton.Text = "Regenerate All";
            this._regenAllButton.UseVisualStyleBackColor = true;
            this._regenAllButton.Click += new System.EventHandler(this._regenAllButton_Click);
            // 
            // _systemListBox
            // 
            this._systemListBox.FormattingEnabled = true;
            this._systemListBox.Location = new System.Drawing.Point(1232, 60);
            this._systemListBox.Name = "_systemListBox";
            this._systemListBox.Size = new System.Drawing.Size(173, 719);
            this._systemListBox.TabIndex = 7;
            this._systemListBox.SelectedValueChanged += new System.EventHandler(this._systemListBox_SelectedValueChanged);
            // 
            // _systemListLabel
            // 
            this._systemListLabel.AutoSize = true;
            this._systemListLabel.Location = new System.Drawing.Point(1232, 34);
            this._systemListLabel.Name = "_systemListLabel";
            this._systemListLabel.Size = new System.Drawing.Size(99, 13);
            this._systemListLabel.TabIndex = 8;
            this._systemListLabel.Text = "Generated Systems";
            // 
            // _genOptionsGroup
            // 
            this._genOptionsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._genOptionsGroup.Controls.Add(this._gasRatioSelector);
            this._genOptionsGroup.Controls.Add(this._gasRatioLabel);
            this._genOptionsGroup.Controls.Add(this._dustDensitySelector);
            this._genOptionsGroup.Controls.Add(this._dustDensityLabel);
            this._genOptionsGroup.Controls.Add(this._eccentricityLabel);
            this._genOptionsGroup.Controls.Add(this._eccentricitySelector);
            this._genOptionsGroup.Controls.Add(this._seedLabel);
            this._genOptionsGroup.Controls.Add(this._seedSelector);
            this._genOptionsGroup.Controls.Add(this._countLabel);
            this._genOptionsGroup.Controls.Add(this._countSelector);
            this._genOptionsGroup.Location = new System.Drawing.Point(12, 783);
            this._genOptionsGroup.Name = "_genOptionsGroup";
            this._genOptionsGroup.Size = new System.Drawing.Size(1090, 47);
            this._genOptionsGroup.TabIndex = 9;
            this._genOptionsGroup.TabStop = false;
            this._genOptionsGroup.Text = "Generation Options";
            // 
            // _countSelector
            // 
            this._countSelector.Location = new System.Drawing.Point(74, 19);
            this._countSelector.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._countSelector.Name = "_countSelector";
            this._countSelector.Size = new System.Drawing.Size(120, 20);
            this._countSelector.TabIndex = 0;
            // 
            // _countLabel
            // 
            this._countLabel.AutoSize = true;
            this._countLabel.Location = new System.Drawing.Point(6, 19);
            this._countLabel.Name = "_countLabel";
            this._countLabel.Size = new System.Drawing.Size(62, 13);
            this._countLabel.TabIndex = 1;
            this._countLabel.Text = "Group Size:";
            // 
            // _seedLabel
            // 
            this._seedLabel.AutoSize = true;
            this._seedLabel.Location = new System.Drawing.Point(202, 19);
            this._seedLabel.Name = "_seedLabel";
            this._seedLabel.Size = new System.Drawing.Size(35, 13);
            this._seedLabel.TabIndex = 3;
            this._seedLabel.Text = "Seed:";
            // 
            // _seedSelector
            // 
            this._seedSelector.Location = new System.Drawing.Point(243, 19);
            this._seedSelector.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this._seedSelector.Name = "_seedSelector";
            this._seedSelector.Size = new System.Drawing.Size(120, 20);
            this._seedSelector.TabIndex = 2;
            // 
            // _eccentricityLabel
            // 
            this._eccentricityLabel.AutoSize = true;
            this._eccentricityLabel.Location = new System.Drawing.Point(369, 19);
            this._eccentricityLabel.Name = "_eccentricityLabel";
            this._eccentricityLabel.Size = new System.Drawing.Size(65, 13);
            this._eccentricityLabel.TabIndex = 5;
            this._eccentricityLabel.Text = "Eccentricity:";
            // 
            // _eccentricitySelector
            // 
            this._eccentricitySelector.DecimalPlaces = 3;
            this._eccentricitySelector.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this._eccentricitySelector.Location = new System.Drawing.Point(436, 19);
            this._eccentricitySelector.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._eccentricitySelector.Name = "_eccentricitySelector";
            this._eccentricitySelector.Size = new System.Drawing.Size(120, 20);
            this._eccentricitySelector.TabIndex = 4;
            // 
            // _orbitMap
            // 
            this._orbitMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._orbitMap.BackColor = System.Drawing.Color.Black;
            this._orbitMap.Location = new System.Drawing.Point(432, 127);
            this._orbitMap.Name = "_orbitMap";
            this._orbitMap.Size = new System.Drawing.Size(771, 608);
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
            this._planetInfoGroup.Size = new System.Drawing.Size(418, 585);
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
            this._systemMap.Size = new System.Drawing.Size(771, 103);
            this._systemMap.TabIndex = 0;
            this._systemMap.TabStop = false;
            // 
            // _dustDensityLabel
            // 
            this._dustDensityLabel.AutoSize = true;
            this._dustDensityLabel.Location = new System.Drawing.Point(562, 19);
            this._dustDensityLabel.Name = "_dustDensityLabel";
            this._dustDensityLabel.Size = new System.Drawing.Size(70, 13);
            this._dustDensityLabel.TabIndex = 6;
            this._dustDensityLabel.Text = "Dust Density:";
            // 
            // _dustDensitySelector
            // 
            this._dustDensitySelector.DecimalPlaces = 4;
            this._dustDensitySelector.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this._dustDensitySelector.Location = new System.Drawing.Point(638, 19);
            this._dustDensitySelector.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._dustDensitySelector.Name = "_dustDensitySelector";
            this._dustDensitySelector.Size = new System.Drawing.Size(120, 20);
            this._dustDensitySelector.TabIndex = 7;
            // 
            // _gasRatioLabel
            // 
            this._gasRatioLabel.AutoSize = true;
            this._gasRatioLabel.Location = new System.Drawing.Point(764, 19);
            this._gasRatioLabel.Name = "_gasRatioLabel";
            this._gasRatioLabel.Size = new System.Drawing.Size(84, 13);
            this._gasRatioLabel.TabIndex = 8;
            this._gasRatioLabel.Text = "Gas/Dust Ratio:";
            // 
            // _gasRatioSelector
            // 
            this._gasRatioSelector.Location = new System.Drawing.Point(854, 19);
            this._gasRatioSelector.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._gasRatioSelector.Name = "_gasRatioSelector";
            this._gasRatioSelector.Size = new System.Drawing.Size(120, 20);
            this._gasRatioSelector.TabIndex = 9;
            // 
            // MainGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 837);
            this.Controls.Add(this._genOptionsGroup);
            this.Controls.Add(this._systemListLabel);
            this.Controls.Add(this._systemListBox);
            this.Controls.Add(this._regenAllButton);
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
            this._genOptionsGroup.ResumeLayout(false);
            this._genOptionsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._countSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._seedSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._eccentricitySelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._orbitMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dustDensitySelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gasRatioSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _regenButton;
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
        private System.Windows.Forms.Button _regenAllButton;
        private System.Windows.Forms.ListBox _systemListBox;
        private System.Windows.Forms.Label _systemListLabel;
        private System.Windows.Forms.GroupBox _genOptionsGroup;
        private System.Windows.Forms.Label _seedLabel;
        private System.Windows.Forms.NumericUpDown _seedSelector;
        private System.Windows.Forms.Label _countLabel;
        private System.Windows.Forms.NumericUpDown _countSelector;
        private System.Windows.Forms.Label _eccentricityLabel;
        private System.Windows.Forms.NumericUpDown _eccentricitySelector;
        private System.Windows.Forms.NumericUpDown _gasRatioSelector;
        private System.Windows.Forms.Label _gasRatioLabel;
        private System.Windows.Forms.NumericUpDown _dustDensitySelector;
        private System.Windows.Forms.Label _dustDensityLabel;
    }
}

