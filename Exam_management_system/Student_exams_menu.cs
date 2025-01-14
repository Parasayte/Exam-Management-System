﻿using System;
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
    public partial class Student_exams_menu : Form
    {
        SqlConnection con;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";
        int student_id;
        DateTime nowtime;

        // Constructor
        public Student_exams_menu(int id)
        {
            InitializeComponent();
            student_id = id;
        }

        // Log out and show login form
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        // Load exams data
        private void Exams_Load(object sender, EventArgs e)
        {
            nowtime = DateTime.Now;
            con = new SqlConnection(connectionString);
            con.Open();

            // Query for not finished exams
            SqlCommand cmd = new SqlCommand($"SELECT exam_name,exam_id,finished,Time,lastdate FROM Exam Where finished='No' and Student_id={student_id} AND lastDate > '{nowtime}'", con);
            // Query for finished exams
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Exam Where finished='Yes' and Student_id=  " + student_id, con);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd1);

            DataTable notfinishedexams = new DataTable();
            DataTable finishedexamstable = new DataTable();

            sqlDataAdapter.Fill(notfinishedexams);
            sqlDataAdapter1.Fill(finishedexamstable);

            dataGridView1.DataSource = notfinishedexams;
            dataGridView2.DataSource = finishedexamstable;

            con.Close();
        }

        // Handle form closing event
        private void Exams_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Enter selected exam
        private void Enter_selected_exam(object sender, EventArgs e)
        {
            try
            {
                int examid = Int32.Parse(textBox4.Text);
                string finished ="No";
                DateTime currentDate = DateTime.Now;

                string cmd = @"
                    SELECT COUNT(1) 
                    FROM Exam 
                    WHERE finished = @finished 
                      AND exam_id = @exam_id 
                      AND lastDate > @currentDate";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand com = new SqlCommand(cmd, connection))
                {
                    com.Parameters.AddWithValue("@finished", finished);
                    com.Parameters.AddWithValue("@exam_id", examid);
                    com.Parameters.AddWithValue("@currentDate", currentDate);

                    connection.Open();
                    int count = Convert.ToInt32(com.ExecuteScalar());
                    connection.Close();

                    if (count > 0)
                    {
                        Students_exam_paper student_Menu = new Students_exam_paper(examid, student_id);
                        student_Menu.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show(@"No exams available for you right now or the date has passed.",
                                        "Empty",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Log out button click event
        private void button2_Click(object sender, EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        // Save exam result as HTML
        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveExamResultAsHtml();
        }

        // Method to save exam result as HTML
        private void SaveExamResultAsHtml()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show(@"Please enter an Exam ID in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox4.Text, out int examId))
                {
                    MessageBox.Show(@"Invalid Exam ID. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "HTML Files (*.html)|*.html",
                    Title = "Save Exam Result as HTML",
                    FileName = "ExamResult.html"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        string currentDate = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                        string htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        /* Genel Stil */
        body {{
            font-family: 'Cascadia Mono', monospace;
            background-color: #f5f5f5;
            color: #333;
            margin: 0;
            padding: 40px;
            line-height: 1.8;
        }}

        /* Başlık */
        h1 {{
            color: #1a1a1a;
            text-align: center;
            font-size: 2.5em;
            margin: 30px 0;
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 2px;
            border-bottom: 3px solid #1a1a1a;
            padding-bottom: 15px;
        }}

        /* Konteyner */
        .container {{
            max-width: 800px;
            margin: 0 auto;
            padding: 40px;
            background-color: #ffffff;
            border: 1px solid #e0e0e0;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }}

        /* Bölümler */
        .section {{
            margin: 30px 0;
            padding: 25px;
            border: 1px solid #e0e0e0;
            background-color: #fafafa;
            border-radius: 8px;
        }}

        .section h2 {{
            font-size: 1.6em;
            color: #1a1a1a;
            margin-bottom: 20px;
            border-bottom: 2px solid #e0e0e0;
            padding-bottom: 10px;
            font-weight: 600;
        }}

        /* Soru ve Cevaplar */
        .question {{
            font-size: 1.1em;
            color: #444;
            margin-bottom: 12px;
            font-weight: bold;
        }}

        .answer {{
            font-size: 1em;
            color: #555;
            margin-left: 25px;
            margin-bottom: 18px;
            line-height: 1.6;
        }}

        /* Sonuç */
        .result {{
            font-size: 1.3em;
            color: #d9534f; /* Kırmızı renk */
            font-weight: bold;
            margin-top: 25px;
            text-align: center;
        }}

        /* Alt Bilgi */
        .footer {{
            text-align: center;
            font-size: 0.9em;
            color: #777;
            margin-top: 50px;
            border-top: 1px solid #e0e0e0;
            padding-top: 20px;
        }}

        /* Anti-Forgery Notu */
        .anti-forgery {{
            font-size: 0.85em;
            color: #888;
            text-align: center;
            margin-top: 40px;
            font-style: italic;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Exam Result</h1>";

                        writer.WriteLine(htmlContent);

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
                                        // Valid result
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
                                string tex = $@"
        <div class='section'>
            <p><strong>Date:</strong> {currentDate}</p>
            <p><strong>Result:</strong> <span class='result'>{resultValue}</span></p>
            <p><strong>Student Name:</strong> {studentName} {studentNickname}</p>
            <p><strong>Exam Name:</strong> {examName}</p>
        </div>";
                                writer.WriteLine(tex);

                                writer.WriteLine("<div class='section'>");
                                writer.WriteLine("<h2>Questions & Answers</h2>");

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

                        writer.WriteLine(@"
        <div class='anti-forgery'>
            <p></p>
        </div>

        <div class='footer'>
            <p>End of Report</p>
        </div>
    </div>
</body>
</html>");
                    }

                    MessageBox.Show(@"HTML file with exam result and details has been saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving HTML file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Open notes directory
        private void notesToolStripMenuItem_Click(object sender, EventArgs e)
        {  // Use a user-friendly directory in the user's profile, such as MyDocuments
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Students Notes", $"{student_id}");

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
                // Try creating the directory in a safe location (MyDocuments folder)
                try
                {
                    // Ensure the parent directory exists before trying to create a subdirectory
                    string parentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Students Notes",$"{student_id}");
                    if (!Directory.Exists(parentDirectory))
                    {
                        Directory.CreateDirectory(parentDirectory);
                    }

                    // Now create the target directory
                    Directory.CreateDirectory(path);

                    Directories_menu a = new Directories_menu(path);
                    a.Show();
                    Hide();
                }
                catch (UnauthorizedAccessException)
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

        // Open pomodoro timer
        private void pomodoroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Time_controler_app pomodoro = new Time_controler_app(student_id);
            pomodoro.Show();
            Hide();
        }

        // Open messages
        private void messagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Announcements_list a = new Announcements_list(student_id);
            a.Show();
            Hide();
        }

        // Open group chat
        private void groupChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(student_id);
            group_Chat.Show();
            Hide();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_menu a = new Student_menu(student_id);
            a.Show();
            Hide();
        }
    }
}
