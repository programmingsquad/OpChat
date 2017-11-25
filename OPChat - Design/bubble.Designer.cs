namespace TestChat
{
    partial class bubble
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.msg = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // msg
            // 
            this.msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg.ForeColor = System.Drawing.Color.White;
            this.msg.Location = new System.Drawing.Point(11, 13);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(433, 57);
            this.msg.TabIndex = 1;
            this.msg.Text = "Lo sdg dasuuiusangf f sadugh oasiudgh asiudg asudgh sauidh gpiuashd iausdhg uiasd" +
    "hg  saudgu  usaudgasg";
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.ForeColor = System.Drawing.Color.White;
            this.time.Location = new System.Drawing.Point(11, 79);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(60, 13);
            this.time.TabIndex = 2;
            this.time.Text = "Wed 13:12";
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bubble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.Controls.Add(this.time);
            this.Controls.Add(this.msg);
            this.Name = "bubble";
            this.Size = new System.Drawing.Size(458, 101);
            this.Resize += new System.EventHandler(this.bubble_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Label time;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
    }
}
