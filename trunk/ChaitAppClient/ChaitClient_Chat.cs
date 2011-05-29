using System;
using System.Collections.Generic;
using ChaitAppProtocol;
namespace ChaitAppClient
{
    public partial class ChaitClient
    {
        #region 聊天协议回调
        // 用户加入事件
        public delegate void OnJoinHandler(String neckName);
        public event OnJoinHandler OnJoinEvent;
        // 用户离开事件
        public delegate void OnQuitHandler(String neckName);
        public event OnQuitHandler OnQuitEvent;
        // 获取用户列表事件
        public delegate void OnNeckListHandler(List<String> neckList);
        public event OnNeckListHandler OnNeckListEvent;
        #endregion

        #region 客户端聊天行为
        public void Chat(String neckName, String chatStr)
        {
            String dataStr = System.Convert.ToChar(CProtocol.Chat).ToString();
            dataStr += (Char)System.Text.Encoding.UTF8.GetBytes(neckName).Length;
            dataStr += neckName;
            dataStr += chatStr;
            sendData(dataStr);
        }
        #endregion
    }
}