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
    public partial class Students_exam : Form
    {
        int Studenid;
        int examid;
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
         int totalTime ; 
         int timeRemaining;
        public Students_exam(int examId,int studentId)
        {
            InitializeComponent();
            examid = examId;
            Studenid = studentId;
            PrintQuestions();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_menu a =new Student_menu(0);
            a.Show();
            Hide();
        }

        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void PrintQuestions()
        {
            string query = "SELECT q1, q2, q3, q4, q5 FROM Exam1 WHERE exam_id="+examid; 
            Label[] labels = { label1, label2, label3, label4, label5 }; 

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
                            for (int i = 0; i < labels.Length; i++)
                            {
                                labels[i].Text = reader[i].ToString(); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                  
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void FillAnswers()
        {
            string a1 = richTextBox1.Text,
                   a2 = richTextBox2.Text,
                   a3 = richTextBox3.Text,
                   a4 = richTextBox4.Text,
                   a5 = richTextBox5.Text;

            string query = "UPDATE Exam1 SET finished = @finished, a1 = @a1, a2 = @a2, a3 = @a3, a4 = @a4, a5 = @a5 WHERE student_id = @student_id AND exam_id = @exam_id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a1", a1);
                cmd.Parameters.AddWithValue("@a2", a2);
                cmd.Parameters.AddWithValue("@a3", a3);
                cmd.Parameters.AddWithValue("@a4", a4);
                cmd.Parameters.AddWithValue("@a5", a5);
                cmd.Parameters.AddWithValue("@finished", "T");
                cmd.Parameters.AddWithValue("@student_id", Studenid);
                cmd.Parameters.AddWithValue("@exam_id", examid);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Exam finished! Please wait for the result.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Student_menu exams = new Student_menu(Studenid);
                    exams.Show();
                    Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Error: {ex.Message}", @"Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_login a =new Student_login();
            a.Show();
            Hide();
        }

        private void Student_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Finish_exam(object sender, EventArgs e)
        {
            FillAnswers();
        }

        private void Student_Exam_Load(object sender, EventArgs e)
        {
            string query = "SELECT Time FROM Exam1 WHERE exam_id = @examid";

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
                        timeRemaining = totalTime * 60; 
                        MessageBox.Show($"Total Exam Time: {totalTime} minutes");
                        timer1.Start(); 
                    }
                    else
                    {
                        MessageBox.Show(@"Error: No time found for the exam", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                label18.Text += $" {totalTime} minutes";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeRemaining <= 0)
            {
                timer1.Stop();
                MessageBox.Show(@"Time is up! The exam will now be submitted.", "Time Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillAnswers();
                return;
            }

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
