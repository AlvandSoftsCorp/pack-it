using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PackIt
{
    public partial class FrmPsw : Form
    {
        public FrmPsw()
        {
            InitializeComponent();
        }

        public string Psw
        {
            get
            {
                return txtPassword.Text;
            }
        }

        bool is_canceled = true;
        public bool IsCanceled
        {
            get
            {
                return this.is_canceled;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.is_canceled = false;
            this.Close();
            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.is_canceled=true;
            txtPassword.Clear();
            return;
        }
    }
}
