using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using TestChat;
using System.Text;
using System.Security.Cryptography;

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

        //Set up loading
        public async Task Loading()
        {
            await Task.Delay(1000);
        }

        //Sign up process 
        private async void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            FirstName.Text = FirstName.Text.Trim();
            LastName.Text = LastName.Text.Trim();
            Username.Text = Username.Text.Trim();
            Password.Text = Password.Text.Trim();
            ConfirmPassword.Text = ConfirmPassword.Text.Trim();

            if (AllValid())
            { 
                if (getDataFromUser(Username.Text) == "false")
                {
                    label2.Visible = false;
                    if (registerUser(FirstName.Text, LastName.Text, Username.Text.Trim(), Password.Text) == "easy")
                    {
                        slideA.Visible = false;

                        slideC.Visible = false;
                        slideC.Left = 53;
                        slideC.Visible = true;
                        slideC.Refresh();

                        await Loading();

                        this.Hide();
                        ChatScreen chat = new ChatScreen(getDataFromUser(Username.Text));
                        chat.ShowDialog();
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
        
            string[] caractersInvalidos = {"@", " ", "#", "$", "%", "_", "-", "+", "=", "|", " < ", ">", ":", ";", ",","ç", "á", "à", "ã", "â", "ô", "ê" , "é", "õ", "ó", "ç", "Á", "À", "Ã", "Â", "Ó", "Ê", "É", "Õ", "Ó", "´", "`", "~", "^"};
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
            if (!isValid(ConfirmPassword.Text))
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }

            if (ConfirmPassword.Text == string.Empty)
            {
                Alert alert = new Alert();
                alert.ShowDialog();
                return false;
            }
            if (Password.Text != ConfirmPassword.Text)
            {

                label5.Visible = true;
                return false;
            }
            return true;
        }

        public string registerUser(string FirstName, string LastName, string Username, string Password)
        {

            string html = string.Empty;
            string url = string.Format(@"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/user/add/firstName={0}/lastName={1}/user={2}/pass={3}", FirstName, LastName, Username.ToLower(), Password);
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
        private async void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (Login())
            {
                slideB.Visible = false;

                slideC.Visible = false;
                slideC.Left = 53;
                slideC.Visible = true;
                slideC.Refresh();

                await Loading();

                this.Hide();
                ChatScreen chat = new ChatScreen(getDataFromUser(Username2.Text));
                chat.ShowDialog();
            }
        }

        //Sign in process
        public bool Login()
        {
            Username.Text = Username.Text.Trim();
            Password.Text = Password.Text.Trim();

            if (AllValid2())
            {
                label3.Visible = false;
                label4.Visible = false;
                SignUp.Enabled = true;
                string response = getDataFromUser(Username2.Text);

                if (response != "false")
                {

                    if (Password2.Text == Parse(response, "Password"))
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

            string[] caractersInvalidos = { "%", "+", "=", "|", ":", "{", "}", "[", "]" };

            foreach (String elemento in caractersInvalidos) { if (texto.Contains(elemento)) { return false; } }

            return true;

        }
    
        //(Changed DB)
        public string getDataFromUser(string Username2)
        {

            string html = string.Empty;
            string url = @"http://passarentrar.madeiratorres.com/opchat/public/index.php/api/user/" + Username2.ToLower();

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

        public static string Parse(string input, string tag)
        {
            input = input.Replace("{", "").Replace("}", "");
            string[] inputArray = input.Split(',');
            foreach (string y in inputArray)
            {
                if (y.Contains(tag))
                {
                    return y.Split(':').Last().ToString().Replace("\"","");
                }
            }
            return "erro";
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

