using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace ChaitAppClient.Video
{
    class AvicapManager
    {
        #region Contants
        private const int WM_USER = 0x400;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_STOP = WM_USER + 68;
        private const int WM_CAP_SAVEDIB = WM_USER + 25;
        private const int WM_CAP_EDIT_COPY = WM_USER + 30;
        private const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_USER + 6;
        private const int WM_CAP_SET_CALLBACK_ERROR = WM_USER + 2;
        private const int WM_CAP_SET_CALLBACK_STATUSA = WM_USER + 3;
        private const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
        private const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;
        private const int WM_CAP_SET_SCALE = WM_USER + 53;
        private const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
        private const int WM_CAP_SET_OVERLAY = WM_USER + 51;
        private const int WM_CAP_SET_PREVIEW = WM_USER + 50;
        private const int WM_CAP_SEQUENCE = WM_USER + 62;
        private const int WM_CAP_FILE_SET_CAPTURE_FILEA = WM_USER + 20;
        #endregion

        #region Variables
        private IntPtr hWndC;
        private bool bWorkStart = false;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;

        private Bitmap lastBitmap;      // 单机测试修正，如果无法获取剪贴板数据，则使用前面一副图像代替
        #endregion

        public AvicapManager(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;

            byte[] lpszName = new byte[100];
            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, 0, 0, mWidth, mHeight, mControlPtr, 0);
            if (hWndC.ToInt32() != 0)
            {
             bool a = SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
             bool b = SendMessage(hWndC, WM_CAP_SET_SCALE, 1, 0);
             bool c = SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);
             bool d = SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
            }
        }
        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);


        public Bitmap GrabImage()
        {
            IDataObject data;
            SendMessage(hWndC, WM_CAP_EDIT_COPY, 0, 0);
            try
            {
                data = Clipboard.GetDataObject();
                if (data == null)
                    return null;
                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    return (Bitmap)data.GetData(typeof(Bitmap));
                }
                return lastBitmap;
            }
            catch (System.Exception ex)
            {
                return lastBitmap;
            }
        }

        public void Stop()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
            bWorkStart = false;
        }

        public void GrabImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }
    }
}
