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
        // Connection string to the database
        string connectionString = "Server=.; Database=SchoolManagementSystem; Integrated Security=True";

        public Admin_login()
        {
            InitializeComponent();
        }

        private void LogIn_as_admin(object sender, EventArgs e)
        {
            // Method to handle admin login
        }

        private void Exit(object sender, EventArgs e)
        {
            // Show teacher login form and hide current form
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Exit the application when the form is closing
            Application.Exit();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            // Create a new SQL connection
            SqlConnection con = new SqlConnection(connectionString);
            // Create a new SQL command to check admin credentials
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Admins WHERE admin_id = @id AND password = @password", con);
            // Add parameters to the SQL command
            cmd.Parameters.AddWithValue("@id", ID_Textbox.Text);
            cmd.Parameters.AddWithValue("@password", Password_Textbox.Text);
            // Open the SQL connection
            con.Open();
            // Execute the SQL command and fill the result into a DataTable
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            // Check if the credentials are correct
            if (dataTable.Rows[0][0].ToString() == "1")
            {
                // Show admin menu and hide current form
                Admin_menu a = new Admin_menu();
                a.Show();
                Hide();
            }
            else
            {
                // Show error message if credentials are incorrect
                MessageBox.Show("Not correct , try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Close the SQL connection
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show teacher login form and hide current form
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show student login form and hide current form
            Student_login student_Login = new Student_login();
            student_Login.Show();
            Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyApp", "-1");

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
                // Create the directory in a safe location (MyDocuments folder)
                try
                {
                    Directory.CreateDirectory(path);
                    Directories_menu a = new Directories_menu(path);
                    a.Show();
                    Hide();
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Show error message if access is denied
                    MessageBox.Show("Access denied. Please ensure the application has proper permissions.");
                }
                catch (Exception ex)
                {
                    // Show error message if any other error occurs
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Show time controller app and hide current form
            Time_controler_app t = new Time_controler_app(-1);
            t.Show();
            Hide();
        }
    }
}
