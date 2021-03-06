﻿using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace OPChat___Design
{
    public partial class friendPanel : UserControl
    {
        string myUsername;
        List<string> contactUsernames;
        screen parent;

        public friendPanel(string myUsername, screen parent)
        {
            InitializeComponent();
            addfriends.Visible = false;
            friendrequests.Visible = false;
            slideDaddfriends.Visible = false;
            slideDfriendrequests.Visible = false;

            this.myUsername = myUsername;
            this.parent = parent;

          
        }

        public void NoFriends()
        {
            if (parent.contactss.Count == 0)
            {

                slideA.Visible = false;
                slideA.Left = 788;

                slideD.Visible = false;
                slideD.Left = 0;
                slideD.Visible = true;
                slideD.Refresh();
            }
        }


        public void friendoptions_Click(object sender, EventArgs e)
        {
            if (addfriends.Visible == false && friendrequests.Visible == false)
            {
                addfriends.Visible = true;
                friendrequests.Visible = true;
            }
            else
            {
                addfriends.Visible = false;
                friendrequests.Visible = false;
            }
        }

        public void addfriends_Click(object sender, EventArgs e)
        {
            addfriends.Visible = false;
            friendrequests.Visible = false;

            slideA.Visible = false;
            slideA.Left = 525;

            slideC.Visible = false;
            slideC.Left = 0;
            slideC.Visible = true;
            slideC.Refresh();
        }

        public void friendrequests_Click(object sender, EventArgs e)
        {

            updateFriendRequests();

            addfriends.Visible = false;
            friendrequests.Visible = false;

            slideA.Visible = false;
            slideA.Left = 263;

            slideB.Visible = false;
            slideB.Left = 0;
            slideB.Visible = true;
            slideB.Refresh();
        }

        public void returnB_Click(object sender, EventArgs e)
        {
            slideB.Visible = false;
            slideB.Left = 263;

            slideA.Visible = false;
            slideA.Left = 0;
            slideA.Visible = true;
            slideA.Refresh();
        }

        public void returnC_Click(object sender, EventArgs e)
        {
            slideC.Visible = false;
            slideC.Left = 525;

            slideA.Visible = false;
            slideA.Left = 0;
            slideA.Visible = true;
            slideA.Refresh();
        }


        public void setList(List<string> contactUsernames) {

            this.contactUsernames = contactUsernames;
        }
      
        

        public void add(contact contactToAdd) {
            panel1.Controls.Add(contactToAdd);
            contactToAdd.Dock = DockStyle.Top;

            slideDAddingError.Visible = false;

            slideDaddUserTextBox.Text = "";
            slideD.Visible = false;
            slideD.Left = 788;

            slideA.Visible = false;
            slideA.Left = 0;
            slideA.Visible = true;
            slideA.Refresh();
        }

       

       

        private void addUserTextBox_Leave(object sender, EventArgs e)
        {
            addUserTextBox.Text = "Add someone";
        }

       

        private void addUserTextBox_Enter(object sender, EventArgs e)
        {
            addUserTextBox.Text = "";
        }



        public void unfriend(string user1, string user2, contact contactToDelete) {

            string infoFrom1 = getDataFromUser(user1);
            string infoFrom2 = getDataFromUser(user2);
            List<string> friendsFrom1 = new List<string>();
            List<string> friendsFrom2 = new List<string>();

            for (int contactNumber = 1; contactNumber < 6; contactNumber++)
            {
                string contactFrom1 = Parse(infoFrom1, "Friend" + contactNumber);
                string contactFrom2 = Parse(infoFrom2, "Friend" + contactNumber);
                friendsFrom1.Add(contactFrom1); 
                friendsFrom2.Add(contactFrom2);
            }

           
            removeFriendInDb(user1, "Friend" + (friendsFrom1.IndexOf(user2) + 1).ToString());
            removeFriendInDb(user2, "Friend" + (friendsFrom2.IndexOf(user1) + 1).ToString());
            localRemove(contactToDelete);

            
        }

        public void localRemove(contact ctc) {
            parent.tempFriends.Clear();
            parent.contactUsernames.Remove(ctc.contactUser);
            parent.contactss.Remove(ctc);
            panel1.Controls.Remove(ctc);
            if (parent.contactss.Count == 0) { NoFriends(); }
        }

        public string removeFriendInDb(string user, string friend)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/removefriend/user={0}/friend={1}", user, friend);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;


        }








        public string getDataFromUser(string username)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/user/" + username.ToLower();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }


        public Boolean canSendFriendRequest(string to) {
            if (contactUsernames.Count == 5) { AddingError.Text = "You have reached the friend limit"; slideDAddingError.Text = "You have reached the friend limit"; return false; }
            if (myUsername == to) { AddingError.Text = "When your only friend is you"; slideDAddingError.Text = "When your only friend is you";  return false; }
            if (verifyFriendRequest(to) != "[]") { AddingError.Text = "Friend request already sent"; slideDAddingError.Text = "Friend request already sent"; return false; }
            if (contactUsernames.Contains(to)) { AddingError.Text = "You are already friends with that person"; slideDAddingError.Text = "You are already friends with that person"; return false; }
            return true;

        }


        public string verifyFriendRequest(string contact)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/verifyfriendrequest/user1={0}/user2={1}", myUsername.ToLower(), contact.ToLower());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public string sendFriendRequest(string contact)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/friendrequest/from={0}/to={1}", myUsername.ToLower(), contact.ToLower());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        public string getRequestsTo(string user)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/getfriendrequests/to=" + user.ToLower();
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

        public static List<string> ParseRqs(string input)
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

        List<FriendRequest> FriendRequests = new List<FriendRequest>();
        List<string> FriendRequestsByUser = new List<string>();


        public void updateFriendRequests() {

            foreach (string request in ParseRqs(getRequestsTo(myUsername))) {
                if (!FriendRequestsByUser.Contains(Parse(request, "fromm"))) { 
                FriendRequestsByUser.Add(Parse(request, "fromm"));
                FriendRequests.Add(new FriendRequest(myUsername, Parse(request, "fromm"), this));
                }
            }

            foreach (FriendRequest request in FriendRequests) {

                request.Dock = DockStyle.Top;
                requestHolder.Controls.Add(request);

            }

        }

        public void removeRequest(FriendRequest request) {

            for (int i = 0; i < FriendRequests.Count; i++) { FriendRequests.RemoveAt(i); }
            requestHolder.Controls.Remove(request);

        }

                     
    public Boolean addFriend(string user1, string user2) {

         string infoFrom1 = getDataFromUser(user1);
         string infoFrom2 = getDataFromUser(user2);
         List<string> friendsFrom1 = new List<string>();
         List<string> friendsFrom2 = new List<string>();

        for (int contactNumber = 5; contactNumber > 0; contactNumber--)
        {
            string contactFrom1 = Parse(infoFrom1, "Friend" + contactNumber);
            string contactFrom2 = Parse(infoFrom2, "Friend" + contactNumber);
            if (contactFrom1 != "") { friendsFrom1.Add(contactFrom1); }
            if (contactFrom2 != "") { friendsFrom2.Add(contactFrom2); }
        }

        if (friendsFrom1.Count == 5 || friendsFrom2.Count == 5) { return false; }

        string placeToAddIn1 = "Friend" + (1 + friendsFrom1.Count);
        string placeToAddIn2 = "Friend" + (1 + friendsFrom2.Count);

        addFriendInDb(user1, user2, getLowestEmpty(infoFrom1));
        addFriendInDb(user2, user1, getLowestEmpty(infoFrom2));

        parent.addContactToPanel(myUsername, Parse(infoFrom2, "FirstName") + " " + Parse(infoFrom2, "LastName"), user2);


        return true;

        }

        public string getLowestEmpty(string info) { 
            for (int contactNumber = 1; contactNumber < 6; contactNumber++)
            {
                string contact = Parse(info, "Friend" + contactNumber);
                if (contact != "") { } else { return "Friend"  + contactNumber; }
            }
            return "";
        }


        public string addFriendInDb(string user, string contact,string inn)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/addfriend/user={0}/contact={1}/in={2}",user.ToLower(), contact.ToLower(), inn);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {
            if (addUserTextBox.Text != "")
            {
                if (getDataFromUser(addUserTextBox.Text) != "false")
                {

                    if (canSendFriendRequest(addUserTextBox.Text))
                    {

                        if (sendFriendRequest(addUserTextBox.Text) == "easy")
                        {
                            AddingError.Visible = false;

                            addUserTextBox.Text = "";
                            slideC.Visible = false;
                            slideC.Left = 525;

                            slideA.Visible = false;
                            slideA.Left = 0;
                            slideA.Visible = true;
                            slideA.Refresh();

                        }
                        else
                        {
                            ConnectionIssues issues = new ConnectionIssues();
                            issues.ShowDialog();
                        }
                    }
                    else
                    {
                        AddingError.Visible = true;
                    }
                }

                else
                {
                    AddingError.Visible = true;
                    AddingError.Text = "Username doesn't exist";
                }
            }
        }

        private void addUserTextBox_Enter_1(object sender, EventArgs e)
        {
            addUserTextBox.Text = "";
        }

        private void addUserTextBox_Leave_1(object sender, EventArgs e)
        {
            addUserTextBox.Text = "Add someone";
        }

        private void slideDfriendoptions_Click(object sender, EventArgs e)
        {
            if (slideDaddfriends.Visible == false && slideDfriendrequests.Visible == false)
            {
                slideDaddfriends.Visible = true;
                slideDfriendrequests.Visible = true;
            }
            else
            {
                slideDaddfriends.Visible = false;
                slideDfriendrequests.Visible = false;
            }
        }

        private void slideDaddfriends_Click(object sender, EventArgs e)
        {
            addfriends.Visible = false;
            friendrequests.Visible = false;

            slideD.Visible = false;
            slideD.Left = 788;

            slideC.Visible = false;
            slideC.Left = 0;
            slideC.Visible = true;
            slideC.Refresh();
        }

        private void slideDfriendrequests_Click(object sender, EventArgs e)
        {
            updateFriendRequests();

            addfriends.Visible = false;
            friendrequests.Visible = false;

            slideD.Visible = false;
            slideD.Left = 788;

            slideB.Visible = false;
            slideB.Left = 0;
            slideB.Visible = true;
            slideB.Refresh();
        }

        private void slideDaddUserTextBox_Leave(object sender, EventArgs e)
        {
            slideDaddUserTextBox.Text = "Add someone";
        }

        private void slideDaddUserTextBox_Enter(object sender, EventArgs e)
        {
            slideDaddUserTextBox.Text = "";
        }

        private void slideDAddButton_Click(object sender, EventArgs e)
        {
            if (slideDaddUserTextBox.Text != "")
            {
                if (getDataFromUser(slideDaddUserTextBox.Text) != "false")
                {

                    if (canSendFriendRequest(slideDaddUserTextBox.Text))
                    {

                        if (sendFriendRequest(slideDaddUserTextBox.Text) == "easy")
                        {
                            //Mensagem a dizer pedido enviado com sucesso
                        }
                        else
                        {
                            ConnectionIssues issues = new ConnectionIssues();
                            issues.ShowDialog();
                        }
                    }
                    else
                    {
                        slideDAddingError.Visible = true;
                    }
                }

                else
                {
                    slideDAddingError.Visible = true;
                    slideDAddingError.Text = "Username doesn't exist";
                }
            }
        }

        private void slideDaddUserTextBox_TextChanged(object sender, EventArgs e)
        {
            slideDAddingError.Visible = false;
        }

        private void addUserTextBox_TextChanged(object sender, EventArgs e)
        {
            AddingError.Visible = false;
        }







    }
}

