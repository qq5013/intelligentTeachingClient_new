namespace Carbinet
{
    partial class frmLegend
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
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(165, 198);
            this.metroTile1.MainText = "";
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(86, 23);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTile1.StyleManager = null;
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile1.TileCount = 0;
            this.metroTile1.Visible = false;
            // 
            // metroButton1
            // 
            this.metroButton1.Enabled = false;
            this.metroButton1.Highlight = false;
            this.metroButton1.Location = new System.Drawing.Point(-3, -1);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(211, 30);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Black;
            this.metroButton1.StyleManager = null;
            this.metroButton1.TabIndex = 2;
            this.metroButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // frmLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 222);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroTile1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLegend";
            this.Opacity = 0.8D;
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroButton metroButton1;

    }
}