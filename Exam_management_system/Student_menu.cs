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
    public partial class Student_menu : Form
    {
        SqlConnection con;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";
        int student_id;
        DateTime nowtime;

        // Constructor
        public Student_menu(int id)
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
            SqlCommand cmd = new SqlCommand($"SELECT exam_name,exam_id,finished,Time,lastdate FROM Exam Where finished='F' and Student_id={student_id} AND lastDate > '{nowtime}'", con);
            // Query for finished exams
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Exam Where finished='T' and Student_id=  " + student_id, con);

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
                char finished = 'F';
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
                        Students_exam student_Menu = new Students_exam(examid, student_id);
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
            body {{ font-family: 'Inter', sans-serif; background-color: #1a1a1a; color: #dcdcdc; margin: 0; padding: 20px; line-height: 1.6; }}
            h1 {{ color: #ffffff; text-align: center; font-size: 2.5em; margin: 30px 0; font-weight: 700; text-shadow: 0 0 10px white; }}
            .container {{ max-width: 800px; margin: 0 auto; padding: 20px; }}
            .section {{ margin: 20px 0; padding: 25px; border: 1px solid #2e2e2e; border-radius: 12px; background-color: #252525; box-shadow: 0 6px 12px rgba(255, 255, 255, 0.3); }}
            .section h2 {{ font-size: 1.5em; color: #f0f0f0; margin-bottom: 15px; }}
            .question {{ font-size: 1.1em; font-family: 'Cascadia Code', monospace; color: #bbbbbb; margin-bottom: 10px; }}
            .answer {{ font-size: 1em; font-family: 'Cascadia Code', monospace; color: #ffffff; margin-left: 20px; }}
            .result {{ font-family: 'Roboto Mono', monospace; font-weight: bold; color: #2ecc71; font-size: 1.25em; margin-top: 20px; text-shadow: 0 0 10px white; }}
            .anti-forgery {{ font-size: 0.9em; color: #888888; text-align: center; margin-top: 40px; }}
            .footer {{ text-align: center; font-size: 0.85em; color: #888888; margin-top: 60px; }}
            button {{ background-color: #3b3b3b; color: #ffffff; border: none; padding: 10px 20px; font-size: 1em; border-radius: 5px; cursor: pointer; transition: background-color 0.3s ease; }}
            button:hover {{ background-color: #4c4c4c; }}
            a {{ color: #00aaff; text-decoration: none; transition: color 0.3s ease; }}
            a:hover {{ color: #66cfff; }}
        </style>
    </head>

    </html>
    ";

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
                                string tex = $" <h1>Exam Result</h1>\r\n    <div class='section'>\r\n        <p><strong>Date:</strong> {currentDate}</p>\r\n        <p><strong>Result:</strong> <span class='result'>{resultValue}</span></p>\r\n        <p><strong>Student Name:</strong> {studentName} {studentNickname}</p>\r\n        <p><strong>Exam Name:</strong> {examName}</p>\r\n    </div>";
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
    }
}
