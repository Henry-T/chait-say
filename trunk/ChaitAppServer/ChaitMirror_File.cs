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

        #region 消息响应 - 文件
        private void appFileRquestHandler()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< FileRequest数据处理 >>");
            int neckLen = (int)(System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0]);
            if (neckLen == 0)
            {
                Console.WriteLine("客户端文件发送请求中含有非法参数，目标客户端昵称长度必须非0");
            }
            else
            {
                // 传递对目标客户端的文件请求
                String neckName = System.Text.Encoding.UTF8.GetString(data, 2, neckLen);
                String safeFileName = System.Text.Encoding.UTF8.GetString(data, 2 + neckLen, bytesRead - 2 - neckLen);

                ChaitMirror targetClient = ChaitServer.Instance.Clients[neckName];
                String msgStr =
                    System.Convert.ToChar(CProtocol.FileRequest).ToString() +
                        (Char)System.Text.Encoding.UTF8.GetBytes(ClientNeck).Length +
                        ClientNeck +
                    safeFileName;
                targetClient.transSendMsg(msgStr);
                OnServerLog(String.Format("'{0}'请求发送文件'{1}'给'{2}'", ClientNeck, safeFileName, neckName));
            }
            if (OnServerLog != null)
                OnServerLog("\n");
        }
        private void appFileAcceptHandler()
        {
            if (OnServerLog != null)
                OnServerLog("\n<< FileAccept数据处理 >>");
            int neckLen =
                (int)(System.Text.Encoding.UTF8.GetChars(data, 1, 1)[0]);
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

        #region 干涉回调 - 文件
        #endregion
    }
}