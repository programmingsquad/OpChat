using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using OPChat___Design;

namespace TestChat
{
    public partial class chatbox2 : UserControl
    {
        public string username;
        public string contact;

        screen parent;

        public chatbox2()
        {

            if (!this.DesignMode)
            {

                InitializeComponent();
                username = "justplaceholder";
                contact = "justplaceholder";

            }

        }

        public chatbox2(string username, string contact, screen parent)
        {

            InitializeComponent();
            this.username = username;
            this.contact = contact;
            this.parent = parent;
        }


        bubble bb1_old = new bubble();
        Boolean isFirst = true;
        public void addIn(string msg, string time)
        {

            bubble bb1 = new TestChat.bubble(msg, time, bubble.msgtype.In, this);
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

            bubble bb1 = new TestChat.bubble(msg, time, bubble.msgtype.Out, this);
            bb1.Location = new Point(21, 18); ; bb1.Left += 110;
            bb1.Size = new Size(629, 87);
            if (!isFirst) { bb1.Top = bb1_old.Bottom + 10; } else { bb1.Top = 17; }
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;
            isFirst = false;
        }

        public void addImageOut(Image img, string time)
        {

            bubble bb1 = new TestChat.bubble(img, time, bubble.msgtype.Out, this);
            bb1.Location = new Point(21, 18); ; bb1.Left += 330;
            bb1.Size = new Size(409, 87);
            if (!isFirst) { bb1.Top = bb1_old.Bottom + 10; } else { bb1.Top = 17; }
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;
            isFirst = false;

        }

        public void addImageIn(Image img, string time)
        {

            bubble bb1 = new TestChat.bubble(img, time, bubble.msgtype.In, this);
            bb1.Location = new Point(21, 18);
            bb1.Size = new Size(409, 87);
            if (!isFirst) { bb1.Top = bb1_old.Bottom + 10; } else { bb1.Top = 17; }
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;
            isFirst = false;

        }



        public void addFileOut(string filename, string path, string time)
        {

            bubble bb1 = new TestChat.bubble(filename, path, time, bubble.msgtype.Out, this);
            bb1.Location = new Point(21, 18); ; bb1.Left += 330;
            bb1.Size = new Size(409, 87);
            if (!isFirst) { bb1.Top = bb1_old.Bottom + 10; } else { bb1.Top = 17; }
            panel2.Controls.Add(bb1);
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            bb1_old = bb1;
            isFirst = false;

        }

        public void addFileIn(string filename, string path, string time)
        {

            bubble bb1 = new TestChat.bubble(filename, path, time, bubble.msgtype.In, this);
            bb1.Location = new Point(21, 18); ; 
            bb1.Size = new Size(409, 87);
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
                if (areFriends(contact, username))
                {

                    if (bunifuMaterialTextbox1.Text.Replace(" ", "") != "")
                    {
                        sendMessage(username, contact, bunifuMaterialTextbox1.Text);
                        bunifuMaterialTextbox1.Text = "";
                        refreshChat();
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else
                {

                    MessageBox.Show("Sorry! But you are no longer friends!!");
                    parent.friendP.localRemove(parent.currentC);
                    parent.removeChat(parent.currentC.chatbox, parent.currentC);
                    
                }




            }
        }

       

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            if (areFriends(contact, username))
            {

                if (bunifuMaterialTextbox1.Text.Replace(" ", "") != "")
                {
                    sendMessage(username, contact, bunifuMaterialTextbox1.Text);
                    bunifuMaterialTextbox1.Text = "";
                    refreshChat();
                }

             
            }
            else
            {

                MessageBox.Show("Sorry! But you are no longer friends!!");
                parent.friendP.localRemove(parent.currentC);
                parent.removeChat(parent.currentC.chatbox, parent.currentC);

            }


        }

        private bool areFriends(string contact, string username)
        {
            parent.updateFriendList();
            return parent.tempFriends.Contains(contact);
        }

        


        private void chatbox2_Load(object sender, EventArgs e)
        {
            List<int> parts = new List<int>();
            loadChat();

        }


        List<int> parts = new List<int>();

