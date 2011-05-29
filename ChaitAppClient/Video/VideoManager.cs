using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Timers;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ChaitAppClient.Video
{
    /// <summary>
    /// 负责多个视频聊天回话的管理
    /// </summary>
    public class VideoManager
    {
        // 端口号
        int usePort = 6000;

        // 视频设备接口
        AvicapManager capMgr;

        // 捕获发送计时器
        System.Timers.Timer capTimer;

        // 视频角色镜像
        public List<VideoMirror> videoList = new List<VideoMirror>();

        // 初始化捕获设备
        public void InitCapture(IntPtr handle, int width, int height)
        {
            capTimer = new System.Timers.Timer(50);
            capTimer.Elapsed += new ElapsedEventHandler(capTimer_Elapsed);
            capTimer.Start();
            capMgr = new AvicapManager(handle, width, height);
        }

        // ==============================================
        // 发送方和接受方采用相同的视频启动方式
        //  发送方：收到视频请求接受信息的时候
        //  接收方：发送出视频请求接受信息之后
        //
        // 这个时候双方都已经拥有自己的：
        //  聊天工具IP 视频端口 私聊窗体中的视频接收回调
        // 并且获得了对方的：
        //  聊天工具IP 视频端口
        // ==============================================
        public void BeginSession(String otherNeck, IPAddress otherIP, int otherSendPort, int otherRecvPort)
        {
            foreach (VideoMirror vm in videoList)
            {
                if (otherNeck == vm.OtherNeck)
                {
                    vm.otherSendEP = new IPEndPoint(otherIP, otherSendPort);
                    vm.otherRecvEP = new IPEndPoint(otherIP, otherRecvPort);
                    vm.Begin();
                    return;
                }
            }
            throw new Exception(String.Format("和{0}之间的视频会话不存在，请尝试重新连接", otherNeck));
        }

        public void BeginSession(String otherNeck)
        {
            foreach (VideoMirror vm in videoList)
            {
                if (otherNeck == vm.OtherNeck)
                {
                    vm.Begin();
                    return;
                }
            }

            throw new Exception(String.Format("不存在和{0}之间的视频会话，请重新建立视频连接", otherNeck));
        }

        #region old
        // 以视频发起方初始化
        public void InitVideoAsRequest(String tgtNeck,int thisSendPort, int thisRecvPort, VideoMirror.OnFrameReceivedHandler onFRH)
        {
            foreach (VideoMirror vs in videoList)
            {
                if (tgtNeck == vs.OtherNeck)
                {
                    throw new Exception(String.Format("视频连接重复！已经存在一个和{0}之间的视频回话", tgtNeck));
                }
            }

            VideoMirror newSource = new VideoMirror(tgtNeck);
            newSource.thisSendEP = new IPEndPoint(ChaitClient.Instance.LocalIP, thisSendPort);
            newSource.thisRecvEP = new IPEndPoint(ChaitClient.Instance.LocalIP, thisRecvPort);
            newSource.OnFrameReceivedEvent += onFRH;

            videoList.Add(newSource);
        }

        // 以视频接收方初始化
        public void InitVideoAsResponse(String otherNeck, 
                                        int thisSendPort, int thisRecvPort,
                                        IPAddress otherIP,
                                        int otherSendPort, int otherRecvPort, 
                                        VideoMirror.OnFrameReceivedHandler srcEP)
        {
            foreach (VideoMirror vr in videoList)
            {
                if (otherNeck == vr.OtherNeck)
                {
                    throw new Exception("视频连接重复！");
                }
            }

            VideoMirror newTarget = new VideoMirror(otherNeck);
            newTarget.thisSendEP = new IPEndPoint(ChaitClient.Instance.LocalIP, thisSendPort);
            newTarget.thisRecvEP = new IPEndPoint(ChaitClient.Instance.LocalIP, thisRecvPort);
            newTarget.otherSendEP = new IPEndPoint(otherIP, otherSendPort);
            newTarget.otherRecvEP = new IPEndPoint(otherIP, otherRecvPort);
            newTarget.OnFrameReceivedEvent += srcEP;

            videoList.Add(newTarget);
        }
        #endregion

        // 在视频请求被拒绝时清理视频模块
        public void Clear(String targetNeck)
        {
            for (int i = 0; i < videoList.Count; i++)
            {
                if (videoList[i].OtherNeck == targetNeck)
                {
                    videoList[i].Clear();
                    videoList.RemoveAt(i);
                }
            }
        }

        // 停止接收
        public void StopReceive(String otherNeck)
        {
            for(int i =0;i<videoList.Count; i++)
            {
                if (videoList[i].OtherNeck == otherNeck)
                {
                    videoList[i].Stop();
                    videoList.RemoveAt(i); 
                    return;
                }
            }
        }

        // 停止发送
        public void StopSend(String receiverNeck)
        {
            for (int i = 0; i < videoList.Count; i++)
            {
                if (videoList[i].OtherNeck == receiverNeck)
                {
                    videoList[i].Stop();
                    videoList.RemoveAt(i);
                    return;
                }
            }
        }

        private void capTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // ++++++++++++++++++++++++++++++++++++++++++++
            // 这里调用GrabImage无法直接获取图像
            // 因此使用外部窗体传入图像的方式
            // Bitmap frame = capMgr.GrabImage();
            // ++++++++++++++++++++++++++++++++++++++++++++
            Bitmap frame = ChaitClient.Instance.VFrame;
            if (frame == null) return;          // TODO 临时
            // 获取缩略图
            frame = (Bitmap)frame.GetThumbnailImage(80, 80, null, IntPtr.Zero);
            MemoryStream ms = new MemoryStream();
            frame.Save(ms, ImageFormat.Jpeg);    // TODO 为什么是Jpeg
            byte[] data = ms.GetBuffer();
            foreach (VideoMirror vs in videoList)
            {
                if (vs.IsReady)
                {
                    vs.SendData(data);
                }
            }
        }

        // 获取下一个端口 
        // TODO 增加空闲端口扫描功能
        public int GetNextPort()
        {
            return usePort++;
        }

        // 测试用
        public Bitmap GrabImage()
        {
            try
            {
                return capMgr.GrabImage();
            }
            catch
            {
                return null;
            }
        }
    }
}
