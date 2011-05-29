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

        #region 消息响应 - 聊天
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
                OnServerLog("[管理]已发送客户端列表给：" + ClientNeck);
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
        #endregion

        #region 干涉回调 - 聊天
        #endregion
    }
}