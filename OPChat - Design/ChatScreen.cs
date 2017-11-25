using OPChat___Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestChat
{
    public partial class ChatScreen : Form
    {

        chatbox2 teste2 = new chatbox2("picha", "andre123aei"); //andre123aei
        public ChatScreen(string userData)
        {

            InitializeComponent();

            List<string> contacts = new List<string>();
            for (int contactNumber = 1; contactNumber < 6; contactNumber++) {
                string newcontact = Parse(userData, "Friend" + contactNumber);
                if (newcontact != "") { contacts.Add(Parse(getDataFromUser(newcontact), "FirstName")); }
               
            }
            foreach (string asf in contacts) { teste2.addIn(asf, ""); }
            

            panel5.Controls.Add(teste2);
            friendPanel friendP = new friendPanel();
            panel1.Controls.Add(friendP);
            friendP.refresh();
        }
        


        /* ------ WINDOW CONTROLS ------*/

       //Movable window
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
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



       

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            chatbox2 teste = new chatbox2();
            panel5.Controls.Remove(teste2);
            panel5.Controls.Add(teste);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            teste2.refreshChat();
        }


        public static string Parse(string input, string tag)
        {
            input = input.Replace("{", "").Replace("}", "");
            string[] inputArray = input.Split(',');
            foreach (string y in inputArray)
            {
                if (y.Contains(tag))
                {
                    return y.Split(':').Last().ToString().Replace("\"", "");
                }
            }
            return "erro";
        }

        public string getDataFromUser(string Username2)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/user/" + Username2;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }


    }
}
