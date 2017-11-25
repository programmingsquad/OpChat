using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestChat;


namespace OPChat___Design
{
    public partial class contact : UserControl
    {
        string contactName;
        string contactUser;
        chatbox2 chatbox;
        Boolean clicked = false;

        public contact(string contactName, string contactUser, chatbox2 chatbox)
        {
            InitializeComponent();
            this.contactName = contactName;
            this.contactUser = contactUser;
            this.chatbox = chatbox;
            this.chatbox.loadChat();
            btn.Text = this.contactName;
        }


        
       

    }
}
