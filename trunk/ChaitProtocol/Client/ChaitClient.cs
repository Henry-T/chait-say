using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ChaitProtocol.Client
{
    public class ChaitClient
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

        public FileSender fileSender;       // TODO 将提供多用户多文件多线程传递功能
        public FileReciever fileReciever;   // 暂时设为Public
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
            try
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
            catch (Exception ex)
            {
                if (OnErrorEvent != null)
                {
                    OnErrorEvent(ex.Message);
                }
            }
        }
        private void sendData(String dataStr)
        {
            try
            {
                NetworkStream ns = client.GetStream();
                // TODO 这里是否可以使用异步方式?
                byte[] d = System.Text.Encoding.UTF8.GetBytes(dataStr);
                ns.Write(d, 0, d.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                if (OnErrorEvent != null)
                {
                    OnErrorEvent(ex.Message);
                }
            }
        }
        private void delRecvData(IAsyncResult ar)
        {
            if (quiting)
                return;

            try
            {
                bytesRead = client.GetStream().EndRead(ar);
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

                    String msg = System.Convert.ToString(para);

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
            catch (Exception ex)
            {
                if (OnErrorEvent != null)
                {
                    OnErrorEvent(ex.Message);
                }
            }
        }
        private void fileRequestHandler()
        {
        }
        #endregion

        #region 协议委托客户端回调
        #region 服务器事件
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
        #endregion

        #region 聊天事件
        // 用户加入事件
        public delegate void OnJoinHandler(String neckName);
        public event OnJoinHandler OnJoinEvent;
        // 用户离开事件
        public delegate void OnQuitHandler(String neckName);
        public event OnQuitHandler OnQuitEvent;
        // 获取用户列表事件
        public delegate void OnNeckListHandler(List<String> neckList);
        public event OnNeckListHandler OnNeckListEvent;
        // 获取群组列表时间
        public delegate void OnGroupListHandler(List<String> groupList);
        public event OnGroupListHandler OnGroupListEvent;
        // 大厅聊天信息事件
        public delegate void OnLobbyChatHandler(String neckName, String chatStr);
        public event OnLobbyChatHandler OnLobbyChatEvent;
        // 聊天信息事件
        public delegate void OnChatHandler(String neckName, String chatStr);
        public event OnChatHandler OnChatEvent;
        #endregion

        #region 群组事件
        // 加入群组事件
        public delegate void OnJoinGroupHandler(String neckName, String groupName);
        public event OnJoinGroupHandler OnJoinGroupEvent;
        // 离开群组事件
        public delegate void OnQuitGroupHandler(String neckName, String groupName);
        public event OnQuitGroupHandler OnQuitGroupEvent;
        // 群组呼喊事件
        public delegate void OnYellHandler(String neckName, String groupName, String yellStr);
        public event OnYellHandler OnYellEvent;
        #endregion

        #region 文件事件
        // 文件请求事件
        public delegate void OnFileRequestHandler(String neckName, String fileName);
        public event OnFileRequestHandler OnFileRequestEvent;
        // 文件接受事件
        public delegate void OnFileAcceptHandler(String neckName, String fileName, IPAddress receiverIP);
        public event OnFileAcceptHandler OnFileAcceptEvent;
        // 文件拒绝事件
        public delegate void OnFileRefuseHandler(String neckName, String fileName);
        public event OnFileRefuseHandler OnFileRefuseEvent;
        // 文件暂停事件
        public delegate void OnFilePauseHandler(String neckName, String fileName);
        public event OnFilePauseHandler OnFilePauseEvent;
        // 文件继续事件
        public delegate void OnFileResumeHandler(String neckName, String fileName);
        public event OnFileResumeHandler OnFileResumeEvent;
        // 文件停止事件
        public delegate void OnFileStopHandler(String neckName, String fileName);
        public event OnFileStopHandler OnFileStopEvent;
        #endregion

        #region 错误事件
        public delegate void OnErrorHandler(String errorInfo);
        public event OnErrorHandler OnErrorEvent;
        #endregion
        #endregion

        #region 公开给客户端
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
        public void Chat(String neckName, String chatStr)
        {
            String dataStr = System.Convert.ToChar(CProtocol.Chat).ToString();
            dataStr += (Char)System.Text.Encoding.UTF8.GetBytes(neckName).Length;
            dataStr += neckName;
            dataStr += chatStr;
            sendData(dataStr);
        }

        public void JoinGroup(String groupName)
        {
            String joinGroupStr =
                System.Convert.ToChar(CProtocol.JoinGroup) +
                groupName;
            sendData(joinGroupStr);
        }
        public void QuitGroup(String groupName)
        {
            String quitGroupStr =
                System.Convert.ToChar(CProtocol.QuitGroup) +
                groupName;
            sendData(quitGroupStr);
        }
        public void GroupList()
        {
            sendData(System.Convert.ToChar(CProtocol.GroupList).ToString());
        }
        public void Yell(String groupName, String yellStr)
        {
            String dataStr =
                System.Convert.ToChar(CProtocol.Yell).ToString() +
                (Char)System.Text.Encoding.UTF8.GetBytes(groupName).Length +
                groupName +
                yellStr;
            sendData(dataStr);
        }

        public void FileRequest(String receiverNeck, String fileName, String filePath)
        {
            fileSender.ReceiverNeck = receiverNeck;
            fileSender.FileName = fileName;
            fileSender.FilePath = filePath;
            
            String dataStr = System.Convert.ToChar(CProtocol.FileRequest).ToString();
            dataStr += (Char)System.Text.Encoding.UTF8.GetBytes(receiverNeck).Length;
            dataStr += receiverNeck;
            dataStr += (Char)System.Text.Encoding.UTF8.GetBytes(fileName).Length;
            dataStr += fileName;
            sendData(dataStr);
        }
        public void AcceptFile(String senderNeck, String fileName, String filePath)
        {
            // 发送确认接收信息
            String dataStr = System.Convert.ToChar(CProtocol.FileAccept).ToString();
            dataStr += (Char)System.Text.Encoding.UTF8.GetBytes(senderNeck).Length;
            dataStr += senderNeck;
            dataStr += (Char)System.Text.Encoding.UTF8.GetBytes(fileReciever.FileName).Length;
            dataStr += fileReciever.FileName;
            sendData(dataStr);
            // 开放端口接收文件
            fileReciever.Receive(filePath);
        }
        public void RefuseFile(String senderNeck, String fileName)
        {
            String transMsg =
                System.Convert.ToChar(CProtocol.FileRefuse) +
                (Char)System.Text.Encoding.UTF8.GetBytes(senderNeck).Length +
                senderNeck +
                fileName;
            sendData(transMsg);
        }
        #endregion
    }
}