        public void loadChat()
        {

            foreach (string msgToRecieve in ParseMsg(getMessages(username, contact)))
            {

                if (!parts.Contains(int.Parse(Parse(msgToRecieve, "id"))))
                {
                    if (Parse(msgToRecieve, "type") == "img")
                    {
                        Image img;
                        String[] imgParsed = Parse(msgToRecieve, "message").Split(':');
                        String imgPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Uploads\" + imgParsed.Last() + "." + imgParsed[1].Split('.').Last();
                        if (File.Exists(imgPath)) { img = Image.FromFile(imgPath); }
                        else
                        {
                            String imgPath2 = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Downloads\" + imgParsed.Last() + "." + imgParsed[1].Split('.').Last();
                            if (File.Exists(imgPath2)) { img = Image.FromFile(imgPath2); }
                            else
                            {
                                Download(imgParsed.Last() + "." + imgParsed[1].Split('.').Last(), imgPath2, "admin@thecybersheep.com", "admin");
                                img = Image.FromFile(imgPath2);
                            }

                        }
                        if (Parse(msgToRecieve, "fromm") == username) { addImageOut(img, GetDate(GetHour(msgToRecieve))); } else { addImageIn(img, GetDate(GetHour(msgToRecieve))); }
                        panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
                        parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                        recieveMsg(Parse(msgToRecieve, "id"));

                    }
                    if (Parse(msgToRecieve, "type") == "file") {
                        string path;
                        String[] fileParsed = Parse(msgToRecieve, "message").Split(':');

                        String filePath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Uploads\" + fileParsed.Last() + "." + fileParsed[1].Split('.').Last();
                        if (File.Exists(filePath)) { path = filePath; }
                        else
                        {
                            String filePath2 = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Downloads\" + fileParsed.Last() + "." + fileParsed[1].Split('.').Last();
                            if (File.Exists(filePath2)) { path = filePath2; }
                            else
                            {
                                Download(fileParsed.Last() + "." + fileParsed[1].Split('.').Last(), filePath2, "admin@thecybersheep.com", "admin");
                                path = filePath2;
                            }

                        }

                        if (Parse(msgToRecieve, "fromm") == username) { addFileOut(fileParsed[1], path, GetDate(GetHour(msgToRecieve))); } else { addFileIn(fileParsed[1], path, GetDate(GetHour(msgToRecieve))); }
                        panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
                        parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                        recieveMsg(Parse(msgToRecieve, "id"));




                    }
                    if (Parse(msgToRecieve, "type") == "msg")
                    {

                        if (Parse(msgToRecieve, "fromm") == username)
                        {
                            addOut(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                            parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                        }
                        else
                        {
                            addIn(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                            parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                            recieveMsg(Parse(msgToRecieve, "id"));
                        }
                    }
                }
            }
            //scrollToBot();
        }


        public void refreshChat()
        {

            foreach (string msgToRecieve in ParseMsg(refreshMessages(username, contact)))
            {
                if (!parts.Contains(int.Parse(Parse(msgToRecieve, "id"))))
                {

                    if (Parse(msgToRecieve, "type") == "img")
                    {
                        Image img;
                        String[] imgParsed = Parse(msgToRecieve, "message").Split(':');
                        String imgPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Uploads\" + imgParsed.Last() + "." + imgParsed[1].Split('.').Last();
                        if (File.Exists(imgPath)) { img = Image.FromFile(imgPath); }
                        else
                        {
                            String imgPath2 = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Downloads\" + imgParsed.Last() + "." + imgParsed[1].Split('.').Last();
                            if (File.Exists(imgPath2)) { img = Image.FromFile(imgPath2); }
                            else
                            {
                                Download(imgParsed.Last() + "." + imgParsed[1].Split('.').Last(), imgPath2, "admin@thecybersheep.com", "admin");
                                img = Image.FromFile(imgPath2);
                            }

                        }
                        if (Parse(msgToRecieve, "fromm") == username) { addImageOut(img, GetDate(GetHour(msgToRecieve))); } else { addImageIn(img, GetDate(GetHour(msgToRecieve))); }
                        panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
                        parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                        recieveMsg(Parse(msgToRecieve, "id"));

                    }
                    if (Parse(msgToRecieve, "type") == "file")
                    {
                        string path;
                        String[] fileParsed = Parse(msgToRecieve, "message").Split(':');

                        String filePath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Uploads\" + fileParsed.Last() + "." + fileParsed[1].Split('.').Last();
                        if (File.Exists(filePath)) { path = filePath; }
                        else
                        {
                            String filePath2 = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName + @"\OpChat\Downloads\" + fileParsed.Last() + "." + fileParsed[1].Split('.').Last();
                            if (File.Exists(filePath2)) { path = filePath2; }
                            else
                            {
                                Download(fileParsed.Last() + "." + fileParsed[1].Split('.').Last(), filePath2, "admin@thecybersheep.com", "admin");
                                path = filePath2;
                            }

                        }

                        if (Parse(msgToRecieve, "fromm") == username) { addFileOut(fileParsed[1], path, GetDate(GetHour(msgToRecieve))); } else { addFileIn(fileParsed[1], path, GetDate(GetHour(msgToRecieve))); }
                        panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
                        parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                        recieveMsg(Parse(msgToRecieve, "id"));




                    }
                    if (Parse(msgToRecieve, "type") == "msg")
                    {

                        if (Parse(msgToRecieve, "fromm") == username)
                        {
                            addOut(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                            parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                        }
                        else
                        {
                            addIn(Parse(msgToRecieve, "message"), GetDate(GetHour(msgToRecieve)));
                            parts.Add(int.Parse(Parse(msgToRecieve, "id")));
                            recieveMsg(Parse(msgToRecieve, "id"));
                        }
                    }
                }

            }

        }

        public string getMessages(string user1, string user2)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/messages/user1={0}/user2={1}", user1.ToLower(), user2.ToLower());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public void recieveMsg(string id)
        {


            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/recieve/" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }



        }



        public string refreshMessages(string user1, string user2)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/newmessages/user1={0}/user2={1}", user1.ToLower(), user2.ToLower());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public static string Parse(string input, string tag)
        {
            string returned;
            int tagsize = tag.Length;
            if (input.Contains(tag))
            {
                string[] output = input.Remove(0, input.IndexOf(tag) + tagsize + 3).Split('"');
                returned = output[0];
                return returned;
            }
            else
            {
                return "Parse Error";
            }
        }

        public static List<string> ParseMsg(string input)
        {

            input = input.Replace("[", "").Replace("]", "");
            List<string> inputList = input.Split('}').ToList();
            inputList.RemoveAt(inputList.Count() - 1);
            for (int y = 0; y < inputList.Count(); y++)
            {
                int min = inputList[y].IndexOf(',');
                if (min < 2) { inputList[y] = inputList[y].Insert(inputList[y].Length, "}").Remove(min, 1); }
            }
            return inputList;
        }

        static public string GetHour(string input)
        {

            return input.Split(',').Last().Replace("\"", "").Replace("}", "").Replace("t", "").Replace("i", "").Replace("m", "").Replace("e", "").TrimStart(':');

        }

        static public string GetDate(string input)
        {

            DateTime time = DateTime.Parse(input);
            DayOfWeek week = time.DayOfWeek;
            string SWeek = week.ToString();
            switch (SWeek)
            {
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

        public static string sendMessage(string from, string to, string message)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/send/from={0}/to={1}/message={2}", from.ToLower(), to.ToLower(), message);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public static string sendImage(string from, string to, string message)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/send/pic/from={0}/to={1}/message={2}", from.ToLower(), to.ToLower(), message);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public static string sendFile(string from, string to, string message)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/chat/send/file/from={0}/to={1}/message={2}", from.ToLower(), to.ToLower(), message);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }
        


        public void scrollToBot()
        {

            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;

        }


        private static void Upload(string ftpServer, string userName, string password, string filename)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.UploadFile(ftpServer + "/" + new FileInfo(filename).Name, "STOR", filename);
              
            }
        }

        private static void Download(string file, string to, string userName, string password)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.DownloadFile(@"ftp://ftp.thecybersheep.com/" + file, to);
            }
        }



