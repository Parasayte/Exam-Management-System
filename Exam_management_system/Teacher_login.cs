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
    public partial class Teacher_login : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Teacher_login()
        {
            InitializeComponent();
        }

        private void Open_student_loginPage(object sender, EventArgs e)
        {
            Student_login a =new Student_login();
            a.Show();
            Hide();
          
        }

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

                        cmd.Parameters.AddWithValue("@teacher_name", name);
                        cmd.Parameters.AddWithValue("@password", password);


                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {


                           
                            Add_results a = new Add_results();
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

        private void Teacher_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"You have to connect with an Admin ", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          
        }

        private void Open_public_notes(object sender, EventArgs e)
        {
            Directories_menu a = new Directories_menu("D:\\Program Files\\-1");
            a.Show();
            Hide();
        }

        private void open_pomodoro_app(object sender, EventArgs e)
        {
            Time_controler_app a = new Time_controler_app(-1);
            a.Show();
            Hide();
        }

        private void Open_admin_login(object sender, EventArgs e)
        {
            Admin_login a = new Admin_login();
            a.Show();
            Hide();
        }
    }
}
