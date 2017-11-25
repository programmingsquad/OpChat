using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TestChat
{
    public partial class chatbox2 : UserControl
    {
        public string username;
        public string contact;

        public chatbox2() {

            if (!this.DesignMode) {

                InitializeComponent();
                username = "justplaceholder";
                contact = "justplaceholder";

            }

        }

        public chatbox2(string username, string contact) {

            InitializeComponent();
            this.username = username;
            this.contact = contact;
            
        }


        bubble bb1_old = new bubble();

        public void addIn(string msg, string time) {

            bubble bb1 = new TestChat.bubble(msg, time, bubble.msgtype.In);
            bb1.Location = new Point(21, 18);
            bb1.Size = new Size(629, 87);
            bb1.Top = bb1_old.Bottom + 10;
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;

        }


        public void addOut(string msg, string time)
        {

            bubble bb1 = new TestChat.bubble(msg, time, bubble.msgtype.Out);
            bb1.Location = new Point(21, 18); ; bb1.Left += 100;
            bb1.Size = new Size(629, 87);
            bb1.Top = bb1_old.Bottom + 10;
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;

        }

        private void bunifuMaterialTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendMessage(username, contact, bunifuMaterialTextbox1.Text);
                bunifuMaterialTextbox1.Text = "";
                refreshChat();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {


            sendMessage(username, contact, bunifuMaterialTextbox1.Text);
            bunifuMaterialTextbox1.Text = "";
            refreshChat();
        }

        private void chatbox2_Load(object sender, EventArgs e) {
            List<int> parts = new List<int>();
            loadChat();

        }


        List<int> parts = new List<int>();

        public void loadChat() {

            foreach (string msgToRecieve in ParseMsg(getMessages(username, contact)))
            {
                if (!parts.Contains(int.Parse(Parse(msgToRecieve, "id"))))
                {

                    if (Parse(msgToRecieve, "fromm") == username) { addOut(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve))); parts.Add(int.Parse(Parse(msgToRecieve, "id"))); }
                    else { addIn(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                           parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                    }
                }
            }
        }


        public void refreshChat() {
         
            foreach (string msgToRecieve in ParseMsg(refreshMessages(username, contact)))
            {
                if (!parts.Contains(int.Parse(Parse(msgToRecieve, "id")))) {

                    if (Parse(msgToRecieve, "fromm") == username) { addOut(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve))); parts.Add(int.Parse(Parse(msgToRecieve, "id"))); }
                    else
                    {

                        addIn(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                        parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                    }


                }

            }

        }

        public string getMessages(string user1, string user2) {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/messages/user1={0}/user2={1}", user1, user2);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public string refreshMessages(string user1, string  user2) {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/newmessages/user1={0}/user2={1}", user1, user2);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }    
            return html;

        }

        public static string Parse(string input, string tag) {

            input = input.Replace("{", "").Replace("}", "");
            string[] inputArray = input.Split(',');
            foreach (string y in inputArray) {
                if (y.Contains(tag)) {
                    return y.Split(':').Last().ToString().Replace("\"","");
                }
            }
            return "erro";
        }

        public static List<string> ParseMsg(string input) {

            input = input.Replace("[", "").Replace("]", "");
            List<string> inputList = input.Split('}').ToList();
            inputList.RemoveAt(inputList.Count() - 1);
            for (int y = 0; y < inputList.Count(); y++) {
                 int min = inputList[y].IndexOf(',');
                 if (min < 2) { inputList[y] = inputList[y].Insert(inputList[y].Length, "}").Remove(min, 1); }
            }
            return inputList;
        }

        static public string GetHour(string input) {

            return input.Split(',').Last().Replace("\"","").Replace("}","").Replace("t", "").Replace("i", "").Replace("m", "").Replace("e", "").TrimStart(':');

        }

        static public string GetDate(string input) {

            DateTime time = DateTime.Parse(input);
            DayOfWeek week = time.DayOfWeek;
            string SWeek = week.ToString();
            switch (SWeek) {
                case "Monday": SWeek = "Mon"; break;
                case "Tuesday": SWeek = "Tue"; break;
                case "Wednesday": SWeek = "Wed"; break;
                case "Thursday": SWeek = "Thu"; break;
                case "Friday": SWeek = "Fri"; break;
                case "Saturday": SWeek = "Sat"; break;
                case "Sunday": SWeek = "Sun"; break;
            }

            string[] times = (input.Split(' ').Last()).Split(':');
            return (SWeek + " " + times[0] + ":" + times[1]);
        }

        public static string sendMessage(string from, string to, string message) {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/send/from={0}/to={1}/message={2}", from, to, message);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (     HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }


    }
}
