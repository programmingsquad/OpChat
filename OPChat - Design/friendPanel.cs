using System;
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

        public friendPanel(string myUsername, screen parent )
        {
            InitializeComponent();
            addfriends.Visible = false;
            friendrequests.Visible = false;
            this.myUsername = myUsername;
            this.parent = parent;
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

        
        }

       

       

        private void addUserTextBox_Leave(object sender, EventArgs e)
        {
            addUserTextBox.Text = "Add someone";
        }

       

        private void addUserTextBox_Enter(object sender, EventArgs e)
        {
            addUserTextBox.Text = "";
        }


        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if (addUserTextBox.Text != "") {
                if(getDataFromUser(addUserTextBox.Text) != "false") {

                    if (canSendFriendRequest(addUserTextBox.Text))
                    {

                        if (sendFriendRequest(addUserTextBox.Text) == "easy")
                        {
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

                            //erro de conexao ADD!

                        }

                    }
                    else { /*ja sao amigos ou ja enviou pedido ou ja tem 5 amigos Erro ADDDDDD */}
                }
            }
        }


        public string getDataFromUser(string username)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/user/" + username;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }


        public Boolean canSendFriendRequest(string to) {
            if (contactUsernames.Count == 5) { return false; }
            if (myUsername == to) { return false; }
            if (verifyFriendRequest(to) != "[]") { return false; }
            if (contactUsernames.Contains(to)) { return false; }
            return true;

        }


        public string verifyFriendRequest(string contact)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/verifyfriendrequest/user1={0}/user2={1}", myUsername, contact);
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
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/friendrequest/from={0}/to={1}", myUsername, contact);
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
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/getfriendrequests/to=" + user;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

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

        public void updateFriendRequests() {

            foreach (string request in ParseRqs(getRequestsTo(myUsername))) {

                FriendRequests.Add(new FriendRequest(myUsername, Parse(request, "fromm"), this));

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

        addFriendInDb(user1, user2, placeToAddIn1);
        addFriendInDb(user2, user1, placeToAddIn2);

        parent.addContactToPanel(myUsername, Parse(infoFrom2, "FirstName") + " " + Parse(infoFrom2, "LastName"), user2);


        return true;

        }


        public string addFriendInDb(string user, string contact,string inn)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/addfriend/user={0}/contact={1}/in={2}",user,contact, inn);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return html;

        }


    }

}
