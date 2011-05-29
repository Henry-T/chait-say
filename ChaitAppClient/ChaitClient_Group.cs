using System;
using System.Collections.Generic;
using ChaitAppProtocol;
namespace ChaitAppClient
{
    public partial class ChaitClient
    {
        #region Variables
        #endregion

        #region 群组协议回调
        // 加入群组事件
        public delegate void OnJoinGroupHandler(String neckName, String groupName);
        public event OnJoinGroupHandler OnJoinGroupEvent;
        // 离开群组事件
        public delegate void OnQuitGroupHandler(String neckName, String groupName);
        public event OnQuitGroupHandler OnQuitGroupEvent;
        // 群组呼喊事件
        public delegate void OnYellHandler(String neckName, String groupName, String yellStr);
        public event OnYellHandler OnYellEvent;

        // 获取群组列表事件
        public delegate void OnGroupListHandler(List<String> groupList);
        public event OnGroupListHandler OnGroupListEvent;
        // 大厅聊天信息事件
        public delegate void OnLobbyChatHandler(String neckName, String chatStr);
        public event OnLobbyChatHandler OnLobbyChatEvent;
        // 聊天信息事件
        public delegate void OnChatHandler(String neckName, String chatStr);
        public event OnChatHandler OnChatEvent;
        #endregion

        #region 客户端群组行为
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
        #endregion
    }
}