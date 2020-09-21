namespace PackIt
{
    partial class FrmPackTest
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
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnSource = new System.Windows.Forms.Button();
            this.lblDestination = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.btnDestination = new System.Windows.Forms.Button();
            this.btnPack = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUnpack = new System.Windows.Forms.Button();
            this.btnRemoveSource = new System.Windows.Forms.Button();
            this.btnShredSource = new System.Windows.Forms.Button();
            this.btnDiff = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfo.BackColor = System.Drawing.SystemColors.Info;
            this.txtInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.ForeColor = System.Drawing.Color.Maroon;
            this.txtInfo.Location = new System.Drawing.Point(127, 66);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(429, 178);
            this.txtInfo.TabIndex = 17;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSource.Location = new System.Drawing.Point(147, 18);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(51, 13);
            this.lblSource.TabIndex = 0;
            this.lblSource.Text = "Source:";
            // 
            // txtSource
            // 
            this.txtSource.AllowDrop = true;
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtSource.Location = new System.Drawing.Point(201, 14);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(309, 20);
            this.txtSource.TabIndex = 1;
            this.txtSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSource_DragDrop);
            this.txtSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtSource_DragEnter);
            // 
            // btnSource
            // 
            this.btnSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSource.Location = new System.Drawing.Point(516, 10);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(40, 27);
            this.btnSource.TabIndex = 2;
            this.btnSource.Text = "...";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.Location = new System.Drawing.Point(123, 44);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(75, 13);
            this.lblDestination.TabIndex = 3;
            this.lblDestination.Text = "Destination:";
            // 
            // txtDestination
            // 
            this.txtDestination.AllowDrop = true;
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtDestination.Location = new System.Drawing.Point(201, 40);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(309, 20);
            this.txtDestination.TabIndex = 4;
            this.txtDestination.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDestination_DragDrop);
            this.txtDestination.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtDestination_DragEnter);
            // 
            // btnDestination
            // 
            this.btnDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDestination.Location = new System.Drawing.Point(516, 36);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(40, 27);
            this.btnDestination.TabIndex = 5;
            this.btnDestination.Text = "...";
            this.btnDestination.UseVisualStyleBackColor = true;
            this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
            // 
            // btnPack
            // 
            this.btnPack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnPack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnPack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPack.Location = new System.Drawing.Point(3, 3);
            this.btnPack.Name = "btnPack";
            this.btnPack.Size = new System.Drawing.Size(111, 40);
            this.btnPack.TabIndex = 14;
            this.btnPack.Text = "Pack";
            this.btnPack.UseVisualStyleBackColor = false;
            this.btnPack.Click += new System.EventHandler(this.btnPack_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.btnPack);
            this.flowLayoutPanel1.Controls.Add(this.btnUnpack);
            this.flowLayoutPanel1.Controls.Add(this.btnRemoveSource);
            this.flowLayoutPanel1.Controls.Add(this.btnShredSource);
            this.flowLayoutPanel1.Controls.Add(this.btnDiff);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(118, 256);
            this.flowLayoutPanel1.TabIndex = 18;
            // 
            // btnUnpack
            // 
            this.btnUnpack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnUnpack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnUnpack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnUnpack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnUnpack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnpack.Location = new System.Drawing.Point(3, 49);
            this.btnUnpack.Name = "btnUnpack";
            this.btnUnpack.Size = new System.Drawing.Size(111, 40);
            this.btnUnpack.TabIndex = 15;
            this.btnUnpack.Text = "Unpack";
            this.btnUnpack.UseVisualStyleBackColor = false;
            this.btnUnpack.Click += new System.EventHandler(this.btnUnpack_Click);
            // 
            // btnRemoveSource
            // 
            this.btnRemoveSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRemoveSource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnRemoveSource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRemoveSource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnRemoveSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSource.Location = new System.Drawing.Point(3, 95);
            this.btnRemoveSource.Name = "btnRemoveSource";
            this.btnRemoveSource.Size = new System.Drawing.Size(111, 40);
            this.btnRemoveSource.TabIndex = 16;
            this.btnRemoveSource.Text = "Remove Source";
            this.btnRemoveSource.UseVisualStyleBackColor = false;
            this.btnRemoveSource.Click += new System.EventHandler(this.btnRemoveSource_Click);
            // 
            // btnShredSource
            // 
            this.btnShredSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnShredSource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnShredSource.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnShredSource.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnShredSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShredSource.Location = new System.Drawing.Point(3, 141);
            this.btnShredSource.Name = "btnShredSource";
            this.btnShredSource.Size = new System.Drawing.Size(111, 40);
            this.btnShredSource.TabIndex = 17;
            this.btnShredSource.Text = "Shred Source";
            this.btnShredSource.UseVisualStyleBackColor = false;
            this.btnShredSource.Click += new System.EventHandler(this.btnShredSource_Click);
            // 
            // btnDiff
            // 
            this.btnDiff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDiff.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnDiff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDiff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDiff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiff.Location = new System.Drawing.Point(3, 187);
            this.btnDiff.Name = "btnDiff";
            this.btnDiff.Size = new System.Drawing.Size(111, 40);
            this.btnDiff.TabIndex = 18;
            this.btnDiff.Text = "Diff";
            this.btnDiff.UseVisualStyleBackColor = false;
            this.btnDiff.Click += new System.EventHandler(this.btnDiff_Click);
            // 
            // FrmPackTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 256);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnDestination);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.txtInfo);
            this.Name = "FrmPackTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pack It";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.Button btnPack;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnUnpack;
        private System.Windows.Forms.Button btnRemoveSource;
        private System.Windows.Forms.Button btnShredSource;
        private System.Windows.Forms.Button btnDiff;
    }
}