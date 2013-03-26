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
            this.metroBtnNext = new MetroFramework.Controls.MetroButton();
            this.metroBtnPre = new MetroFramework.Controls.MetroButton();
            this.metroBtnStart = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.PieChart1 = new Nexus.Windows.Forms.PieChart();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.editor1 = new HtmlEditor.Editor();
            this.groupBoxQuestion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxQuestion
            // 
            this.groupBoxQuestion.Controls.Add(this.metroBtnNext);
            this.groupBoxQuestion.Controls.Add(this.metroBtnPre);
            this.groupBoxQuestion.Controls.Add(this.metroBtnStart);
            this.groupBoxQuestion.Controls.Add(this.editor1);
            this.groupBoxQuestion.Location = new System.Drawing.Point(24, 98);
            this.groupBoxQuestion.Name = "groupBoxQuestion";
            this.groupBoxQuestion.Size = new System.Drawing.Size(657, 469);
            this.groupBoxQuestion.TabIndex = 0;
            this.groupBoxQuestion.TabStop = false;
            this.groupBoxQuestion.Text = "题目信息";
            // 
            // metroBtnNext
            // 
            this.metroBtnNext.Highlight = false;
            this.metroBtnNext.Location = new System.Drawing.Point(570, 433);
            this.metroBtnNext.Name = "metroBtnNext";
            this.metroBtnNext.Size = new System.Drawing.Size(75, 23);
            this.metroBtnNext.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnNext.StyleManager = null;
            this.metroBtnNext.TabIndex = 6;
            this.metroBtnNext.Text = "下一题(&N)";
            this.metroBtnNext.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // metroBtnPre
            // 
            this.metroBtnPre.Highlight = false;
            this.metroBtnPre.Location = new System.Drawing.Point(488, 434);
            this.metroBtnPre.Name = "metroBtnPre";
            this.metroBtnPre.Size = new System.Drawing.Size(75, 23);
            this.metroBtnPre.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnPre.StyleManager = null;
            this.metroBtnPre.TabIndex = 5;
            this.metroBtnPre.Text = "上一题(&P)";
            this.metroBtnPre.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // metroBtnStart
            // 
            this.metroBtnStart.Highlight = false;
            this.metroBtnStart.Location = new System.Drawing.Point(6, 434);
            this.metroBtnStart.Name = "metroBtnStart";
            this.metroBtnStart.Size = new System.Drawing.Size(75, 23);
            this.metroBtnStart.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnStart.StyleManager = null;
            this.metroBtnStart.TabIndex = 4;
            this.metroBtnStart.Text = "开始(&S)";
            this.metroBtnStart.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "A 已完成";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 395);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "B 未完成";
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
            this.groupBox2.Controls.Add(this.metroTile2);
            this.groupBox2.Controls.Add(this.metroTile1);
            this.groupBox2.Controls.Add(this.PieChart1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(702, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 469);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.Location = new System.Drawing.Point(59, 340);
            this.metroTile2.MainText = "";
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(96, 23);
            this.metroTile2.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTile2.StyleManager = null;
            this.metroTile2.TabIndex = 6;
            this.metroTile2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile2.TileCount = 0;
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(59, 391);
            this.metroTile1.MainText = "";
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(96, 23);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTile1.StyleManager = null;
            this.metroTile1.TabIndex = 5;
            this.metroTile1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile1.TileCount = 0;
            // 
            // PieChart1
            // 
            this.PieChart1.Location = new System.Drawing.Point(7, 18);
            this.PieChart1.Name = "PieChart1";
            this.PieChart1.Radius = 200F;
            this.PieChart1.Size = new System.Drawing.Size(259, 214);
            this.PieChart1.TabIndex = 4;
            this.PieChart1.Text = "pieChart1";
            this.PieChart1.Thickness = 10F;
            // 
            // metroButton1
            // 
            this.metroButton1.Highlight = false;
            this.metroButton1.Location = new System.Drawing.Point(585, 69);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(90, 30);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Lime;
            this.metroButton1.StyleManager = null;
            this.metroButton1.TabIndex = 6;
            this.metroButton1.Text = "显示座位";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
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
            // frmRTTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 652);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBoxQuestion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmRTTest";
            this.ShowInTaskbar = false;
            this.Text = "课堂测验";
            this.groupBoxQuestion.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxQuestion;
        //private System.Drawing.PieChart.PieChartControl m_panelDrawing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private HtmlEditor.Editor editor1;
        private Nexus.Windows.Forms.PieChart PieChart1;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroButton metroBtnStart;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton metroBtnNext;
        private MetroFramework.Controls.MetroButton metroBtnPre;
    }
}