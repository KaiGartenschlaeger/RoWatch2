using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Rowatch2.Librarys
{
    static class MouseHook
    {
        public static void Start()
        {
            if (_hookID == IntPtr.Zero)
            {
                _hookID = SetHook(_proc);
            }
        }

        public static void Stop()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        
        public static event EventHandler<MouseHookEventArgs> MouseEvent;
        
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                MouseHookEventType type = MouseHookEventType.LButtonDown;
                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.WM_LBUTTONDOWN:
                        type = MouseHookEventType.LButtonDown;
                        break;
                    case MouseMessages.WM_LBUTTONUP:
                        type = MouseHookEventType.LButtonUp;
                        break;

                    case MouseMessages.WM_RBUTTONDOWN:
                        type = MouseHookEventType.RButtonDown;
                        break;
                    case MouseMessages.WM_RBUTTONUP:
                        type = MouseHookEventType.RButtonUp;
                        break;

                    case MouseMessages.WM_MOUSEWHEEL:
                        type = MouseHookEventType.MouseWheel;
                        break;

                    case MouseMessages.WM_MOUSEMOVE:
                        type = MouseHookEventType.MouseMove;
                        break;
                }

                if (MouseEvent != null)
                {
                    MouseEvent(null, new MouseHookEventArgs()
                    {
                        Type = type,
                        X = hookStruct.pt.x,
                        Y = hookStruct.pt.y
                    });
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    enum MouseHookEventType
    {
        LButtonDown,
        LButtonUp,
        RButtonDown,
        RButtonUp,
        MouseMove,
        MouseWheel
    }

    class MouseHookEventArgs : EventArgs
    {
        public MouseHookEventType Type { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}