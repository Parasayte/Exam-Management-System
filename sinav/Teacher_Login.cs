using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinav
{
    public partial class Teacher_Login : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Teacher_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student_Login a =new Student_Login();
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

                    string query = "SELECT COUNT(1) FROM Teacher WHERE teacher_name = @teacher_name AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        cmd.Parameters.AddWithValue("@teacher_name", name);
                        cmd.Parameters.AddWithValue("@password", password);


                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {


                            MessageBox.Show("Welcome to the system", "succeful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Read_Answers a = new Read_Answers();
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

        private void Teacher_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have to connect with an Admin ", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          
        }

        private void label6_Click(object sender, EventArgs e)
        {
            directories a = new directories("D:\\Program Files\\-1");
            a.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Pomodoro a = new Pomodoro(-1);
            a.Show();
            Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Admin_Login a = new Admin_Login();
            a.Show();
            Hide();
        }
    }
}
