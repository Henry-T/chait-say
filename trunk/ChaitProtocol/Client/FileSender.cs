using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ChaitProtocol.Client
{
    public class FileSender
    {
        #region Variables
        public String FilePath;
        public String FileName;
        public IPAddress ReceiverIP;
        public String ReceiverNeck;

        private TcpClient receiverClient;   // TODO 可以使用一个集合以实现多线程(?)/多文件传输

        public delegate void OnFSSendingHandler(String receiverNeck, String fileName, String filePath, int bytesSended);
        public event OnFSSendingHandler OnFSSendingEvent;
        public delegate void OnFSSendDoneHandler(String receiverNeck, String fileName, String filePath);
        public event OnFSSendDoneHandler OnFSSendDoneEvent;
        public delegate void OnErrorHandler(String info);
        public event OnErrorHandler OnFSErrorEvent;
        #endregion

        // 连接接收方端口并注册连接回调
        public void SendFile(IPAddress receiverIP)
        {
            ReceiverIP = receiverIP;
            // 目前同一时刻只支持一个文件传输
            receiverClient = new TcpClient();
            receiverClient.BeginConnect(ReceiverIP, CProtocol.FilePort, onFileReceiverConnected, receiverClient);
        }

        // 已连接文件接收方回调
        private void onFileReceiverConnected(IAsyncResult ar)
        {
            NetworkStream ns = receiverClient.GetStream();

            lock (ns)
            {
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                byte[] fileBytes = new byte[fs.Length];
                fs.Read(fileBytes, 0, (int)fs.Length);
                int sendBufSize = receiverClient.ReceiveBufferSize;
                int sendedBytes = 0;
                for (int i = 0; i < fs.Length / sendBufSize; i++)
                {
                    if (fs.Length - (i * sendBufSize) > sendBufSize)
                    {
                        ns.Write(fileBytes, i * sendBufSize, sendBufSize);
                        sendedBytes += sendBufSize;
                    }
                    else
                    {
                        ns.Write(fileBytes, i * sendBufSize, (int)fs.Length - i * sendBufSize);
                        sendedBytes = (int)fs.Length;
                    }
                    if (OnFSErrorEvent != null)
                    {
                        OnFSErrorEvent("已发送 " + sendedBytes + "B...");
                    }
                }
                if (OnFSErrorEvent != null)
                {
                    OnFSErrorEvent("已发送 " + sendedBytes + "B... 发送完成！");
                }
                fs.Close();
                ns.Close();
                receiverClient.Close();

                FileName = "";
                ReceiverNeck = "";
            }
        }
    }
}
