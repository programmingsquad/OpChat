namespace OPChat___Design
{
    partial class contact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(contact));
            this.btn = new Bunifu.Framework.UI.BunifuFlatButton();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn.BorderRadius = 0;
            this.btn.ButtonText = " Contact1";
            this.btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn.DisabledColor = System.Drawing.Color.Gray;
            this.btn.Iconcolor = System.Drawing.Color.Transparent;
            this.btn.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn.Iconimage")));
            this.btn.Iconimage_right = null;
            this.btn.Iconimage_right_Selected = null;
            this.btn.Iconimage_Selected = null;
            this.btn.IconMarginLeft = 0;
            this.btn.IconMarginRight = 0;
            this.btn.IconRightVisible = true;
            this.btn.IconRightZoom = 0D;
            this.btn.IconVisible = true;
            this.btn.IconZoom = 70D;
            this.btn.IsTab = false;
            this.btn.Location = new System.Drawing.Point(-5, -6);
            this.btn.Margin = new System.Windows.Forms.Padding(0);
            this.btn.Name = "btn";
            this.btn.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.btn.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(120)))));
            this.btn.OnHoverTextColor = System.Drawing.Color.White;
            this.btn.selected = false;
            this.btn.Size = new System.Drawing.Size(275, 85);
            this.btn.TabIndex = 2;
            this.btn.Text = " Contact1";
            this.btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn.Textcolor = System.Drawing.Color.White;
            this.btn.TextFont = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            // 
            // contact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn);
            this.Name = "contact";
            this.Size = new System.Drawing.Size(265, 74);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuFlatButton btn;
    }
}
