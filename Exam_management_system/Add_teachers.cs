﻿using System;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exam_management_system
{
    public partial class Add_teachers : Form
    {
        // Connection string to the database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Add_teachers()
        {
            InitializeComponent();
            BrigTeachersData();
        }

        // Method to fetch and display teacher data
        private void BrigTeachersData()
        {
            string com = "SELECT*FROM Teacher;";
            SqlDataAdapter dt = new SqlDataAdapter(com, connectionString);
            DataTable dataTable = new DataTable();
            dt.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }

        // Method to add a new teacher
        private void Add_teacher(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string phone = textBox2.Text;
            DateTime birthd = dateTimePicker1.Value;
            string password = textBox5.Text;
            string gmail = textBox6.Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO Teacher (teacher_name, password, gmail, birthdate, phone_no) " +
                               "VALUES (@teacher_name, @password, @gmail, @birthdate, @phone_no)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Adding parameters to the query
                    cmd.Parameters.AddWithValue("@teacher_name", name);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gmail", gmail);
                    cmd.Parameters.AddWithValue("@birthdate", birthd);
                    cmd.Parameters.AddWithValue("@phone_no", phone);

                    // Executing the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher added");
                }
                con.Close();
                ClearTextbox();
            }
            
            BrigTeachersData();
        }

        // Method to clear textboxes
        private void ClearTextbox()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        // Method to handle form closing event
        private void Add_Teacher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Method to navigate to the admin menu
        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin_menu adminMenu = new Admin_menu();
            adminMenu.Show();
            Hide();
        }

        // Method to delete a teacher
        private void Delete_teacher(object sender, EventArgs e)
        {
            string id = textBox3.Text;
            string com3 = $@"UPDATE Students
SET teacher_id = {1}
WHERE teacher_id= {id};
";
            string com2 = "DELETE FROM Announcements WHERE teacher_id = '" + id + "'";
            string com = "DELETE FROM Teacher WHERE teacher_id = '" + id + "'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand(com2, connection);
            SqlCommand cmd3 = new SqlCommand(com3, connection);
            SqlCommand cmd = new SqlCommand(com, connection);

            connection.Open();
            cmd3.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            connection.Close();
            BrigTeachersData();
        }
    }
}
