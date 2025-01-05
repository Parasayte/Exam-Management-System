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

namespace Exam_management_system
{
    public partial class Add_results : Form
    {
        // Connection string to the database
        string con =  @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        SqlDataAdapter sqlDataAdapter;

        public Add_results()
        {
            InitializeComponent();
        }

        // Event handler for form closing
        private void ReadStudentsNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Event handler for logging out
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_login a = new Teacher_login();
            a.Show();
            Hide();
        }

        // Event handler for form load
        private void ReadStudentsNotes_Load(object sender, EventArgs e)
        {
            BrigExamsData();
        }

        // Event handler for adding exam
        private void addExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_exams addExam = new Add_exams();
            addExam.Show();
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
        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        // Method to bring exam data
        private void BrigExamsData()
        {
            string com = "SELECT * FROM Exam;";
            sqlDataAdapter = new SqlDataAdapter(com, con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private string selectedStudentId;

        // Event handler for cell click in DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                selectedStudentId = selectedRow.Cells["Student_id"].Value.ToString();
            }
        }

        // Method to add student result
        private void Add_student_result(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == null)
                {
                    MessageBox.Show("Please select the result in the DataGridView.");
                    return;
                }

                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];

                string examId = selectedRow.Cells["Exam_id"].Value?.ToString();
                string result = selectedRow.Cells["Result"].Value?.ToString();

                if (string.IsNullOrEmpty(examId))
                {
                    MessageBox.Show("No Exam ID found in the selected row.");
                    return;
                }

                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("No result found in the selected row.");
                    return;
                }

                string updateQuery = "UPDATE Exam SET finished = 'T', result = @Result WHERE exam_id = @Exam_id";

                using (SqlConnection conn = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Exam_id", examId);
                        cmd.Parameters.AddWithValue("@Result", result);

                        conn.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Finished column and result updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No matching exam found to update in the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes to the database: " + ex.Message);
            }

            BrigExamsData();
        }

        // Event handler for adding announcements
        private void addAnnouncementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_announcements addAnnouncements = new Add_announcements();
            addAnnouncements.Show();
            Hide();
        }

        // Event handler for cell click in DataGridView
        private void gdataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                var studentId = selectedRow.Cells["Student_id"].Value;
            }
        }

        // Method to search student exam paper
        private void Search_student_exam_paper(object sender, EventArgs e)
        {
            var searchTerm = searchtextbox.Text.ToLower();
            string filterExpression = $"Exam_name LIKE '%{searchTerm}%'";

            if (int.TryParse(searchTerm, out int parsedSearchTerm))
            {
                filterExpression += $" OR Student_id = {parsedSearchTerm} OR Exam_id = {parsedSearchTerm}";
            }

            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = filterExpression;
        }

        // Event handler for chat
        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(0);
            group_Chat.Show();
            Hide();
        }

        private void notesAppToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void timeControlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Time_controler_app a = new Time_controler_app(-1);
            a.Show();
            Hide();

        }
    }
}
