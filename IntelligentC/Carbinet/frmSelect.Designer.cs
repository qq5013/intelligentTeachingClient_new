using System.Drawing;
namespace Carbinet
{
    partial class frmSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelect));
            this.btnQuit = new System.Windows.Forms.Button();
            this.groupPie = new System.Windows.Forms.GroupBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroTile4 = new MetroFramework.Controls.MetroTile();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.btnClearState = new System.Windows.Forms.Button();
            this.btnHideSeat = new System.Windows.Forms.Button();
            this.PieChart1 = new Nexus.Windows.Forms.PieChart();
            this.groupPie.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.BackgroundImage = global::Carbinet.Properties.Resources.PowerShut_Down;
            this.btnQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuit.FlatAppearance.BorderSize = 0;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Location = new System.Drawing.Point(171, 441);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(49, 49);
            this.btnQuit.TabIndex = 25;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupPie
            // 
            this.groupPie.Controls.Add(this.PieChart1);
            this.groupPie.Controls.Add(this.metroLabel4);
            this.groupPie.Controls.Add(this.metroLabel3);
            this.groupPie.Controls.Add(this.metroLabel2);
            this.groupPie.Controls.Add(this.metroLabel1);
            this.groupPie.Controls.Add(this.metroTile4);
            this.groupPie.Controls.Add(this.metroTile3);
            this.groupPie.Controls.Add(this.metroTile2);
            this.groupPie.Controls.Add(this.metroTile1);
            this.groupPie.Location = new System.Drawing.Point(1, 1);
            this.groupPie.Name = "groupPie";
            this.groupPie.Size = new System.Drawing.Size(243, 414);
            this.groupPie.TabIndex = 32;
            this.groupPie.TabStop = false;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.CustomBackground = false;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel4.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel4.Location = new System.Drawing.Point(114, 364);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(64, 19);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel4.StyleManager = null;
            this.metroLabel4.TabIndex = 29;
            this.metroLabel4.Text = "D 不选择";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel4.UseStyleColors = false;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.CustomBackground = false;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel3.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel3.Location = new System.Drawing.Point(114, 323);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(78, 19);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel3.StyleManager = null;
            this.metroLabel3.TabIndex = 28;
            this.metroLabel3.Text = "C 不知所云";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel3.UseStyleColors = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.CustomBackground = false;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel2.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel2.Location = new System.Drawing.Point(114, 282);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(77, 19);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel2.StyleManager = null;
            this.metroLabel2.TabIndex = 27;
            this.metroLabel2.Text = "B 一知半解";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseStyleColors = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.CustomBackground = false;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Medium;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Light;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Default;
            this.metroLabel1.Location = new System.Drawing.Point(114, 242);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(78, 19);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroLabel1.StyleManager = null;
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "A 完全明白";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel1.UseStyleColors = false;
            // 
            // metroTile4
            // 
            this.metroTile4.ActiveControl = null;
            this.metroTile4.Location = new System.Drawing.Point(18, 318);
            this.metroTile4.MainText = "";
            this.metroTile4.Name = "metroTile4";
            this.metroTile4.Size = new System.Drawing.Size(81, 28);
            this.metroTile4.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTile4.StyleManager = null;
            this.metroTile4.TabIndex = 25;
            this.metroTile4.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile4.TileCount = 0;
            // 
            // metroTile3
            // 
            this.metroTile3.ActiveControl = null;
            this.metroTile3.Location = new System.Drawing.Point(18, 236);
            this.metroTile3.MainText = "";
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(81, 28);
            this.metroTile3.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTile3.StyleManager = null;
            this.metroTile3.TabIndex = 24;
            this.metroTile3.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile3.TileCount = 0;
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.Location = new System.Drawing.Point(18, 359);
            this.metroTile2.MainText = "";
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(81, 28);
            this.metroTile2.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTile2.StyleManager = null;
            this.metroTile2.TabIndex = 23;
            this.metroTile2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile2.TileCount = 0;
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(18, 277);
            this.metroTile1.MainText = "";
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(81, 28);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroTile1.StyleManager = null;
            this.metroTile1.TabIndex = 22;
            this.metroTile1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTile1.TileCount = 0;
            // 
            // btnClearState
            // 
            this.btnClearState.BackgroundImage = global::Carbinet.Properties.Resources.PowerRestart;
            this.btnClearState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClearState.FlatAppearance.BorderSize = 0;
            this.btnClearState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearState.Location = new System.Drawing.Point(94, 441);
            this.btnClearState.Name = "btnClearState";
            this.btnClearState.Size = new System.Drawing.Size(49, 49);
            this.btnClearState.TabIndex = 33;
            this.btnClearState.UseVisualStyleBackColor = true;
            this.btnClearState.Click += new System.EventHandler(this.btnClearState_Click);
            // 
            // btnHideSeat
            // 
            this.btnHideSeat.BackgroundImage = global::Carbinet.Properties.Resources.form_expand;
            this.btnHideSeat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHideSeat.FlatAppearance.BorderSize = 0;
            this.btnHideSeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHideSeat.Location = new System.Drawing.Point(17, 441);
            this.btnHideSeat.Name = "btnHideSeat";
            this.btnHideSeat.Size = new System.Drawing.Size(49, 49);
            this.btnHideSeat.TabIndex = 34;
            this.btnHideSeat.UseVisualStyleBackColor = true;
            this.btnHideSeat.Click += new System.EventHandler(this.button2_Click);
            // 
            // PieChart1
            // 
            this.PieChart1.Location = new System.Drawing.Point(6, 20);
            this.PieChart1.Name = "PieChart1";
            this.PieChart1.Radius = 200F;
            this.PieChart1.Size = new System.Drawing.Size(231, 192);
            this.PieChart1.TabIndex = 30;
            this.PieChart1.Text = "pieChart1";
            this.PieChart1.Thickness = 10F;
            // 
            // frmSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(244, 498);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnClearState);
            this.Controls.Add(this.groupPie);
            this.Controls.Add(this.btnHideSeat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelect";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "课堂互动";
            this.TopMost = true;
            this.groupPie.ResumeLayout(false);
            this.groupPie.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Drawing.PieChart.PieChartControl m_panelDrawing;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.GroupBox groupPie;
        private System.Windows.Forms.Button btnClearState;
        private System.Windows.Forms.Button btnHideSeat;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroTile metroTile4;
        private MetroFramework.Controls.MetroTile metroTile3;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private Nexus.Windows.Forms.PieChart PieChart1;
    }
}

