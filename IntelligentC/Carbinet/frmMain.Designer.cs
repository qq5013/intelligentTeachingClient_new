namespace Carbinet
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.考勤ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动考勤ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.考勤统计AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.课堂互动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.即时互动RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.课堂测试TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.学生管理SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口设置CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.教师设置LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.题目管理QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.考勤 = new System.Windows.Forms.ToolStripButton();
            this.课堂互动 = new System.Windows.Forms.ToolStripButton();
            this.教室设置 = new System.Windows.Forms.ToolStripButton();
            this.考勤统计 = new System.Windows.Forms.ToolStripButton();
            this.串口设置 = new System.Windows.Forms.ToolStripButton();
            this.课堂测试 = new System.Windows.Forms.ToolStripButton();
            this.题目管理 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.考勤ToolStripMenuItem,
            this.课堂互动ToolStripMenuItem,
            this.设置TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 考勤ToolStripMenuItem
            // 
            this.考勤ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动考勤ToolStripMenuItem,
            this.考勤统计AToolStripMenuItem});
            this.考勤ToolStripMenuItem.Name = "考勤ToolStripMenuItem";
            this.考勤ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.考勤ToolStripMenuItem.Text = "考勤(&C)";
            // 
            // 启动考勤ToolStripMenuItem
            // 
            this.启动考勤ToolStripMenuItem.Name = "启动考勤ToolStripMenuItem";
            this.启动考勤ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.启动考勤ToolStripMenuItem.Text = "启动考勤(&S)";
            this.启动考勤ToolStripMenuItem.Click += new System.EventHandler(this.启动考勤ToolStripMenuItem_Click);
            // 
            // 考勤统计AToolStripMenuItem
            // 
            this.考勤统计AToolStripMenuItem.Name = "考勤统计AToolStripMenuItem";
            this.考勤统计AToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.考勤统计AToolStripMenuItem.Text = "考勤统计(&A)";
            this.考勤统计AToolStripMenuItem.Click += new System.EventHandler(this.考勤统计AToolStripMenuItem_Click);
            // 
            // 课堂互动ToolStripMenuItem
            // 
            this.课堂互动ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.即时互动RToolStripMenuItem,
            this.课堂测试TToolStripMenuItem});
            this.课堂互动ToolStripMenuItem.Name = "课堂互动ToolStripMenuItem";
            this.课堂互动ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.课堂互动ToolStripMenuItem.Text = "课堂互动(&I)";
            // 
            // 即时互动RToolStripMenuItem
            // 
            this.即时互动RToolStripMenuItem.Name = "即时互动RToolStripMenuItem";
            this.即时互动RToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.即时互动RToolStripMenuItem.Text = "即时互动(&R)";
            this.即时互动RToolStripMenuItem.Click += new System.EventHandler(this.即时互动RToolStripMenuItem_Click);
            // 
            // 课堂测试TToolStripMenuItem
            // 
            this.课堂测试TToolStripMenuItem.Name = "课堂测试TToolStripMenuItem";
            this.课堂测试TToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.课堂测试TToolStripMenuItem.Text = "课堂测试(&T)";
            this.课堂测试TToolStripMenuItem.Click += new System.EventHandler(this.课堂测试TToolStripMenuItem_Click);
            // 
            // 设置TToolStripMenuItem
            // 
            this.设置TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.学生管理SToolStripMenuItem,
            this.串口设置CToolStripMenuItem,
            this.教师设置LToolStripMenuItem,
            this.题目管理QToolStripMenuItem});
            this.设置TToolStripMenuItem.Name = "设置TToolStripMenuItem";
            this.设置TToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.设置TToolStripMenuItem.Text = "设置(&T)";
            // 
            // 学生管理SToolStripMenuItem
            // 
            this.学生管理SToolStripMenuItem.Name = "学生管理SToolStripMenuItem";
            this.学生管理SToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.学生管理SToolStripMenuItem.Text = "学生管理(&S)";
            this.学生管理SToolStripMenuItem.Click += new System.EventHandler(this.学生管理SToolStripMenuItem_Click);
            // 
            // 串口设置CToolStripMenuItem
            // 
            this.串口设置CToolStripMenuItem.Name = "串口设置CToolStripMenuItem";
            this.串口设置CToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.串口设置CToolStripMenuItem.Text = "串口设置(&C)";
            this.串口设置CToolStripMenuItem.Click += new System.EventHandler(this.串口设置CToolStripMenuItem_Click);
            // 
            // 教师设置LToolStripMenuItem
            // 
            this.教师设置LToolStripMenuItem.Name = "教师设置LToolStripMenuItem";
            this.教师设置LToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.教师设置LToolStripMenuItem.Text = "教室设置(&L)";
            this.教师设置LToolStripMenuItem.Click += new System.EventHandler(this.教师设置LToolStripMenuItem_Click);
            // 
            // 题目管理QToolStripMenuItem
            // 
            this.题目管理QToolStripMenuItem.Name = "题目管理QToolStripMenuItem";
            this.题目管理QToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.题目管理QToolStripMenuItem.Text = "题目管理(&Q)";
            this.题目管理QToolStripMenuItem.Click += new System.EventHandler(this.题目管理QToolStripMenuItem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.考勤,
            this.课堂互动,
            this.教室设置,
            this.考勤统计,
            this.串口设置,
            this.课堂测试,
            this.题目管理,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(764, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // 考勤
            // 
            this.考勤.Image = ((System.Drawing.Image)(resources.GetObject("考勤.Image")));
            this.考勤.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.考勤.Name = "考勤";
            this.考勤.Size = new System.Drawing.Size(52, 22);
            this.考勤.Text = "考勤";
            this.考勤.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // 课堂互动
            // 
            this.课堂互动.Image = ((System.Drawing.Image)(resources.GetObject("课堂互动.Image")));
            this.课堂互动.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.课堂互动.Name = "课堂互动";
            this.课堂互动.Size = new System.Drawing.Size(76, 22);
            this.课堂互动.Text = "课堂互动";
            this.课堂互动.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // 教室设置
            // 
            this.教室设置.Image = ((System.Drawing.Image)(resources.GetObject("教室设置.Image")));
            this.教室设置.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.教室设置.Name = "教室设置";
            this.教室设置.Size = new System.Drawing.Size(76, 22);
            this.教室设置.Text = "教室设置";
            this.教室设置.ToolTipText = "设置系统参数";
            this.教室设置.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // 考勤统计
            // 
            this.考勤统计.Image = ((System.Drawing.Image)(resources.GetObject("考勤统计.Image")));
            this.考勤统计.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.考勤统计.Name = "考勤统计";
            this.考勤统计.Size = new System.Drawing.Size(76, 22);
            this.考勤统计.Text = "考勤统计";
            this.考勤统计.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // 串口设置
            // 
            this.串口设置.Image = ((System.Drawing.Image)(resources.GetObject("串口设置.Image")));
            this.串口设置.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.串口设置.Name = "串口设置";
            this.串口设置.Size = new System.Drawing.Size(76, 22);
            this.串口设置.Text = "串口设置";
            this.串口设置.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // 课堂测试
            // 
            this.课堂测试.Image = ((System.Drawing.Image)(resources.GetObject("课堂测试.Image")));
            this.课堂测试.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.课堂测试.Name = "课堂测试";
            this.课堂测试.Size = new System.Drawing.Size(76, 22);
            this.课堂测试.Text = "课堂测试";
            this.课堂测试.Click += new System.EventHandler(this.课堂测试_Click);
            // 
            // 题目管理
            // 
            this.题目管理.Image = ((System.Drawing.Image)(resources.GetObject("题目管理.Image")));
            this.题目管理.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.题目管理.Name = "题目管理";
            this.题目管理.Size = new System.Drawing.Size(76, 22);
            this.题目管理.Text = "题目管理";
            this.题目管理.Click += new System.EventHandler(this.题目管理_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "学生管理";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 494);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能教室";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 考勤ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 课堂互动ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton 考勤;
        private System.Windows.Forms.ToolStripButton 教室设置;
        private System.Windows.Forms.ToolStripButton 课堂互动;
        private System.Windows.Forms.ToolStripButton 考勤统计;
        private System.Windows.Forms.ToolStripButton 串口设置;
        private System.Windows.Forms.ToolStripButton 课堂测试;
        private System.Windows.Forms.ToolStripButton 题目管理;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 设置TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 学生管理SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口设置CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 教师设置LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 题目管理QToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动考勤ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 即时互动RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 课堂测试TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 考勤统计AToolStripMenuItem;
    }
}