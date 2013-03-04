namespace Carbinet
{
    partial class frmMainFloat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainFloat));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.pbFloatPie = new System.Windows.Forms.PictureBox();
            this.pbTest = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFloatPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTest)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(155)))), ((int)(((byte)(233)))));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(155)))), ((int)(((byte)(233)))));
            this.pbClose.Image = ((System.Drawing.Image)(resources.GetObject("pbClose.Image")));
            this.pbClose.Location = new System.Drawing.Point(111, 0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(30, 26);
            this.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClose.TabIndex = 2;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbHide_Click);
            // 
            // pbFloatPie
            // 
            this.pbFloatPie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(155)))), ((int)(((byte)(233)))));
            this.pbFloatPie.Image = ((System.Drawing.Image)(resources.GetObject("pbFloatPie.Image")));
            this.pbFloatPie.Location = new System.Drawing.Point(43, 0);
            this.pbFloatPie.Name = "pbFloatPie";
            this.pbFloatPie.Size = new System.Drawing.Size(30, 26);
            this.pbFloatPie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFloatPie.TabIndex = 3;
            this.pbFloatPie.TabStop = false;
            this.pbFloatPie.Click += new System.EventHandler(this.pbFloatPie_Click);
            // 
            // pbTest
            // 
            this.pbTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(155)))), ((int)(((byte)(233)))));
            this.pbTest.Image = ((System.Drawing.Image)(resources.GetObject("pbTest.Image")));
            this.pbTest.Location = new System.Drawing.Point(78, 0);
            this.pbTest.Name = "pbTest";
            this.pbTest.Size = new System.Drawing.Size(30, 26);
            this.pbTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTest.TabIndex = 4;
            this.pbTest.TabStop = false;
            this.pbTest.Click += new System.EventHandler(this.pbTest_Click);
            // 
            // frmMainFloat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Carbinet.Properties.Resources._8;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(156, 27);
            this.Controls.Add(this.pbTest);
            this.Controls.Add(this.pbFloatPie);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.pictureBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMainFloat";
            this.Opacity = 0.8;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFloatPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.PictureBox pbFloatPie;
        private System.Windows.Forms.PictureBox pbTest;

    }
}