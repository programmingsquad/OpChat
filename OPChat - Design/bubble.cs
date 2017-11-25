using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestChat
{
    public partial class bubble : UserControl
    {
        public bubble()
        {
            InitializeComponent();
        }


        public bubble(string message, string timee, msgtype messagetype)
        {
            InitializeComponent();
            msg.Text = message;
            time.Text = timee;

            if (messagetype.ToString() == "In")
            {
                this.BackColor = Color.Gray;

            }
            else {

                this.BackColor = Color.FromArgb(26, 177, 136);
            }
            Setheight();

        }


        public enum msgtype {

            In,Out


        }





        void Setheight()
        {

            Size maxSize = new Size(495, int.MaxValue);
            Graphics g = CreateGraphics();
            SizeF size = g.MeasureString(msg.Text, msg.Font, msg.Width);

            msg.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());

            
            time.Top = msg.Bottom + 10;
            this.Height = time.Bottom + 10;


        }


        private void bubble_Resize(object sender, EventArgs e)
        {
            Setheight();
        }
    }
}
