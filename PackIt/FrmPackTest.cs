using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PackIt
{
    public partial class FrmPackTest : Form
    {
        public FrmPackTest()
        {
            InitializeComponent();
        }

        private void btnPack_Click(object sender, EventArgs e)
        {
            txtInfo.Clear();
            txtInfo.WordWrap = true;
            txtInfo.ScrollBars = ScrollBars.Vertical;
            string source_dir_path = txtSource.Text.Trim();
            string output_file = txtDestination.Text.Trim();

            if (source_dir_path == "") return;
            if (output_file == "") return;

            FrmSetPassword f = new FrmSetPassword();
            f.ShowDialog();
            if (f.IsCanceled) return;
            

            Pack p = new Pack();
            p.OnInfo += new Pack.PackInfo(Pack_OnInfo);

            bool success = p.DoPack(source_dir_path, output_file, 0, f.Psw);
            if (success == false)
            {
                MessageBox.Show("Pack Failed");
            }
            else
            {
                MessageBox.Show("Pack Done");
            }
        }

        void Pack_OnInfo(string Info)
        {
            txtInfo.AppendText(Info);
        }

        private void btnUnpack_Click(object sender, EventArgs e)
        {
            txtInfo.Clear();
            string source_file = txtSource.Text.Trim();
            string output_path = txtDestination.Text.Trim();

            if (source_file == "") return;
            if (output_path == "") return;

            FrmPsw f = new FrmPsw();
            f.ShowDialog();
            if (f.IsCanceled) return;


            Pack p = new Pack();
            p.OnInfo += new Pack.PackInfo(Pack_OnInfo); 
            
            bool success = p.DoUnpack(output_path, source_file, f.Psw);
            if (success == false)
            {
                MessageBox.Show("Unpack Failed");
            }
            else
            {
                MessageBox.Show("Unpack Done");
            }
        }

        private void Diff(string DirA, string DirB)
        {
            Pack p = new Pack();
            string[] a_files = p.GetAllFiles(DirA);
            string[] b_files = p.GetAllFiles(DirB);

            if (a_files == null || b_files == null)
            {
                MessageBox.Show("Invalid Input");
                return;
            }

            if (a_files.Length != b_files.Length)
            {
                MessageBox.Show("Number of files mismatch");
                return;
            }

            for (int i = 0; i < a_files.Length; i++)
            {
                FileInfo fi_a = new FileInfo(a_files[i]);
                FileInfo fi_b = new FileInfo(b_files[i]);

                if (fi_a.Length != fi_b.Length)
                {
                    MessageBox.Show("File length mismatch\n" + fi_b.FullName);
                    return;
                }
            }
            
            
            for (int i = 0; i < a_files.Length; i++)
            {
                if (IdenticalFiles(a_files[i], b_files[i])==false)
                {
                    MessageBox.Show("File content mismatch");
                    return;
                }
            }

            MessageBox.Show("Folders are identical");
        }


        private bool IdenticalFiles(string A, string B)
        {
            FileInfo fi_a = new FileInfo(A);
            FileInfo fi_b = new FileInfo(B);

            if (fi_a.Exists == false) return false;
            if (fi_b.Exists == false) return false;

            if (fi_a.Length != fi_b.Length) return false;

            byte[] a_bytes = File.ReadAllBytes(A);
            byte[] b_bytes = File.ReadAllBytes(B);

            for (int i = 0; i < a_bytes.Length;i++ )
            {
                if (a_bytes[i] != b_bytes[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void txtSource_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void txtSource_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] f = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (f == null) return;
                if (f.Length != 1) return;
                txtSource.Text = f[0];

                // file or folder name
                string fn = f[0];
                if (File.Exists(fn))
                {
                    FileInfo fi = new FileInfo(fn);
                    if (fi.Extension.ToLower() == ".cpk")
                    {
                        string fo = fi.FullName;
                        fo = fo.Replace(fi.Extension, "");
                        fo += "\\";
                        txtDestination.Text = fo;
                        btnPack.Enabled = false;
                        btnUnpack.Enabled = true;
                    }
                    else
                    {
                        string fo = fi.FullName;
                        fo = fo.Replace(fi.Extension, ".cpk");
                        txtDestination.Text = fo;
                        btnPack.Enabled = true;
                        btnUnpack.Enabled = false;
                    }

                }
                else if (Directory.Exists(fn))
                {
                    DirectoryInfo di = new DirectoryInfo(fn);
                    string fo = di.FullName;
                    fo += ".cpk";
                    txtDestination.Text = fo;

                    btnPack.Enabled = true;
                    btnUnpack.Enabled = false;
                }

            }
        }


        private void txtDestination_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void txtDestination_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] f = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (f == null) return;
                if (f.Length != 1) return;
                txtDestination.Text = f[0];

                //// file or folder name
                //string fn = f[0];
                //if (File.Exists(fn))
                //{
                //    FileInfo fi = new FileInfo(fn);
                //    if (fi.Extension.ToLower() == ".cpk")
                //    {
                //        string fo = fi.FullName;
                //        fo = fo.Replace(fi.Extension, "");
                //        fo += "\\";
                //        txtDestination.Text = fo;
                //        btnPack.Enabled = false;
                //        btnUnpack.Enabled = true;
                //    }
                //    else
                //    {
                //        string fo = fi.FullName;
                //        fo = fo.Replace(fi.Extension, ".cpk");
                //        txtDestination.Text = fo;
                //        btnPack.Enabled = true;
                //        btnUnpack.Enabled = false;
                //    }

                //}
                //else if (Directory.Exists(fn))
                //{
                //    DirectoryInfo di = new DirectoryInfo(fn);
                //    string fo = di.FullName;
                //    fo += ".cpk";
                //    txtDestination.Text = fo;

                //    btnPack.Enabled = true;
                //    btnUnpack.Enabled = false;
                //}

            }
        }

        private void btnShredSource_Click(object sender, EventArgs e)
        {
            Shred(txtSource.Text);
        }

        private void btnRemoveSource_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Delete Folder:\n" + txtSource.Text, "Warning", MessageBoxButtons.YesNoCancel);
                if (dr != System.Windows.Forms.DialogResult.Yes) return;

                Directory.Delete(txtSource.Text,true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiff_Click(object sender, EventArgs e)
        {
            Diff(txtSource.Text, txtDestination.Text);
        }

        private void Shred(string Source)
        {
            try
            {

                if (File.Exists(Source))
                {
                    DialogResult dr = MessageBox.Show("Delete File: \n" + txtSource.Text, "Warning", MessageBoxButtons.YesNoCancel);
                    if (dr != System.Windows.Forms.DialogResult.Yes) return;
                    
                    Pack p = new Pack();
                    txtInfo.Clear();
                    p.OnInfo += Pack_OnInfo;
                    bool is_ok = p.ShredFile(Source);
                    if (is_ok == false)
                    {
                        MessageBox.Show("Shred Failed!");
                    }
                    txtInfo.AppendText("Shred Done.");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Delete Folder:\n" + txtSource.Text, "Warning", MessageBoxButtons.YesNoCancel);
                    if (dr != System.Windows.Forms.DialogResult.Yes) return;

                    Pack p = new Pack();
                    txtInfo.Clear();
                    p.OnInfo += Pack_OnInfo;
                    bool is_ok = p.ShredFiles(Source);
                    if (is_ok == false)
                    {
                        MessageBox.Show("Shred Failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Process.Start(path);
            }
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowNewFolderButton = true;
                DialogResult dr = fbd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    txtSource.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Process.Start(path);
            }
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowNewFolderButton = true;
                DialogResult dr = fbd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    txtDestination.Text = fbd.SelectedPath;
                }
            }

        }
    }
}
