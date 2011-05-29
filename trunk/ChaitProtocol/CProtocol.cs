using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChaitProtocol
{
    public class CProtocol
    {
        #region 协议功能说明
        // ===========================================================================================
        // 协议已支持：
        // ===========================================================================================
        // 1 聊天室大厅可以供用户向所有人发送公共消息
        // 2 用户可以选择大厅中的用户进行私聊
        // 3 同一个IP创建多个客户端（根据Socket辨别用户）
        // 4 基本的昵称检测，禁止昵称重复

        // ===========================================================================================
        // 协议将支持：
        // ===========================================================================================
        // 1 用户可以自由分组来进行小范围群聊
        // 2 用户可以选择是否接受群组邀请
        // 3 用户可以选择自由组中的用户进行私聊
        // 4 随时获取所有在线用户列表
        // 5 用户权限 - 如：已连接但是未提交昵称的用户只能够进行提交昵称操作
        #endregion


        // ===========================================================================================
        // 网络配置
        // ===========================================================================================
        public const int FilePort = 5342;   // 文件传输侦听端口


        // =========================================================================================== 
        // Client -> Server                         名称            目标      格式
        // ===========================================================================================
        // 目标-服务器：客户端产生的聊天和文件控制信息必须经过服务器，经服务器分析处理进行进一步操作

        // 聊天
        public const int Join = 11;             // 加入聊天室消息  服务器     Join NeckLength MyNeck
        public const int Quit = 12;             // 退出聊天室      服务器     Quit
        public const int NeckList = 14;         // 获取昵称列表    服务器     FriendList
        public const int LobbyChat = 15;        // 大厅聊天        服务器     LobbyChat ChatString
        public const int Chat = 13;             // 发送聊天消息    服务器     Chat NeckLength TargetNeck ChatString

        // 群组
        public const int JoinGroup = 101;       // 加入/创建群组   服务器     JoinGroup    GroupName
        public const int QuitGroup = 102;       // 退出群组        服务器     QuitGroup    GroupName
        public const int GroupList = 104;       // 获取群组列表    服务器
        public const int Yell = 103;            // 群组内呼喊      服务器     Yell         GroupLength GroupName YellString

        // 文件
        public const int FileRequest = 201;     // 文件发送请求    服务器     FileRequest  NeckLength RecieverNeck SafeFileName
        public const int FileAccept = 202;      // 文件接收方接受  服务器     FileAccept   NeckLength SenderNeck SafeFileName -- [S->C]: FileAccept NeckLength NeckName FileNameLength SafeFileName IPAddr
        public const int FileRefuse = 203;      // 文件接收方拒绝  服务器     FileRefuse   NeckLength SenderName SafeFileName
        public const int FilePause = 204;       // 文件传递暂停    服务器       FilePause  NeckLength NeckName SaveFileName
        public const int FileResume = 205;      // 文件传递继续    服务器       FileResume NeckLength NeckName SaveFileName
        public const int FileStop = 206;        // 文件传递停止    服务器       FileStop   NeckLength NeckName SaveFileName


        // =========================================================================================== 
        // Server -> Client                         名称          目标          格式
        // ===========================================================================================
        // 目标-全体：信息将无差异地发送给所有客户端
        // 目标-全体/特例：信息将有差异地发送给所有客户端
        //                 发送给某些客户端的信息稍有不同或对某些客户端进行特殊操作
        //                 特例客户端本身也通常需要对这种信息进行特殊的表示
        //                 例如：加入退出聊天室、踢出等
        // 目标-单一：信息只发送给指定的客户端
        //                 例如：昵称已被使用
        // 目标-群组：信息将无差异地发送给所有群组成员
        // 目标-群组/特例：信息有差异地发送给群组的所有成员，
        //                 某些客户端收到信息的差异性表现在，信息内容不同或者服务器会对客户端进行一些特殊操作

        // 服务器
        public const int ServerStop = 1;        // 服务器关闭      全体       ServerStop
        public const int Kick = 2;              // 踢出聊天室      全体/特例  Kick  KickedNeck
        public const int NeckExist = 3;         // 昵称重复        单一       NeckExist

        // 聊天       
        //public const int Join = 11;             // 加入聊天室      全体/特例  Join        NewJoinNeck
        //public const int Quit = 12;             // 退出聊天室      全体       Quit        NeckLength QuitNeck
        //public const int FriendsList = 14;      // 获取昵称列表    单一       FriendList  ClientNum NeckLen1 Neck1 NeckLen2 Neck2 ... 
        //public const int LobbyChat = 15;        // 大厅聊天        全体       LobbyChat   NeckLength SourceNeck ChatString
        //public const int Chat = 13;             // 发送聊天消息    单一       Chat        NeckLength SourceNeck ChatString

        // 群组
        //public const int JoinGroup = 101;       // 加入群组        群组/特例  JoinGroup    NeckLength NeckName GroupName
        //public const int QuitGroup = 102;       // 退出群组        群组/特例  QuitGroup    NeckLength NeckName GroupName
        //public const int GroupList = 104;       // 获取群组列表    单一       GroupList    GroupNum GroupLen1 GroupName1 GroupLen2 GroupName2 ...
        //public const int Yell = 103;            // 群组内呼喊      群组       Yell         NeckLength NeckName GroupLength GroupName YellString

        // 文件
        //public const int FileRequest = 201;     // 文件发送请求    单一       FileRequest  NeckLength SenderName SafeFileName
        //public const int FileAccept = 202;      // 文件接收方接受  单一       FileAccept   NeckLength RecieverNeck FileNameLength SafeFileName IPAddr
        //public const int FileRefuse = 203;      // 文件接收方拒绝  单一       FileRefuse   NeckLength RecieverNeck SafeFileName
        //public const int FilePause = 204;       // 文件传递暂停    单一       FilePause  NeckLength NeckName SaveFileName
        //public const int FileResume = 205;      // 文件传递继续    单一       FileResume NeckLength NeckName SaveFileName
        //public const int FileStop = 206;        // 文件传递停止    单一       FileStop   NeckLength NeckName SaveFileName


        //============================================================================================
        // Client -> Client                         名称          目标          格式
        //============================================================================================
        // 文件数据传递                                           单一
    }
}
