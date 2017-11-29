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
        Boolean isFirst = true;
        public void addIn(string msg, string time) {

            bubble bb1 = new TestChat.bubble(msg, time, bubble.msgtype.In);
            bb1.Location = new Point(21, 18);
            bb1.Size = new Size(629, 87);
            if (!isFirst) { bb1.Top = bb1_old.Bottom + 10; } else { bb1.Top = 17; }
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;
            isFirst = false;
        }

        public void addOut(string msg, string time)
        {

            bubble bb1 = new TestChat.bubble(msg, time, bubble.msgtype.Out);
            bb1.Location = new Point(21, 18); ; bb1.Left += 110;
            bb1.Size = new Size(629, 87);
            if (!isFirst) { bb1.Top = bb1_old.Bottom + 10; } else { bb1.Top = 17; }
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;
            isFirst = false;
        }

        private void bunifuMaterialTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (bunifuMaterialTextbox1.Text.Replace(" ", "") != "")
                {
                    sendMessage(username, contact, bunifuMaterialTextbox1.Text);
                    bunifuMaterialTextbox1.Text = "";
                    refreshChat(); 
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {


            if (bunifuMaterialTextbox1.Text.Replace(" ", "") != "")
            {
                sendMessage(username, contact, bunifuMaterialTextbox1.Text);
                bunifuMaterialTextbox1.Text = "";
                refreshChat();
            }
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
                        recieveMsg(Parse(msgToRecieve, "id"));
                    }
                }
            }
            scrollToBot();
        }


        public void refreshChat() {
         
            foreach (string msgToRecieve in ParseMsg(refreshMessages(username, contact)))
            {
                if (!parts.Contains(int.Parse(Parse(msgToRecieve, "id")))) {

                    if (Parse(msgToRecieve, "fromm") == username) { addOut(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve))); parts.Add(int.Parse(Parse(msgToRecieve, "id"))); }
                    else
                    {
                        recieveMsg(Parse(msgToRecieve, "id"));
                        addIn(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                        parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                    }


                }

            }

        }

        public string getMessages(string user1, string user2) {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/messages/user1={0}/user2={1}", user1.ToLower(), user2.ToLower());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public void recieveMsg(string id) {


            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/recieve/" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            


        }

       

        public string refreshMessages(string user1, string  user2) {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/newmessages/user1={0}/user2={1}", user1.ToLower(), user2.ToLower());
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
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/send/from={0}/to={1}/message={2}", from.ToLower(), to.ToLower(), message);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (     HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public void scrollToBot() {

            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;

        }


        private static void Upload(string ftpServer, string userName, string password, string filename)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.UploadFile(ftpServer + "/" + new FileInfo(filename).Name, "STOR", filename);
                client.DownloadFile(@"ftp://ftp.thecybersheep.com/sadads#.txt", @"C:\Users\Utilizador\Desktop\as\fodasse.txt");
            }
        }



        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFile.FileName;
                FileInfo fileInfo = new FileInfo(fullPath);
                var size = fileInfo.Length;
                if (size > 5000000) {/* RAFA file too large */ } else
                {
                  //  uploadingPanel.Visible = true;
                    
              
                    uploadingPanel.Visible = true;
               
                    Upload("ftp://ftp.thecybersheep.com/", "admin@thecybersheep.com", "admin", fullPath);
                 
                    uploadingPanel.Visible = false;
                   
                }


            }

        }

        private void uploadingPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
