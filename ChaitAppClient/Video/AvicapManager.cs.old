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
        #endregion

        #region Struct
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSize;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biWidth;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biHeight;
            [MarshalAs(UnmanagedType.I2)]
            public short biPlanes;
            [MarshalAs(UnmanagedType.I2)]
            public short biBitCount;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biCompression;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSizeImage;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biXPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biYPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrUsed;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 40)]
            public BITMAPINFOHEADER bmiHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public Int32[] bmiColors;
        }
        #endregion

        public AvicapManager(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
        }
        [DllImport("avicap32.dll")]
        private static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        [DllImport("avicap32.dll")]
        private static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoForm, int wSize);

        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport("User32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);

        public void GrabImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }

        public Bitmap GrabImage()
        {
            IDataObject data;
            SendMessage(hWndC, WM_CAP_EDIT_COPY, 0, 0);
            data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(Bitmap)))
            {
                return (Bitmap)data.GetData(typeof(Bitmap));
            }
            return null;
        }

        public void Test()
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi("13.bmp");
            SendMessage(hWndC, WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }
        public void Start()
        {
            if (bWorkStart)
                return;
            bWorkStart = true;
            byte[] lpszName = new byte[100];
            hWndC = capCreateCaptureWindowA(lpszName, WS_CHILD | WS_VISIBLE, 0, 0, mWidth, mHeight, mControlPtr, 0);
            if (hWndC.ToInt32() != 0)
            {
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_VIDEOSTREAM,0,0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_ERROR,0,0);
                SendMessage(hWndC, WM_CAP_SET_CALLBACK_STATUSA,0,0);
                SendMessage(hWndC, WM_CAP_DRIVER_CONNECT, 0, 0);
                SendMessage(hWndC, WM_CAP_SET_SCALE,1,0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEWRATE, 66, 0);
                SendMessage(hWndC, WM_CAP_SET_OVERLAY, 1, 0);
                SendMessage(hWndC, WM_CAP_SET_PREVIEW, 1, 0);
                BITMAPINFO bmpInfo = new BITMAPINFO();
                SendMessage(hWndC, WM_CAP_SET_VIDEOFORMAT, Marshal.SizeOf(bmpInfo), ref bmpInfo);
            }
        }

        public void Stop()
        {
            SendMessage(hWndC, WM_CAP_DRIVER_DISCONNECT, 0, 0);
            bWorkStart = false;
        }

        public void Kinescope(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            SendMessage(hWndC, WM_CAP_FILE_SET_CAPTURE_FILEA, 0, hBmp.ToInt32());
            SendMessage(hWndC, WM_CAP_SEQUENCE, 0, 0);
        }

        public void StopKinescope(string path)
        {
            SendMessage(hWndC, WM_CAP_STOP, 0, 0);
        }
    }
}
