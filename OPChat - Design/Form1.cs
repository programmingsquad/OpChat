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

            //Creates Upload and download folders
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (!Directory.Exists(path + @"\OpChat\Uploads"))
            {
                Directory.CreateDirectory(path + @"\OpChat\Uploads");
            }
            if (!Directory.Exists(path + @"\OpChat\Downloads"))
            {
                Directory.CreateDirectory(path + @"\OpChat\Downloads");
            }
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
            await Task.Delay(500);
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
                        label1.Visible = false;

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
                    label2.Text = "Username already taken";

                    SignUp.Enabled = true;
                }
            }
        }

        private async void bunifuThinButton23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                            e.Handled = true;
                            e.SuppressKeyPress = true;

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

                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
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

                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
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
        
            string[] caractersInvalidos = {"*", ".","\"", "/", "\\", "[", "]", ":", ";", "|", "=", ",", "{", "}", "%" };
            foreach (String elemento in caractersInvalidos) { if (texto.Contains(elemento)) { return false; } }

            return true;
        }

        public bool isValid2(string texto)
        {

            string[] caractersInvalidos = { "*", ".", "\"", "/", "\\", "[", "]", ":", ";", "|", "=", ",", "{", "}", "à", "á", "ã", "â", "À", "Á", "Ã", "Â", "ò", "ó", "õ", "ô", "Ò", "Ó", "Õ", "Ô", "è", "é", "ê", "É", "È", "Ê", "ç", "Ç", "%" };
            foreach (String elemento in caractersInvalidos) { if (texto.Contains(elemento)) { return false; } }

            return true;
        }

        public Boolean AllValid()
        {
            if (!isValid(FirstName.Text))
            {
                label7.Visible = true;
                label7.Text = "Symbols aren't allowed";
                return false;
            }

            if (FirstName.Text == string.Empty)
            {
                label7.Visible = true;
                label7.Text = "Fill in all the textboxes";
                return false;
            }

            if (!isValid(LastName.Text))
            {
                label8.Visible = true;
                label8.Text = "Symbols aren't allowed";
                return false;
            }

            if (LastName.Text == string.Empty)
            {
                label8.Visible = true;
                label8.Text = "Fill in all the textboxes";
                return false;
            }

            if (!isValid2(Username.Text))
            {
                label2.Visible = true;
                label2.Text = "Symbols and accent marks aren't allowed";
                return false;
            }

            if (Username.Text == string.Empty)
            {
                label2.Visible = true;
                label2.Text = "Fill in all the textboxes";
                return false;
            }
            if (!isValid2(Password.Text))
            {
                label5.Visible = true;
                label5.Text = "Symbols and accent marks aren't allowed";
                return false;
            }
            if (Password.Text == string.Empty)
            {
                label5.Visible = true;
                label5.Text = "Fill in all the textboxes";
                return false;
            }
            if (!isValid2(ConfirmPassword.Text))
            {
                label5.Visible = true;
                label5.Text = "Symbols and accent marks aren't allowed";
                return false;
            }

            if (ConfirmPassword.Text == string.Empty)
            {
                label7.Visible = true;
                label7.Text = "Fill in all the textboxes";
                return false;
            }
            if (Password.Text != ConfirmPassword.Text)
            {
                label5.Visible = true;
                label5.Text = "Passwords don't match";
                return false;
            }
            if (Username.Text.Length > 12)
            {
                label2.Visible = true;
                label2.Text = "Usernames can only have up to 12 characters";
                return false;
            }
            /*if (Password.Text.Length < 8) //Activate when testing is finished
            {
                label5.Visible = true;
                label5.Text = "Your password must be at least 8 characters long";
                return false;
            }*/
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

        private async void bunifuThinButton24_KeyDown (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Login())
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;

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
        }

        //Sign in process
        public bool Login()
        {
            Username.Text = Username.Text.Trim();
            Password.Text = Password.Text.Trim();

            if (AllValid2())
            {
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
                        label4.Text = "Incorrect password";

                        SignIn.Enabled = true;
                        return false;
                    }

                }
                else
                {
                    label3.Visible = true;
                    label3.Text = "Username not found";

                    SignIn.Enabled = true;
                    return false;
                }
            }

            return false;
        }
        
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

        public Boolean AllValid2()
        {

            if (!isValid(Username2.Text))
            {
                label3.Visible = true;
                label3.Text = "Symbols and accent marks aren't allowed";
                return false;
            }

            if (Username2.Text == string.Empty)
            {
                label3.Visible = true;
                label3.Text = "Fill in all the textboxes";
                return false;
            }
            if (!isValid(Password2.Text))
            {
                label4.Visible = true;
                label4.Text = "Symbols and accent marks aren't allowed";
                return false;
            }

            if (Password2.Text == string.Empty)
            {
                label4.Visible = true;
                label4.Text = "Fill in all the textboxes";
                return false;
            }

            return true;

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
        }
    }
}

