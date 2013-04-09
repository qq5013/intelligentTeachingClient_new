using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using IntelligentTeaching;

namespace IntelligentTeachingClient
{
    public partial class main_form : Form
    {
        List<SGSClient> client_list = new List<SGSClient>();


        public main_form()
        {
            InitializeComponent();
            this.button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check_input())
            {
                int client_index = (int)this.numericUpDown1.Value;
                for (int i = 1; i <= client_index; i++)
                {
                    string rfid = "stu" + i.ToString("D6");
                    string equip = "equip" + i.ToString("D6");

                    SGSClient sc = new SGSClient(rfid, equip);
                    this.client_list.Add(sc);
                }

                foreach (SGSClient s in this.client_list)
                {
                    s.Show();
                }

                this.button1.Enabled = false;
                this.button2.Enabled = true;
            }
        }
        bool check_input()
        {
            bool bR = false;
            string strIP = this.txtIP.Text;
            try
            {
                IPAddress ip = IPAddress.Parse(strIP);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            string strPort = this.txtPort.Text;
            try
            {
                int i = int.Parse(strPort);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            bR = true;
            GlobalPara.dest_IP = this.txtIP.Text;
            GlobalPara.dest_port = this.txtPort.Text;
            return bR;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (SGSClient s in this.client_list)
            {
                s.Close();
            }
            this.client_list.Clear();
            this.button1.Enabled = true;
            this.button2.Enabled = false;
        }
    }
}
