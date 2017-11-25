using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OPChat___Design
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
        }

        //Movable window
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        //Sign up process 
        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (AllValid())
            { 
                if (getDataSignUp(Username.Text) == "false")
                {
                    label2.Visible = false;
                    if (getDataSignUp2(FirstName.Text, LastName.Text, Username.Text, Password.Text) == "easy")
                    {
                        SuccessfulRegister register = new SuccessfulRegister();
                        register.ShowDialog();
                        SignUp.Enabled = true;
                    }
                    else
                    {
                        ConnectionIssues issues = new ConnectionIssues();
                        issues.ShowDialog();
                        SignUp.Enabled = true;
                    }

                }
                else
                {
                    label2.Visible = true;
                    SignUp.Enabled = true;
                }
            }
        }

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox8_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }
       
        //Go to sign in
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (slideB.Left == 574)
            {
                slideA.Visible = false;
                slideA.Left = 574;

                slideB.Visible = false;
                slideB.Left = 53;
                slideB.Visible = true;
                slideB.Refresh();

                bunifuSeparator1.Left = bunifuThinButton22.Left;
                bunifuSeparator1.Width = bunifuThinButton22.Width;
            }
        }

        //Go to sign up
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (slideA.Left == 574)
            {
                slideB.Visible = false;
                slideB.Left = 574;

                slideA.Visible = false;
                slideA.Left = 53;
                slideA.Visible = true;
                slideA.Refresh();

                bunifuSeparator1.Left = bunifuThinButton21.Left;
                bunifuSeparator1.Width = bunifuThinButton21.Width;
            }
        }

        //Close the window
        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Minimize the window
        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        public bool isValid(string texto) { 
        
            string[] caractersInvalidos = { "@", "#", "$", "%", "_", "-", "+", "=", "|", " < ", ">", ":", ";", "," };
            foreach (String elemento in caractersInvalidos) { if (texto.Contains(elemento)) { return false; } }

            return true;
        }

        public Boolean AllValid()
        {
            label5.Visible = false;
            if (!isValid(FirstName.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog(); 
                return false;
            }

            if (FirstName.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (!isValid(LastName.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (LastName.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (!isValid(Username.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (Username.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }
            if (!isValid(Password.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }
            if (Password.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }
            if (!isValid(bunifuMetroTextbox4.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (bunifuMetroTextbox4.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }
            if (Password.Text != bunifuMetroTextbox4.Text)
            {

                label5.Visible = true;
                return false;
            }
            return true;
        }

        public string getDataSignUp(string Username)
        {

            string html = string.Empty;
            string url = @"http://opchat.x10.mx/public/index.php/api/user/" + Username;

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

        public string getDataSignUp2(string FirstName, string LastName, string Username, string Password)
        {

            string html = string.Empty;
            string url = string.Format(@"http://opchat.x10.mx/public/index.php/api/user/add/firstName={0}/lastName={1}/user={2}/pass={3}", FirstName, LastName, Username, Password);
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

        //Code for opening chat 
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (Login())
            {

            }
        }

        //Sign in process
        public bool Login()
        {

            if (AllValid2())
            {
                label3.Visible = false;
                label4.Visible = false;
                SignUp.Enabled = true;
                string response = getDataSignIn(Username2.Text);

                if (response != "false")
                {

                    if (Password2.Text == parsePassword(response))
                    {
                        slideB.Visible = false;

                        slideC.Visible = false;
                        slideC.Left = 53;
                        slideC.Visible = true;
                        slideC.Refresh();

                        label1.Visible = false;

                        return true;
                    }
                    else
                    {
                        label4.Visible = true;
                        SignIn.Enabled = true;
                        return false;
                    }

                }
                else
                {
                    label3.Visible = true;
                    SignIn.Enabled = true;
                    return false;
                }
            }

            return false;
        }
      
        public bool isValid2(string texto)
        {

            string[] caractersInvalidos = { "@", "#", "$", "%", "_", "-", "+", "=", "|", " < ", ">", ":", ";", "," };

            foreach (String elemento in caractersInvalidos) { if (texto.Contains(elemento)) { return false; } }

            return true;

        }
    
        //(Changed DB)
        public string getDataSignIn(string Username2)
        {

            string html = string.Empty;
            string url = @"http://opchat.x10.mx/public/index.php/api/user/" + Username2;

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

        public static String parsePassword(string input)
        {

            return input.Split(':').Last().Replace("\"", "").Split(':').Last().Replace("}", "");

        }

        public static String parseFirstName(string input)
        {

            input = input.Replace(input.Split(':').First(), "").Replace("\"", "").Replace(":", "").Split(',').First();

            return input;
        }

        public Boolean AllValid2()
        {

            if (!isValid(Username2.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (Username2.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }
            if (!isValid(Password2.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (Password2.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            return true;

        }

    }
}

