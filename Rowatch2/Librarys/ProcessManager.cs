using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Rowatch2.Librarys
{
    public class ProcessManager
    {
        #region Win API

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern Int32 CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

        [Flags]
        enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        #endregion

        #region Fields

        private IntPtr _processHandle;
        private IntPtr _windowHandle;

        #endregion

        #region Constructor

        public ProcessManager()
        {
            _encoding = Encoding.ASCII;
            _stringTerminationChar = '\0';

            _processHandle = IntPtr.Zero;
            _windowHandle = IntPtr.Zero;
        }

        #endregion

        #region Private Methods

        private byte[] ReadMemoryAtAdress(IntPtr MemoryAddress, uint bytesToRead, out int bytesRead)
        {
            byte[] buffer = new byte[bytesToRead];

            IntPtr ptrBytesRead;
            ReadProcessMemory(_processHandle, MemoryAddress, buffer, bytesToRead, out ptrBytesRead);

            bytesRead = ptrBytesRead.ToInt32();

            return buffer;
        }

        //private int WriteMemoryAtAdress(IntPtr MemoryAddress, byte[] bytesToWrite)
        //{
        //    IntPtr ptrBytesWritten;
        //    WriteProcessMemory(_process.Handle, MemoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out ptrBytesWritten);

        //    return ptrBytesWritten.ToInt32();
        //}

        #endregion

        #region Methods

        public OpenProcessResult OpenProcess(Process process)
        {
            try
            {
                IntPtr processHandle = OpenProcess((uint)ProcessAccessFlags.VMRead, 0, (uint)process.Id);
                int processID = processHandle.ToInt32();
                if (processID == 0)
                {
                    return OpenProcessResult.NoAccess;
                }

                _processHandle = new IntPtr(processID);
                _windowHandle = process.MainWindowHandle;

                return OpenProcessResult.Successfull;
            }
            catch (Exception)
            {
                return OpenProcessResult.Failed;
            }
        }

        public int ReadInt32(int address)
        {
            if (_processHandle == IntPtr.Zero)
            {
                throw new Exception("Process not opened");
            }

            int result = 0;

            int readedBytes;

            byte[] data = ReadMemoryAtAdress(new IntPtr(address), 4, out readedBytes);
            if (readedBytes > 0 && data.Length > 0)
            {
                result = BitConverter.ToInt32(data, 0);
            }

            return result;
        }

        public string ReadString(int address, int bytesToRead)
        {
            if (_processHandle == IntPtr.Zero)
            {
                throw new Exception("Process not opened");
            }

            string result = string.Empty;

            int readedBytes;
            byte[] data = ReadMemoryAtAdress(new IntPtr(address), (uint)bytesToRead, out readedBytes);
            if (readedBytes > 0 && data.Length > 0)
            {
                int endPos = data.Length;
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == 0)
                    {
                        endPos = i;
                        break;
                    }
                }

                result = Encoding.ASCII.GetString(data, 0, endPos);

                //result = Encoding.ASCII.GetString(data);

                //int endPos = -1;
                //for (int i = 0; i < result.Length; i++)
                //{
                //    if (result[i] == _stringTerminationChar)
                //    {
                //        endPos = i;
                //        break;
                //    }
                //}

                //if (endPos >= 0)
                //{
                //    result = result.Substring(0, endPos);
                //}
            }

            return result;
        }

        #endregion

        #region Properties

        private Encoding _encoding;
        public Encoding TextEncoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        private char _stringTerminationChar;
        public char StringTerminationCharacter
        {
            get { return _stringTerminationChar; }
            set { _stringTerminationChar = value; }
        }

        public bool IsActive
        {
            get
            {
                if (_windowHandle != IntPtr.Zero)
                {
                    return IsWindow(_windowHandle);
                }
                else
                {
                    return false;
                }
            }
        }

        public IntPtr ProcessHandle
        {
            get
            {
                return _processHandle;
            }
        }

        public IntPtr WindowHandle
        {
            get
            {
                return _windowHandle;
            }
        }

        #endregion
    }
}