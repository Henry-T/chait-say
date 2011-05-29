using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace ChaitProtocol.Server
{
    public class ChaitServer
    {
        #region Variables
        IPAddress ipAddr;
        int port;

        Thread listenThread;
        TcpListener listener;

        public Dictionary<EndPoint, ChaitMirror> NewClients;
        public Dictionary<String, ChaitMirror> Clients;
        public Dictionary<String, List<String>> Groups;
        public ChaitMirror.OnServerLogHandler serverLogHandler;
        #endregion

        #region Instance
        private static ChaitServer instance;
        public static ChaitServer Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChaitServer();
                return instance;
            }
        }
        private ChaitServer()
        {
        }
        #endregion

        #region 公开给服务器
        public void Start(IPAddress ipAddr, int portNum)
        {
            this.ipAddr = ipAddr;
            this.port = portNum;

            NewClients = new Dictionary<EndPoint, ChaitMirror>();
            Clients = new Dictionary<String, ChaitMirror>();
            Groups = new Dictionary<String, List<String>>();

            listener = new TcpListener(ipAddr, port);
            listener.Start();

            // 在子线程中侦听端口
            listenThread = new Thread(serverListen);
            listenThread.Start();
        }
        public void RemoveClient(String neckname)
        {
            // 将用户移出加入的群组
            foreach (List<String> neckList in Groups.Values)
            {
                if (neckList.Contains(neckname))
                    neckList.Remove(neckname);
            }
            Clients[neckname].transDisconnect();
            Clients.Remove(neckname);
        }
        public void BroadCast(String message)
        {
            foreach (ChaitMirror cs in Clients.Values)
            {
                cs.transSendMsg(message);
            }
        }
        public void SendGroupMsg(String groupName, String message)
        {
            foreach (String neckname in Groups[groupName])
            {
                Clients[neckname].transSendMsg(message);
            }
        }
        public void Stop()
        {
            listenThread.Abort();
            listener.Stop();

            ChaitServer.Instance.BroadCast(System.Convert.ToChar(CProtocol.ServerStop).ToString());
            // 服务器主动中断
            foreach (String neckname in Clients.Keys)
            {
                ChaitServer.Instance.Clients.Remove(neckname);
            }
        }
        #endregion

        // 端口侦听线程
        private void serverListen()
        {
            while (true)
            {
                ChaitMirror newClient =
                    new ChaitMirror(listener.AcceptTcpClient());
                newClient.OnServerLog = serverLogHandler;

                NewClients.Add(newClient.RemoteEndPoint, newClient);
            }
        }
    }
}