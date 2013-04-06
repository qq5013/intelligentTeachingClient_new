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
            this.editor1 = new HtmlEditor.Editor();
            this.metroBtnNext = new MetroFramework.Controls.MetroButton();
            this.metroBtnPre = new MetroFramework.Controls.MetroButton();
            this.metroBtnStart = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.groupBoxQuestion.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxQuestion
            // 
            this.groupBoxQuestion.Controls.Add(this.editor1);
            this.groupBoxQuestion.Location = new System.Drawing.Point(23, 63);
            this.groupBoxQuestion.Name = "groupBoxQuestion";
            this.groupBoxQuestion.Size = new System.Drawing.Size(827, 520);
            this.groupBoxQuestion.TabIndex = 0;
            this.groupBoxQuestion.TabStop = false;
            this.groupBoxQuestion.Text = "题目信息";
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
            this.editor1.Size = new System.Drawing.Size(821, 411);
            this.editor1.TabIndex = 3;
            this.editor1.TabStop = false;
            // 
            // metroBtnNext
            // 
            this.metroBtnNext.Highlight = false;
            this.metroBtnNext.Location = new System.Drawing.Point(772, 589);
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
            this.metroBtnPre.Location = new System.Drawing.Point(690, 590);
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
            this.metroBtnStart.Location = new System.Drawing.Point(26, 589);
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
            this.label1.Location = new System.Drawing.Point(980, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "A 已完成";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(980, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "B 未完成";
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.Location = new System.Drawing.Point(878, 310);
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
            this.metroTile1.Location = new System.Drawing.Point(878, 361);
            this.metroTile1.MainText = "";
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(96, 23);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTile1.StyleManager = null;
            this.metroTile1.TabIndex = 5;
            this.metroTile1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile1.TileCount = 0;
            // 
            // frmRTTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 629);
            this.Controls.Add(this.metroBtnNext);
            this.Controls.Add(this.metroBtnPre);
            this.Controls.Add(this.metroTile2);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.metroBtnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxQuestion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRTTest";
            this.Resizable = false;
            this.Text = "课堂测验";
            this.groupBoxQuestion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxQuestion;
        //private System.Drawing.PieChart.PieChartControl m_panelDrawing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private HtmlEditor.Editor editor1;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroButton metroBtnStart;
        private MetroFramework.Controls.MetroButton metroBtnNext;
        private MetroFramework.Controls.MetroButton metroBtnPre;
    }
}