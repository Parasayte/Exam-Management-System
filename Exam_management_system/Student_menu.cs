using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Student_menu : Form
    {
        int student_id;
        public Student_menu(int student_id)
        {
            InitializeComponent();
            this.student_id = student_id;
        }

        private void Student_menu1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student_exams_menu student_Exams_Menu = new Student_exams_menu(student_id);
            student_Exams_Menu.Show();
            Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Announcements_list announcements_List = new Announcements_list(student_id);
            announcements_List.Show();
            Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Students Notes", $"{student_id}");

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
                    string parentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Students Notes", $"{student_id}");
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

        private void button8_Click(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(student_id);
            group_Chat.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Time_controler_app time_Controler_App = new Time_controler_app(student_id);
            time_Controler_App.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login_menu login_Menu = new Login_menu();
            login_Menu.Show();
            Hide();
        }
    }
}
