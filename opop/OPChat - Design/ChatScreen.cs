using OPChat___Design;
using System;
using System.Windows.Forms;


namespace TestChat
{
    public partial class ChatScreen : Form
    {
        public ChatScreen(string info) {

            InitializeComponent();
            screen chatscreen = new screen(info);
            panel1.Controls.Add(chatscreen);

        }


        /* ------ WINDOW CONTROLS ------*/

        //Movable window
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m) {

            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION); 

        }

        //Close the window
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Minimize the window
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        /*--------------------------------------*/


    }
}
