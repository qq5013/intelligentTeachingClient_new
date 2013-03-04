namespace Carbinet
{
    partial class frmRTTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRTTest));
            this.groupBoxQuestion = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.editor1 = new HtmlEditor.Editor();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.m_panelDrawing = new System.Drawing.PieChart.PieChartControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxChair = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBoxQuestion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxChair.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxQuestion
            // 
            this.groupBoxQuestion.Controls.Add(this.btnStart);
            this.groupBoxQuestion.Controls.Add(this.editor1);
            this.groupBoxQuestion.Controls.Add(this.btnPre);
            this.groupBoxQuestion.Controls.Add(this.btnNext);
            this.groupBoxQuestion.Location = new System.Drawing.Point(24, 98);
            this.groupBoxQuestion.Name = "groupBoxQuestion";
            this.groupBoxQuestion.Size = new System.Drawing.Size(657, 469);
            this.groupBoxQuestion.TabIndex = 0;
            this.groupBoxQuestion.TabStop = false;
            this.groupBoxQuestion.Text = "题目信息";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 434);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始(&S)";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // editor1
            // 
            this.editor1.BodyHtml = null;
            this.editor1.BodyText = null;
            this.editor1.Dock = System.Windows.Forms.DockStyle.Top;
            this.editor1.DocumentText = resources.GetString("editor1.DocumentText");
            this.editor1.EditorBackColor = System.Drawing.Color.White;
            this.editor1.EditorForeColor = System.Drawing.Color.Black;
            this.editor1.FontName = null;
            this.editor1.FontSize = HtmlEditor.FontSize.NA;
            this.editor1.Location = new System.Drawing.Point(3, 17);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(651, 411);
            this.editor1.TabIndex = 3;
            this.editor1.TabStop = false;
            // 
            // btnPre
            // 
            this.btnPre.Enabled = false;
            this.btnPre.Location = new System.Drawing.Point(463, 435);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(75, 23);
            this.btnPre.TabIndex = 2;
            this.btnPre.Text = "上一题(&P)";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(561, 435);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一题(&N)";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // m_panelDrawing
            // 
            this.m_panelDrawing.Location = new System.Drawing.Point(6, 20);
            this.m_panelDrawing.Name = "m_panelDrawing";
            this.m_panelDrawing.Size = new System.Drawing.Size(260, 203);
            this.m_panelDrawing.TabIndex = 1;
            this.m_panelDrawing.ToolTips = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "已完成";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "未完成";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(738, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "计时：";
            this.label3.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.Location = new System.Drawing.Point(794, 46);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(219, 62);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "00:00:00";
            this.lblTime.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_panelDrawing);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(702, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 469);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::Carbinet.Properties.Resources.grey;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(92, 386);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Carbinet.Properties.Resources.orange;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(92, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBoxChair
            // 
            this.groupBoxChair.BackColor = System.Drawing.Color.White;
            this.groupBoxChair.Controls.Add(this.button6);
            this.groupBoxChair.Controls.Add(this.pictureBox1);
            this.groupBoxChair.Location = new System.Drawing.Point(24, 591);
            this.groupBoxChair.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxChair.Name = "groupBoxChair";
            this.groupBoxChair.Size = new System.Drawing.Size(657, 560);
            this.groupBoxChair.TabIndex = 21;
            this.groupBoxChair.TabStop = false;
            this.groupBoxChair.Visible = false;
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::Carbinet.Properties.Resources.grey;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(242, 17);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(150, 34);
            this.button6.TabIndex = 22;
            this.button6.Text = "教师讲台";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(651, 540);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(606, 73);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "显示座位";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmRTTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 652);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBoxQuestion);
            this.Controls.Add(this.groupBoxChair);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRTTest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "课堂测验";
            this.groupBoxQuestion.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxChair.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxQuestion;
        private System.Drawing.PieChart.PieChartControl m_panelDrawing;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox groupBoxChair;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private HtmlEditor.Editor editor1;
        private System.Windows.Forms.Button btnStart;
    }
}