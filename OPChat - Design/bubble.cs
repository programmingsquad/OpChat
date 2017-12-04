using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using OPChat___Design;

namespace TestChat
{
    public partial class bubble : UserControl
    {

        Image img;
        string path;
        chatbox2 parent;


        public bubble()
        {
            InitializeComponent();
        }


        public bubble(string message, string timee, msgtype messagetype, chatbox2 parent)
        {
            InitializeComponent();
            msg.Text = message;
            time.Text = timee;
            this.parent = parent;
            if (messagetype.ToString() == "In")
            {
                this.BackColor = Color.Gray;

            }
            else {

                this.BackColor = Color.FromArgb(26, 177, 136);
            }
            Setheight();

        }

        public bubble(Image img, String timee , msgtype messagetype, chatbox2 parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.img = img;
            msg.Text = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdfffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffsdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
            time.Text = timee;

            if (messagetype.ToString() == "In")
            {
                this.BackColor = Color.Gray;

            }
            else
            {

                this.BackColor = Color.FromArgb(26, 177, 136);
            }
            this.Height = 360;
            this.Width = 458;
            pictureBox1.Visible = true;
            pictureBox1.Image = ResizeImage(img, 350, 350);
            pictureBox1.Top = 25;
            pictureBox1.Left = 30;
            msg.Visible = false;
        }


        public bubble( string fileName, string filePath, string timee, msgtype messagetype, chatbox2 parent)
        {

           
            InitializeComponent();
            this.parent = parent;
            saveFileDialog1.FileName = fileName;
            this.path = filePath;
            time.Text = timee;
            msg.Text = "Filenadsssssssssssssssssssssssssssssssmesssssssssssssssssssssssssssssssssssssssssssme:";
            nameOfFile.Text = fileName;

            if (messagetype.ToString() == "In")
            {
                this.BackColor = Color.Gray;
                this.bunifuFlatButton1.Normalcolor = Color.FromArgb(26, 177, 136);
                this.bunifuFlatButton1.OnHovercolor = Color.FromArgb(20, 155, 118);
            }
            else
            {

                this.BackColor = Color.FromArgb(26, 177, 136);
                this.bunifuFlatButton1.Normalcolor = Color.Gray;
                this.bunifuFlatButton1.OnHovercolor = Color.DimGray;
            }
            msg.Visible = false;
            bunifuFlatButton1.Top = 25;
            bunifuFlatButton1.Left = 250;
            bunifuFlatButton1.Visible = true;
            labelFilename.Visible = true;
            nameOfFile.Visible = true;
        }





        public enum msgtype {

            In,Out


        }


        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveFileDialog1.FileName;
                if (!File.Exists(savePath))
                {
                    File.Copy(path, savePath);
                }



            }


       }

        private void bubble_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {



                contextMenuStrip1.Show(PointToScreen(e.Location));
             

            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parent.removeMsg(this);
        }

    }
}
