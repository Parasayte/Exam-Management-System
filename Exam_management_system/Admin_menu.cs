using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Admin_menu : Form
    {
        // Connection string to the database
        string connectionString = "Server=.; Database=SchoolManagementSystem; Integrated Security=True;";

        public Admin_menu()
        {
            InitializeComponent();
        }

        // Open Teacher Login form
        private void button4_Click(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        // Open Add Student form
        private void Open_Add_student(object sender, EventArgs e)
        {
            Add_students teacher_AddSt = new Add_students();
            teacher_AddSt.Show();
            Hide();
        }

        // Open Add Exam form
        private void Open_Add_exam(object sender, EventArgs e)
        {
            Add_exams addExam = new Add_exams();
            addExam.Show();
            Hide();
        }

        // Open Add Teachers form
        private void Open_Add_teachers(object sender, EventArgs e)
        {
            Add_teachers add_Teacher = new Add_teachers();
            add_Teacher.Show();
            Hide();
        }

        // Exit application on form closing
        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Open Add Result form
        private void Open_Add_result(object sender, EventArgs e)
        {
            Add_results read_Answers = new Add_results();
            read_Answers.Show();
            Hide();
        }

        // Open Print Student Exam Paper form
        private void Open_print_student_exam_paper(object sender, EventArgs e)
        {
            Print_result printResult = new Print_result();
            printResult.Show();
            Hide();
        }

        // Open Add Announcements form
        private void Open_Add_announcements(object sender, EventArgs e)
        {
            Add_announcements add_Announcement = new Add_announcements();
            add_Announcement.Show();
            Hide();
        }

        // Open Group Chat form
        private void Open_Group_Chat(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(1);
            group_Chat.Show();
            Hide();
        }

        // Delete all messages in group chat
        private void Delete_Group_chats_messages(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand($"Delete  FROM Chat;", sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Chat is cleared.");
            sqlConnection.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Admins Notes", "-1");

            // Check if the directory exists
            if (Directory.Exists(path))
            {
                // If it exists, open the menu
                Directories_menu a = new Directories_menu(path);
                a.Show();
                Hide();
            }
            else
            {
                // Try creating the directory in a safe location (MyDocuments folder)
                try
                {
                    // Ensure the parent directory exists before trying to create a subdirectory
                    string parentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Public Notes");
                    if (!Directory.Exists(parentDirectory))
                    {
                        Directory.CreateDirectory(parentDirectory);
                    }

                    // Now create the target directory
                    Directory.CreateDirectory(path);

                    Directories_menu a = new Directories_menu(path);
                    a.Show();
                    Hide();
                }
                catch (UnauthorizedAccessException)
                {
                    // Show an error message if access is denied
                    MessageBox.Show("Access denied. Please ensure the application has proper permissions.");
                }
                catch (Exception ex)
                {
                    // Show an error message if an exception occurs
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Announcements_list a = new Announcements_list(-1);
            a.Show();
            Hide();
        }
    }
}
