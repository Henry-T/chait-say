using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerApp
{
    public class NetResource
    {
        #region Instance
        private static NetResource instance;
        public static NetResource Instance
        {
            get
            {
                if (instance == null)
                    instance = new NetResource();
                return instance;
            }
        }
        private NetResource()
        {
        }
        #endregion

        private int baseNum = 3000;

        public void SetBaseNumber(int bn)
        {
            baseNum = bn;
        }

        public int GetNextUDPPort()
        {
            return ++baseNum;
        }
    }
}
