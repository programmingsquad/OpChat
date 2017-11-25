using System;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace OPChat___Design
{
    public partial class friendPanel : UserControl
    {
        public friendPanel()
        {
            InitializeComponent();
            addfriends.Visible = false;
            friendrequests.Visible = false;
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

        public string getMessages(string user1, string user2)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/messages/user1={0}/user2={1}", user1, user2);

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
        

        public void add(contact contactToAdd) {
            panel1.Controls.Add(contactToAdd);
            contactToAdd.Dock = DockStyle.Top;

        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          
        }
    }
}
