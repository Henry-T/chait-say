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

        #region 群组消息响应
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
                OnServerLog("[管理]已发送客户端列表给：" + ClientNeck);
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
        #endregion

        #region 群组干涉回调
        #endregion
    }
}