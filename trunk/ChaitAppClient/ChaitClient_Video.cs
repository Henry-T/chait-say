using System;
using System.Collections.Generic;
using ChaitAppProtocol;
using ChaitAppClient.Video;
using System.Net;
using System.Drawing;
using ServerApp;
namespace ChaitAppClient
{
    public partial class ChaitClient
    {
        // Variables
        public VideoManager VideoMgr;

        // ++++++++++++++++++++++++++++++++++++++++++
        // 临时
        public Bitmap VFrame;
        // ++++++++++++++++++++++++++++++++++++++++++

        public void InitVideo(IntPtr handle)
        {
            VideoMgr = new VideoManager();
            VideoMgr.InitCapture(handle, 100, 100);
        }

        #region 协议回调 - 视频
        // 视频请求事件
        public delegate void OnVideoRequestHandler(String neckname, String otherIP, int otherSendPort, int otherRecvPort);
        public event OnVideoRequestHandler OnVideoRequestEvent;
        // 接受请求事件
        public delegate void OnVideoAcceptHandler(String neckname, String otherIP, int otherSendPort, int otherRecvPort);
        public event OnVideoAcceptHandler OnVideoAcceptEvent;
        // 拒绝请求事件
        public delegate void OnVideoRefuseHandler(String neckname);
        public event OnVideoRefuseHandler OnVideoRefuseEvent;
        // 结束视频事件
        public delegate void OnVideoStopHandler(String neckname);
        public event OnVideoStopHandler OnVideoStopEvent;
        #endregion


        #region 客户端行为 - 视频
        public void VideoRequest(String targetNeck, VideoMirror.OnFrameReceivedHandler onFRH)
        {
            // 获取本机的发送接收端口
            int thisSendPort = NetResource.Instance.GetNextUDPPort();
            int thisRecvPort = NetResource.Instance.GetNextUDPPort();

            // 初始化视频
            VideoMgr.InitVideoAsRequest(targetNeck, thisSendPort, thisRecvPort, onFRH);

            // 发送请求消息
            String dataStr = System.Convert.ToChar(CProtocol.VideoRequest).ToString();
            dataStr += ((char)System.Text.Encoding.UTF8.GetBytes(targetNeck).Length).ToString();
            dataStr += targetNeck;
            dataStr += ((char)thisSendPort).ToString();
            dataStr += ((char)thisRecvPort).ToString();
            sendData(dataStr);
        }

        public void AcceptVideo(String otherNeck, IPAddress otherIP, int otherSendPort, int otherRecvPort, VideoMirror.OnFrameReceivedHandler onFRH)
        {
            // 视频收发准备
            int thisSendPort = NetResource.Instance.GetNextUDPPort();
            int thisRecvPort = NetResource.Instance.GetNextUDPPort();

            // 初始化视频
            VideoMgr.InitVideoAsResponse(otherNeck, thisSendPort, thisRecvPort, otherIP, otherSendPort, otherRecvPort, onFRH);

            // 发送接受消息
            String dataStr = System.Convert.ToChar(CProtocol.VideoAccept).ToString();
            dataStr += ((char)System.Text.Encoding.UTF8.GetBytes(otherNeck).Length).ToString();
            dataStr += otherNeck;
            dataStr += ((char)thisSendPort).ToString();
            dataStr += ((char)thisRecvPort).ToString();
            sendData(dataStr);

            // 开始收发视频
            VideoMgr.BeginSession(otherNeck);


            // 另一种方式..
            //          注意参数:           对方昵称   对方EP   自己Port 接受回调
            // VideoMgr.BeginSession(otherNeck, otherEP.Address.ToString(), otherEP.Port, thisPort, onFRH);
        }

        public void RefuseVideo(String sourceNeck)
        {
            // 发送拒绝消息
            String dataStr = System.Convert.ToChar(CProtocol.VideoRefused).ToString();
            dataStr += sourceNeck;
            sendData(dataStr);
        }

        public void RequesterStartVideo(String otherNeck, IPAddress otherIP, int otherSendPort, int otherRecvPort)
        {
            
            VideoMgr.BeginSession(otherNeck, otherIP, otherSendPort, otherRecvPort);
        }

        // 在视频请求被拒绝时清理视频模块
        public void ClearVideo(String otherNeck)
        {
            VideoMgr.Clear(otherNeck);
        }

        // 主动停止视频
        public void StopVideo(String targetNeck)
        {
            // 发送结束消息
            String dataStr = System.Convert.ToChar(CProtocol.VideoStop).ToString();
            dataStr += targetNeck;
            sendData(dataStr);
            // 清理视频模块
            VideoMgr.Clear(targetNeck);
        }
        #endregion

        // 测试用
        public Bitmap GrabImage()
        {
            return VideoMgr.GrabImage();
        }
    }
}