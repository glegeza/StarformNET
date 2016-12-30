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
            this._systemMap = new DLS.StarformNET.Display.SystemMap();
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).BeginInit();
            this.SuspendLayout();
            // 
            // _descriptionBox
            // 
            this._descriptionBox.Location = new System.Drawing.Point(13, 128);
            this._descriptionBox.Multiline = true;
            this._descriptionBox.Name = "_descriptionBox";
            this._descriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._descriptionBox.Size = new System.Drawing.Size(859, 314);
            this._descriptionBox.TabIndex = 1;
            // 
            // _regenButton
            // 
            this._regenButton.Location = new System.Drawing.Point(13, 449);
            this._regenButton.Name = "_regenButton";
            this._regenButton.Size = new System.Drawing.Size(117, 23);
            this._regenButton.TabIndex = 2;
            this._regenButton.Text = "Regenerate";
            this._regenButton.UseVisualStyleBackColor = true;
            this._regenButton.Click += new System.EventHandler(this._regenButton_Click);
            // 
            // _systemMap
            // 
            this._systemMap.BackColor = System.Drawing.Color.Black;
            this._systemMap.Location = new System.Drawing.Point(13, 13);
            this._systemMap.Name = "_systemMap";
            this._systemMap.PlanetPadding = 0;
            this._systemMap.Size = new System.Drawing.Size(859, 108);
            this._systemMap.TabIndex = 0;
            this._systemMap.TabStop = false;
            // 
            // MainGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 480);
            this.Controls.Add(this._regenButton);
            this.Controls.Add(this._descriptionBox);
            this.Controls.Add(this._systemMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainGenerator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Solar System Generator";
            ((System.ComponentModel.ISupportInitialize)(this._systemMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Display.SystemMap _systemMap;
        private System.Windows.Forms.TextBox _descriptionBox;
        private System.Windows.Forms.Button _regenButton;
    }
}

