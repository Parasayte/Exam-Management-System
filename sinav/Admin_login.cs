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
    public partial class Admin_login : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

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

        private void Login_Button_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Admins WHERE id = @id AND password = @password", con);
            cmd.Parameters.AddWithValue("@id", ID_Textbox.Text);
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
            Edit_notes editNotes = new Edit_notes("D:\\Program Files\\-1");
            editNotes.Show();
            Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Time_controler_app t = new Time_controler_app(-1);
            t.Show();
            Hide();
        }
    }
}
