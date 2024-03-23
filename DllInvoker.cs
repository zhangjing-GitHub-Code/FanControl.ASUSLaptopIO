using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace ASUSLaptop
{
    // COPYRIGHT CSDN flymoon87/a/d/112609650
    class DllInvoke
    {
        /// <summary>
        /// LoadLibraryFlags
        /// </summary>
        public enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);
        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, String funcName);
        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);
        private IntPtr hLib;
        public DllInvoke(String DllName)
        {
            hLib = LoadLibraryEx(DllName, IntPtr.Zero, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);
            if (hLib == IntPtr.Zero)
            {
                var err = Marshal.GetLastWin32Error(); //Only when 'SetLastError = true', we can get Error Code
            }
        }
        ~DllInvoke()
        {
            FreeLibrary(hLib);
        }
        // Turn func to exec TO delegate
        public Delegate Invoke(String ApiName, Type t)
        {
            IntPtr api = GetProcAddress(hLib, ApiName);
            return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);
        }
    }
}