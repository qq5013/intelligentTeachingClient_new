namespace Carbinet
{
    partial class frmEquipmentConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEquipmentConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numLocofGroup = new System.Windows.Forms.NumericUpDown();
            this.txtEquipmentID = new System.Windows.Forms.TextBox();
            this.numLocofRow = new System.Windows.Forms.NumericUpDown();
            this.numLocofColumn = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numCountofColumn = new System.Windows.Forms.NumericUpDown();
            this.numCountofRow = new System.Windows.Forms.NumericUpDown();
            this.cmbSelectedRow = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numCountofGroup = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowEquipMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLocofGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocofRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocofColumn)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountofColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountofRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountofGroup)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "所在排：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "所在行：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "所在列：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "设备编号：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 26);
            this.button1.TabIndex = 3;
            this.button1.Text = "分配";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numLocofGroup
            // 
            this.numLocofGroup.Location = new System.Drawing.Point(88, 26);
            this.numLocofGroup.Name = "numLocofGroup";
            this.numLocofGroup.ReadOnly = true;
            this.numLocofGroup.Size = new System.Drawing.Size(130, 21);
            this.numLocofGroup.TabIndex = 4;
            // 
            // txtEquipmentID
            // 
            this.txtEquipmentID.Location = new System.Drawing.Point(22, 175);
            this.txtEquipmentID.Name = "txtEquipmentID";
            this.txtEquipmentID.Size = new System.Drawing.Size(196, 21);
            this.txtEquipmentID.TabIndex = 2;
            // 
            // numLocofRow
            // 
            this.numLocofRow.Location = new System.Drawing.Point(88, 61);
            this.numLocofRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLocofRow.Name = "numLocofRow";
            this.numLocofRow.ReadOnly = true;
            this.numLocofRow.Size = new System.Drawing.Size(130, 21);
            this.numLocofRow.TabIndex = 4;
            this.numLocofRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numLocofColumn
            // 
            this.numLocofColumn.Location = new System.Drawing.Point(88, 95);
            this.numLocofColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLocofColumn.Name = "numLocofColumn";
            this.numLocofColumn.ReadOnly = true;
            this.numLocofColumn.Size = new System.Drawing.Size(130, 21);
            this.numLocofColumn.TabIndex = 4;
            this.numLocofColumn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numCountofColumn);
            this.groupBox1.Controls.Add(this.numCountofRow);
            this.groupBox1.Controls.Add(this.cmbSelectedRow);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numCountofGroup);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(13, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 191);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置每排参数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "排：";
            // 
            // numCountofColumn
            // 
            this.numCountofColumn.Location = new System.Drawing.Point(78, 144);
            this.numCountofColumn.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numCountofColumn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCountofColumn.Name = "numCountofColumn";
            this.numCountofColumn.Size = new System.Drawing.Size(140, 21);
            this.numCountofColumn.TabIndex = 1;
            this.numCountofColumn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCountofRow
            // 
            this.numCountofRow.Location = new System.Drawing.Point(78, 109);
            this.numCountofRow.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCountofRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCountofRow.Name = "numCountofRow";
            this.numCountofRow.Size = new System.Drawing.Size(140, 21);
            this.numCountofRow.TabIndex = 1;
            this.numCountofRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbSelectedRow
            // 
            this.cmbSelectedRow.FormattingEnabled = true;
            this.cmbSelectedRow.Location = new System.Drawing.Point(78, 76);
            this.cmbSelectedRow.Name = "cmbSelectedRow";
            this.cmbSelectedRow.Size = new System.Drawing.Size(140, 20);
            this.cmbSelectedRow.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "列数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "行数：";
            // 
            // numCountofGroup
            // 
            this.numCountofGroup.Location = new System.Drawing.Point(78, 34);
            this.numCountofGroup.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numCountofGroup.Name = "numCountofGroup";
            this.numCountofGroup.Size = new System.Drawing.Size(140, 21);
            this.numCountofGroup.TabIndex = 1;
            this.numCountofGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "排数：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnShowEquipMap);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numLocofColumn);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numLocofRow);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numLocofGroup);
            this.groupBox2.Controls.Add(this.txtEquipmentID);
            this.groupBox2.Location = new System.Drawing.Point(13, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 483);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置学生端设备位置";
            // 
            // btnShowEquipMap
            // 
            this.btnShowEquipMap.Location = new System.Drawing.Point(88, 252);
            this.btnShowEquipMap.Name = "btnShowEquipMap";
            this.btnShowEquipMap.Size = new System.Drawing.Size(130, 26);
            this.btnShowEquipMap.TabIndex = 5;
            this.btnShowEquipMap.Text = "显示设备映射";
            this.btnShowEquipMap.UseVisualStyleBackColor = true;
            this.btnShowEquipMap.Click += new System.EventHandler(this.btnShowEquipMap_Click);
            // 
            // frmEquipmentConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 788);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEquipmentConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "教室配置";
            ((System.ComponentModel.ISupportInitialize)(this.numLocofGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocofRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLocofColumn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountofColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountofRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountofGroup)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numLocofGroup;
        private System.Windows.Forms.TextBox txtEquipmentID;
        private System.Windows.Forms.NumericUpDown numLocofRow;
        private System.Windows.Forms.NumericUpDown numLocofColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numCountofColumn;
        private System.Windows.Forms.NumericUpDown numCountofRow;
        private System.Windows.Forms.NumericUpDown numCountofGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbSelectedRow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnShowEquipMap;
    }
}