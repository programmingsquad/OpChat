namespace OPChat___Design
{
    partial class Alert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alert));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuThinButton27 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 1;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuThinButton27
            // 
            this.bunifuThinButton27.ActiveBorderThickness = 1;
            this.bunifuThinButton27.ActiveCornerRadius = 1;
            this.bunifuThinButton27.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.bunifuThinButton27.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.bunifuThinButton27.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.bunifuThinButton27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.bunifuThinButton27.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuThinButton27.BackgroundImage")));
            this.bunifuThinButton27.ButtonText = "X";
            this.bunifuThinButton27.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuThinButton27.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuThinButton27.ForeColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton27.IdleBorderThickness = 1;
            this.bunifuThinButton27.IdleCornerRadius = 1;
            this.bunifuThinButton27.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.bunifuThinButton27.IdleForecolor = System.Drawing.Color.White;
            this.bunifuThinButton27.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.bunifuThinButton27.Location = new System.Drawing.Point(279, 2);
            this.bunifuThinButton27.Margin = new System.Windows.Forms.Padding(4);
            this.bunifuThinButton27.Name = "bunifuThinButton27";
            this.bunifuThinButton27.Size = new System.Drawing.Size(19, 16);
            this.bunifuThinButton27.TabIndex = 16;
            this.bunifuThinButton27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuThinButton27.Click += new System.EventHandler(this.bunifuThinButton27_Click);
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.Silver;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(12, 32);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(266, 51);
            this.bunifuCustomLabel5.TabIndex = 17;
            this.bunifuCustomLabel5.Text = "Warning! Something went awry. Please \r\nverify that you\'ve filled all the fields a" +
    "nd \r\nthat you haven\'t used any symbols.\r\n";
            // 
            // Alert
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(300, 104);
            this.Controls.Add(this.bunifuCustomLabel5);
            this.Controls.Add(this.bunifuThinButton27);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Alert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuThinButton2 bunifuThinButton27;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
    }
}