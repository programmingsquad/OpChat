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
        string myUsername;
        List<string> contactUsernames = new List<string>();
        List<contact> contactss = new List<contact>();
        List<chatbox2> chats = new List<chatbox2>();
        friendPanel friendP;
     

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
       
                chats.Add(new chatbox2(myUsername, contactUser));
                string contactData = getDataFromUser(contactUser);
                contactss.Add(new contact("  " + Parse(contactData, "FirstName") + " " + Parse(contactData, "LastName"), contactUser, chats[chats.Count - 1], this));
                friendP.add(contactss[contactss.Count - 1]);
             
            }

        }

       


        public void addContactToPanel(string username, string name, string contactUsername) {

            chats.Add(new chatbox2(username, contactUsername));
            contactss.Add(new contact("  " + name, contactUsername, chats[chats.Count - 1], this));
            contactUsernames.Add(contactUsername);
            friendP.add(contactss[contactss.Count - 1]);

        }


        public void ChangeToChat(chatbox2 chatToShow)
        {
            if (current != chatToShow) { 
                panel5.Controls.Remove(current);
            panel5.Controls.Add(chatToShow);
            current = chatToShow;
             }
        }

        public static string Parse(string input, string tag) {

            input = input.Replace("{", "").Replace("}", "");
            string[] inputArray = input.Split(',');
            foreach (string y in inputArray) { if (y.Contains(tag)) { return y.Split(':').Last().ToString().Replace("\"", ""); } }
            return "erro";

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
            current.refreshChat();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           updateFriendList();
        }


        public void updateFriendList() {

            string data = getDataFromUser(myUsername);
            List<string> tempFriends = new List<string>();
            

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
                    chats.Add(new chatbox2(myUsername, contactToVerify));
                    string contactData = getDataFromUser(contactToVerify);
                    contactss.Add(new contact("  " + Parse(contactData, "FirstName") + " " + Parse(contactData, "LastName"), contactToVerify, chats[chats.Count - 1], this));
                    friendP.add(contactss[contactss.Count - 1]);
                }

            }


        }





    }
}
