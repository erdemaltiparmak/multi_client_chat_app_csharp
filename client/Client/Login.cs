using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {

            var db = Database.Connection();
            db.Open();

            SqlCommand query = new SqlCommand($"select count(*) from Kullanici where KullaniciAdi='{textBox1.Text}' and Sifre='{textBox2.Text}'",db);
            
            var x = (int) query.ExecuteScalar();

            if(x>0)
            {
                Single sn = Single.GetSingle();
                sn.KullaniciAdi = textBox1.Text;
                var screen = new Form1();
                screen.Owner = this;

                Thread th = new Thread(screen.LoopConnect);
                th.Start();
                th.Join();


                screen.Show();
            }
            else
            {
                MessageBox.Show(this, "Girdiğiniz kullanıcı adı veya şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnKayit_Click(object sender, EventArgs e)
        {

            var db = Database.Connection();
            db.Open();

            SqlCommand query = new SqlCommand($"insert into Kullanici values ('{textBox1.Text}', '{textBox2.Text}')", db);

            var rowCount = query.ExecuteNonQuery();

            if (rowCount > 0)
                MessageBox.Show($"{textBox1.Text} , kaydınız başarıyla tamamlandı. Giriş Yapabilirsiniz.");
            else
                MessageBox.Show("hata");

            textBox1.Clear();
            textBox2.Clear();

        }
    }
}
