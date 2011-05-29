using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ChaitProtocol.Server
{
    public class ChaitMirror
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
        public ChaitMirror(TcpClient client)
        {
            this.client = client;
            this.ClientIP = client.Client.RemoteEndPoint.ToString();

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
            NetworkStream ns = client.GetStream();
            lock (ns)   // 使用SyncLock阻止多线程同时访问NetworkStream TODO 为什么会出现同时访问的情况
            {
                bytesRead = ns.EndRead(ar);

                if (bytesRead < 1)
                {
                    ChaitServer.Instance.BroadCast(System.Convert.ToChar(CProtocol.Quit).ToString() + ClientNeck);
                    ChaitServer.Instance.RemoveClient(ClientNeck);
                    OnServerLog("[管理]"+ClientNeck + "离开了聊天室\n");
                    return;
                }
                else
                {
                    appMsgHandler();
                }
            }
            if (client.Connected == true)
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
            NetworkStream ns = client.GetStream();
            byte[] sendData = System.Text.Encoding.UTF8.GetBytes(msg);
            ns.Write(sendData, 0, sendData.Length);
            ns.Flush();
        }
        internal void transDisconnect()
        {
            if(client.Connected)
                client.Close();
        }
        #endregion

        #region 应用层 - 根据Chait协议对数据流进行处理
        internal void appDisconnect()
        {
            client.Close();
        }
        private void appMsgHandler()
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
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appQuit()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< Quit数据处理 >>");
            ChaitServer.Instance.RemoveClient(ClientNeck);
            if (OnServerLog != null)
                OnServerLog("[管理]已移除客户端：" + ClientNeck);
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appNeckList()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< NeckList数据处理 >>");
            int neckNumber = ChaitServer.Instance.Clients.Count;
            String msgStr =
                System.Convert.ToChar(CProtocol.NeckList).ToString() +
                (Char)neckNumber;
            for (int i = 0; i < neckNumber; i++)
            {
                String neck = ChaitServer.Instance.Clients.Keys.ElementAt(i);
                msgStr += (Char)System.Text.Encoding.UTF8.GetBytes(neck).Length;
                msgStr += neck;
            }

            try
            {
                transSendMsg(msgStr);
            }
            catch (Exception)
            {
                throw;
            }
            if (OnServerLog != null)
                OnServerLog("[管理]已发送客户端列表给："+ClientNeck);
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appLobbyChat()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< LobbyChat数据处理 >>");
            try
            {
                String message = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);

                String msgStr =
                    System.Convert.ToChar(CProtocol.LobbyChat).ToString() +
                    (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                    ClientNeck +
                    message;
                ChaitServer.Instance.BroadCast(msgStr);

                if (OnServerLog != null)
                    OnServerLog("[数据][大厅消息][来源：" + ClientNeck + "][消息：" + message + "]");
            }
            catch (Exception)
            {
                throw;
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appChat()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< Chat数据处理 >>");
            try
            {
                int neckLen = (int)(System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0]);
                String neckName = System.Text.Encoding.UTF8.GetString(data, 2, neckLen);
                String message = System.Text.Encoding.UTF8.GetString(data, 2 + neckLen, bytesRead - 2 - neckLen);

                ChaitMirror targetClient = (ChaitMirror)ChaitServer.Instance.Clients[neckName];
                String msgStr =
                    System.Convert.ToChar(CProtocol.Chat).ToString() +
                    (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                    ClientNeck +
                    message;
                targetClient.transSendMsg(msgStr);

                if (OnServerLog != null)
                    OnServerLog("[数据][私聊消息][来源：" + ClientNeck + "][目标：" + neckName + "][消息：******]");
            }
            catch (Exception)
            {
                throw;
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appJoinGroup()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< JoinGroup数据处理 >>");
            string groupName = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
            if (OnServerLog != null)
                OnServerLog("[数据][加入群组][来源：" + ClientNeck + "][群组名称：" + groupName + "]");
            
            if (!ChaitServer.Instance.Groups.ContainsKey(groupName))
            {
                if (OnServerLog != null)
                    OnServerLog("[管理][群组不存在][群组名称：" + groupName + "]");
                ChaitServer.Instance.Groups.Add(groupName, new List<string>());
                if (OnServerLog != null)
                    OnServerLog("[管理][创建群组][群组名称：" + groupName + "]");
            }
            ChaitServer.Instance.Groups[groupName].Add(ClientNeck);

            String joinGroupStr =
                System.Convert.ToChar(CProtocol.JoinGroup).ToString() +
                (char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                ClientNeck +
                groupName;
            try
            {
                ChaitServer.Instance.SendGroupMsg(groupName, joinGroupStr);
            }
            catch (Exception ex)
            {
                if (OnServerLog != null)
                    OnServerLog("[系统][异常：" + ex.Message + "]");
                return;
            }

            if (OnServerLog != null)
                OnServerLog("[管理][加入群组][用户：" + ClientNeck + "][群组：" + groupName + "]");
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appQuitGroup()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< QuitGroup数据处理 >>");
            string groupName = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
            ChaitServer.Instance.Groups[groupName].Remove(ClientNeck);
            if (ChaitServer.Instance.Groups[groupName].Count <= 0)
            {
                ChaitServer.Instance.Groups.Remove(groupName);
            }
            else
            {
                String quitGroupStr =
                    System.Convert.ToChar(CProtocol.QuitGroup) +
                    ClientNeck;
                ChaitServer.Instance.SendGroupMsg(groupName, quitGroupStr);
            }

            if (OnServerLog != null)
                OnServerLog("'" + ClientNeck + "'退出了群组：" + groupName);
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appGroupList()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< GroupList数据处理 >>");
            int groupNumber = ChaitServer.Instance.Groups.Count;
            String msgStr =
                System.Convert.ToChar(CProtocol.GroupList).ToString() +
                (Char)groupNumber;
            for (int i = 0; i < groupNumber; i++)
            {
                String groupName = ChaitServer.Instance.Groups.Keys.ElementAt(i);
                msgStr += (Char)System.Text.Encoding.UTF8.GetBytes(groupName).Length;
                msgStr += groupName;
            }

            try
            {
                transSendMsg(msgStr);
            }
            catch (Exception)
            {
                throw;
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appYell()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< Yell数据处理 >>");
            int groupLength = (int)(System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0]);
            String groupName = System.Text.Encoding.UTF8.GetString(data, 2, groupLength);
            String yellMsg = System.Text.Encoding.UTF8.GetString(data, 2 + groupLength, bytesRead - 2 - groupLength);

            String yellStr =
                System.Convert.ToChar(CProtocol.Yell).ToString() +
                (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                ClientNeck +
                (Char)System.Text.Encoding.UTF8.GetBytes(groupName).Length +
                groupName +
                yellMsg;
            ChaitServer.Instance.SendGroupMsg(groupName, yellStr);

            if (OnServerLog != null)
                OnServerLog("'" + ClientNeck + "'在群组'" + groupName + "'中说：" + yellMsg);
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appFileRquestHandler()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< FileRequest数据处理 >>");
            int neckLen = (int)(System.Text.Encoding.UTF8.GetChars(data, 1, 2)[0]);
            if (neckLen == 0)
            {
                Console.WriteLine("客户端文件发送请求中含有非法参数，目标客户端昵称长度必须非0");
            }
            else
            {
                // 传递对目标客户端的文件请求
                String neckName = System.Text.Encoding.UTF8.GetString(data, 2, neckLen);
                String safeFileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLen, bytesRead - 2 - neckLen);

                ChaitMirror targetClient = (ChaitMirror)ChaitServer.Instance.Clients[neckName];
                String msgStr =
                    System.Convert.ToChar(CProtocol.FileRequest).ToString() +
                        (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                        ClientNeck +
                    safeFileName;
                targetClient.transSendMsg(msgStr);
                OnServerLog(String.Format("'{0}'请求发送文件'{1}'给'{2}'",ClientNeck, safeFileName, neckName));
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appFileAcceptHandler()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< FileAccept数据处理 >>");
            int neckLen =
                (int)(System.Text.Encoding.UTF8.GetChars(data, 1, 2)[0]);
            if (neckLen == 0)
            {
                Console.WriteLine("文件接受信息中含有非法参数，目标客户端昵称长度必须非0");
            }
            else
            {
                // 传递对提供客户端的文件接受
                String neckName = System.Text.Encoding.UTF8.GetString(data, 2, neckLen);
                String safeFileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLen, bytesRead - 2 - neckLen);

                ChaitMirror targetClient = (ChaitMirror)ChaitServer.Instance.Clients[neckName];
                String msgStr =
                        System.Convert.ToChar(CProtocol.FileAccept).ToString() +
                        (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                        ClientNeck +
                        (Char)System.Text.Encoding.UTF8.GetBytes(safeFileName).Length +
                        safeFileName +
                        ClientIP;
                targetClient.transSendMsg(msgStr);
                OnServerLog(String.Format("'{0}'接受了'{1}'发送文件'{2}'的请求", ClientNeck, neckName, safeFileName));
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        #endregion
    }
}