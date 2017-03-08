using System;
using System.Runtime.InteropServices;

namespace GAVPI
{

    //
    //  This class defines and declares several useful native (Win32) Windows APIs that offer functionality either above
    //  those C# APIs provided by Microsoft, or functionality not supported at all by any C# APIs provided by Microsoft.
    //

    internal class Win32_APIs
    {

        //  Win32 APIs relating to Interprocess Communication:

        [DllImport("user32", SetLastError = true)]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32")]
        public static extern bool PostMessage( IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam );

        [DllImport("user32")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //  Win32 parameter constants:

        public const int HWND_BROADCAST = 0xffff;

    }  //  class Win32_APIs

}
