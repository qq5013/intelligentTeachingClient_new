using HtmlEditor;
namespace Carbinet
{
    partial class frmQuestionMng
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuestionMng));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.btnNewQuestion = new System.Windows.Forms.Button();
            this.txtQuestionName = new System.Windows.Forms.TextBox();
            this.lblOrder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.editor1 = new HtmlEditor.Editor();
            this.cmbAnswer = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUP);
            this.groupBox1.Controls.Add(this.btnNewQuestion);
            this.groupBox1.Controls.Add(this.txtQuestionName);
            this.groupBox1.Controls.Add(this.lblOrder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.editor1);
            this.groupBox1.Controls.Add(this.cmbAnswer);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(219, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(745, 506);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "题目编辑";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 476);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "无";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 476);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "正确率：";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(636, 465);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 28);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(678, 44);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(55, 23);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "下移(&N)";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUP
            // 
            this.btnUP.Location = new System.Drawing.Point(619, 44);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(55, 23);
            this.btnUP.TabIndex = 7;
            this.btnUP.Text = "上移(&P)";
            this.btnUP.UseVisualStyleBackColor = true;
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
            // 
            // btnNewQuestion
            // 
            this.btnNewQuestion.Location = new System.Drawing.Point(399, 465);
            this.btnNewQuestion.Name = "btnNewQuestion";
            this.btnNewQuestion.Size = new System.Drawing.Size(92, 28);
            this.btnNewQuestion.TabIndex = 6;
            this.btnNewQuestion.Text = "新建题目(&N)";
            this.btnNewQuestion.UseVisualStyleBackColor = true;
            this.btnNewQuestion.Click += new System.EventHandler(this.btnNewQuestion_Click);
            // 
            // txtQuestionName
            // 
            this.txtQuestionName.Location = new System.Drawing.Point(14, 45);
            this.txtQuestionName.Name = "txtQuestionName";
            this.txtQuestionName.Size = new System.Drawing.Size(512, 21);
            this.txtQuestionName.TabIndex = 5;
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrder.Location = new System.Drawing.Point(579, 46);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(37, 14);
            this.lblOrder.TabIndex = 4;
            this.lblOrder.Text = "次序";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(545, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "次序：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "名称：";
            // 
            // editor1
            // 
            this.editor1.BodyHtml = null;
            this.editor1.BodyText = null;
            this.editor1.DocumentText = resources.GetString("editor1.DocumentText");
            this.editor1.EditorBackColor = System.Drawing.Color.White;
            this.editor1.EditorForeColor = System.Drawing.Color.Black;
            this.editor1.FontName = null;
            this.editor1.FontSize = HtmlEditor.FontSize.NA;
            this.editor1.Location = new System.Drawing.Point(14, 106);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(717, 349);
            this.editor1.TabIndex = 3;
            // 
            // cmbAnswer
            // 
            this.cmbAnswer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnswer.FormattingEnabled = true;
            this.cmbAnswer.Location = new System.Drawing.Point(53, 471);
            this.cmbAnswer.Name = "cmbAnswer";
            this.cmbAnswer.Size = new System.Drawing.Size(135, 20);
            this.cmbAnswer.TabIndex = 2;
            this.cmbAnswer.SelectedIndexChanged += new System.EventHandler(this.cmbAnswer_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(518, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 474);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "答案：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "内容：";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(4, 24);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(209, 506);
            this.treeView1.TabIndex = 4;
            // 
            // frmQuestionMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 586);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuestionMng";
            this.Text = "课堂测验题目管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAnswer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private Editor editor1;
        private System.Windows.Forms.TextBox txtQuestionName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnNewQuestion;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}