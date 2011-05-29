using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ChaitAppServer;

namespace ChaitPresServer
{
    class RoleMirror
    {
        public const int Speed = 10;     // 100 pixel/second
        public const int MinOffset = 5; // 最小移动间隙5px

        public String Name;
        public int X = 100;
        public int Y = 100;
        public int TargetX = 100;
        public int TargetY = 100;

        public void Update()
        {
            // 是否应该移动
            float dist = Vector2.Distance(new Vector2(X, Y), new Vector2(TargetX, TargetY));
            if (dist < MinOffset)
                return;

            // 线性插值
            Vector2 newPos;
            float amt = Speed/dist;
            newPos = Vector2.Lerp(new Vector2(X, Y), new Vector2(TargetX, TargetY), amt);

            // 更新位置
            X = (int)newPos.X;
            Y = (int)newPos.Y;

            // 修正位置 -- 这里引用了XNA类库函数
            // 服务器进行计算，将结果返回客户端
            X = (int)MathHelper.Clamp(X, 0f, 800f);
            Y = (int)MathHelper.Clamp(Y, 0f, 600f);

            // 广播位置 
            // TODO 需修改，广播行为应该在所有角色位置更新完成后，由Manager统一封装到一个数据包中发送
            ChaitServer.Instance.RolePos(Name, X, Y);
        }
    }
}
