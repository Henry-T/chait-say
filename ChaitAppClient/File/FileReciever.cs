using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using ChaitAppProtocol;

namespace ChaitAppClient
{
    public class FileReciever
    {
        #region Variables
        public String FileName { get; private set; }
        public String FilePath { get; private set; }
        public String FileSenderNeck { get; private set; }
        public IPAddress LocalIP { get; private set; }

        private TcpListener listener;
        private TcpClient senderClient;    // TODO 可以使用一个集合以实现多线程(?)/多文件传输

        public delegate void OnFRReceivingHandler(String senderNeck, String fileName, String filePath, int bytesReceived);
        public event OnFRReceivingHandler OnFRReceivingEvent;
        public delegate void OnFRReceiveDoneHandler(String senderNeck, String fileName, String filePath);
        public event OnFRReceiveDoneHandler OnFRReceiveDoneEvent;
        public delegate void OnErrorHandler(String info);
        public event OnErrorHandler OnFRErrorEvent;
        #endregion

        public FileReciever(IPAddress localIP)
        {
            LocalIP = localIP;
        }

        // 开始接收文件侦听
        public void Receive(String filePath)
        {
            FilePath = filePath;
            listener = new TcpListener(LocalIP, CProtocol.FilePort);
            listener.BeginAcceptTcpClient(onFileSenderConnected, listener);
        }

        // 已连接文件发送方回调
        private void onFileSenderConnected(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            senderClient = listener.EndAcceptTcpClient(ar);
            int recvBufSize = senderClient.SendBufferSize;
            NetworkStream ns = senderClient.GetStream();

            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            int receivedBytes = 0;
            while (ns.DataAvailable)
            {
                byte[] recvBytes = new byte[recvBufSize];
                int bytesRecv = ns.Read(recvBytes, 0, recvBufSize); // TODO 如果剩余比特不足sendBufSize?
                recvBufSize += recvBufSize;    // TODO 末值不准确
                fs.Write(recvBytes, 0, recvBytes.Length);

                if (OnFRReceivingEvent != null)
                    OnFRReceivingEvent(FileSenderNeck, FileName, FilePath, receivedBytes);
            }
            if (OnFRReceiveDoneEvent != null)
                OnFRReceiveDoneEvent(FileSenderNeck, FileName, FilePath);
            fs.Close();
            ns.Close();
            senderClient.Close();
        }
    }
}