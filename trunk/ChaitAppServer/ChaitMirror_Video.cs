using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.IO;
using ChaitAppProtocol;

namespace ChaitAppServer
{
    public partial class ChaitMirror
    {
        // Variables

        #region 消息响应
        private void appVideoRequestHandler()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< VideoRequest数据处理 >>");
            int neckLen = (int)(Encoding.UTF8.GetChars(data, 1, 1)[0]);
            String neckname = Encoding.UTF8.GetString(data, 2, neckLen);
            int sendPort = (int)(Encoding.UTF8.GetChars(data, 2 + neckLen, 4)[0]);
            int spLen = Encoding.UTF8.GetByteCount(((char)sendPort).ToString());
            int recvPort = (int)(Encoding.UTF8.GetChars(data, 2 + neckLen + spLen, 4)[0]);

            ChaitMirror targetClient = ChaitServer.Instance.Clients[neckname];
            String ip = ClientIP.Split(new char[]{':'})[0];
            String msgStr =
                System.Convert.ToChar(CProtocol.VideoRequest).ToString() +
                ((char)Encoding.UTF8.GetBytes(ClientNeck).Length).ToString() +
                ClientNeck +
                ((char)Encoding.UTF8.GetBytes(ip).Length).ToString() +
                ip +
                ((char)sendPort).ToString() + 
                ((char)recvPort).ToString();
            targetClient.transSendMsg(msgStr);
            if (OnServerLog != null)
                OnServerLog(String.Format("{0}向{1}请求视频聊天", ClientNeck, neckname));

            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appVideoAcceptHandler()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< VideoAccept数据处理 >>");
            int neckLen = (int)(Encoding.UTF8.GetChars(data, 1, 1)[0]);
            String neckname = Encoding.UTF8.GetString(data, 2, neckLen);
            int sendPort = (int)(Encoding.UTF8.GetChars(data, 2 + neckLen, 4)[0]);
            int spLen = Encoding.UTF8.GetByteCount(((char)sendPort).ToString());
            int recvPort = (int)(Encoding.UTF8.GetChars(data, 2 + neckLen + spLen, 4)[0]);

            ChaitMirror targetClient = ChaitServer.Instance.Clients[neckname];
            String ip = ClientIP.Split(new char[] { ':' })[0];
            String msgStr =
                System.Convert.ToChar(CProtocol.VideoAccept).ToString() +
                ((char)Encoding.UTF8.GetBytes(ClientNeck).Length).ToString() +
                ClientNeck +
                ((char)Encoding.UTF8.GetBytes(ip).Length).ToString() +
                ip +
                ((char)sendPort).ToString() +
                ((char)recvPort).ToString();
            targetClient.transSendMsg(msgStr);
            if (OnServerLog != null)
                OnServerLog(String.Format("{0}接受了{1}的视频聊天请求", ClientNeck, neckname));

            if (OnServerLog != null)
                OnServerLog("\n");

        }
        private void appVideoRefusedHandler(int bytesRead)
        {
            if (OnServerLog != null)
                OnServerLog("\n<< VideoRefuse数据处理 >>");
            String neckname = Encoding.UTF8.GetString(data, 1, bytesRead - 1);

            ChaitMirror targetClient = ChaitServer.Instance.Clients[neckname];
            String msgStr =
                System.Convert.ToChar(CProtocol.VideoRefused).ToString() +
                ClientNeck;
            targetClient.transSendMsg(msgStr);
            if (OnServerLog != null)
                OnServerLog(String.Format("{0}拒绝了{1}的视频聊天请求", ClientNeck, neckname));

            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appVideoStopHandler(int byteRead)
        {
            if (OnServerLog != null)
                OnServerLog("\n<< VideoStop数据处理 >>");
            String neckname = Encoding.UTF8.GetString(data, 1, bytesRead - 1);

            ChaitMirror targetClient = ChaitServer.Instance.Clients[neckname];
            String msgStr =
                System.Convert.ToChar(CProtocol.VideoStop).ToString() +
                ClientNeck;
            targetClient.transSendMsg(msgStr);
            if (OnServerLog != null)
                OnServerLog(String.Format("{0}停止了和{1}的视频聊天", ClientNeck, neckname));

            if (OnServerLog != null)
                OnServerLog("\n");
        }
        #endregion

        #region 干涉回调
        #endregion
    }
}