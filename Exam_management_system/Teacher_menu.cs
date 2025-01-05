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
    public partial class Teacher_menu : Form
    {
        
        string teacher = "teacher";
        public Teacher_menu()
        {
            InitializeComponent();
           
        }

        private void Teacher_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_students add_Students = new Add_students(teacher);
            add_Students.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_exams add_Exams = new Add_exams(teacher);
            add_Exams.Show();
            Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Print_result print_result = new Print_result(teacher);
            print_result.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Time_controler_app time_Controler_App = new Time_controler_app(0);
            time_Controler_App.Show();
            Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Teachers Notes", "-1");

            // Check if the directory exists
            if (Directory.Exists(path))
            {
                Directories_menu a = new Directories_menu(path);
                a.Show();
                Hide();
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                    Directories_menu a = new Directories_menu(path);
                    a.Show();
                    Hide();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("Access denied. Please ensure the application has proper permissions.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(0);
            group_Chat.Show();
            Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Add_announcements addAnnouncements = new Add_announcements(teacher);
            addAnnouncements.Show();
            Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Announcements_list a = new Announcements_list(0);
            a.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Add_results readStudentsNotes = new Add_results(teacher);
            readStudentsNotes.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Login_menu login_Menu = new Login_menu();
            login_Menu.Show();
            Hide();
        }
    }
}
