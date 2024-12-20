﻿using System;
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
                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ReadStudentsNotes a = new ReadStudentsNotes();
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
          Admin a = new Admin();
            a.Show();
            Hide();
        }
    }
}
