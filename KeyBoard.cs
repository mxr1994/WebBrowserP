using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserP
{
    public partial class KeyBoard : Form
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(

          byte bVk,    //虚拟键值

          byte bScan,// 一般为0

          int dwFlags,  //这里是整数类型  0 为按下，2为释放

          int dwExtraInfo  //这里是整数类型 一般情况下设成为 0

      );
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        public KeyBoard()
        {
            InitializeComponent();
        }

        private void KeyBoard_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EventToFoucus()
        {
            ;
        }

        private void q_Click(object sender, EventArgs e)
        {
            EventToFoucus();
            keybd_event((byte)Keys.Q, 0, 0, 0);
            keybd_event((byte)Keys.Q, 0, 2, 0);
        }
    }
}
