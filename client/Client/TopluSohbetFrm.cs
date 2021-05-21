using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class TopluSohbetFrm : Form
    {
        private ListBox.ObjectCollection liste;
        private Single sn;
        private Socket _clientSocket;
        public TopluSohbetFrm(ListBox.ObjectCollection lb,Socket socket)
        {
            InitializeComponent();
            this.liste = lb;
            this._clientSocket = socket;
            sn = Single.GetSingle();
        }

        private void TopluSohbetFrm_Load(object sender, EventArgs e)
        {

            lbToplu.Items.AddRange(liste);
        }

        private void btnTopluGonder_Click(object sender, EventArgs e)
        {
            if (_clientSocket.Connected)
            {

                string tmpStr = "";

               
                foreach (var item in lbToplu.SelectedItems)
                {
                        tmpStr = lbToplu.GetItemText(item);

                        byte[] buffer = Encoding.ASCII.GetBytes(tmpStr + " :" + rbToplu.Text + "*" + sn.KullaniciAdi);//byte çevir

                        _clientSocket.Send(buffer);
                        Thread.Sleep(20);
                  

                }



            }

           if(MessageBox.Show(this,"Mesaj Gönderildi!","",MessageBoxButtons.OK) == DialogResult.OK)
            {

                rbToplu.Clear();
                lbToplu.SelectedItems.Clear();
            }
        }
    }
}
