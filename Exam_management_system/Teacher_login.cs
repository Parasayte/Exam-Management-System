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
    public partial class Teacher_login : Form
    {
        // Connection string to the database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Teacher_login()
        {
            InitializeComponent();
        }

        // Method to open the student login page
        private void Open_student_loginPage(object sender, EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        // Method to handle teacher login
        private void Login(object sender, EventArgs e)
        {
            string name = ID_Textbox.Text;
            string password = Password_Textbox.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(1) FROM Teacher WHERE teacher_name = @teacher_name AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@teacher_name", name);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Execute the query and get the result
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            con.Close();

                            // If login is successful, open the Add_results form
                            Teacher_menu a = new Teacher_menu();
                            a.Show();
                            Hide();
                        }
                        else
                        {
                            // If login fails, show an error message
                            MessageBox.Show(@"Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Method to handle form closing event
        private void Teacher_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Method to handle label click event
        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"You have to connect with an Admin ", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Method to open public notes
        private void Open_public_notes(object sender, EventArgs e)
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

        

        // Method to open the pomodoro app
        private void open_pomodoro_app(object sender, EventArgs e)
        {
            Time_controler_app a = new Time_controler_app(-2);
            a.Show();
            Hide();
        }

        // Method to open the admin login page
        private void Open_admin_login(object sender, EventArgs e)
        {
            Admin_login a = new Admin_login();
            a.Show();
            Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
