using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace sinav
{
    public partial class Student_Login : Form
    {
        
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
            string id = ID_Textbox.Text; 
            string password = Password_Textbox.Text; 

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                   
                    string query = "SELECT COUNT(1) FROM Students WHERE id = @id AND password = @password";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@password", password);

                      
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Student_Menu a = new Student_Menu(Int32.Parse(id));
                        
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
            MessageBox.Show("You have to connect with this Admin SELAM\n05411100000\nhougjgrxkj@gmail.com", "STOP",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        private void Student_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}