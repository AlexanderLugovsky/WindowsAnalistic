using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAnalistic
{
    public partial class Form1 : Form
    {
            public struct POINTAPI
            {
                public int x;
                public int y;
            }
                  
            [System.Runtime.InteropServices.DllImport("user32")]
            public static extern int GetCursorPos(out POINTAPI lpPoint);
            //public static extern bool GetCursorPos(out POINTAPI lpPoint);

            [System.Runtime.InteropServices.DllImport("user32")]
            public static extern int GetDC(int hwnd);

            [System.Runtime.InteropServices.DllImport("gdi32")]
            public static extern int GetPixel(int hdc, int x, int y);

            [System.Runtime.InteropServices.DllImport("user32")]
            public static extern int ReleaseDC(int hwnd, int hdc);

            [System.Runtime.InteropServices.DllImport("user32")]
            public static extern int WindowFromPoint(int xPoint, int yPoint);

            [System.Runtime.InteropServices.DllImport("user32")]
            internal static extern Int32 GetWindowTextLength(IntPtr hwnd);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            internal static extern Int32 GetWindowText(Int32 hwnd, System.Text.StringBuilder lpString, Int32 cch);

            [System.Runtime.InteropServices.DllImport("user32")]
            private static extern int GetWindowText(int hwnd, string lpString, int cch);

        public POINTAPI structCursorPosition;
        
        public int w1, x, y, lColor, hdc;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ((timer1.Enabled == true))
            {
                timer1.Enabled = false;
                button1.Text = "Start";
            }
            else
            {
                timer1.Enabled = true;
                button1.Text = "Stop";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public string GetText(IntPtr hWnd)
        {
            int len = GetWindowTextLength(hWnd);
            if (len > 0)
            {
                System.Text.StringBuilder b = new System.Text.StringBuilder('\0', len + 1);
                var ret = GetWindowText((int)hWnd, b, b.Capacity);
                if (ret != 0)
                    return b.ToString();
                else
                    return null;
            }
            else
                return null;
        }

        private string GetCaption(int hWnd)
        {
            string capt;
            int lenC;
            capt = new String(' ', 255); // заполняем строку пробелами 
            lenC = GetWindowText(hWnd, capt, 255); // получаем заголовок окна hwnd 
            capt = capt.Substring(1, lenC); // обрезаем его, оставляя только нужную часть 
            return capt;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetCursorPos(out structCursorPosition);
            w1 =  WindowFromPoint(structCursorPosition.x, structCursorPosition.y);
            hdc = (int)GetDC(0);
            lColor = GetPixel(hdc, structCursorPosition.x, structCursorPosition.y);
            ReleaseDC(0, hdc);
            label2.Text = "X=" + System.Convert.ToString(structCursorPosition.x) + " Y=" + System.Convert.ToString(structCursorPosition.y);
            label4.Text = GetText((IntPtr)w1);
            panel1.BackColor = System.Drawing.ColorTranslator.FromOle(lColor);


        }
    }
}