        Random rnd = new Random();


        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFile.FileName;
                FileInfo fileInfo = new FileInfo(fullPath);
                var size = fileInfo.Length;
                if (size > 5000000) {/* RAFA file too large */  MessageBox.Show("File too Large! (Max: 5 Mb)"); }
                else
                {
                    uploadingPanel.Visible = true;
                    string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                    long teste = (rnd.Next(1000000000, 1999999999) * rnd.Next(1000000000, 1999999999));
                    string extension = fullPath.Split('.').Last();
                    saveFileInDb(extension, teste.ToString());
                    string id = Parse(getFileId(teste.ToString()), "id");
                    string tempPath = path + @"\OpChat\Uploads\" + id + "." + extension;

                    File.Copy(fullPath, tempPath);
                    Upload("ftp://ftp.thecybersheep.com/", "admin@thecybersheep.com", "admin", tempPath);

                    uploadingPanel.Visible = false;
                    if (isImage(extension))
                    {
                        sendImage(username, contact, "filename:" + openFile.FileName.Split('\\').Last() + ":id:" + id);
                    } else {
                        sendFile(username, contact, "filename:" + openFile.FileName.Split('\\').Last() + ":id:" + id);
                    }

                }



            }

        }

        public string saveFileInDb(string ext, string hash)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/friendrequest/ext=" + ext + "/hash=" + hash;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public string getFileId(string hash)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/getImgId/hash=" + hash;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public Boolean isImage(string extension) {

            return (extension == "jpg" || extension == "jpeg" || extension == "jpe" || extension == "jfif" || extension == "gif" || extension == "tif" || extension == "tiff" || extension == "png");

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.AllowDrop = true;
           
        }

        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    // Code to read the contents of the text file
                    if (File.Exists(fileLoc))
                    {
                        using (TextReader tr = new StreamReader(fileLoc))
                        {
                            MessageBox.Show(tr.ReadToEnd());
                        }
                    }

                }
            }
        }

        public void removeMsg(bubble bb) {


            panel2.Controls.Remove(bb);


        }




    }
}
