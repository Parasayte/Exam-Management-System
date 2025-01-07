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

    public partial class Print_result : Form 
    {
        string Role;
        // Connection string to the database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Print_result(string role)
        {
            InitializeComponent();
            Role = role;
        }

        // Event handler for button click to save exam result as HTML
        private void button2_Click(object sender, EventArgs e)
        {
            SaveExamResultAsHtml();
        }

        // Method to save exam result as HTML
        private void SaveExamResultAsHtml()
        {
            try
            {
                // Check if Exam ID is provided
                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show(@"Please enter an Exam ID in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate Exam ID
                if (!int.TryParse(textBox4.Text, out int examId))
                {
                    MessageBox.Show(@"Invalid Exam ID. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Configure SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "HTML Files (*.html)|*.html",
                    Title = "Save Exam Result as HTML",
                    FileName = "ExamResult.html"
                };

                // Show SaveFileDialog and save file if user clicks OK
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        string currentDate = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                        writer.WriteLine("<!DOCTYPE html>");
                        writer.WriteLine("<html>");
                        writer.WriteLine("<head>");
                        writer.WriteLine("<style>");
                        writer.WriteLine("body { font-family: 'Inter', sans-serif; background-color: #1a1a1a; color: #dcdcdc; margin: 0; padding: 20px; line-height: 1.6; }");
                        writer.WriteLine("h1 { color: #ffffff; text-align: center; font-size: 2.5em; margin: 30px 0; font-weight: 700; text-shadow: 0 0 10px white; }");
                        writer.WriteLine(".container { max-width: 800px; margin: 0 auto; padding: 20px; }");
                        writer.WriteLine(".section { margin: 20px 0; padding: 25px; border: 1px solid #2e2e2e; border-radius: 12px; background-color: #252525; box-shadow: 0 6px 12px rgba(255, 255, 255, 0.3); }");
                        writer.WriteLine(".section h2 { font-size: 1.5em; color: #f0f0f0; margin-bottom: 15px; }");
                        writer.WriteLine(".question { font-size: 1.1em; font-family: 'Cascadia Code', monospace; color: #bbbbbb; margin-bottom: 10px; }");
                        writer.WriteLine(".answer { font-size: 1em; font-family: 'Cascadia Code', monospace; color: #ffffff; margin-left: 20px; }");
                        writer.WriteLine(".result { font-family: 'Roboto Mono', monospace; font-weight: bold; color: #2ecc71; font-size: 1.25em; margin-top: 20px; text-shadow: 0 0 10px white; }");
                        writer.WriteLine(".anti-forgery { font-size: 0.9em; color: #888888; text-align: center; margin-top: 40px; }");
                        writer.WriteLine(".footer { text-align: center; font-size: 0.85em; color: #888888; margin-top: 60px; }");
                        writer.WriteLine("button { background-color: #3b3b3b; color: #ffffff; border: none; padding: 10px 20px; font-size: 1em; border-radius: 5px; cursor: pointer; transition: background-color 0.3s ease; }");
                        writer.WriteLine("button:hover { background-color: #4c4c4c; }");
                        writer.WriteLine("a { color: #00aaff; text-decoration: none; transition: color 0.3s ease; }");
                        writer.WriteLine("a:hover { color: #66cfff; }");
                        writer.WriteLine("</style>");
                        writer.WriteLine("</head>");
                        writer.WriteLine("<body>");

                        writer.WriteLine("<h1>Exam Result</h1>");
                        writer.WriteLine("<div class='section'>");
                        writer.WriteLine($"<p><strong>Date:</strong> {currentDate}</p>");

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Query to get exam result
                            SqlCommand resultCmd = new SqlCommand(
                                "SELECT result, finished FROM Exam WHERE exam_id = @exam_id",
                                connection
                            );
                            resultCmd.Parameters.AddWithValue("@exam_id", examId);

                            SqlDataReader resultReader = resultCmd.ExecuteReader();
                            float resultValue = 0;
                            bool isFinished = false;

                            if (resultReader.Read())
                            {
                                isFinished = resultReader["finished"]?.ToString() == "T";

                                if (!isFinished)
                                {
                                    var resultObj = resultReader["result"];
                                    if (resultObj != DBNull.Value && float.TryParse(resultObj.ToString(), out resultValue))
                                    {
                                        writer.WriteLine($"<p><strong>Result:</strong> <span class='result'>{resultValue}</span></p>");
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Result not available or invalid for this exam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        resultReader.Close();
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(@"The exam is not finished yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    resultReader.Close();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(@"No exam found with the provided Exam ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                resultReader.Close();
                                return;
                            }

                            resultReader.Close();

                            // Query to get exam details
                            SqlCommand examDetailsCmd = new SqlCommand(
                                "SELECT s.name, s.nick_name, e.exam_name, e.q1, e.a1, e.q2, e.a2, e.q3, e.a3, e.q4, e.a4, e.q5, e.a5 " +
                                "FROM Exam e " +
                                "INNER JOIN Students s ON e.student_id = s.student_id " +
                                "WHERE e.exam_id = @exam_id", connection);
                            examDetailsCmd.Parameters.AddWithValue("@exam_id", examId);

                            SqlDataReader examDetailsReader = examDetailsCmd.ExecuteReader();

                            if (examDetailsReader.Read())
                            {
                                string studentName = examDetailsReader["name"].ToString();
                                string studentNickname = examDetailsReader["nick_name"].ToString();
                                string examName = examDetailsReader["exam_name"].ToString();

                                writer.WriteLine($"<p><strong>Student Name:</strong> {studentName} {studentNickname}</p>");

                                writer.WriteLine($"<p><strong>Exam Name:</strong> {examName}</p>");
                                writer.WriteLine("</div>");

                                writer.WriteLine("<div class='section'>");
                                writer.WriteLine("<h2>Questions & Answers</h2>");

                                // Loop through questions and answers
                                for (int i = 1; i <= 5; i++)
                                {
                                    string question = examDetailsReader[$"q{i}"].ToString();
                                    string answer = examDetailsReader[$"a{i}"].ToString();

                                    writer.WriteLine($"<p class='question'>Question {i}: {question}</p>");
                                    writer.WriteLine($"<p class='answer'>Answer {i}: {answer}</p>");
                                }

                                writer.WriteLine("</div>");
                            }
                            else
                            {
                                MessageBox.Show(@"No details found for the exam with the provided Exam ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                examDetailsReader.Close();
                                return;
                            }

                            examDetailsReader.Close();
                        }

                        writer.WriteLine("<div class='anti-forgery'>");
                        writer.WriteLine("<p>This document is generated and secured. Unauthorized alterations are prohibited.</p>");
                        writer.WriteLine("</div>");

                        writer.WriteLine("<div class='footer'>");
                        writer.WriteLine("<p>End of Report</p>");
                        writer.WriteLine("</div>");

                        writer.WriteLine("</body>");
                        writer.WriteLine("</html>");
                    }

                    MessageBox.Show(@"HTML file with exam result and details has been saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving HTML file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for form load
        private void printResult_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            // Query to get finished exams
            SqlCommand cmd = new SqlCommand("SELECT exam_name,exam_id,Student_id,finished,result FROM Exam Where finished='Yes' ;", con);
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd);
            DataTable finishedexamstable = new DataTable();
            sqlDataAdapter1.Fill(finishedexamstable);
            dataGridView2.DataSource = finishedexamstable;
            con.Close();
        }

        // Event handler for form closing
        private void printResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Event handler for exit button click
        private void Exit(object sender, EventArgs e)
        {
            if (Role == "admin")
            {
                Admin_menu admin_Menu = new Admin_menu();
                admin_Menu.Show();
                Hide();
            }
            else
            {
                Teacher_menu teacher_Menu = new Teacher_menu();
                teacher_Menu.Show();
                Hide();
            }
        }
    }
}
