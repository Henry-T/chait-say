using System;
using System.Net;
using ChaitAppProtocol;
namespace ChaitAppClient
{
    public partial class ChaitClient
    {
        // Variables
        public FileSender fileSender;       // TODO 将提供多用户多文件多线程传递功能
        public FileReciever fileReciever;   // 暂时设为Public

        #region 文件协议回调
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

        #region 客户端文件行为
        public void FileRequest(String receiverNeck, String fileName, String filePath)
        {
            fileSender.ReceiverNeck = receiverNeck;
            fileSender.FileName = fileName;
            fileSender.FilePath = filePath;

            String dataStr = System.Convert.ToChar(CProtocol.FileRequest).ToString();
            dataStr += ((Char)System.Text.Encoding.UTF8.GetBytes(receiverNeck).Length).ToString();
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