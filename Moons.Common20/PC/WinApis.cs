using System;
using System.Runtime.InteropServices;

namespace Moons.Common20.PC
{
    /// <summary>
    /// 常用的windows api
    /// </summary>
    public static class WinApis
    {
        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        public static extern IntPtr SendMessage( HandleRef hWnd, int msg, int wParam, int lParam );

        [DllImport( "gdi32.dll",
            EntryPoint = "BitBlt",
            CallingConvention = CallingConvention.StdCall )]
        public static extern int BitBlt(
            IntPtr hdcDesc, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc, int nXSrc, int nYSrcs, uint dwRop );
    }
}