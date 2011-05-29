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
        #region Variables
        public String ClientIP{ get; private set;}
        public EndPoint RemoteEndPoint { get { return client.Client.RemoteEndPoint; } }
        public string ClientNeck { get; private set; }
        private TcpClient client;

        private byte[] data;
        private int bytesRead;

        public delegate void OnServerLogHandler(String logStr);
        public OnServerLogHandler OnServerLog;
        #endregion

        #region Constructor
        public ChaitMirror(TcpClient client,OnJoinHandler onJoinHandler, OnSetTargetHandler onSetTargetHandler)
        {
            this.client = client;
            this.ClientIP = client.Client.RemoteEndPoint.ToString();
            OnJoinEvent += onJoinHandler;
            OnSetTargetEvent += onSetTargetHandler;


            data = new byte[client.ReceiveBufferSize];
            client.GetStream().BeginRead(data, 0,
                System.Convert.ToInt32(client.ReceiveBufferSize),
                onTransRecvMsg, null);
        }
        #endregion

        #region 传输层封装
        /// <summary>
        /// TCP数据接收回调
        /// </summary>
        /// <param name="ar"></param>
        private void onTransRecvMsg(IAsyncResult ar)
        {
            lock (client.GetStream())   // 使用SyncLock阻止多线程同时访问NetworkStream TODO 为什么会出现同时访问的情况
            {
                try
                {
                    bytesRead = client.GetStream().EndRead(ar);
                }
                catch (Exception ex)
                {
                    // 客户端强行断开连接处理
                    if (OnServerLog != null)
                        OnServerLog("\n<< 客户端强行退出异常处理 >>");
                    client.Close();
                    ChaitServer.Instance.RemoveClient(ClientNeck);
                    if (OnServerLog != null)
                        OnServerLog("[管理]已移除客户端：" + ClientNeck);
                    if (OnServerLog != null)
                        OnServerLog("\n");

                    return;
                }
            }

            if (bytesRead < 1)
            {
                ChaitServer.Instance.BroadCast(System.Convert.ToChar(CProtocol.Quit).ToString() + ClientNeck);
                ChaitServer.Instance.RemoveClient(ClientNeck);
                OnServerLog("[管理]"+ClientNeck + "离开了聊天室\n");
                return;
            }
            else
            {
                appMsgHandler(bytesRead);
            }

            if (client != null && client.Connected == true)
            {
                client.GetStream().BeginRead(data, 0,
                    System.Convert.ToInt32(client.ReceiveBufferSize),
                    onTransRecvMsg, null);
            }
        }
        /// <summary>
        /// 发送TCP数据
        /// </summary>
        /// <param name="msg"></param>
        internal void transSendMsg(String msg)
        {
            byte[] sendData = System.Text.Encoding.UTF8.GetBytes(msg);
            lock (client.GetStream())
            {
                client.GetStream().Write(sendData, 0, sendData.Length);
            }
        }
        internal void transDisconnect()
        {
            if(client != null && client.Connected)
                client.Close();
        }
        #endregion

        #region 消息响应 - 系统
        internal void appDisconnect()
        {
            client.Close();
        }
        private void appMsgHandler(int bytesRead)
        {
            int protocolKey =
                (int)(System.Text.Encoding.UTF8.GetChars(data, 0, 1)[0]);
            switch (protocolKey)
            {
                case CProtocol.Join:
                    appJoin();
                    break;
                case CProtocol.Quit:
                    appQuit();
                    break;
                case CProtocol.NeckList:
                    appNeckList();
                    break;
                case CProtocol.LobbyChat:
                    appLobbyChat();
                    break;
                case CProtocol.Chat:
                    appChat();
                    break;
                case CProtocol.JoinGroup:
                    appJoinGroup();
                    break;
                case CProtocol.QuitGroup:
                    appQuitGroup();
                    break;
                case CProtocol.GroupList:
                    appGroupList();
                    break;
                case CProtocol.Yell:
                    appYell();
                    break;
                case CProtocol.FileRequest:
                    appFileRquestHandler();
                    break;
                case CProtocol.FileAccept:
                    appFileAcceptHandler();
                    break;
                case CProtocol.VideoRequest:
                    appVideoRequestHandler();
                    break;
                case CProtocol.VideoAccept:
                    appVideoAcceptHandler();
                    break;
                case CProtocol.VideoRefused:
                    appVideoRefusedHandler(bytesRead);
                    break;
                case CProtocol.VideoStop:
                    appVideoStopHandler(bytesRead);
                    break;
                case CProtocol.SetTarget:
                    appSetTarget();
                    break;
                default:
                    break;
            }
        }
        private void appJoin()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< Join数据处理 >>");
            // 昵称重复检查
            ClientNeck = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
            bool neckExist = false;
            foreach (ChaitMirror cs in ChaitServer.Instance.Clients.Values)
            {
                if (cs.ClientNeck == ClientNeck)
                {
                    neckExist = true;
                    break;
                }
            }
            if (neckExist)
            {
                String neckExistStr =
                    System.Convert.ToChar(CProtocol.NeckExist) +
                    (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                    ClientNeck;
                transSendMsg(neckExistStr);
                ChaitServer.Instance.NewClients.Remove(this.RemoteEndPoint);    // TODO 需要修改
                if (OnServerLog != null)
                    OnServerLog("[管理]聊天室中已经存在昵称'" + ClientNeck + "'，加入请求被拒绝。");
            }
            else
            {
                ChaitServer.Instance.Clients.Add(ClientNeck, this);
                ChaitServer.Instance.NewClients.Remove(this.RemoteEndPoint);

                String joinStr =
                    System.Convert.ToChar(CProtocol.Join) +
                    ClientNeck;
                ChaitServer.Instance.BroadCast(joinStr);
                if (OnServerLog != null)
                    OnServerLog("[管理]昵称'" + ClientNeck + "'被接受");
            }

            if (OnJoinEvent != null)
                OnJoinEvent(ClientNeck);
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appQuit()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< Quit数据处理 >>");
            client.Close();
            client = null;
            ChaitServer.Instance.RemoveClient(ClientNeck);
            if (OnServerLog != null)
                OnServerLog("[管理]已移除客户端：" + ClientNeck);
            if (OnServerLog != null)
                OnServerLog("\n");
        }

        private void appSetTarget()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< SetTarget数据处理 >>");
            int x = System.Text.Encoding.UTF8.GetChars(data, 1, 3)[0];
            int xLen = System.Text.Encoding.UTF8.GetByteCount(((char)x).ToString());
            int y = System.Text.Encoding.UTF8.GetChars(data, 1 + xLen, 3)[0];

            if (OnSetTargetEvent != null)
                OnSetTargetEvent(ClientNeck, x, y);

            if (OnServerLog != null)
            {
                String ls = String.Format("\n昵称为{0}的玩家设定目标：{1},{2}", ClientNeck, x, y);
                OnServerLog(ls);
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        #endregion

        #region 干涉回调 - 系统
        // 用户加入
        public delegate void OnJoinHandler(String neckname);
        public event OnJoinHandler OnJoinEvent;
        // 设定目标事件
        public delegate void OnSetTargetHandler(String neckname, int x, int y);
        public event OnSetTargetHandler OnSetTargetEvent;
        #endregion
    }
}