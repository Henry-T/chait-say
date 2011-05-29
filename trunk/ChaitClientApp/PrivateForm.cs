using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChaitClientApp
{
    public partial class PrivateForm : Form
    {
        public PrivateForm()
        {
            InitializeComponent();
        }

        // 安全访问控件
        private delegate void delUpdateChatContent(String str);
        private void updateChatContent(String str)
        {
            tb_chatHistory.AppendText(str + "\n");
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            ChaitProtocol.Client.ChaitClient.Instance.Chat(this.Text, tb_send.Text);
            tb_chatHistory.AppendText(ChaitProtocol.Client.ChaitClient.Instance.Neckname + "：" + tb_send.Text + "\n");
        }

        public void ShowChatMsg(String msg)
        {
            Invoke(new delUpdateChatContent(updateChatContent), msg);
        }
    }
}
