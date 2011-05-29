using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChaitPresServer
{
    class RoleManager
    {
        #region Variavles
        private Dictionary<String, RoleMirror> roleDic = new Dictionary<string,RoleMirror>();
        #endregion

        #region Instance
        private static RoleManager instance;
        public static RoleManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new RoleManager();
                }
                return instance;
            }
        }
        private RoleManager()
        {
        }
        #endregion

        #region Update & Draw
        public void Update()
        {
            // 计算新的位置
            foreach (RoleMirror rm in roleDic.Values)
            {
                rm.Update();
            }
        }
        #endregion

        #region Role Manager
        public void OnJoinHandler(String roleName)
        {
            if(roleDic.Keys.Contains(roleName))
            {
                // ...
                return;
            }
            else
            {
                RoleMirror sr = new RoleMirror();
                sr.Name = roleName;
                sr.X = 100;
                sr.Y = 100;
                roleDic.Add(roleName, sr);
            }
        }

        public void OnSetTargetHandler(String roleName, int x, int y)
        {
            x = (int)MathHelper.Clamp(x, 0, 800);
            y = (int)MathHelper.Clamp(y, 0, 600);

            roleDic[roleName].TargetX = x;
            roleDic[roleName].TargetY = y;
        }
        #endregion
    }
}
