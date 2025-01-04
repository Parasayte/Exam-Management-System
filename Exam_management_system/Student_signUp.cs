using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Data;
namespace Exam_management_system
{
    public partial class Student_signUp : Form
    {
        // Connection string to the database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Student_signUp()
        {
            InitializeComponent();
            // Initialize birth date from textBox4
            string birthd = textBox4.Text;
        }

        // Event handler for form closing
        private void Teacher_AddSt_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Event handler for button2 click
        private void button2_Click(object sender, System.EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        // Event handler for adding exam
        private void addExamToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Add_exams addExam = new Add_exams();
            addExam.Show();
            Hide();
        }

        // Event handler for reading notes
        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_results readStudentsNotes = new Add_results();
            readStudentsNotes.Show();
            Hide();
        }

        // Event handler for adding student
        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_students addSt = new Add_students();
            addSt.Show();
            Hide();
        }

        // Event handler for logging out
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        // Event handler for button3 click (currently empty)
        private void button3_Click(object sender, EventArgs e)
        {

        }

        // Event handler for sign up button click
        private void Sign_up(object sender, EventArgs e)
        {
            // Get values from input fields
            string birthd = dateTimePicker4.Value.ToString("yyyy-MM-dd");
            string name = textBox1.Text;
            string nickn = textBox2.Text;
            string password = textBox5.Text;
            string gmail = textBox6.Text;

            // Insert student data into the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO Students (name, nick_name, birth_date, password, gmail) " +
                               "VALUES (@name, @nick_name, @birth_date, @password, @gmail)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to the query
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@nick_name", nickn);
                    cmd.Parameters.AddWithValue("@birth_date", birthd);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gmail", gmail);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($@"Welcome to the system {name}", "Congrat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                Go_back_to_loginPage(sender, e);
            }
        }

        // Event handler for form load (currently empty)
        private void Student_signUp_Load(object sender, EventArgs e)
        {

        }

        // Navigate back to login page
        private void Go_back_to_loginPage(object sender, EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        // Event handler for form closing
        private void Student_signUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}