using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntelligentTeachingClient
{
    public partial class frmLogin : MetroForm, I_new_msg_arrived
    {
        bool logining = false;

        public frmLogin()
        {
            InitializeComponent();
            this.lblStatus.Text = string.Empty;
            this.textBox1.Click += (sender, e) =>
            {
                this.textBox1.ForeColor = Color.Black;
                this.textBox1.Text = string.Empty;
            };
            this.btnLogin.Enabled = false;
            this.Shown += frmLogin_Shown;
        }

        void frmLogin_Shown(object sender, EventArgs e)
        {
            //开始收听广播
            UDPHelper.StartLogin_UDPServer(15000);
            UDPHelper.handler = this;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //向教师端发送登陆请求
            string id = this.textBox1.Text;
            if (id == null || id == string.Empty || id.Length <= 0)
            {
                MessageBox.Show("请填写您的学号！", "提示");
                return;
            }
            else
            {
                if (GlobalPara.dest_IP != string.Empty && GlobalPara.dest_login_port != string.Empty)
                {
                    GlobalPara.clientID = id;
                    string strLoginInfo = string.Format("[id,{0},epc,]", id);
                    UDPHelper.SendData(strLoginInfo, GlobalPara.dest_IP, int.Parse(GlobalPara.dest_login_port));
                    this.logining = true;//标明窗体处于登陆过程中，不要将登陆按钮重置
                    this.btnLogin.Enabled = false;
                    this.textBox1.Enabled = false;
                    this.lblStatus.Text = "登录中，请确认您已刷卡...";
                }
            }
        }

        public void handle_new_message_login()
        {

            Debug.WriteLine(" => handle_new_message_login");
            if (GlobalPara.client_epc != string.Empty)
            {
                UDPHelper.handler = null;
                //UDPHelper.stopped = true;
                Debug.WriteLine("登陆成功！ epc = " + GlobalPara.client_epc);
            }
            Action action = delegate()
            {
                this.Hide();
                SGSClient frm = new SGSClient(GlobalPara.client_epc, GlobalPara.equipmentID);
                frm.Show();
            };
            this.Invoke(action);

            //MessageBox.Show("登陆成功！");
        }

        public void handle_new_message_config()
        {
            Debug.WriteLine(" => handle_new_message_config");
            Action action = delegate()
            {
                this.txtIP.Text = GlobalPara.dest_IP;
                this.txtPort.Text = GlobalPara.dest_login_port;
                if (!logining)
                    this.btnLogin.Enabled = true;
            };
            this.Invoke(action);
        }
    }
}
