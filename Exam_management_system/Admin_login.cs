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
    public partial class Admin_login : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Admin_login()
        {
            InitializeComponent();
        }

        private void LogIn_as_admin(object sender, EventArgs e)
        {
          
        }

        private void Exit(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Try_login(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Admins WHERE name = @name AND password = @password", con);
            cmd.Parameters.AddWithValue("@name", ID_Textbox.Text);
            cmd.Parameters.AddWithValue("@password", Password_Textbox.Text);
            con.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
          if(dataTable.Rows[0][0].ToString() == "1")
            {
                Admin_menu a = new Admin_menu();
                a.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Not correct , try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student_login student_Login = new Student_login();
            student_Login.Show();
            Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Public Notes", "-1");

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

        private void label2_Click(object sender, EventArgs e)
        {
            Time_controler_app t = new Time_controler_app(-2);
            t.Show();
            Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Admin_signup a = new Admin_signup();
            a.Show();
            Hide();
        }
    }
}
