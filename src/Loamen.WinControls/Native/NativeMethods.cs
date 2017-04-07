using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Loamen.WinControls
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        #region Fields

        public static HandleRef HWND_TOP;
        public static HandleRef HWND_TOPMOST;
        public static HandleRef HWND_BOTTOM;
        public static HandleRef NullHandleRef;

        private static int wmMouseEnterMessage;

        public static readonly int TTM_ADDTOOL;
        public static readonly int TTM_DELTOOL;
        public static readonly int TTM_ENUMTOOLS;
        public static readonly int TTM_GETCURRENTTOOL;
        public static readonly int TTM_GETTEXT;
        public static readonly int TTM_GETTOOLINFO;
        public static readonly int TTM_HITTEST;
        public static readonly int TTM_NEWTOOLRECT;
        public static readonly int TTM_SETTITLE;
        public static readonly int TTM_SETTOOLINFO;
        public static readonly int TTM_UPDATETIPTEXT;

        #endregion

        static NativeMethods()
        {
            HWND_TOP = new HandleRef(null, IntPtr.Zero);
            HWND_TOPMOST = new HandleRef(null, new IntPtr(-1));
            HWND_BOTTOM = new HandleRef(null, (IntPtr) 1);
            NullHandleRef = new HandleRef(null, IntPtr.Zero);
            wmMouseEnterMessage = -1;

            // RadToolTip
            if (Marshal.SystemDefaultCharSize == 1)
            {
                TTM_ADDTOOL = 0x404;
                TTM_SETTITLE = 0x420;
                TTM_DELTOOL = 0x405;
                TTM_NEWTOOLRECT = 0x406;
                TTM_GETTOOLINFO = 0x408;
                TTM_SETTOOLINFO = 0x409;
                TTM_HITTEST = 0x40a;
                TTM_GETTEXT = 0x40b;
                TTM_UPDATETIPTEXT = 0x40c;
                TTM_ENUMTOOLS = 0x40e;
                TTM_GETCURRENTTOOL = 0x40f;
            }
            else
            {
                TTM_ADDTOOL = 0x432;
                TTM_SETTITLE = 0x421;
                TTM_DELTOOL = 0x433;
                TTM_NEWTOOLRECT = 0x434;
                TTM_GETTOOLINFO = 0x435;
                TTM_SETTOOLINFO = 0x436;
                TTM_HITTEST = 0x437;
                TTM_GETTEXT = 0x438;
                TTM_UPDATETIPTEXT = 0x439;
                TTM_ENUMTOOLS = 0x43a;
                TTM_GETCURRENTTOOL = 0x43b;
            }
        }

        #region Constants

        #region Messages

        public const int WM_KEYFIRST = 0x100;
        public const int WM_KEYLAST = 0x108;
        public const int WM_MOUSEFIRST = 0x200;
        public const int WM_MOUSELAST = 0x20a;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_CHAR = 0x102;
        public const int WM_SYSCHAR = 0x106;
        public const int WM_MOUSEACTIVATE = 0x21;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_ACTIVATE = 6;
        public const int WM_ACTIVATEAPP = 0x1c;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCDESTROY = 130;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCMBUTTONDBLCLK = 0xa9;
        public const int WM_NCMBUTTONDOWN = 0xa7;
        public const int WM_NCMBUTTONUP = 0xa8;
        public const int WM_NCMOUSELEAVE = 0x2a2;
        public const int WM_NCMOUSEMOVE = 160;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCRBUTTONDBLCLK = 0xa6;
        public const int WM_NCRBUTTONDOWN = 0xa4;
        public const int WM_NCRBUTTONUP = 0xa5;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 520;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_XBUTTONDBLCLK = 0x20d;
        public const int WM_XBUTTONDOWN = 0x20b;
        public const int WM_XBUTTONUP = 0x20c;
        public const int WM_PAINT = 15;
        public const int WM_ERASEBKGND = 20;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_CAPTURECHANGED = 0x215;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x31e;
        public const int WM_NCUAHDRAWCAPTION = 0xae;
        public const int WM_NCUAHDRAWFRAME = 0xaf;
        public const int WM_SIZE = 5;
        public const int WM_SIZING = 0x214;
        public const int WM_MOVE = 3;
        public const int WM_MOVING = 0x216;
        public const int WM_GETMINMAXINFO = 0x24;
        public const int WM_PRINT = 0x317;
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        public const int WM_MOUSEWHEEL = 0x20a;
        public const int WM_SETFOCUS = 7;
        public const int WM_KILLFOCUS = 8;
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_POPUPSYSTEMMENU = 0x313;
        public const int WM_SETTEXT = 12;
        public const int WM_SETICON = 0x80;
        public const int WM_STYLECHANGED = 0x7d;
        public const int WM_MDIACTIVATE = 0x222;
        public const int WM_WINDOWPOSCHANGED = 0x47;
        public const int WM_WINDOWPOSCHANGING = 70;
        public const int WM_MOUSELEAVE = 0x2a3;
        public const int WM_SETREDRAW = 11;
        public const int WM_PARENTNOTIFY = 0x210;

        #endregion

        #region Window styles

        public const int WS_BORDER = 0x800000;
        public const int WS_CAPTION = 0xc00000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_CLIPCHILDREN = 0x2000000;
        public const int WS_CLIPSIBLINGS = 0x4000000;
        public const int WS_DISABLED = 0x8000000;
        public const int WS_DLGFRAME = 0x400000;
        public const int WS_EX_APPWINDOW = 0x40000;
        public const int WS_EX_CLIENTEDGE = 0x200;
        public const int WS_EX_CONTEXTHELP = 0x400;
        public const int WS_EX_CONTROLPARENT = 0x10000;
        public const int WS_EX_DLGMODALFRAME = 1;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WS_EX_LAYOUTRTL = 0x400000;
        public const int WS_EX_LEFT = 0;
        public const int WS_EX_LTRREADING = 0x00000000;
        public const int WS_EX_LEFTSCROLLBAR = 0x4000;
        public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;
        public const int WS_EX_MDICHILD = 0x40;
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_RIGHT = 0x1000;
        public const int WS_EX_RTLREADING = 0x2000;
        public const int WS_EX_STATICEDGE = 0x20000;
        public const int WS_EX_TOOLWINDOW = 0x80;
        public const int WS_EX_TOPMOST = 8;
        public const int WS_HSCROLL = 0x100000;
        public const int WS_MAXIMIZE = 0x1000000;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_OVERLAPPED = 0;
        public const int WS_POPUP = -2147483648;
        public const int WS_SYSMENU = 0x80000;
        public const int WS_TABSTOP = 0x10000;
        public const int WS_THICKFRAME = 0x40000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_VSCROLL = 0x200000;

        public const int CS_DBLCLKS = 8;
        public const int CS_DROPSHADOW = 0x20000;
        public const int CS_SAVEBITS = 0x800;

        #endregion

        #region Hit points

        public const int HTERROR = -2;
        public const int HTTRANSPARENT = -1;
        public const int HTNOWHERE = 0;
        public const int HTCLIENT = 1;
        public const int HTCAPTION = 2;
        public const int HTSYSMENU = 3;
        public const int HTGROWBOX = 4;
        public const int HTSIZE = HTGROWBOX;
        public const int HTMENU = 5;
        public const int HTHSCROLL = 6;
        public const int HTVSCROLL = 7;
        public const int HTMINBUTTON = 8;
        public const int HTMAXBUTTON = 9;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTBORDER = 18;
        public const int HTREDUCE = HTMINBUTTON;
        public const int HTZOOM = HTMAXBUTTON;
        public const int HTSIZEFIRST = HTLEFT;
        public const int HTSIZELAST = HTBOTTOMRIGHT;
        public const int HTOBJECT = 19;
        public const int HTCLOSE = 20;
        public const int HTHELP = 21;

        #endregion

        #region BitBlt Operations

        public const int SRCAND = 0x8800c6;
        public const int SRCCOPY = 0xcc0020;
        public const int SRCPAINT = 0xee0086;

        #endregion

        #region SetWindowPos

        public const int SWP_DRAWFRAME = 0x20;
        public const int SWP_NOSENDCHANGING = 0x400;
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_FRAMECHANGED = SWP_DRAWFRAME;
        public const int SWP_HIDEWINDOW = 0x80;
        public const int SWP_NOACTIVATE = 0x10;
        public const int SWP_NOCOPYBITS = 0x100;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOOWNERZORDER = 0x200;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOZORDER = 4;
        public const int SWP_NOREDRAW = 0x0008;
        public const int SWP_SHOWWINDOW = 0x40;

        #endregion

        #region Virtual Keys

        public const int VK_RETURN = 0x0D;
        public const int VK_CONTROL = 0x11;
        public const int VK_DOWN = 40;
        public const int VK_ESCAPE = 0x1b;
        public const int VK_INSERT = 0x2d;
        public const int VK_LEFT = 0x25;
        public const int VK_MENU = 0x12;
        public const int VK_RIGHT = 0x27;
        public const int VK_SHIFT = 0x10;
        public const int VK_TAB = 9;
        public const int VK_UP = 0x26;
        public const int VK_SPACE = 0x20;

        #endregion

        #region SysCommand types

        public const int SC_CLOSE = 0xf060;
        public const int SC_CONTEXTHELP = 0xf180;
        public const int SC_KEYMENU = 0xf100;
        public const int SC_MAXIMIZE = 0xf030;
        public const int SC_MINIMIZE = 0xf020;
        public const int SC_MOVE = 0xf010;
        public const int SC_RESTORE = 0xf120;
        public const int SC_SIZE = 0xf000;

        #endregion

        #region Track movement

        public const int TME_HOVER = 1;
        public const int TME_LEAVE = 2;
        public const int TME_NONCLIENT = 0x10;
        public const int TME_QUERY = 0x40000000;
        public const int TME_CANCEL = 0x8;

        #endregion

        #region Print areas

        public const int PRF_CHECKVISIBLE = 1;
        public const int PRF_CHILDREN = 0x10;
        public const int PRF_CLIENT = 4;
        public const int PRF_ERASEBKGND = 8;
        public const int PRF_OWNED = 0x00000020;
        public const int PRF_NONCLIENT = 2;

        #endregion

        #region Graphics objects

        public const int OBJ_BITMAP = 7;
        public const int OBJ_BRUSH = 2;
        public const int OBJ_DC = 3;
        public const int OBJ_ENHMETADC = 12;
        public const int OBJ_EXTPEN = 11;
        public const int OBJ_FONT = 6;
        public const int OBJ_MEMDC = 10;
        public const int OBJ_METADC = 4;
        public const int OBJ_METAFILE = 9;
        public const int OBJ_PAL = 5;
        public const int OBJ_PEN = 1;
        public const int OBJ_REGION = 8;

        #endregion

        public const int EM_POSFROMCHAR = 0xd6;
        public const int EM_LINEFROMCHAR = 0xc9;

        public const int SW_SHOWNOACTIVATE = 4;
        public const int WA_ACTIVE = 1;
        public const int WA_CLICKACTIVE = 2;
        public const int MA_NOACTIVATE = 3;

        public const int DCX_CACHE = 2;
        public const int DCX_INTERSECTRGN = 0x80;
        public const int DCX_LOCKWINDOWUPDATE = 0x400;
        public const int DCX_WINDOW = 1;
        public const int DCX_CLIPSIBLINGS = 0x00000010;
        public const int DCX_VALIDATE = 0x00200000;

        public const int SIZE_MAXIMIZED = 2;
        public const int SIZE_MINIMIZED = 1;
        public const int SIZE_RESTORED = 0;

        public const int RDW_ALLCHILDREN = 0x80;
        public const int RDW_ERASE = 4;
        public const int RDW_ERASENOW = 0x200;
        public const int RDW_FRAME = 0x400;
        public const int RDW_INVALIDATE = 1;
        public const int RDW_UPDATENOW = 0x100;

        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;

        public const int LWA_ALPHA = 2;
        public const int LWA_COLORKEY = 1;

        public const int DWM_BB_ENABLE = 0x00000001;
        public const int DWM_BB_BLURREGION = 0x00000002;
        public const int DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;

        public const int GW_CHILD = 5;
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_ID = -12;
        public const int GWL_STYLE = -16;
        public const int GWL_WNDPROC = -4;
        public const int GA_PARENT = 1;
        public const int GA_ROOT = 2;

        public const int TTM_GETDELAYTIME = 0x415;

        public static int WM_MOUSEENTER
        {
            get
            {
                if (wmMouseEnterMessage == -1)
                {
                    wmMouseEnterMessage = RegisterWindowMessage("WinFormsMouseEnter");
                }
                return wmMouseEnterMessage;
            }
        }

        #endregion

        #region Nested Types

        [StructLayout(LayoutKind.Sequential)]
        public class BITMAP
        {
            public int bmType;
            public int bmWidth;
            public int bmHeight;
            public int bmWidthBytes;
            public short bmPlanes;
            public short bmBitsPixel;
            public IntPtr bmBits;

            public BITMAP()
            {
                bmBits = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFO
        {
            public int bmiHeader_biSize;
            public int bmiHeader_biWidth;
            public int bmiHeader_biHeight;
            public short bmiHeader_biPlanes;
            public short bmiHeader_biBitCount;
            public int bmiHeader_biCompression;
            public int bmiHeader_biSizeImage;
            public int bmiHeader_biXPelsPerMeter;
            public int bmiHeader_biYPelsPerMeter;
            public int bmiHeader_biClrUsed;
            public int bmiHeader_biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;

            internal BITMAPINFO()
            {
                bmiHeader_biSize = 40;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public sealed class CommonHandles
        {
            // Methods

            // Fields
            public static readonly int Accelerator;
            public static readonly int CompatibleHDC;
            public static readonly int Cursor;
            public static readonly int EMF;
            public static readonly int Find;
            public static readonly int GDI;
            public static readonly int HDC;
            public static readonly int Icon;
            public static readonly int Kernel;
            public static readonly int Menu;
            public static readonly int Window;

            static CommonHandles()
            {
                Accelerator = HandleCollector.RegisterType("Accelerator", 80, 50);
                Cursor = HandleCollector.RegisterType("Cursor", 20, 500);
                EMF = HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
                Find = HandleCollector.RegisterType("Find", 0, 0x3e8);
                GDI = HandleCollector.RegisterType("GDI", 50, 500);
                HDC = HandleCollector.RegisterType("HDC", 100, 2);
                CompatibleHDC = HandleCollector.RegisterType("ComptibleHDC", 50, 50);
                Icon = HandleCollector.RegisterType("Icon", 20, 500);
                Kernel = HandleCollector.RegisterType("Kernel", 0, 0x3e8);
                Menu = HandleCollector.RegisterType("Menu", 30, 0x3e8);
                Window = HandleCollector.RegisterType("Window", 5, 0x3e8);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class INITCOMMONCONTROLSEX
        {
            public int dwSize;
            public int dwICC;

            public INITCOMMONCONTROLSEX()
            {
                dwSize = 8;
            }
        }

        public struct IconInfo
        {
            public bool fIcon;
            public IntPtr hbmColor;
            public IntPtr hbmMask;
            public int xHotspot;
            public int yHotspot;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class LOGBRUSH
        {
            public int lbStyle;
            public int lbColor;
            public IntPtr lbHatch;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)] public string lfFaceName;

            public LOGFONT()
            {
            }

            public LOGFONT(LOGFONT lf)
            {
                lfHeight = lf.lfHeight;
                lfWidth = lf.lfWidth;
                lfEscapement = lf.lfEscapement;
                lfOrientation = lf.lfOrientation;
                lfWeight = lf.lfWeight;
                lfItalic = lf.lfItalic;
                lfUnderline = lf.lfUnderline;
                lfStrikeOut = lf.lfStrikeOut;
                lfCharSet = lf.lfCharSet;
                lfOutPrecision = lf.lfOutPrecision;
                lfClipPrecision = lf.lfClipPrecision;
                lfQuality = lf.lfQuality;
                lfPitchAndFamily = lf.lfPitchAndFamily;
                lfFaceName = lf.lfFaceName;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class LOGPEN
        {
            public int lopnStyle;
            public int lopnWidth_x;
            public int lopnWidth_y;
            public int lopnColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public RECT[] rgrc;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public int rcPaint_left;
            public int rcPaint_top;
            public int rcPaint_right;
            public int rcPaint_bottom;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;

            public POINT()
            {
            }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTSTRUCT
        {
            public int x;
            public int y;

            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public static RECT FromRectangle(Rectangle rect)
            {
                return new RECT(rect.Left,
                                rect.Top,
                                rect.Right,
                                rect.Bottom);
            }

            public Size Size
            {
                get { return new Size(right - left, bottom - top); }
            }

            public Rectangle Rect
            {
                get { return new Rectangle(left, top, right - left, bottom - top); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SIZE
        {
            public int cx;
            public int cy;

            public SIZE()
            {
            }

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZESTRUCT
        {
            public int cx;
            public int cy;

            public SIZESTRUCT(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO_TOOLTIP
        {
            public int cbSize;
            public int uFlags;
            public IntPtr hwnd;
            public IntPtr uId;
            public RECT rect;
            public IntPtr hinst;
            public IntPtr lpszText;
            public IntPtr lParam;

            public TOOLINFO_TOOLTIP()
            {
                cbSize = Marshal.SizeOf(typeof (TOOLINFO_TOOLTIP));
                hinst = IntPtr.Zero;
                lParam = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TRACKMOUSEEVENT
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr hwndTrack;
            public int dwHoverTime;

            public TRACKMOUSEEVENT()
            {
                cbSize = Marshal.SizeOf(typeof (TRACKMOUSEEVENT));
                dwHoverTime = 100;
            }
        }

        public static class Util
        {
            private static int GetEmbeddedNullStringLengthAnsi(string s)
            {
                int num1 = s.IndexOf('\0');
                if (num1 > -1)
                {
                    string text1 = s.Substring(0, num1);
                    string text2 = s.Substring(num1 + 1);
                    return ((GetPInvokeStringLength(text1) + GetEmbeddedNullStringLengthAnsi(text2)) + 1);
                }
                return GetPInvokeStringLength(s);
            }

            public static int GetPInvokeStringLength(string s)
            {
                if (s == null)
                {
                    return 0;
                }
                if (Marshal.SystemDefaultCharSize == 2)
                {
                    return s.Length;
                }
                if (s.Length == 0)
                {
                    return 0;
                }
                if (s.IndexOf('\0') > -1)
                {
                    return GetEmbeddedNullStringLengthAnsi(s);
                }
                return lstrlen(s);
            }

            public static int HIWORD(int n)
            {
                return ((n >> 0x10) & 0xffff);
            }

            public static int HIWORD(IntPtr n)
            {
                return HIWORD((int) ((long) n));
            }

            public static int LOWORD(int n)
            {
                return (n & 0xffff);
            }

            public static int LOWORD(IntPtr n)
            {
                return LOWORD((int) ((long) n));
            }

            public static int MAKELONG(int low, int high)
            {
                return ((high << 0x10) | (low & 0xffff));
            }

            public static IntPtr MAKELPARAM(int low, int high)
            {
                return (IntPtr) ((high << 0x10) | (low & 0xffff));
            }

            public static int SignedHIWORD(int n)
            {
                return (short) ((n >> 0x10) & 0xffff);
            }

            public static int SignedHIWORD(IntPtr n)
            {
                return SignedHIWORD((int) ((long) n));
            }

            public static int SignedLOWORD(int n)
            {
                return (short) (n & 0xffff);
            }

            public static int SignedLOWORD(IntPtr n)
            {
                return SignedLOWORD((int) ((long) n));
            }

            public static int LowOrder(int param)
            {
                return (param & 0xffff);
            }

            public static int HighOrder(int param)
            {
                return ((param >> 0x10) & 0xffff);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        #endregion

        #region Kernel32

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string libname);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int lstrlen(string s);

        #endregion

        #region User32

        #region SendMessage Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, bool wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In, Out] ref Rectangle lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg,
                                                [In, Out, MarshalAs(UnmanagedType.Bool)] ref bool wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, ref short wParam, ref short lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, int msg, int wParam,
                                             [MarshalAs(UnmanagedType.IUnknown)] out object editOle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, bool wparam, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, POINT lParam);

        #endregion

        #region PostMessage Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, IntPtr lparam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy,
                                               int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RedrawWindow(HandleRef hwnd, IntPtr rcUpdate, HandleRef hrgnUpdate, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RedrawWindow(HandleRef hwnd, ref RECT rcUpdate, HandleRef hrgnUpdate, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr _WindowFromPoint(POINTSTRUCT pt);

        [DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr IntGetDC(HandleRef hWnd);

        [DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr IntGetWindowDC(HandleRef hWnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINTSTRUCT pptDst,
                                                     ref SIZESTRUCT psize, IntPtr hdcSrc, ref POINTSTRUCT pprSrc,
                                                     int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindow(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsZoomed(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsIconic(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(HandleRef handle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ClientToScreen(HandleRef hWnd, [In, Out] POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In, Out] ref RECT rect,
                                                 int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In, Out] POINT pt, int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetLayeredWindowAttributes(HandleRef hwnd, int crKey, byte bAlpha, int dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool MessageBeep(int type);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        #endregion

        #region GDI32

        [DllImport("Gdi32.dll")]
        public static extern bool GetTextMetrics(IntPtr hdc, ref TEXTMETRIC tm);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        public static extern bool IntDeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc,
                                         int ySrc, int dwRop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        internal static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        private static extern bool IntDeleteDC(HandleRef hDC);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
                                                       int nWidthEllipse, int nHeightEllipse);

        [DllImport("gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        private static extern IntPtr IntCreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel,
                                                     IntPtr lpvBits);

        [DllImport("gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        private static extern IntPtr IntCreateBitmapByte(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel,
                                                         byte[] lpvBits);

        [DllImport("gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        private static extern IntPtr IntCreateBitmapShort(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel,
                                                          short[] lpvBits);

        [DllImport("gdi32.dll", EntryPoint = "CreateBrushIndirect", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        private static extern IntPtr IntCreateBrushIndirect(LOGBRUSH lb);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr CreateDIBSection(IntPtr hdc,
                                                       [In, MarshalAs(UnmanagedType.LPStruct)] BITMAPINFO pbmi,
                                                       uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        public static extern bool PtInRegion(IntPtr hRgn, int x, int y);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string strDriver, string strDevice, string strOutput, IntPtr pData);

        [DllImport("gdi32.dll")]
        public static extern int GetPixel(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll", EntryPoint = "SaveDC", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true
            )]
        public static extern int SaveDC(HandleRef hDC);

        [DllImport("gdi32.dll", EntryPoint = "RestoreDC", CharSet = CharSet.Auto, SetLastError = true,
            ExactSpelling = true)]
        public static extern bool RestoreDC(HandleRef hDC, int nSavedDC);

        #endregion

        #region COMCTL32

        [CLSCompliant(false), DllImport("comctl32.dll", ExactSpelling = true)]
        public static extern bool _TrackMouseEvent(TRACKMOUSEEVENT tme);

        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);

        #endregion

        public static IntPtr GetDC(HandleRef hWnd)
        {
            return HandleCollector.Add(IntGetDC(hWnd), CommonHandles.HDC);
        }

        public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
        {
            return HandleCollector.Add(IntGetDCEx(hWnd, hrgnClip, flags), CommonHandles.HDC);
        }

        public static IntPtr GetWindowDC(HandleRef hWnd)
        {
            return HandleCollector.Add(IntGetWindowDC(hWnd), CommonHandles.HDC);
        }

        public static IntPtr CreateCompatibleDC(HandleRef hDC)
        {
            return HandleCollector.Add(IntCreateCompatibleDC(hDC), CommonHandles.CompatibleHDC);
        }

        public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
        {
            HandleCollector.Remove((IntPtr) hDC, CommonHandles.HDC);
            return IntReleaseDC(hWnd, hDC);
        }

        public static bool DeleteDC(HandleRef hDC)
        {
            HandleCollector.Remove((IntPtr) hDC, CommonHandles.HDC);
            return IntDeleteDC(hDC);
        }

        public static IntPtr WindowFromPoint(int x, int y)
        {
            var pointstruct1 = new POINTSTRUCT(x, y);
            return _WindowFromPoint(pointstruct1);
        }

        public static bool DeleteObject(HandleRef hObject)
        {
            HandleCollector.Remove((IntPtr) hObject, CommonHandles.GDI);
            return IntDeleteObject((IntPtr) hObject);
        }

        public static void UpdateZOrder(HandleRef handle, HandleRef pos, bool activate)
        {
            int flags = 0x603;
            if (!activate)
            {
                flags |= 0x10;
            }
            SetWindowPos(handle, pos, 0, 0, 0, 0, flags);
        }

        public static Region CreateRoundRectRgn(Rectangle bounds, int radius)
        {
            IntPtr region = CreateRoundRectRgn(bounds.X, bounds.Y, bounds.Width + 1, bounds.Height + 1, radius, radius);
            Region roundRegion = Region.FromHrgn(region);
            DeleteObject(new HandleRef(null, region));
            return roundRegion;
        }

        public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits)
        {
            return HandleCollector.Add(IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits),
                                       CommonHandles.GDI);
        }

        public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits)
        {
            return HandleCollector.Add(IntCreateBitmapByte(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits),
                                       CommonHandles.GDI);
        }

        public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits)
        {
            return HandleCollector.Add(IntCreateBitmapShort(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits),
                                       CommonHandles.GDI);
        }

        public static IntPtr CreateBrushIndirect(LOGBRUSH lb)
        {
            return HandleCollector.Add(IntCreateBrushIndirect(lb), CommonHandles.GDI);
        }

        #region RadToolTip

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(HandleRef hWnd, int nIndex);

        public static IntPtr GetClassLongPtr(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        [DllImport("user32.dll", EntryPoint = "SetClassLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClassLongPtr32(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClassLongPtr64(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

        public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetClassLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetClassLongPtr64(hWnd, nIndex, dwNewLong);
        }

        #endregion

        #region RadDock

        [Flags]
        public enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        [DllImport("user32.dll", EntryPoint = "AnimateWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

        #endregion

        #region PDF export

        public delegate int FontEnumDelegate(
            [MarshalAs(UnmanagedType.Struct)] ref EnumLogFont lpelf,
            [MarshalAs(UnmanagedType.Struct)] ref NewTextMetric lpntm, int fontType, int lParam);

        public enum GdiDcObject
        {
            Pen = 1,
            Brush = 2,
            Pal = 5,
            Font = 6,
            Bitmap = 7
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int GetTextFace(IntPtr hdc, int nCount,
                                             [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFaceName);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int GetFontUnicodeRanges(IntPtr hdc, [Out, MarshalAs(UnmanagedType.LPStruct)] GlyphSet lpgs);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int GetFontData(IntPtr hdc, int dwTable, int dwOffset,
                                             [MarshalAs(UnmanagedType.LPArray)] byte[] lpvBuffer, int cbData);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int GetGlyphIndices(IntPtr hdc, string lpstr, int c,
                                                 [MarshalAs(UnmanagedType.LPArray)] Int16[] pgi, int fl);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCurrentObject(IntPtr hdc, GdiDcObject uObjectType);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCurrentObject(HandleRef hdc, int uObjectType);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int EnumFontFamilies(IntPtr hdc, [MarshalAs(UnmanagedType.LPTStr)] string lpszFamily,
                                                  FontEnumDelegate lpEnumFontFamProc, int lParam);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int EnumFontFamiliesEx(IntPtr hdc, [MarshalAs(UnmanagedType.LPStruct)] LOGFONT lplf,
                                                    FontEnumDelegate lpEnumFontFamProc, int lParam, int dwFlags);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFontIndirect([MarshalAs(UnmanagedType.LPStruct)] LOGFONT lplf);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int AddFontResourceEx([In, MarshalAs(UnmanagedType.LPTStr)] string lpszFilename, int fl,
                                                   int pdv);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct EnumLogFont
        {
            public LOGFONT elfLogFont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public char[] elfFullName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] public char[] elfStyle;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class GlyphSet
        {
            public int cbThis = 0;
            public int flAccel = 0;
            public int cGlyphsSupported = 0;
            public int cRanges = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20000)] public byte[] ranges = null;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NewTextMetric
        {
            public long tmHeight;
            public long tmAscent;
            public long tmDescent;
            public long tmInternalLeading;
            public long tmExternalLeading;
            public long tmAvecharWidth;
            public long tmMaxcharWidth;
            public long tmWeight;
            public long tmOverhang;
            public long tmDigitizedAspectX;
            public long tmDigitizedAspectY;
            public char tmFirstchar;
            public char tmLastchar;
            public char tmDefaultchar;
            public char tmBreakchar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmcharSet;
            public ulong ntmFlags;
            public int ntmSizeEM;
            public int ntmCellHeight;
            public int ntmAvgWidth;
        }

        #endregion

        #region GetObject

        public static int GetObject(HandleRef hObject, LOGBRUSH lb)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof (LOGBRUSH)), lb);
        }

        public static int GetObject(HandleRef hObject, LOGFONT lp)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof (LOGFONT)), lp);
        }

        public static int GetObject(HandleRef hObject, LOGPEN lp)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof (LOGPEN)), lp);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, ref int nEntries);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] BITMAP bm);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGPEN lp);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, int[] nEntries);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGBRUSH lb);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGFONT lf);

        #endregion
    }
}