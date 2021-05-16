using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupServer.RecvRCInfo += SetupServer_RecvedRCInfoEventHandler;
            SetupServer.Start("127.0.0.1", 10200);
        }

        private void SetupServer_RecvedRCInfoEventHandler(object sender, RecvRCInfoEventArgs e)
        {
            tbox_controller_ip.Text = e.IPAddressStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = tbox_ip.Text;
            SetupClient.Setup(ip, 10200);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

        }

        private void tbox_ip_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
