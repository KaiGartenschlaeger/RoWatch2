using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Rowatch2.Librarys
{
    public static class SendInputManager
    {
        #region Win API

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_KEYDOWN = 0x0;
        private const int KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        private enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        #endregion

        #region Methods

        public static void Press(VKey key)
        {
            keybd_event((byte)key, 0, KEYEVENTF_KEYDOWN, 0);
        }

        public static void Release(VKey key)
        {
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
        }

        public static bool SendInput(VKey key)
        {
            Press(key);
            Release(key);

            return true;
        }

        public static string GetKeyDescription(VKey key)
        {
            FieldInfo fi = key.GetType().GetField(key.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return key.ToString();
            }
        }

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void MouseClick(int x, int y, int delay)
        {
            Cursor.Position = new Point(x, y);

            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);

            if (delay > 0)
            {
                Thread.Sleep(100);
            }

            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        #endregion
    }

    public enum VKey : ushort
    {
        [Description("Umschalt")]
        SHIFT = 0x10,
        [Description("Strg")]
        CONTROL = 0x11,
        [Description("Alt")]
        MENU = 0x12,
        [Description("Escape")]
        ESCAPE = 0x1B,
        [Description("Löschen")]
        BACK = 0x08,
        [Description("Tabulator")]
        TAB = 0x09,
        [Description("Enter")]
        RETURN = 0x0D,
        [Description("Bild hoch")]
        PRIOR = 0x21,
        [Description("Bild runter")]
        NEXT = 0x22,
        [Description("Ende")]
        END = 0x23,
        [Description("Pos 1")]
        HOME = 0x24,
        [Description("Links")]
        LEFT = 0x25,
        [Description("Aufwärts")]
        UP = 0x26,
        [Description("Rechts")]
        RIGHT = 0x27,
        [Description("Abwärts")]
        DOWN = 0x28,

        //SELECT = 0x29,

        [Description("0")]
        NUM_00 = 0x30,
        [Description("1")]
        NUM_01 = 0x31,
        [Description("2")]
        NUM_02 = 0x32,
        [Description("3")]
        NUM_03 = 0x33,
        [Description("4")]
        NUM_04 = 0x34,
        [Description("5")]
        NUM_05 = 0x35,
        [Description("6")]
        NUM_06 = 0x36,
        [Description("7")]
        NUM_07 = 0x37,
        [Description("8")]
        NUM_08 = 0x38,
        [Description("9")]
        NUM_09 = 0x39,

        [Description("Druck")]
        PRINT = 0x2A,
        [Description("Ausführen")]
        EXECUTE = 0x2B,
        [Description("Bildschirmfoto")]
        SNAPSHOT = 0x2C,
        [Description("Einfügen")]
        INSERT = 0x2D,
        [Description("Entfernen")]
        DELETE = 0x2E,
        [Description("Hilfe")]
        HELP = 0x2F,

        [Description("Numpad 0")]
        NUMPAD0 = 0x60,
        [Description("Numpad 1")]
        NUMPAD1 = 0x61,
        [Description("Numpad 2")]
        NUMPAD2 = 0x62,
        [Description("Numpad 3")]
        NUMPAD3 = 0x63,
        [Description("Numpad 4")]
        NUMPAD4 = 0x64,
        [Description("Numpad 5")]
        NUMPAD5 = 0x65,
        [Description("Numpad 6")]
        NUMPAD6 = 0x66,
        [Description("Numpad 7")]
        NUMPAD7 = 0x67,
        [Description("Numpad 8")]
        NUMPAD8 = 0x68,
        [Description("Numpad 9")]
        NUMPAD9 = 0x69,

        [Description("Multiplizieren")]
        MULTIPLY = 0x6A,
        [Description("Addieren")]
        ADD = 0x6B,

        [Description("Separator")]
        SEPARATOR = 0x6C,

        [Description("Subtrahieren")]
        SUBTRACT = 0x6D,
        [Description("Komma")]
        DECIMAL = 0x6E,
        [Description("Dividieren")]
        DIVIDE = 0x6F,

        [Description("F1")]
        F1 = 0x70,
        [Description("F2")]
        F2 = 0x71,
        [Description("F3")]
        F3 = 0x72,
        [Description("F4")]
        F4 = 0x73,
        [Description("F5")]
        F5 = 0x74,
        [Description("F6")]
        F6 = 0x75,
        [Description("F7")]
        F7 = 0x76,
        [Description("F8")]
        F8 = 0x77,
        [Description("F9")]
        F9 = 0x78,
        [Description("F10")]
        F10 = 0x79,
        [Description("F11")]
        F11 = 0x7A,
        [Description("F12")]
        F12 = 0x7B,

        //OEM_1 = 0xBA,
        //OEM_PLUS = 0xBB,
        //OEM_COMMA = 0xBC,
        //OEM_MINUS = 0xBD,
        //OEM_PERIOD = 0xBE,
        //OEM_2 = 0xBF,
        //OEM_3 = 0xC0,

        [Description("Media Zurück")]
        MEDIA_NEXT_TRACK = 0xB0,
        [Description("Media Weiter")]
        MEDIA_PREV_TRACK = 0xB1,
        [Description("Media Stop")]
        MEDIA_STOP = 0xB2,
        [Description("Media Play")]
        MEDIA_PLAY_PAUSE = 0xB3,

        [Description("Windows Links")]
        LWIN = 0x5B,
        [Description("Windows Rechts")]
        RWIN = 0x5C
    }
}