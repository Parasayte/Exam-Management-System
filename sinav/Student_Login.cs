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
               
        

        private void button1_Click(object sender, EventArgs e)
        {
            Teacher_Login a =new Teacher_Login();
            a.Show();
            Hide();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string name = ID_Textbox.Text; 
            string password = Password_Textbox.Text; 

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                   
                    string query = "SELECT COUNT(1) FROM Students WHERE name = @name AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@password", password);

                      
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Student_Menu a = new Student_Menu();
                            a.Show();
                             Hide();
                            con.Close();
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

        private void label1_Click(object sender, EventArgs e)
        {
            Teacher_AddSt t=new Teacher_AddSt();
            t.Show();
            Hide();
        }

        private void Student_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}