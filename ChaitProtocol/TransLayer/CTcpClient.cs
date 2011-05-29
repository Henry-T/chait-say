using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChaitProtocol.TransLayer
{
    class CTcpClient
    {
        #region Variables
        public IPAddress ServerIP { get; private set; }
        public int ServerPort { get; private set; }
        private TcpClient tcpClient;
        public byte[] ReceivedData { get; private set; }

        public delegate void OnErrorHandler(String errorStr);
        public OnErrorHandler OnErrorEvent;
        #endregion

        public void Connect(IPAddress serverIP, int serverPort)
        {
            ServerIP = serverIP;
            ServerPort = serverPort;

        }

        public void SendData(byte[] sendData)
        {
            try
            {
                // 连接服务器
                tcpClient = new TcpClient();
                tcpClient.Connect(ServerIP, ServerPort);

                // 创建接收缓冲区
                ReceivedData = new byte[tcpClient.ReceiveBufferSize];
                tcpClient.GetStream().BeginRead(
                    ReceivedData, 0,
                    System.Convert.ToInt32(tcpClient.ReceiveBufferSize),
                    ACOnReceiveData, null);
            }
            catch (Exception ex)
            {
                if (OnErrorEvent != null)
                {
                    OnErrorEvent(ex.Message);
                }
            }
        }

        public AsyncCallback ACOnReceiveData;
    }
}
