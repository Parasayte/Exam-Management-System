using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Data;
using System.IO;
namespace Exam_management_system
{
    public partial class Add_students : Form
    {
        // Connection string to the database
        string connectionString = "Server=.; Database=SchoolManagementSystem; Integrated Security=True;";

        public Add_students()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            // Handle text changed event for textBox1
        }

        private void Teacher_AddSt_Load(object sender, System.EventArgs e)
        {
            // Load student data when the form loads
            BrigStudentData();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // Handle button1 click event
        }

        private void ClearTextbox()
        {
            // Clear all textboxes
            textBox1.Text = null;
            textBox2.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        private void Teacher_AddSt_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Exit the application when the form is closing
            Application.Exit();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            // Open Student_login form and hide the current form
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        private void addExamToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Open Add_exams form and hide the current form
            Add_exams addExam = new Add_exams();
            addExam.Show();
            Hide();
        }

        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Add_results form and hide the current form
            Add_results readStudentsNotes = new Add_results();
            readStudentsNotes.Show();
            Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Add_students form and hide the current form
            Add_students addSt = new Add_students();
            addSt.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Teacher_login form and hide the current form
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Handle button3 click event
        }

        private void Add_student(object sender, EventArgs e)
        {
            // Get student details from textboxes
            string name = textBox1.Text;
            string nickn = textBox2.Text;
            string birthd = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string password = textBox5.Text;
            string gmail = textBox6.Text;

            // Insert student details into the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Students (name, nick_name, birth_date, password, gmail) " +
                               "VALUES (@name, @nick_name, @birth_date, @password, @gmail)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@nick_name", nickn);
                    cmd.Parameters.AddWithValue("@birth_date", birthd);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gmail", gmail);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Student added");
                }
                con.Close();
                ClearTextbox();
            }
            BrigStudentData();
        }

        private void BrigStudentData()
        {
            // Retrieve student data from the database and display it in the DataGridView
            string com = "SELECT*FROM Students;";
            SqlDataAdapter dt = new SqlDataAdapter(com, connectionString);
            DataTable dataTable = new DataTable();
            dt.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void Delete_student(object sender, EventArgs e)
        {
            // Get student ID from textbox
            string id = textBox3.Text;
            // Delete student from the database
            string com = "DELETE FROM Students WHERE student_id = " + id;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(com, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            BrigStudentData();
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Open Teacher_login form and hide the current form
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void readNotesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Open Add_results form and hide the current form
            Add_results readStudentsNotes = new Add_results();
            readStudentsNotes.Show();
            Hide();
        }

        private void operatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handle operatToolStripMenuItem click event
        }

        private void addAnnouncementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Add_announcements form and hide the current form
            Add_announcements addAnnouncements = new Add_announcements();
            addAnnouncements.Show();
            Hide();
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Group_chat form and hide the current form
            Group_chat group_Chat = new Group_chat(2);
            group_Chat.Show();
            Hide();
        }

        private void notesAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), " Teachers Notes", "-1");

            // Check if the directory exists
            if (Directory.Exists(path))
            {
                // If it exists, open the menu
                Directories_menu a = new Directories_menu(path);
                a.Show();
                Hide();
            }
            else
            {
                // Create the directory in a safe location (MyDocuments folder)
                try
                {
                    Directory.CreateDirectory(path);
                    Directories_menu a = new Directories_menu(path);
                    a.Show();
                    Hide();
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Show an error message if access is denied
                    MessageBox.Show("Access denied. Please ensure the application has proper permissions.");
                }
                catch (Exception ex)
                {
                    // Show an error message if an exception occurs
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void timeControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Time_controler_app a = new Time_controler_app(-1);
            a.Show();
            Hide();
        }
    }
}