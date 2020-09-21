namespace PackIt
{
    partial class FrmMyCipher
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
            this.pbOrg = new System.Windows.Forms.PictureBox();
            this.btnCipher = new System.Windows.Forms.Button();
            this.btnDecipher = new System.Windows.Forms.Button();
            this.pbCiphered = new System.Windows.Forms.PictureBox();
            this.pbDeciphered = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtRand = new System.Windows.Forms.TextBox();
            this.btnSetKey = new System.Windows.Forms.Button();
            this.btnSetRand = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCiphered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeciphered)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOrg
            // 
            this.pbOrg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbOrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOrg.Location = new System.Drawing.Point(12, 124);
            this.pbOrg.Name = "pbOrg";
            this.pbOrg.Size = new System.Drawing.Size(276, 291);
            this.pbOrg.TabIndex = 0;
            this.pbOrg.TabStop = false;
            // 
            // btnCipher
            // 
            this.btnCipher.Location = new System.Drawing.Point(294, 86);
            this.btnCipher.Name = "btnCipher";
            this.btnCipher.Size = new System.Drawing.Size(143, 35);
            this.btnCipher.TabIndex = 5;
            this.btnCipher.Text = "Cipher";
            this.btnCipher.UseVisualStyleBackColor = true;
            this.btnCipher.Click += new System.EventHandler(this.btnCipher_Click);
            // 
            // btnDecipher
            // 
            this.btnDecipher.Location = new System.Drawing.Point(576, 86);
            this.btnDecipher.Name = "btnDecipher";
            this.btnDecipher.Size = new System.Drawing.Size(143, 35);
            this.btnDecipher.TabIndex = 6;
            this.btnDecipher.Text = "Decipher";
            this.btnDecipher.UseVisualStyleBackColor = true;
            this.btnDecipher.Click += new System.EventHandler(this.btnDecipher_Click);
            // 
            // pbCiphered
            // 
            this.pbCiphered.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCiphered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCiphered.Location = new System.Drawing.Point(294, 124);
            this.pbCiphered.Name = "pbCiphered";
            this.pbCiphered.Size = new System.Drawing.Size(276, 291);
            this.pbCiphered.TabIndex = 4;
            this.pbCiphered.TabStop = false;
            // 
            // pbDeciphered
            // 
            this.pbDeciphered.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDeciphered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDeciphered.Location = new System.Drawing.Point(576, 124);
            this.pbDeciphered.Name = "pbDeciphered";
            this.pbDeciphered.Size = new System.Drawing.Size(276, 291);
            this.pbDeciphered.TabIndex = 5;
            this.pbDeciphered.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 84);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(143, 35);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKey.Location = new System.Drawing.Point(12, 12);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(276, 29);
            this.txtKey.TabIndex = 0;
            // 
            // txtRand
            // 
            this.txtRand.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRand.Location = new System.Drawing.Point(12, 45);
            this.txtRand.Name = "txtRand";
            this.txtRand.ReadOnly = true;
            this.txtRand.Size = new System.Drawing.Size(276, 29);
            this.txtRand.TabIndex = 2;
            // 
            // btnSetKey
            // 
            this.btnSetKey.Location = new System.Drawing.Point(289, 11);
            this.btnSetKey.Name = "btnSetKey";
            this.btnSetKey.Size = new System.Drawing.Size(70, 31);
            this.btnSetKey.TabIndex = 1;
            this.btnSetKey.Text = "Set Key";
            this.btnSetKey.UseVisualStyleBackColor = true;
            this.btnSetKey.Click += new System.EventHandler(this.btnSetKey_Click);
            // 
            // btnSetRand
            // 
            this.btnSetRand.Location = new System.Drawing.Point(289, 44);
            this.btnSetRand.Name = "btnSetRand";
            this.btnSetRand.Size = new System.Drawing.Size(70, 31);
            this.btnSetRand.TabIndex = 3;
            this.btnSetRand.Text = "Set Rand";
            this.btnSetRand.UseVisualStyleBackColor = true;
            this.btnSetRand.Click += new System.EventHandler(this.btnSetRand_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(383, 13);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(28, 13);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Info:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(383, 44);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(53, 13);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "Message:";
            // 
            // FrmMyCipher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 422);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnSetRand);
            this.Controls.Add(this.btnSetKey);
            this.Controls.Add(this.txtRand);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pbDeciphered);
            this.Controls.Add(this.pbCiphered);
            this.Controls.Add(this.btnDecipher);
            this.Controls.Add(this.btnCipher);
            this.Controls.Add(this.pbOrg);
            this.Name = "FrmMyCipher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stream Cipher Test Sweet";
            ((System.ComponentModel.ISupportInitialize)(this.pbOrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCiphered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeciphered)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbOrg;
        private System.Windows.Forms.Button btnCipher;
        private System.Windows.Forms.Button btnDecipher;
        private System.Windows.Forms.PictureBox pbCiphered;
        private System.Windows.Forms.PictureBox pbDeciphered;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtRand;
        private System.Windows.Forms.Button btnSetKey;
        private System.Windows.Forms.Button btnSetRand;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblMessage;
    }
}

