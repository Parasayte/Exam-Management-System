using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exam_management_system
{
    public partial class Add_exams : Form
    {
        // Connection string to the database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";
        DateTime date;

        public Add_exams()
        {
            InitializeComponent();
        }

        // Event handler for form closing
        private void addquestions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Event handler for reading notes
        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_results readStudentsNotes = new Add_results();
            readStudentsNotes.Show();
            Hide();
        }

        // Event handler for adding a student
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

        // Method to clear textboxes
        private void ClearTextbox()
        {
            richTextBox3.Text = null;
            richTextBox2.Text = null;
            richTextBox4.Text = null;
            richTextBox5.Text = null;
            richTextBox6.Text = null;
            richTextBox7.Text = null;
        }

        // Event handler for adding an exam
        private void Add_exam(object sender, EventArgs e)
        {
            DateTime lastdate = dateTimePicker1.Value;
            DateTime now = DateTime.Now;

            // Check if the last date is in the future
            if (lastdate < now)
            {
                MessageBox.Show(@"Last date must be greater than now", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int time = Int32.Parse(numericUpDown1.Value.ToString());

            // Check if the time is greater than 1 minute
            if (time < 1)
            {
                MessageBox.Show(@"Time must be greater than 1 minutes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string q1 = richTextBox2.Text.Trim();
                string q2 = richTextBox3.Text.Trim();
                string q3 = richTextBox6.Text.Trim();
                string q4 = richTextBox4.Text.Trim();
                string q5 = richTextBox5.Text.Trim();
                string name = richTextBox7.Text.Trim();

                try
                {
                    sqlConnection.Open();

                    // SQL query to insert exam details
                    string assignExamQuery = @"
                        INSERT INTO Exam (q1, q2, q3, q4, q5, exam_name, finished, Student_id, Time, lastDate)
                        SELECT @q1, @q2, @q3, @q4, @q5, @exam_name, @finished, student_id, @Time, @lastDate
                        FROM Students;";

                    SqlCommand assignExamCommand = new SqlCommand(assignExamQuery, sqlConnection);
                    assignExamCommand.Parameters.AddWithValue("@q1", q1);
                    assignExamCommand.Parameters.AddWithValue("@q2", q2);
                    assignExamCommand.Parameters.AddWithValue("@q3", q3);
                    assignExamCommand.Parameters.AddWithValue("@q4", q4);
                    assignExamCommand.Parameters.AddWithValue("@q5", q5);
                    assignExamCommand.Parameters.AddWithValue("@exam_name", name);
                    assignExamCommand.Parameters.AddWithValue("@finished", "F");
                    assignExamCommand.Parameters.AddWithValue("@Time", time);
                    assignExamCommand.Parameters.AddWithValue("@lastDate", lastdate);
                    assignExamCommand.ExecuteNonQuery();

                    MessageBox.Show("Exam its for all students");
                    ClearTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            BrigExamsData();
        }

        // Event handler for text change in richTextBox4
        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        // Event handler for form load
        private void AddExam_Load(object sender, EventArgs e)
        {
            BrigExamsData();
        }

        // Method to bring exam data
        private void BrigExamsData()
        {
            string com = "SELECT * FROM Exam;";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(com, connectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        // Event handler for deleting an exam
        private void Delete_exam(object sender, EventArgs e)
        {
            try
            {
                string examName = richTextBox7.Text;

                string com = "DELETE FROM Exam WHERE exam_name = @exam_name";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(com, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@exam_name", examName);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                MessageBox.Show("Exam deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            BrigExamsData();
            ClearTextbox();
        }

        // Event handler for adding announcements
        private void addAnnouncementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_announcements addAnnouncements = new Add_announcements();
            addAnnouncements.Show();
            Hide();
        }

        // Event handler for deleting time-passed exams
        private void Delete_time_passed_exams(object sender, EventArgs e)
        {
            date = DateTime.Now;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand($"Delete FROM Exam WHERE lastDate <'{date}';", sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("If there are Old exams, will be deleteded.");
            sqlConnection.Close();
            BrigExamsData();
        }

        // Event handler for chat
        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(2);
            group_Chat.Show();
            Hide();
        }

        private void notesAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

     

        private void notesAppToolStripMenuItem1_Click(object sender, EventArgs e)
        {
// Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Teachers Notes", "-1");

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
};