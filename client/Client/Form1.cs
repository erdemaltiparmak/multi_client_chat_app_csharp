using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        internal Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Single sn;
        private SqlConnection conn;
 
        public Form1()
        {
            InitializeComponent();
            
            this.conn = Database.Connection();
            conn.Open();
            sn = Single.GetSingle();

        }
        
        byte[] receivedBuf = new byte[1024];
        private void ReceiveData(IAsyncResult ar)
        {
            
            int listede_yok = 0;
            try
            {

                Socket socket = (Socket)ar.AsyncState;
                int received = socket.EndReceive(ar);
                byte[] dataBuf = new byte[received];
                Array.Copy(receivedBuf, dataBuf, received);
                string gelen = Encoding.ASCII.GetString(dataBuf).ToString();
                if (gelen.Contains("sil*"))
                {
                    string parcala = gelen.Substring(4, (gelen.Length - 4));
                    Console.WriteLine("degerim  " + parcala);
                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        if (listBox1.Items[j].Equals(parcala))
                        {
                            listBox1.Items.RemoveAt(j);

                        }
                    }
                }
                else if (gelen.Contains("@"))
                {

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i].ToString().Equals(gelen))
                        {
                            listede_yok = 1;
                        }
                    }
                    if (listede_yok == 0)
                    {
                        string ben = "@" + sn.KullaniciAdi;
                        if (ben.Equals(gelen))
                        {

                        }
                        else
                        {
                            listBox1.Items.Add(gelen);
                        }
                    }

                }
                else
                {
                    FillPersonTab(gelen);
                }
                _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
       
        private Task FillTabs()
        {
            return Task.Run(() => {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand query = new SqlCommand("sp_sohbetler", conn);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.Add("@Gonderen", SqlDbType.VarChar).Value = sn.KullaniciAdi;
                da.SelectCommand = query;
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataTable tables in ds.Tables)
                {
                    foreach (DataRow row in tables.Rows)
                    {
                        
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate ()
                              {
                                  TabPage tp = new TabPage(row[0].ToString());
                                  RichTextBox rb = new RichTextBox();
                                  rb.Dock = DockStyle.Fill;
                                  rb.ReadOnly = true;
                                  MesajlarıGetir(row[0].ToString(), rb);
                                  tp.Controls.Add(rb);
                                  tabControl1.TabPages.Add(tp);
                              });
                        }

                    }
                }
                });
        }
     

        private Task FillPersonTab(string gelen)
        {
            return Task.Run(() =>
            {
                var x = gelen.Split(':');
                var y = x[0].Replace("@", "");

                var tabs = tabControl1.TabPages;
                List<string> tabNames = new List<string>();
                foreach (TabPage tab in tabs)
                {
                    tabNames.Add(tab.Text);
                }

                if (!tabNames.Contains(y))
                {

                    if (this.InvokeRequired)
                    {
                        TabPage tp = new TabPage(y);

                        this.Invoke((MethodInvoker)delegate ()
                        {
                            RichTextBox rb = new RichTextBox();
                            rb.Dock = DockStyle.Fill;
                            rb.ReadOnly = true;
                            rb.AppendText(gelen);
                            tp.Controls.Add(rb);

                            tabControl1.TabPages.Add(tp);
                        });
                    }

                }
                else
                {
                    var indexTab = 0;
                    for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    {
                        if (tabControl1.TabPages[i].Text == y)
                        {
                            indexTab = i;
                            break;
                        }

                    }
                    var theTab = tabControl1.TabPages[indexTab];
                    var u = (RichTextBox)theTab.Controls[0];
                    u.AppendText("\n" + gelen);
                }


            });

        }

        private void SendLoop()
        {
            while (true)
            {
                byte[] receivedBuf = new byte[1024];
                int rev = _clientSocket.Receive(receivedBuf);
                if (rev != 0)
                {
                    byte[] data = new byte[rev];
                    Array.Copy(receivedBuf, data, rev);
                    rb_chat.AppendText("\nServer: " + Encoding.ASCII.GetString(data) + "\n");
                }
                else _clientSocket.Close();
            }
        }

        public void LoopConnect()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect("127.0.0.1", 100);
                }
                catch (SocketException)
                {
                    Console.WriteLine("bağlantılar: " + attempts.ToString());
                }
            }
            // SendLoop();
            _clientSocket.BeginReceive(receivedBuf, 0, receivedBuf.Length, SocketFlags.None, new AsyncCallback(ReceiveData), _clientSocket);
            byte[] buffer = Encoding.ASCII.GetBytes("@@" + sn.KullaniciAdi);
            _clientSocket.Send(buffer);
            label3.Text = "servere bağlandı!";
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
           
        }
      
        internal void btnSend_Click(object sender, EventArgs e)
        {

            if (_clientSocket.Connected)
            {
                if(tabControl1.SelectedTab.Text!="")
                {
                    string tmpStr2 = "";
                    tmpStr2 = "@" + tabControl1.SelectedTab.Text;

                    byte[] buffer2 = Encoding.ASCII.GetBytes(tmpStr2 + " :" + txt_text.Text + "*" + sn.KullaniciAdi);
                    _clientSocket.Send(buffer2);
                    Thread.Sleep(20);

                    var x = (RichTextBox)tabControl1.SelectedTab.Controls[0];
                    bool b = String.IsNullOrEmpty(x.Text);
                    if(b)
                    {
                        x.AppendText(sn.KullaniciAdi + ": " + txt_text.Text);

                    }
                    else
                    {
                        x.AppendText("\n"+sn.KullaniciAdi + ": " + txt_text.Text);

                    }

                }
                else
                {
                    MessageBox.Show("Bir sohbet seçin");
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            FillTabs();
            textBox1.Text = sn.KullaniciAdi;
        }

        #region MyRegion
        private void rb_chat_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSohbetler_ValueMemberChanged(object sender, EventArgs e)
        {

        }



        private void cmbSohbetler_Click(object sender, EventArgs e)
        {


        } 
        #endregion

        private void btnTopluSohbet_Click(object sender, EventArgs e)
        {
            var topluEkran = new TopluSohbetFrm(listBox1.Items,_clientSocket);
            topluEkran.Owner = this;
            topluEkran.Show();
        }

        private void btnYeniSohbet_Click(object sender, EventArgs e)
        {
            var tabs = tabControl1.TabPages;
            List<string> tabNames = new List<string>();
            foreach (TabPage tab in tabs)
            {
                tabNames.Add(tab.Text);
            }

            if (!tabNames.Contains(listBox1.SelectedItem.ToString().Replace("@","")))
            {
        
                TabPage tp = new TabPage(listBox1.SelectedItem.ToString().Replace("@", ""));
                tp.Controls.Add(new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly=true

                });
                tabControl1.TabPages.Add(tp);
            }
            else if(tabNames.Contains(listBox1.SelectedItem.ToString()))
            {
                MessageBox.Show($"Zaten \"{listBox1.SelectedItem.ToString()}\" adlı kiliyle bir sohbet başlattınız.");

            }
            else
            {
                MessageBox.Show("Listeden bir kişi seçiniz");
            }
        }

        private void  MesajlarıGetir(string mesajlasilacakKisi,RichTextBox rb)
        {
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                using (SqlCommand query = new SqlCommand("sp_mesajlarigetir", conn))
                {
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.Add("@bakilan", SqlDbType.VarChar).Value = mesajlasilacakKisi;
                    query.Parameters.Add("@aktifkullanici", SqlDbType.VarChar).Value = sn.KullaniciAdi;
                    da.SelectCommand = query;
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    foreach (DataTable tables in ds.Tables)
                    {
                        foreach (DataRow row in tables.Rows)
                        {
                                rb.AppendText(row[0].ToString()+": "+row[2].ToString()+"\n");

                        }
                        
                    }
                    if(ds!=null)
                    {
                        rb.AppendText("^^^ Önceki Mesajlar ^^^");
                    }
                }


            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
