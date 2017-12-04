using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using TestChat;

namespace OPChat___Design
{
    public partial class screen : UserControl
    {
        string userData;
        chatbox2 current = new chatbox2();
        public contact currentC = new contact();
        public string myUsername;
        public List<string> contactUsernames = new List<string>();
        public List<contact> contactss = new List<contact>();
        public List<chatbox2> chats = new List<chatbox2>();
        public friendPanel friendP;
        public List<string> tempFriends;



        public screen(string userData) {

            InitializeComponent();
            this.userData = userData;
            myUsername = Parse(userData, "Username");
            friendP = new friendPanel(myUsername, this);
            panel1.Controls.Add(friendP);

            for (int contactNumber = 5; contactNumber > 0; contactNumber--)
            {

                string newcontact = Parse(userData, "Friend" + contactNumber);
                if (newcontact != "") { contactUsernames.Add(newcontact); }

            }

            friendP.setList(contactUsernames);

            foreach (string contactUser in contactUsernames)
            {
       
                chats.Add(new chatbox2(myUsername, contactUser, this));
                string contactData = getDataFromUser(contactUser);
                contactss.Add(new contact("  " + Parse(contactData, "FirstName") + " " + Parse(contactData, "LastName"), contactUser, chats[chats.Count - 1], this));
                friendP.add(contactss[contactss.Count - 1]);
             
            }
            friendP.NoFriends();
        }


        public void addContactToPanel(string username, string name, string contactUsername) {

            chats.Add(new chatbox2(username, contactUsername, this));
            contactss.Add(new contact("  " + name, contactUsername, chats[chats.Count - 1], this));
            contactUsernames.Add(contactUsername);
            friendP.add(contactss[contactss.Count - 1]);

        }

        public void removeChat(chatbox2 chatToRemove, contact ctc) {
            chats.Add(new chatbox2(chatToRemove.username, chatToRemove.contact, this));
            ctc.chatbox = chats[chats.Count - 1];
            chats.Remove(chatToRemove);
            
            panel5.Controls.Remove(chatToRemove);
            ctcIcon.Visible = false;
            ctcName.Visible = false;
            pictureBox1.Visible = true;
            label1.Visible = true;

        }   


        public void ChangeToChat(chatbox2 chatToShow)
        {
            if (current != chatToShow) {
                ctcName.Text = currentC.contactName;
                panel5.Controls.Remove(current);
                panel5.Controls.Add(chatToShow);
                current = chatToShow;
                ctcIcon.Visible = true;
                ctcName.Visible = true;
                pictureBox1.Visible = false;
                label1.Visible = false;
             }
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

        public string getDataFromUser(string username) {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/user/" + username.ToLower();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            current.loadChat();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           updateFriendList();
        }


        public void updateFriendList() {
            
            string data = getDataFromUser(myUsername);
            tempFriends = new List<string>();
            

            for (int contactNumber = 5; contactNumber > 0; contactNumber--)
            {
                string contact = Parse(data, "Friend" + contactNumber);
                if (contact != "")
                {
                    tempFriends.Add(contact);
                }                

            }

            foreach (string contactToVerify in tempFriends) {

                if (!contactUsernames.Contains(contactToVerify))
                {
                    contactUsernames.Add(contactToVerify);
                    chats.Add(new chatbox2(myUsername, contactToVerify, this));
                    string contactData = getDataFromUser(contactToVerify);
                    contactss.Add(new contact("  " + Parse(contactData, "FirstName") + " " + Parse(contactData, "LastName"), contactToVerify, chats[chats.Count - 1], this));
                    friendP.add(contactss[contactss.Count - 1]);
                }

            }

            
        }

        private void searchBar_Enter(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
        }

        private void searchBar_Leave(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "Add someone";
        }

      
    }
}
