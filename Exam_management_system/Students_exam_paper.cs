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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exam_management_system
{
    public partial class Students_exam_paper : Form
    {
        int Studenid; // Student ID
        int examid; // Exam ID
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";
        int totalTime; // Total time for the exam
        int timeRemaining; // Time remaining for the exam

        public Students_exam_paper(int examId, int studentId)
        {
            InitializeComponent();
            examid = examId;
            Studenid = studentId;
            PrintQuestions(); // Print exam questions
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Handle panel paint event
        }

        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Navigate to student menu
            Student_exams_menu a = new Student_exams_menu(0);
            a.Show();
            Hide();
        }

        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Handle results menu item click
        }

        private void PrintQuestions()
        {
            // Query to get exam questions
            string query = "SELECT q1, q2, q3, q4, q5 FROM Exam WHERE exam_id=" + examid;
            Label[] labels = { label1, label2, label3, label4, label5 }; // Labels to display questions

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set questions to labels
                            for (int i = 0; i < labels.Length; i++)
                            {
                                labels[i].Text = reader[i].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Show error message
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void SaveAnswers()
        {
            string a1 = richTextBox1.Text,
                   a2 = richTextBox2.Text,
                   a3 = richTextBox3.Text,
                   a4 = richTextBox4.Text,
                   a5 = richTextBox5.Text;

            string query = "UPDATE Exam SET finished = @finished, a1 = @a1, a2 = @a2, a3 = @a3, a4 = @a4, a5 = @a5 WHERE student_id = @student_id AND exam_id = @exam_id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a1", a1);
                cmd.Parameters.AddWithValue("@a2", a2);
                cmd.Parameters.AddWithValue("@a3", a3);
                cmd.Parameters.AddWithValue("@a4", a4);
                cmd.Parameters.AddWithValue("@a5", a5);
                cmd.Parameters.AddWithValue("@finished", "Yes");
                cmd.Parameters.AddWithValue("@student_id", Studenid);
                cmd.Parameters.AddWithValue("@exam_id", examid);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Exam finished! Please wait for the result.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Error: {ex.Message}", @"Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Log out and navigate to login form
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        private void Student_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ask the user for confirmation
            DialogResult dialog = MessageBox.Show(
                @"Are you sure you want to exit? Your answers will be saved, and the exam will be marked as finished.",
                @"Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation
            );

            if (dialog == DialogResult.Yes)
            {
                // Save answers and mark the exam as finished
                SaveAnswers();
                Application.Exit();
            }
            else if (dialog == DialogResult.No)
            {
                // Cancel the form closing event
                e.Cancel = true;
            }
        }

        private void Finish_exam(object sender, EventArgs e)
        {
            // Finish exam and save answers
            SaveAnswers();
            Student_exams_menu exams = new Student_exams_menu(Studenid);
            exams.Show();
            Hide();
        }

        private void Student_Exam_Load(object sender, EventArgs e)
        {
            // Query to get exam time
            string query = "SELECT Time FROM Exam WHERE exam_id = @examid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@examid", examid);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        totalTime = Convert.ToInt32(result);
                        timeRemaining = totalTime * 60; // Convert minutes to seconds
                        MessageBox.Show($"Total Exam Time: {totalTime} minutes");
                        timer1.Start(); // Start timer
                    }
                    else
                    {
                        // Show error message if no time found
                        MessageBox.Show(@"Error: No time found for the exam", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    // Show error message
                    MessageBox.Show($@"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                label18.Text += $" {totalTime} minutes";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeRemaining <= 0)
            {
                // Stop timer and submit exam when time is up
                timer1.Stop();
                MessageBox.Show(@"Time is up! The exam will now be submitted.", "Time Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveAnswers();
                return;
            }

            // Update timer label
            int minutes = timeRemaining / 60;
            int seconds = timeRemaining % 60;

            Point scrollPosition = panel2.AutoScrollPosition;

            panel2.SuspendLayout();
            label16.Text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
            panel2.ResumeLayout();

            panel2.AutoScrollPosition = new Point(-scrollPosition.X, -scrollPosition.Y);

            timeRemaining--;
        }
    }
}
