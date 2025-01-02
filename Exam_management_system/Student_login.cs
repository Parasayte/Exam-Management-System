using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Student_login : Form
    {
        
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        public Student_login()
        {
            InitializeComponent(); 
        
        }
               
        

        private void Open_teacher_login_page(object sender, EventArgs e)
        {
            Teacher_login a =new Teacher_login();
            a.Show();
            Hide();
        }

        private void Log_in(object sender, EventArgs e)
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
                            Student_menu a = new Student_menu(Int32.Parse(id));
                        
                            a.Show();
                             Hide();
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show(@"Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Student_signUp a = new Student_signUp();
            a.Show();
            Hide();
        }

        private void Student_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Directories_menu a = new Directories_menu("D:\\Program Files\\-1");
            a.Show();
          Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Time_controler_app a = new Time_controler_app(-1);
            a.Show();
            Hide();
        }
    }
}