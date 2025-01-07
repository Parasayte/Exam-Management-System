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
    public partial class Add_new_directory : Form
    {
        int student_id;
        string path1;
        public Add_new_directory(int id,string path)
        {
            InitializeComponent();
            student_id = id;
            path1 = path;
        }

        private void AddNewDirectory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Add_newDirectory(object sender, EventArgs e)
        {

            // Define base paths for different note types
            string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Trim();
            string teacherNotesPath = Path.Combine(baseDirectory, "Teachers Notes", "-1").Trim();
            string adminNotesPath = Path.Combine(baseDirectory, "Admins Notes", "-1").Trim();
            string studentNotesBasePath = Path.Combine(baseDirectory, "Students Notes").Trim();

            path1 = path1.Trim();
            if (path1 == teacherNotesPath)
            {
                // Open Teacher menu
                Teacher_menu teacher_Menu = new Teacher_menu();
                teacher_Menu.Show();
                Hide();
                return;
            }
            else if (path1 == adminNotesPath)
            {
                // Show the Admin menu form
                Admin_menu admin_Menu = new Admin_menu();
                admin_Menu.Show();
                Hide();
                return;
            }
            else if (path1.StartsWith(studentNotesBasePath))
            {
                try
                {
                    // Extract the directory name (student ID) from the path
                    string folderName = Path.GetFileName(path1);

                    if (int.TryParse(folderName.Trim(), out int studentId))
                    {
                        // Open Student Menu with the ID as a parameter
                        Student_menu student_Menu = new Student_menu(studentId);
                        student_Menu.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid directory name format. Unable to parse student ID.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions and notify the user
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {

                Login_menu login = new Login_menu();
                login.Show();
                Hide();
            }
        }

      

        private void Cancel(object sender, EventArgs e)
        {
            // Define base paths for different note types
            string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Trim();
            string teacherNotesPath = Path.Combine(baseDirectory, "Teachers Notes", "-1").Trim();
            string adminNotesPath = Path.Combine(baseDirectory, "Admins Notes", "-1").Trim();
            string studentNotesBasePath = Path.Combine(baseDirectory, "Students Notes").Trim();

            path1 = path1.Trim();
            if (path1 == teacherNotesPath)
            {
                // Open Teacher menu
                Teacher_menu teacher_Menu = new Teacher_menu();
                teacher_Menu.Show();
                Hide();
                return;
            }
            else if (path1 == adminNotesPath)
            {
                // Show the Admin menu form
                Admin_menu admin_Menu = new Admin_menu();
                admin_Menu.Show();
                Hide();
                return;
            }
            else if (path1.StartsWith(studentNotesBasePath))
            {
                try
                {
                    // Extract the directory name (student ID) from the path
                    string folderName = Path.GetFileName(path1);

                    if (int.TryParse(folderName.Trim(), out int studentId))
                    {
                        // Open Student Menu with the ID as a parameter
                        Student_menu student_Menu = new Student_menu(studentId);
                        student_Menu.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid directory name format. Unable to parse student ID.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions and notify the user
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {

                Login_menu login = new Login_menu();
                login.Show();
                Hide();
            }
        }
    }
}
