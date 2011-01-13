using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Demo.WindowsMobile.Extensions
{
    internal static class GraphicsExtensions
    {
        private struct Rect
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            public Rect(Rectangle r)
            {
                Left = r.Left;
                Top = r.Top;
                Bottom = r.Bottom;
                Right = r.Right;
            }
        }

        [DllImport("coredll.dll")]
        static extern int DrawText(IntPtr hdc, string lpStr, int nCount, ref Rect lpRect, int wFormat);
        private const int DT_CALCRECT = 0x00000400;
        private const int DT_WORDBREAK = 0x00000010;
        public static Size MeasureString(this Graphics gr, string text, Rectangle rect)
        {
            var bounds = new Rect(rect);
            IntPtr hdc = gr.GetHdc();
            const int flags = DT_CALCRECT | DT_WORDBREAK;

            DrawText(hdc, text, text.Length, ref bounds, flags);
            gr.ReleaseHdc(hdc);
            return new Size(bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
        }
    }
}
