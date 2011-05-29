using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChaitClient
{
    public partial class GroupForm : Form
    {
        public GroupForm()
        {
            InitializeComponent();
        }

        // 安全访问控件
        private delegate void delUpdateChatContent(String str);
        private void updateChatContent(String str)
        {
            tb_groupHistory.AppendText(str + "\n");
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            ChaitProtocol.Client.ChaitClient.Instance.Yell(this.Text, tb_chatStr.Text);
        }

        private void btn_updateMembers_Click(object sender, EventArgs e)
        {
            ChaitProtocol.Client.ChaitClient.Instance.GroupList();
        }

        public void ShowChatMsg(String msg)
        {
            Invoke(new delUpdateChatContent(updateChatContent), msg);
        }

        public void SwitchMember(String neckname)
        {
            if (lsb_members.Items.Contains(neckname))
            {
                lsb_members.Items.Remove(neckname);
            }
            else
            {
                lsb_members.Items.Add(neckname);
            }
        }
    }
}
