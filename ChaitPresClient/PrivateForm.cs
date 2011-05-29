using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChaitAppClient;

namespace ChaitPresClient
{
    public partial class PrivateForm : Form
    {
        public PrivateForm()
        {
            InitializeComponent();

            btn_sendFile.Enabled = true;
            // btn_sendFile.Enabled = false;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            ChaitAppClient.ChaitClient.Instance.Chat(this.Text, tb_send.Text);
            tb_chatHistory.AppendText(ChaitClient.Instance.Neckname + "：" + tb_send.Text + "\n");
        }

        public void ShowChatMsg(String msg)
        {
            ExThreadUICtrl.AddTextRow(this, tb_chatHistory, msg);
        }

        private void btn_videoCmd_Click(object sender, EventArgs e)
        {
            if (btn_videoCmd.Text == "请求视频聊天")
            {
               ChaitClient.Instance.VideoRequest(this.Text, this.OnFrameReceivedHandler);
            }
            else if (btn_videoCmd.Text == "结束视频聊天")
            {
                ChaitClient.Instance.StopVideo(this.Text);
                btn_videoCmd.Text = "请求视频聊天";
            }
        }

        // 视频/非视频模式切换
        public void ToggleVideoMove(bool hasVideo)
        {
            if (hasVideo)
            {
                ExThreadUICtrl.SetEnabled(this, grb_video, true); 
                ExThreadUICtrl.SetText(this, btn_videoCmd, "结束视频聊天");
            }
            else
            {
                ExThreadUICtrl.SetEnabled(this, grb_video, false);
                ExThreadUICtrl.SetText(this, btn_videoCmd, "请求视频聊天");
            }
        }

        // 显示接收到的视频
        public void OnFrameReceivedHandler(Bitmap frame)
        {
            ExThreadUICtrl.SetPictureBoxImage(this, ptb_otherVideo, frame);
        }


        private void onFSSendingHandler(String receiverNeck, String fileName, String sourcePath, int bytesSended)
        {
            String str = "从" + receiverNeck + "发送文件" + fileName + "：" + bytesSended + "...";
            ExThreadUICtrl.SetText(this, sts_fileSend, str);

            Application.DoEvents(); // TODO 有什么作用?
        }
        private void onFSSendDoneHandler(String receiverNeck, String fileName, String sourcePath)
        {
            String str = "向" + receiverNeck + "发送文件" + fileName + "：完成";
            ExThreadUICtrl.SetText(this, sts_fileSend, str);

            Application.DoEvents(); // TODO 有什么作用?
        }
        private void onFSErrorHandler(String errorStr)
        {
            ExThreadUICtrl.ShowErrorBox(this, "文件发送系统错误");
        }
        private void onFRReceivingHandler(String senderNeck, String fileName, String savePath, int bytesReceiveed)
        {
            String str = "从" + senderNeck + "接收文件" + fileName + "：" + bytesReceiveed + "...";
            ExThreadUICtrl.SetText(this, sts_fileReceive, str);

            Application.DoEvents(); // TODO 有什么作用?
        }
        private void onFRReceiveDoneHandler(String senderNeck, String fileName, String savePath)
        {
            String str = "从" + senderNeck + "接收文件" + fileName + "：完成";
            ExThreadUICtrl.SetText(this, sts_fileReceive, str);

            Application.DoEvents(); // TODO 有什么作用?
        }
    }
}
