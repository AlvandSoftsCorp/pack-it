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
    public partial class FrmSetPassword : Form
    {
        public FrmSetPassword()
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
            
            if (txtPassword.Text != txtConfirm.Text)
            {
                if (chbxShowPassword.Checked == false)
                {
                    MessageBox.Show("Password Mismatch!");
                    return;
                }
            }

            this.is_canceled = false;
            this.Close();
        }

        private void chbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxShowPassword.Checked)
            {
                txtConfirm.Hide();
                lblConfirm.Hide();
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtConfirm.Show();
                lblConfirm.Show();
                txtPassword.PasswordChar = '-';
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.is_canceled = true;
            this.Close();
        }
    }
}
