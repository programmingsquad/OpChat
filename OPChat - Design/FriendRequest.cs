using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace OPChat___Design
{
    public partial class FriendRequest : UserControl
    {
        string myUsername;
        string from;
        friendPanel parent;

        public FriendRequest(string myUsername, string from, friendPanel parent)
        {
            InitializeComponent();
            this.myUsername = myUsername;
            this.from = from;
            bunifuFlatButton1.Text = this.from;
            this.parent = parent;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            deleteRequest(getRequestId(from, myUsername));
            parent.removeRequest(this);
        }


        public string getRequestId(string from, string to)
        {

            string html = string.Empty;
            string url = String.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/getrequesttodelete/from={0}/to={1}", from, to);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) { html = reader.ReadToEnd(); }
            return Parse(html, "id");

        }

        public string deleteRequest(string id)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/request/delete/" + id;
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (parent.addFriend(myUsername, from)) {

                deleteRequest(getRequestId(from, myUsername));
                parent.removeRequest(this);

            }
        }
    }
}
