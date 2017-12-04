using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using TestChat;


namespace OPChat___Design
{
    public partial class contact : UserControl
    {
        public string contactName;
       public string contactUser;
       public chatbox2 chatbox;
        screen parent;

        public contact()
        {
            InitializeComponent();
        }

            public contact(string contactName, string contactUser, chatbox2 chatbox, screen parent)
        {
            InitializeComponent();
            this.contactName = contactName;
            this.contactUser = contactUser;
            this.chatbox = chatbox;
            this.parent = parent;
            this.chatbox.loadChat();
            label1.Text = this.contactName;
        }
        
   

        private void deleteConversationToolStripMenuItem_Click(object sender, EventArgs e)
        {



            DialogResult dr = MessageBox.Show("Are you sure?", "Delete conversation", MessageBoxButtons.YesNo,
      MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                deleteConversation(parent.myUsername, contactUser, chatbox);
            }
            
        }



        public void deleteConversation(string username, string contact, chatbox2 chatbox)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/deleteconversation/user1={0}/user2={1}", username, contact);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            parent.removeChat(chatbox, this);

        }


        private void unfriendToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Are you sure?", "Unfriend" + contactName, MessageBoxButtons.YesNo,
      MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                deleteConversation(parent.myUsername, contactUser, chatbox);
                parent.friendP.unfriend(parent.myUsername, contactUser, this);
            }
        }

      
        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(PointToScreen(e.Location));
            } else {
                parent.currentC = this;
                chatbox.scrollToBot();
            parent.ChangeToChat(chatbox);
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(PointToScreen(e.Location));
            }
            else
            {
                parent.currentC = this;
                chatbox.scrollToBot();
                parent.ChangeToChat(chatbox);
            }
        }

        private void bunifuImageButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(PointToScreen(e.Location));
            }
            else
            {
                parent.currentC = this;
                chatbox.scrollToBot();
                parent.ChangeToChat(chatbox);
                
            }
        }

        private void contact_MouseEnter(object sender, EventArgs e)
        {
     
        }
       
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(22, 147, 110);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            
            
                panel1.BackColor = Color.FromArgb(26, 177, 136); 
            
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(22, 147, 110);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(26, 177, 136);
        }

        private void bunifuImageButton1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(22, 147, 110);
        }

        private void bunifuImageButton1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(26, 177, 136);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
