
namespace Client
{
    partial class TopluSohbetFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbToplu = new System.Windows.Forms.ListBox();
            this.rbToplu = new System.Windows.Forms.RichTextBox();
            this.btnTopluGonder = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbToplu);
            this.groupBox1.Location = new System.Drawing.Point(319, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arkadaş Listesi";
            // 
            // lbToplu
            // 
            this.lbToplu.FormattingEnabled = true;
            this.lbToplu.ItemHeight = 16;
            this.lbToplu.Location = new System.Drawing.Point(7, 22);
            this.lbToplu.Name = "lbToplu";
            this.lbToplu.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbToplu.Size = new System.Drawing.Size(159, 276);
            this.lbToplu.TabIndex = 0;
            // 
            // rbToplu
            // 
            this.rbToplu.Location = new System.Drawing.Point(35, 77);
            this.rbToplu.Name = "rbToplu";
            this.rbToplu.Size = new System.Drawing.Size(249, 237);
            this.rbToplu.TabIndex = 1;
            this.rbToplu.Text = "";
            // 
            // btnTopluGonder
            // 
            this.btnTopluGonder.Location = new System.Drawing.Point(95, 332);
            this.btnTopluGonder.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnTopluGonder.Name = "btnTopluGonder";
            this.btnTopluGonder.Size = new System.Drawing.Size(122, 45);
            this.btnTopluGonder.TabIndex = 2;
            this.btnTopluGonder.Text = "Gönder";
            this.btnTopluGonder.Click += new System.EventHandler(this.btnTopluGonder_Click);
            // 
            // TopluSohbetFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 450);
            this.Controls.Add(this.btnTopluGonder);
            this.Controls.Add(this.rbToplu);
            this.Controls.Add(this.groupBox1);
            this.Name = "TopluSohbetFrm";
            this.Text = "TopluSohbetFrm";
            this.Load += new System.EventHandler(this.TopluSohbetFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbToplu;
        private System.Windows.Forms.RichTextBox rbToplu;
        private DevExpress.XtraEditors.SimpleButton btnTopluGonder;
    }
}