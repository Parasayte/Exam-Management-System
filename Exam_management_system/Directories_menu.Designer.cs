﻿namespace Exam_management_system
{
    partial class Directories_menu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Selectall = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Selectall);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(8, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 94);
            this.panel1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.AutoSize = true;
            this.textBox1.BackColor = System.Drawing.Color.DarkGray;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.textBox1.Location = new System.Drawing.Point(215, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(2, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Scrap_Folder_Closed_Red_16;
            this.label4.Location = new System.Drawing.Point(177, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 20);
            this.label4.TabIndex = 5;
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Selectall
            // 
            this.Selectall.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Files_48;
            this.Selectall.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Selectall.Location = new System.Drawing.Point(90, 9);
            this.Selectall.Name = "Selectall";
            this.Selectall.Size = new System.Drawing.Size(81, 71);
            this.Selectall.TabIndex = 2;
            this.Selectall.Text = "Select All";
            this.Selectall.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Selectall.Click += new System.EventHandler(this.Selectall_folders);
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Close_48;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 71);
            this.label2.TabIndex = 1;
            this.label2.Text = "Delete";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label2.Click += new System.EventHandler(this.Delete_selected_folders);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menuStrip1.ForeColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(824, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Applications_Cascade_16;
            this.fileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Cancel_16;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Image = global::Exam_management_system.Properties.Resources.P;
            this.label1.Location = new System.Drawing.Point(743, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 81);
            this.label1.TabIndex = 3;
            this.label1.Click += new System.EventHandler(this.Add_new_directory);
            // 
            // Directories_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(824, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Silver;
            this.Name = "Directories_menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folders Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.directories_FormClosing);
            this.Load += new System.EventHandler(this.directories_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Selectall;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label textBox1;
    }
}