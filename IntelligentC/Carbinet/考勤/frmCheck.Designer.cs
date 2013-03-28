namespace Carbinet
{
    partial class frmCheck
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheck));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnHideSeat = new System.Windows.Forms.Button();
            this.PieChart1 = new Nexus.Windows.Forms.PieChart();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "出勤";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "缺勤";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.PieChart1);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(1, 1);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(243, 414);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImage = global::Carbinet.Properties.Resources.orange;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(18, 309);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 18;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.UseWaitCursor = true;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.BackgroundImage = global::Carbinet.Properties.Resources.grey;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(18, 338);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 18;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // btnQuit
            // 
            this.btnQuit.BackgroundImage = global::Carbinet.Properties.Resources.PowerShut_Down;
            this.btnQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuit.FlatAppearance.BorderSize = 0;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Location = new System.Drawing.Point(155, 436);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(49, 49);
            this.btnQuit.TabIndex = 35;
            this.btnQuit.UseVisualStyleBackColor = true;
            // 
            // btnHideSeat
            // 
            this.btnHideSeat.BackgroundImage = global::Carbinet.Properties.Resources.form_expand;
            this.btnHideSeat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHideSeat.FlatAppearance.BorderSize = 0;
            this.btnHideSeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHideSeat.Location = new System.Drawing.Point(35, 436);
            this.btnHideSeat.Name = "btnHideSeat";
            this.btnHideSeat.Size = new System.Drawing.Size(49, 49);
            this.btnHideSeat.TabIndex = 37;
            this.btnHideSeat.UseVisualStyleBackColor = true;
            // 
            // PieChart1
            // 
            this.PieChart1.Location = new System.Drawing.Point(6, 20);
            this.PieChart1.Name = "PieChart1";
            this.PieChart1.Radius = 200F;
            this.PieChart1.Size = new System.Drawing.Size(231, 192);
            this.PieChart1.TabIndex = 20;
            this.PieChart1.Text = "pieChart1";
            this.PieChart1.Thickness = 10F;
            // 
            // frmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 498);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnHideSeat);
            this.Controls.Add(this.groupBox5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmCheck";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnHideSeat;
        private Nexus.Windows.Forms.PieChart PieChart1;
    }
}

