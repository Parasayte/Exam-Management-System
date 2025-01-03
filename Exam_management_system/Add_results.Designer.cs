using System.Windows.Forms;

namespace Exam_management_system
{
    partial class Add_results
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.operatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStudentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAnnouncementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notesAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeControlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.searchtextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(140)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(140)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.dataGridView1.Location = new System.Drawing.Point(10, 56);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(140)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(900, 474);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.menuStrip1.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operatToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(916, 24);
            this.menuStrip1.TabIndex = 39;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // operatToolStripMenuItem
            // 
            this.operatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addExamToolStripMenuItem,
            this.readNotesToolStripMenuItem,
            this.addStudentToolStripMenuItem,
            this.addAnnouncementsToolStripMenuItem,
            this.chatToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.operatToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Applications_Cascade_16;
            this.operatToolStripMenuItem.Name = "operatToolStripMenuItem";
            this.operatToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.operatToolStripMenuItem.Text = "Operat";
            // 
            // addExamToolStripMenuItem
            // 
            this.addExamToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Soft_Scraps_File_New_16;
            this.addExamToolStripMenuItem.Name = "addExamToolStripMenuItem";
            this.addExamToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.addExamToolStripMenuItem.Text = "Add Exam";
            this.addExamToolStripMenuItem.Click += new System.EventHandler(this.addExamToolStripMenuItem_Click);
            // 
            // readNotesToolStripMenuItem
            // 
            this.readNotesToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Document_Write_16;
            this.readNotesToolStripMenuItem.Name = "readNotesToolStripMenuItem";
            this.readNotesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.readNotesToolStripMenuItem.Text = "Read Notes";
            // 
            // addStudentToolStripMenuItem
            // 
            this.addStudentToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Administrator_16;
            this.addStudentToolStripMenuItem.Name = "addStudentToolStripMenuItem";
            this.addStudentToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.addStudentToolStripMenuItem.Text = "Add Student";
            this.addStudentToolStripMenuItem.Click += new System.EventHandler(this.addStudentToolStripMenuItem_Click);
            // 
            // addAnnouncementsToolStripMenuItem
            // 
            this.addAnnouncementsToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Custom_Icon_Design_Pretty_Office_8_Comment_add_16;
            this.addAnnouncementsToolStripMenuItem.Name = "addAnnouncementsToolStripMenuItem";
            this.addAnnouncementsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.addAnnouncementsToolStripMenuItem.Text = "Add announcements";
            this.addAnnouncementsToolStripMenuItem.Click += new System.EventHandler(this.addAnnouncementsToolStripMenuItem_Click);
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Text_Bubble1;
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.chatToolStripMenuItem.Text = "Chat";
            this.chatToolStripMenuItem.Click += new System.EventHandler(this.chatToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notesAppToolStripMenuItem,
            this.timeControlerToolStripMenuItem});
            this.toolsToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Plastic_Mini_Tool_16;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // notesAppToolStripMenuItem
            // 
            this.notesAppToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Contract_16;
            this.notesAppToolStripMenuItem.Name = "notesAppToolStripMenuItem";
            this.notesAppToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.notesAppToolStripMenuItem.Text = "Notes app";
            this.notesAppToolStripMenuItem.Click += new System.EventHandler(this.notesAppToolStripMenuItem_Click);
            // 
            // timeControlerToolStripMenuItem
            // 
            this.timeControlerToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Scrap_Clock_16;
            this.timeControlerToolStripMenuItem.Name = "timeControlerToolStripMenuItem";
            this.timeControlerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.timeControlerToolStripMenuItem.Text = "Time controler";
            this.timeControlerToolStripMenuItem.Click += new System.EventHandler(this.timeControlerToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Image = global::Exam_management_system.Properties.Resources.Hopstarter_Sleek_Xp_Basic_Cancel_16;
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.logOutToolStripMenuItem.Text = "Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Code SemiLight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(161, 539);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(455, 32);
            this.label3.TabIndex = 40;
            this.label3.Text = "Go to result cell and write the result and click on Add button .\r\n\r\n";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(96)))), ((int)(((byte)(56)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Image = global::Exam_management_system.Properties.Resources.add_document;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(12, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 27);
            this.button1.TabIndex = 33;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Add_student_result);
            // 
            // searchtextbox
            // 
            this.searchtextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.searchtextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchtextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchtextbox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.searchtextbox.Location = new System.Drawing.Point(10, 28);
            this.searchtextbox.Name = "searchtextbox";
            this.searchtextbox.Size = new System.Drawing.Size(123, 22);
            this.searchtextbox.TabIndex = 42;
            this.searchtextbox.TextChanged += new System.EventHandler(this.Search_student_exam_paper);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Cascadia Code SemiLight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(139, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 22);
            this.label1.TabIndex = 43;
            this.label1.Text = "Search by exam name , exam id and student id";
            // 
            // Add_results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(916, 563);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchtextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Add_results";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add results";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReadStudentsNotes_FormClosing);
            this.Load += new System.EventHandler(this.ReadStudentsNotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem operatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStudentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem addAnnouncementsToolStripMenuItem;
        private TextBox searchtextbox;
        private Label label1;
        private ToolStripMenuItem chatToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem notesAppToolStripMenuItem;
        private ToolStripMenuItem timeControlerToolStripMenuItem;
    }
}