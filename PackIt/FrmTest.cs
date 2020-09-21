using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PackIt
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            // FileInfo fi = new FileInfo("");

            Pack p = new Pack();

            for (int i = 0; i < 100; i++)
            {
                textBox1.AppendText(p.GetRandomFileName()+"\r\n");
            }
        }
    }
}
