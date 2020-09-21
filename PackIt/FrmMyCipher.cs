using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PackIt
{
    public partial class FrmMyCipher : Form
    {
        public FrmMyCipher()
        {
            InitializeComponent();
        }
        byte[] ciphered_bytes = null;

        string Key = "";
        string Rand = "";

        private void btnCipher_Click(object sender, EventArgs e)
        {
            MyCypher r = new MyCypher(this.Key, this.Rand);
            Color clr = Color.White;
            List<byte> file_bytes = new List<byte>();
            Bitmap bmp = (Bitmap)pbOrg.BackgroundImage;

            if (bmp == null)
            {
                MessageBox.Show("Input file is not loaded.");
                return;
            }

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    clr = bmp.GetPixel(x, y);
                    file_bytes.AddRange(new byte[] { clr.R, clr.G, clr.B });
                }
            }
            byte[] bytes = file_bytes.ToArray();
            
            DateTime dt1 = DateTime.Now;
            r.Cipher(ref bytes);
            DateTime dt2 = DateTime.Now;
            
            double totoal_time = dt2.Subtract(dt1).TotalMilliseconds;
            double performance_mbps = 0d;
            performance_mbps = bytes.Length * 8d;        // total bits
            performance_mbps /= 1024d;                   // total kilo bits
            performance_mbps /= 1024d;                   // total mega bits
            performance_mbps /= (totoal_time / 1000d);    // total mega bits per second
            lblMessage.Text = string.Format("Message: {0}\nTotal Time(ms): {1:N0}\nPerformance:{2:N3}(Mega bit/Sec)", "Cipher done. Loading results.", totoal_time, performance_mbps);
            Application.DoEvents();

            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            int k = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    clr = Color.FromArgb(bytes[k], bytes[k + 1], bytes[k + 2]);
                    bmp2.SetPixel(x, y, clr);
                    k += 3;
                }
            }
            pbCiphered.BackgroundImage = (Image)bmp2;
            this.ciphered_bytes = bytes;

            MessageBox.Show(r.GetStat());

            //MyCypher mcc = new MyCypher("123", "456");
            //mcc.Cipher(ref this.cip_bytes);
        }

        private void btnDecipher_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;

            MyCypher r = new MyCypher(this.Key, this.Rand);
            Color clr = Color.White;
            List<byte> file_bytes = new List<byte>();
            Bitmap bmp = (Bitmap)pbOrg.BackgroundImage;

            byte[] bytes = this.ciphered_bytes;
            r.Decipher(ref bytes);

            DateTime dt2 = DateTime.Now;
            double totoal_time = dt2.Subtract(dt1).TotalMilliseconds;
            double performance_mbps = 0d;
            performance_mbps = bytes.Length * 8d;        // total bits
            performance_mbps /= 1024d;                   // total kilo bits
            performance_mbps /= 1024d;                   // total mega bits
            performance_mbps /= (totoal_time / 1000d);    // total mega bits per second
            lblMessage.Text = string.Format("Message: {0}\nTotal Time(ms): {1:N0}\nPerformance:{2:N3}(Mega bit/Sec)", "Decipher done. Loading results.", totoal_time, performance_mbps);
            Application.DoEvents();

            Bitmap bmp3 = new Bitmap(bmp.Width, bmp.Height);
            int k = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    clr = Color.FromArgb(bytes[k], bytes[k + 1], bytes[k + 2]);
                    bmp3.SetPixel(x, y, clr);
                    k += 3;
                }
            }
            pbDeciphered.BackgroundImage = (Image)bmp3;
            MessageBox.Show(r.GetStat());

            //MyCypher mcc = new MyCypher("123", "456");
            //mcc.Decipher(ref this.cip_bytes);
            //File.WriteAllBytes(Application.StartupPath + "\\B.jpg", cip_bytes);
        }


        byte[] cip_bytes = null;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            string A = Application.StartupPath + "\\A.jpg";
            Bitmap bmp = (Bitmap)Bitmap.FromFile(A);
            pbOrg.BackgroundImage = (Image)bmp;
            Application.DoEvents();

            long bytes_cnt = bmp.Height * bmp.Width * 3;
            string info = string.Format("Image size W:{0}  H: {1}\nTotal Bytes: {2:N0}", bmp.Width, bmp.Height, bytes_cnt);
            lblInfo.Text = info;
            Application.DoEvents();


            cip_bytes = File.ReadAllBytes(Application.StartupPath + "\\A.jpg");
        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {
            this.Key = txtKey.Text.Trim();
        }

        private void btnSetRand_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            txtRand.Text = rnd.Next().ToString() + rnd.Next().ToString();
            this.Rand = txtRand.Text;
        }
    }
}
