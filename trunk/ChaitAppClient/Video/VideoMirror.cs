using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ChaitAppClient.Video
{
    public class VideoMirror
    {
        // Variables
        public String OtherNeck { get; private set; }

        public IPEndPoint otherSendEP;
        public IPEndPoint otherRecvEP;

        public IPEndPoint thisSendEP;
        public IPEndPoint thisRecvEP;

        private UdpClient thisSendClient;
        private UdpClient thisRecvClient;

        public bool IsReady{get; private set;}

        private Thread recvThread;

        // 图像接收消息
        public delegate void OnFrameReceivedHandler(Bitmap frame);
        public event OnFrameReceivedHandler OnFrameReceivedEvent;

        public VideoMirror(String otherNeck)
        {
            IsReady = false;

            this.OtherNeck = otherNeck;
        }

        // 开始
        public void Begin()
        {
            // 创建UDPClient
            thisSendClient = new UdpClient(thisSendEP);
            thisRecvClient = new UdpClient(thisRecvEP);

            // 激活发送模式
            IsReady = true;         
            // thisSendClient.BeginReceive(onReceive, null);
            // 启动接收
            recvThread = new Thread(recvProc);
            recvThread.Start();
        }

        // 请求被拒绝时的清理
        public void Clear()
        {
            // 这里没有清理的必要，因为该对象将在VideoManager中被删除
            IsReady = false;
            if (thisSendClient != null)
            {
                thisSendClient.Close();
                thisSendClient = null;
            }
            if (thisRecvClient != null)
            {
                thisRecvClient.Close();
                thisRecvClient.Close();
            }
        }
        private void recvProc()
        {
            while (true)
            {
                byte[] recvBuffer = thisRecvClient.Receive(ref otherSendEP);
                Stream s = new MemoryStream(recvBuffer);
                Bitmap frame = new Bitmap(s);
                if (OnFrameReceivedEvent != null)
                    OnFrameReceivedEvent(frame);
            }
        }
        class UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }
        private void onReceive(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;
            byte[] receiveBytes = u.EndReceive(ar, ref e);
            if (e == otherSendEP)    // 过滤出有效的视频数据
            {
                Stream s = new MemoryStream(receiveBytes);
                // 获取单帧图片
                Bitmap frame = new Bitmap(s);
                if (OnFrameReceivedEvent != null)
                    OnFrameReceivedEvent(frame);
            }

            thisSendClient.BeginReceive(onReceive, null);
        }

        // SendFrame
        public void SendData(byte[] data)
        {
            if(thisSendClient != null)
                thisSendClient.Send(data, data.Length, otherRecvEP);
        }

        // 停止发送
        public void Stop()
        {
            thisSendClient.Close();
        }
    }
}
