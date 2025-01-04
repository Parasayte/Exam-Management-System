using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Exam_management_system
{
    public partial class Admin_signup : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Admin_signup()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string password = textBox5.Text;
            if(name == null || password == null)
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
            }
            if (textBox2.Text == "selam")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO Admins (name,  password) " +
                                   "VALUES (@name,  @password)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@password", password);


                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($@"Welcome to the system {name}", "Congrat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();

                }
            }
            else
            {
                MessageBox.Show("Not correct , try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Admin_signup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_login a = new Admin_login();
            a.Show();
            Hide();
        }
    }
}