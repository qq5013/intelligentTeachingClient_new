using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Carbinet
{
    public partial class frmQuestionMng : Form
    {
        #region 成员

        //关于题目的播放顺序
        //如果新增题目，那就用当前题目的数目+1作为顺序号
        //如果调整题目顺序，则要更改当前题目和影响到的题目
        frmQuestionMngCtl ctl = new frmQuestionMngCtl();
        DataTable _dt = new DataTable();
        string question_relative_path = "questions";
        int Question_index
        {
            get { return this._dt.Rows.Count + 1; }
        }
        #endregion
        #region 初始化


        public frmQuestionMng()
        {
            InitializeComponent();


            TreeNode mainNode = new TreeNode();
            mainNode.Name = "mainNode";
            mainNode.Text = "题目列表";
            this.treeView1.Nodes.Add(mainNode);
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);

            this.cmbAnswer.Items.Clear();
            this.cmbAnswer.Items.Add("A");
            this.cmbAnswer.Items.Add("B");
            this.cmbAnswer.Items.Add("C");
            this.cmbAnswer.Items.Add("D");
            this.cmbAnswer.SelectedIndex = 0;

            _dt.Columns.Add("question_id", typeof(string));
            _dt.Columns.Add("caption", typeof(string));
            _dt.Columns.Add("answer", typeof(string));
            _dt.Columns.Add("question_index", typeof(string));//播放次序
            //标识是在增加题目还是在编辑题目
            //state值为true表明在编辑
            _dt.Columns.Add("state", typeof(string));

            this.refreshQuestionInfo();

        }

        void refreshQuestionInfo()
        {
            this.lblOrder.Text = string.Empty;
            this.txtQuestionName.Text = string.Empty;
            this.editor1.BodyHtml = string.Empty;
            this._dt.Rows.Clear();
            treeView1.Nodes[0].Nodes.Clear();
            DataTable dtTemp = frmQuestionMngCtl.getAllQuestion();
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    DataRow dr = dtTemp.Rows[i];
                    _dt.Rows.Add(dr["question_id"], dr["caption"], dr["answer"], dr["question_index"], "false");
                    TreeNode nod = new TreeNode();
                    nod.Name = dr["question_id"].ToString();
                    nod.Text = dr["caption"].ToString();

                    treeView1.Nodes[0].Nodes.Add(nod);

                    ////treeView1.SelectedNode = nod;
                    //nod.BackColor = Color.Gray;
                }
            }
            treeView1.Nodes[0].ExpandAll();

        }
        #endregion



        #region 事件处理
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //删除数据库的数据即可
            TreeNode selected_node = this.treeView1.SelectedNode;
            if (selected_node.Name != "mainNode")
            {
                string question_id = selected_node.Name;
                //载入题目
                DataRow[] rows = _dt.Select("question_id = '" + question_id + "'");
                if (rows.Length > 0)
                {
                    _dt.Rows.Remove(rows[0]);
                    frmQuestionMngCtl.deleteQuestion(question_id);
                    this.refreshQuestionInfo();
                }
            }
        }
        private void btnNewQuestion_Click(object sender, EventArgs e)
        {
            //将编辑状态置为新增题目状态，这通过将表里的state全置为false实现
            foreach (DataRow dr in this._dt.Rows)
            {
                dr["state"] = "false";
            }
            this.txtQuestionName.Text = string.Empty;
            this.editor1.BodyHtml = string.Empty;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //如果题目没有设置名称，通过截取部分内容生成一个
            if (this.txtQuestionName.Text == string.Empty)
            {
                string text = this.editor1.BodyText;
                if (text == string.Empty)
                {
                    MessageBox.Show("缺少题目内容，请补充完整后保存！", "信息提示", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    string[] strs = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if (strs.Length > 0)
                    {
                        this.txtQuestionName.Text = strs[0];
                    }
                    else
                    {
                        MessageBox.Show("缺少题目标题，请补充完整后保存！", "信息提示", MessageBoxButtons.OK);
                        return;
                    }

                }
            }

            string question_id = string.Empty;
            question_id = this.check_current_edit_state();
            Debug.WriteLine("question_id => " + question_id);
            if (question_id == string.Empty)//说明在新增题目状态
            {
                //将题目生成html文件，文件的名称为用户名称+时间后缀
                //生成的html文件保存在以同样名称名称创建的文件夹中，如果有图片或者其它资源，都保存在该文件夹中
                question_id = "user_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                if (this.create_question_files(question_id))
                {
                    //处在增加题目的状态，当前的状态也变为编辑状态
                    _dt.Rows.Add(question_id, this.txtQuestionName.Text, this.cmbAnswer.Text, this.Question_index.ToString(), "true");
                    frmQuestionMngCtl.AddQuestion(question_id, this.txtQuestionName.Text, this.cmbAnswer.Text, this.Question_index.ToString());

                    TreeNode nod = new TreeNode();
                    nod.Name = question_id;
                    nod.Text = this.txtQuestionName.Text;

                    //TreeNode[] tn = treeView1.Nodes.Find("mainNode", true);
                    //for (int i = 0; i < tn.Length; i++)
                    //{
                    //    treeView1.SelectedNode = tn[i];
                    //}
                    //将新题目添加到树形列表中
                    treeView1.Nodes[0].Nodes.Add(nod);
                    treeView1.Nodes[0].ExpandAll();
                    treeView1.SelectedNode = nod;
                    //nod.BackColor = Color.Blue;
                }

            }
            else
            {
                if (this.create_question_files(question_id))
                {
                    DataRow[] rows = _dt.Select("question_id = '" + question_id + "'");
                    if (rows.Length > 0)
                    {
                        DataRow dr = rows[0];
                        frmQuestionMngCtl.updateQuestion(
                            question_id, this.txtQuestionName.Text, this.cmbAnswer.Text, (string)dr["question_index"]);
                        this.refreshQuestionInfo();
                    }
                }
            }



            //将信息保存到数据库

            //this.refreshQuestionInfo();
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            // 把当前题目的顺序值-1，而其上的题目的顺序值+1
            TreeNode selected_node = this.treeView1.SelectedNode;
            if (selected_node.Name != "mainNode")
            {
                string question_id = selected_node.Name;
                //载入题目
                DataRow[] rows = _dt.Select("question_id = '" + question_id + "'");
                if (rows.Length > 0)
                {
                    int order = int.Parse(rows[0]["question_index"].ToString());//当前的顺序
                    int order_minus_1 = order - 1;//要变成的顺序
                    //查找当前为该顺序的题目，将其顺序值变为order
                    DataRow[] rows_source = _dt.Select("question_index = '" + order_minus_1.ToString() + "'");
                    if (rows_source.Length > 0)
                    {
                        rows_source[0]["question_index"] = order.ToString();
                        //将当前题目的顺序提升
                        rows[0]["question_index"] = order_minus_1.ToString();
                        //保存到数据库
                        frmQuestionMngCtl.updateQuestion(
                                    question_id,
                                     rows[0]["caption"].ToString(),
                                     rows[0]["answer"].ToString(),
                                     rows[0]["question_index"].ToString());

                        frmQuestionMngCtl.updateQuestion(
                                    rows_source[0]["question_id"].ToString(),
                                     rows_source[0]["caption"].ToString(),
                                     rows_source[0]["answer"].ToString(),
                                     rows_source[0]["question_index"].ToString());
                        //刷新
                        this.refreshQuestionInfo();
                    }

                }

            }
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            // 把当前题目的顺序值+1，而其下的题目的顺序值-1
            TreeNode selected_node = this.treeView1.SelectedNode;
            if (selected_node.Name != "mainNode")
            {
                string question_id = selected_node.Name;
                //载入题目
                DataRow[] rows = _dt.Select("question_id = '" + question_id + "'");
                if (rows.Length > 0)
                {
                    int order = int.Parse(rows[0]["question_index"].ToString());//当前的顺序
                    int order_add_1 = order + 1;//要变成的顺序
                    //查找当前为该顺序的题目，将其顺序值变为order
                    DataRow[] rows_source = _dt.Select("question_index = '" + order_add_1.ToString() + "'");
                    if (rows_source.Length > 0)
                    {
                        rows_source[0]["question_index"] = order.ToString();
                        //将当前题目的顺序提升
                        rows[0]["question_index"] = order_add_1.ToString();
                        //保存到数据库
                        frmQuestionMngCtl.updateQuestion(
                                    question_id,
                                     rows[0]["caption"].ToString(),
                                     rows[0]["answer"].ToString(),
                                     rows[0]["question_index"].ToString());

                        frmQuestionMngCtl.updateQuestion(
                                    rows_source[0]["question_id"].ToString(),
                                     rows_source[0]["caption"].ToString(),
                                     rows_source[0]["answer"].ToString(),
                                     rows_source[0]["question_index"].ToString());
                        //刷新
                        this.refreshQuestionInfo();
                    }

                }

            }
        }
        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected_node = this.treeView1.SelectedNode;
            if (selected_node.Name != "mainNode")
            {
                string question_id = selected_node.Name;
                string question_caption = selected_node.Text;
                //载入题目
                this.txtQuestionName.Text = question_caption;
                DataRow[] rows = _dt.Select("question_id = '" + question_id + "'");
                if (rows.Length > 0)
                {
                    int order = int.Parse(rows[0]["question_index"].ToString());
                    if (order <= 1)
                    {
                        this.btnUP.Enabled = false;
                    }
                    else
                    {
                        this.btnUP.Enabled = true;
                    }
                    if (order >= _dt.Rows.Count)
                    {
                        this.btnDown.Enabled = false;
                    }
                    else
                    {
                        this.btnDown.Enabled = true;
                    }
                    this.lblOrder.Text = rows[0]["question_index"].ToString();
                }
                string question_id_temp = string.Empty;
                question_id_temp = this.check_current_edit_state();
                if (question_id_temp != string.Empty)
                {
                    this.change_question_state(question_id_temp, false);
                }
                this.change_question_state(question_id, true);
                string html = this.GetHtmlFile(question_id);
                this.editor1.Clear();
                if (html != string.Empty)
                {
                    this.editor1.DocumentText = html;
                }
            }
        }
        private void cmbAnswer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.checkAddCondition() == false)
            {
                return;
            }
            this.btnSave.Enabled = true;
        }
        #endregion


        #region 内部函数
        //检查当前编辑状态，是新题目还是就题目
        string check_current_edit_state()
        {
            string strR = string.Empty;
            DataRow[] rows = _dt.Select("state = 'true'");
            if (rows.Length > 0)
            {
                strR = rows[0]["question_id"].ToString();
            }
            return strR;
        }
        void change_question_state(string _question_id, bool _state)
        {
            Debug.WriteLine(string.Format("question_id => {0}  value => {1}", _question_id, _state.ToString()));
            DataRow[] rows = _dt.Select("question_id = '" + _question_id + "'");
            if (rows.Length > 0)
            {
                if (_state)
                {
                    rows[0]["state"] = "true";
                }
                else
                {
                    rows[0]["state"] = "false";
                }
            }
        }
        private void ClearBackColor()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                ClearRecursive(n);
            }
        }
        // called by ClearBackColor function
        private void ClearRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.BackColor = Color.White;
                ClearRecursive(tn);
            }
        }
        bool create_question_files(string question_id)
        {
            string path = this.question_relative_path;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                    return false;
                }
            }
            //如果之前建立过，先将其删除
            path = path + "\\" + question_id;
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                    return false;
                }
            }
            //重新建立文件夹
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                return false;
            }
            return this.SaveFile(path + "\\" + question_id + ".html");
        }


        private bool SaveFile(string filename)
        {
            // 这里把html文档和图片保存在同一个文件夹中
            try
            {
                using (StreamWriter writer = File.CreateText(filename))
                {
                    string body = this.editor1.DocumentText;
                    //获取所有涉及到的图片
                    string[] images = editor1.Images;
                    if (images.Length != 0)
                    {
                        for (int i = 0, count = images.Length; i < count; ++i)
                        {
                            string image = images[i];

                            if (image.Trim() == "")

                                if (!image.StartsWith("file"))
                                {
                                    continue;
                                }

                            string image_path = Path.GetFullPath(image.Replace("%20", " ").Replace("file:///", ""));
                            //为图片设置一个单独的名称
                            string cid = string.Format("image_{0:00}", i);
                            string image_ext = Path.GetExtension(image_path);
                            string new_image_name = cid + image_ext;
                            //放置图片和html文件的文件夹
                            string file_path = Path.GetDirectoryName(filename);
                            try
                            {
                                File.Copy(image_path, file_path + "\\" + new_image_name);
                            }
                            catch
                            {
                                MessageBox.Show("复制文件中引用的图片是出现异常", "信息提示", MessageBoxButtons.OK);
                                return false;
                            }
                            //网页中的图片在播放的时候必须使用绝对路径，这里使用一个标记以为之后替换路径做准备
                            body = body.Replace(image_path, "image_path:\\" + new_image_name);
                        }
                    }

                    writer.Write(body);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }


        private string GetHtmlFile(string filename)
        {
            string path = Application.StartupPath + "\\" + this.question_relative_path + "\\" + filename + "\\" + filename + ".html";

            string html = string.Empty;
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                    string file_path = Path.GetDirectoryName(path);
                    html = html.Replace("image_path:", "file://" + file_path);
                }
            }
            return html;
        }
        bool checkAddCondition()
        {
            if (this.cmbAnswer.SelectedIndex == -1)
            {
                this.btnSave.Enabled = false;
                return false;
            }
            return true;
        }
        #endregion





    }
}
