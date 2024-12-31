using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinav
{
    public partial class Admin_Menu : Form
    {
        public Admin_Menu()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Teacher_Login teacher_Login = new Teacher_Login();
            teacher_Login.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Student teacher_AddSt = new Add_Student();
            teacher_AddSt.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_Exam addExam = new Add_Exam();
            addExam.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Teacher add_Teacher = new Add_Teacher();
            add_Teacher.Show();
            Hide();
        }

        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Read_Answers read_Answers = new Read_Answers();
            read_Answers.Show();
            Hide();
         
        }

        private void button6_Click(object sender, EventArgs e)
        {
            printResult printResult = new printResult();
            printResult.Show();
            Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Add_announcement add_Announcement = new Add_announcement();
            add_Announcement.Show();
            Hide();
        }
    }
}
