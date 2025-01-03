using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Student_login : Form
    {
        // Connection string to the database
        string connectionString = "Server=.; Database=SchoolManagementSystem; Integrated Security=True;";

        public Student_login()
        {
            InitializeComponent();
        }

        // Method to open the teacher login page
        private void Open_teacher_login_page(object sender, EventArgs e)
        {
            Teacher_login a = new Teacher_login();
            a.Show();
            Hide();
        }

        // Method to handle student login
        private void Log_in(object sender, EventArgs e)
        {
            string id = ID_Textbox.Text;
            string password = Password_Textbox.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(1) FROM Students WHERE student_id = @id AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Execute the query and get the result
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            // If login is successful, open the student menu
                            Student_menu a = new Student_menu(Int32.Parse(id));
                            a.Show();
                            Hide();
                            con.Close();
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

        private void Password_Textbox_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for password textbox
        }

        // Method to open the student sign-up page
        private void label1_Click(object sender, EventArgs e)
        {
            Student_signUp a = new Student_signUp();
            a.Show();
            Hide();
        }

        // Method to handle form closing event
        private void Student_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Method to handle label click event
        private void label6_Click(object sender, EventArgs e)
        {
            // Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Teachers Notes", "-1");

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

        // Method to handle label click event
        private void label5_Click(object sender, EventArgs e)
        {
            Time_controler_app a = new Time_controler_app(-1);
            a.Show();
            Hide();
        }
    }
}