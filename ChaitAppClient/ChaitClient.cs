using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using ChaitAppProtocol;
using ServerApp;

namespace ChaitAppClient
{
    public partial class ChaitClient
    {
        #region Variables
        public IPAddress ServerIP { get; private set; }
        public int ServerPort { get; private set; }
        public String Neckname { get; private set; }
        public IPAddress LocalIP { get; private set; }

        private TcpClient client;
        private byte[] data;
        private int bytesRead;

        private bool quiting = false;
        #endregion
        
        #region Instance
        private static ChaitClient instance;
        public static ChaitClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChaitClient();
                return instance;
            }
        }
        private ChaitClient()
        {
        }
        #endregion

        #region 传输层封装
        private void connect()
        {
            //try
            {
                // 连接服务器
                client = new TcpClient();
                client.Connect(ServerIP, ServerPort);
                quiting = false;

                // 创建接收缓冲区
                data = new byte[client.ReceiveBufferSize];
                client.GetStream().BeginRead(
                    data, 0,
                    System.Convert.ToInt32(client.ReceiveBufferSize),
                    delRecvData, null);
            }
            //catch (Exception ex)
            //{
            //    if (OnErrorEvent != null)
            //    {
            //        OnErrorEvent(ex.Message);
            //    }
            //}
        }
        private void sendData(String dataStr)
        {
            //try
            {
                NetworkStream ns = client.GetStream();
                // TODO 这里是否可以使用异步方式?
                byte[] d = System.Text.Encoding.UTF8.GetBytes(dataStr);
                lock (ns)
                {
                    ns.Write(d, 0, d.Length);
                }
            }
            //catch (Exception ex)
            //{
            //    if (OnErrorEvent != null)
            //    {
            //        OnErrorEvent(ex.Message);
            //    }
            //}
        }
        private void delRecvData(IAsyncResult ar)
        {
            if (quiting)
                return;

            //try
            {
                lock (client.GetStream())
                {
                    bytesRead = client.GetStream().EndRead(ar);
                }
                if (bytesRead < 1)
                {
                    if (OnServerStopEvent != null)
                        OnServerStopEvent();
                    return;
                }
                else
                {
                    // TODO 为什么要加大括号？
                    object[] para = {System.Text.Encoding.UTF8.GetString(
                                        data,0, bytesRead)};

                    int protocolKey = (int)(System.Text.Encoding.UTF8.GetChars(data, 0, 1)[0]);
                    int neckNum;
                    List<String> neckList = new List<string>();
                    int neckLength;
                    String neckname;
                    int groupNum;
                    List<String> groupList = new List<string>();
                    int groupLength;
                    String groupName;
                    int fileNameLength;
                    String fileName;
                    String chatStr;
                    String yellStr;
                    IPAddress receiverIP;
                    int ipLength;
                    String otherIP;
                    int otherSendPort;
                    int otherRecvPort;
                    int roleX;
                    int roleY;

                    switch (protocolKey)
                    {
                        case CProtocol.Join:
                            neckname = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
                            if (OnJoinEvent != null)
                                OnJoinEvent(neckname);
                            break;
                        case CProtocol.Quit:
                            neckname = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
                            if (OnQuitEvent != null)
                                OnQuitEvent(neckname);
                            break;
                        case CProtocol.NeckList:
                            neckNum = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            int nameByteLoaded = 0;
                            for (int i = 0; i < neckNum; i++)
                            {
                                neckLength = System.Text.Encoding.UTF8.GetChars(data, 2 + nameByteLoaded, 1)[0];
                                neckname = System.Text.Encoding.UTF8.GetString(data, 3 + nameByteLoaded, neckLength);
                                nameByteLoaded += neckLength + 1;
                                neckList.Add(neckname);
                            }
                            if (OnNeckListEvent != null)
                                OnNeckListEvent(neckList);
                            break;
                        case CProtocol.LobbyChat:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            chatStr = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnLobbyChatEvent != null)
                                OnLobbyChatEvent(neckname, chatStr);
                            break;

                        case CProtocol.Chat:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            chatStr = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnChatEvent != null)
                                OnChatEvent(neckname, chatStr);
                            break;

                        case CProtocol.JoinGroup:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            groupName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnJoinGroupEvent != null)
                                OnJoinGroupEvent(neckname, groupName);
                            break;
                        case CProtocol.QuitGroup:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            groupName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnQuitGroupEvent != null)
                                OnQuitGroupEvent(neckname, groupName);
                            break;
                        case CProtocol.GroupList:
                            groupNum = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            int groupByteLoaded = 0;
                            for (int i = 0; i < groupNum; i++)
                            {
                                groupLength = System.Text.Encoding.UTF8.GetChars(data, 2 + groupByteLoaded, 1)[0];
                                groupName = System.Text.Encoding.UTF8.GetString(data, 3 + groupByteLoaded, groupLength);
                                groupByteLoaded += groupLength + 1;
                                neckList.Add(groupName);
                            }
                            if (OnGroupListEvent != null)
                                OnGroupListEvent(neckList);
                            break;
                        case CProtocol.Yell:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            groupLength = System.Text.Encoding.UTF8.GetChars(data, 2 + neckLength, 1)[0];
                            groupName = System.Text.Encoding.UTF8.GetString(data, 3 + neckLength, groupLength);
                            yellStr = System.Text.Encoding.UTF8.GetString(data, 3 + neckLength + groupLength, bytesRead - 3 - neckLength - groupLength);
                            if (OnYellEvent != null)
                                OnYellEvent(neckname, groupName, yellStr);
                            break;

                        case CProtocol.FileRequest:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            fileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnFileRequestEvent != null)
                                OnFileRequestEvent(neckname, fileName);
                            break;
                        case CProtocol.FileAccept:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            fileNameLength = System.Text.Encoding.UTF8.GetChars(data, 2 + neckLength, 1)[0];
                            fileName = System.Text.Encoding.UTF8.GetString(data, 3 + neckLength, fileNameLength);
                            receiverIP = IPAddress.Parse(
                                System.Text.Encoding.UTF8.GetString(data, 3 + neckLength + fileNameLength, bytesRead - 3 - neckLength - fileNameLength));
                            
                            if (OnFileAcceptEvent != null)
                                OnFileAcceptEvent(neckname, fileName, receiverIP);
                            break;
                        case CProtocol.FileRefuse:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            fileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnFileRefuseEvent != null)
                                OnFileRefuseEvent(neckname, fileName);
                            break;
                        case CProtocol.FilePause:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            fileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnFilePauseEvent != null)
                                OnFilePauseEvent(neckname, fileName);
                            break;
                        case CProtocol.FileResume:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            fileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnFileResumeEvent != null)
                                OnFileResumeEvent(neckname, fileName);
                            break;
                        case CProtocol.FileStop:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            fileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLength, bytesRead - 2 - neckLength);
                            if (OnFileStopEvent != null)
                                OnFileStopEvent(neckname, fileName);
                            break;
                        case CProtocol.VideoRequest:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            ipLength = System.Text.Encoding.UTF8.GetChars(data, 2 + neckLength, 1)[0];
                            otherIP = System.Text.Encoding.UTF8.GetString(data, 3 + neckLength, ipLength);
                            otherSendPort = System.Text.Encoding.UTF8.GetChars(data, 3 + neckLength + ipLength, 4)[0];
                            int spLen = System.Text.Encoding.UTF8.GetByteCount(((char)otherSendPort).ToString());
                            otherRecvPort = System.Text.Encoding.UTF8.GetChars(data, 3 + neckLength + ipLength + spLen, 4)[0];
                            if (OnVideoRequestEvent != null)
                                OnVideoRequestEvent(neckname, otherIP, otherSendPort, otherRecvPort);
                            break;
                        case CProtocol.VideoAccept:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            ipLength = System.Text.Encoding.UTF8.GetChars(data, 2 + neckLength, 1)[0];
                            otherIP = System.Text.Encoding.UTF8.GetString(data, 3 + neckLength, ipLength);
                            otherSendPort = System.Text.Encoding.UTF8.GetChars(data, 3 + neckLength + ipLength, 4)[0];
                            int spLen2 = System.Text.Encoding.UTF8.GetByteCount(((char)otherSendPort).ToString());
                            otherRecvPort = System.Text.Encoding.UTF8.GetChars(data, 3 + neckLength + ipLength + spLen2, 4)[0];
                            if (OnVideoAcceptEvent != null)
                                OnVideoAcceptEvent(neckname, otherIP, otherSendPort, otherRecvPort);
                            break;
                        case CProtocol.VideoRefused:
                            neckname = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
                            if (OnVideoRefuseEvent != null)
                                OnVideoRefuseEvent(neckname);
                            break;
                        case CProtocol.VideoStop:
                            neckname = System.Text.Encoding.UTF8.GetString(data, 1, bytesRead - 1);
                            if (OnVideoStopEvent != null)
                                OnVideoStopEvent(neckname);
                            break;
                        case CProtocol.RolePos:
                            neckLength = System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0];
                            neckname = System.Text.Encoding.UTF8.GetString(data, 2, neckLength);
                            roleX = (int)System.Text.Encoding.UTF8.GetChars(data, 2 + neckLength, 3)[0];
                            int xLen = System.Text.Encoding.UTF8.GetByteCount(((char)roleX).ToString());
                            roleY = (int)System.Text.Encoding.UTF8.GetChars(data, 2 + neckLength + xLen, 3)[0];
                            if (OnRolePosEvent != null)
                                OnRolePosEvent(neckname, roleX, roleY);
                            break;
                        default:
                            break;
                    }
                }
                // 清空接收缓冲 - 可以不清空
                data = new byte[client.Client.ReceiveBufferSize];
                // 递归调用
                client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize),
                    delRecvData, null);
            }
            //catch (Exception ex)
            //{
            //    if (OnErrorEvent != null)
            //    {
            //        OnErrorEvent(ex.Message);
            //    }
            //}
        }
        private void fileRequestHandler()
        {
        }
        #endregion

        #region 系统协议回调
        // 服务器停止事件
        public delegate void OnServerStopHandler();
        public event OnServerStopHandler OnServerStopEvent;
        // 用户被踢出事件
        public delegate void OnKickHandler(String neckName);
        public event OnKickHandler OnKickEvent;
        // 昵称重复事件
        public delegate void OnNeckExistHandler();
        public event OnNeckExistHandler OnNeckExistEvent;
        // 连接断开事件
        public delegate void OnDisconnectHandler();
        public event OnDisconnectHandler OnDisconnectEvent;

        public delegate void OnErrorHandler(String errorInfo);
        public event OnErrorHandler OnErrorEvent;

        #region 角色事件
        public delegate void OnRolePosHandler(String neckname, int x, int y);
        public event OnRolePosHandler OnRolePosEvent;
        #endregion
        #endregion

        #region 客户端行为
        public void Join(IPAddress serverIP, int serverPort, String neckname, IPAddress localIP)
        {
            ServerIP = serverIP;
            ServerPort = serverPort;
            Neckname = neckname;
            LocalIP = localIP;

            fileSender = new FileSender();
            fileReciever = new FileReciever(LocalIP);

            connect();
            sendData(System.Convert.ToChar(CProtocol.Join) + Neckname);
        }
        public void Quit()
        {
            String dataStr = System.Convert.ToChar(CProtocol.Quit).ToString();
            sendData(dataStr);
            quiting = true;
        }
        public void NeckList()
        {
            sendData(System.Convert.ToChar(CProtocol.NeckList).ToString());
        }
        public void LobbyChat(String chatStr)
        {
            String dataStr = System.Convert.ToChar(CProtocol.LobbyChat).ToString();
            dataStr += chatStr;
            sendData(dataStr);
        }

        public void SetTarget(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > 800) x = 800;
            if (y < 0) y = 0;
            if (y > 600) y = 600;

            String dataStr = System.Convert.ToChar(CProtocol.SetTarget).ToString();
            dataStr += (char)x;
            dataStr += (char)y;
            sendData(dataStr);
        }
        #endregion

        public void ChangePortBase(int pb)
        {
            NetResource.Instance.SetBaseNumber(pb);
        }
    }
}