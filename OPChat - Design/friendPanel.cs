using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace OPChat___Design
{
    public partial class friendPanel : UserControl
    {
        string myUsername;
        List<string> contactUsernames;

        public friendPanel(string myUsername )
        {
            InitializeComponent();
            addfriends.Visible = false;
            friendrequests.Visible = false;
            this.myUsername = myUsername;
            
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


        public void updateFriendRequests() {



        }


    }
}


