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

        chatbox2 current;


        chatbox2 teste2 = new chatbox2("picha", "andre123aei"); //andre123aei
        public ChatScreen(string userData)
        {

            InitializeComponent();

            friendPanel friendP = new friendPanel();
            panel1.Controls.Add(friendP);
            List<string> contactUsernames = new List<string>();
            List<contact> contactss = new List<contact>();
            List<chatbox2> chats = new List<chatbox2>();

            for (int contactNumber = 5; contactNumber > 0; contactNumber--) {

                string newcontact = Parse(userData, "Friend" + contactNumber);
                if (newcontact != "") { contactUsernames.Add(Parse(getDataFromUser(newcontact), "Username")); }

            }
         
            foreach (string contactUser in contactUsernames) {

                chats.Add(new chatbox2(Parse(userData, "Username"), contactUser));
                string contactData = getDataFromUser(contactUser);
                contactss.Add(new contact("  "+ Parse(contactData, "FirstName") + " " + Parse(contactData, "LastName"), contactUser, chats[chats.Count - 1]));
                friendP.add(contactss[contactss.Count - 1]);
                teste2.addIn(contactUser, "");


            }
            current = teste2;
         panel5.Controls.Add(chats[chats.Count - 1]);
           



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



        public void ChangeToChat(chatbox2 chatToShow) {

            panel5.Controls.Remove(current);
            panel5.Controls.Add(chatToShow);

        }



        




        private void  timer2_Tick(object sender, EventArgs e)
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
