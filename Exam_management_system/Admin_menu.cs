using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Admin_menu : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Admin_menu()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void Open_Add_student(object sender, EventArgs e)
        {
            Add_students teacher_AddSt = new Add_students();
            teacher_AddSt.Show();
            Hide();
        }

        private void Open_Add_exam(object sender, EventArgs e)
        {
            Add_exams addExam = new Add_exams();
            addExam.Show();
            Hide();
        }

        private void Open_Add_teachers(object sender, EventArgs e)
        {
            Add_teachers add_Teacher = new Add_teachers();
            add_Teacher.Show();
            Hide();
        }

        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Open_Add_result(object sender, EventArgs e)
        {
            Add_results read_Answers = new Add_results();
            read_Answers.Show();
            Hide();
         
        }

        private void Open_print_student_exam_paper(object sender, EventArgs e)
        {
            Print_result printResult = new Print_result();
            printResult.Show();
            Hide();
        }

        private void Open_Add_announcements(object sender, EventArgs e)
        {
            Add_announcements add_Announcement = new Add_announcements();
            add_Announcement.Show();
            Hide();
        }

        private void Open_Group_Chat(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(-1);
            group_Chat.Show();
            Hide();
        }

        private void Delete_Group_chats_messages(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand($"Delete  FROM Chat;", sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Chat is cleared.");
            sqlConnection.Close();
        }
    }
}
