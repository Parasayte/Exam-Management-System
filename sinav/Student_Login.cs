using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace sinav
{
    public partial class Student_Login : Form
    {
        SqlConnection con;
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        public Student_Login()
        {
            InitializeComponent(); 
        
        }
                private void InitializeSqlConnection()
        {
            try
            {

                string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
                con = new SqlConnection(connectionString);
                con.Open();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Failed: " + ex.Message);
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Teacher_Login a =new Teacher_Login();
            a.Show();
            Hide();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string id = ID_Textbox.Text; // Assuming txtUsername is your username input TextBox
            string password = Password_Textbox.Text; // Assuming txtPassword is your password input TextBox

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // SQL query to check for matching username and password
                    string query = "SELECT COUNT(1) FROM Student WHERE id = @id AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use parameters to prevent SQL Injection
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Execute the query and check for matching records
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Navigate to the next form or perform desired action here
                            Student_Menu a = new Student_Menu();
                            a.Show();
                             Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Password_Textbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}