using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using ChaitAppServer;
using System.Net.Sockets;

namespace ChaitPresServer
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
            // 获取本机IP
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostAddresses(Environment.MachineName);
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    cbb_localIP.Items.Add(ips[i]);
                }
            }
            if(cbb_localIP.Items.Count != 0)
            {
                cbb_localIP.SelectedIndex = 0;
            }
        }

        // UI事件处理
        private void btn_start_Click(object sender, EventArgs e)
        {
            Button startBtn = (Button)sender;
            if (startBtn.Text == "启动")
            {
                // 检查IP和端口
                IPAddress localIP;
                if (!IPAddress.TryParse(cbb_localIP.Text.Trim(), out localIP))
                {
                    MessageBox.Show("请输入一个合法的服务器IP地址");
                    return;
                }
                int port;
                if (!int.TryParse(tb_port.Text.Trim(), out port))
                {
                    MessageBox.Show("请输入一个合法的服务器端口");
                    return;
                }
                ChaitServer.Instance.serverLogHandler = onServerLog;
                ChaitServer.Instance.Start(localIP, port);
                ChaitServer.Instance.onJoinHandler = RoleManager.Instance.OnJoinHandler;
                ChaitServer.Instance.onSetTargetHandler = RoleManager.Instance.OnSetTargetHandler;
                tb_log.AppendText("[系统]服务器已经启动 \n");
                tb_log.AppendText("[系统]聊天系统Ready \n");
                tb_log.AppendText("[系统]文件系统Ready \n");
                tb_log.AppendText("\n");
                
                startBtn.Text = "停止";

                // TODO 临时设定:创建默认组
                ChaitServer.Instance.Groups.Add("灵异八卦团", new List<string>());
                ChaitServer.Instance.Groups.Add("豆瓣党支部", new List<string>());
                ChaitServer.Instance.Groups.Add("偷菜群英会", new List<string>());
                ChaitServer.Instance.Groups.Add("毛毛一族", new List<string>());
                ChaitServer.Instance.Groups.Add("网络多媒体讨论", new List<string>());
                ChaitServer.Instance.Groups.Add("界面美化讨论", new List<string>()); // 保存文件
                ChaitServer.Instance.Groups.Add("软件功能讨论", new List<string>());
                ChaitServer.Instance.Groups.Add("妆吧的假面舞会", new List<string>());
                ChaitServer.Instance.Groups.Add("你看啥电影", new List<string>());
                ChaitServer.Instance.Groups.Add("这会儿脑残的进..呵呵", new List<string>());

            }
            else if (startBtn.Text == "停止")
            {
                ChaitServer.Instance.Stop();
                tb_log.AppendText("服务器已经停止 \n");
                startBtn.Text = "启动";
            }
        }

        // 安全访问控件委托
        private delegate void delAddLogHandler(String log);
        private void addLog(String log)
        {
            tb_log.AppendText(log + "\n");
        }

        // 协议事件委托
        public void onServerLog(String log)
        {
            Invoke(new delAddLogHandler(addLog), log);
        }

        private void tmr_update_Tick(object sender, EventArgs e)
        {
            RoleManager.Instance.Update();
        }
    }
}
