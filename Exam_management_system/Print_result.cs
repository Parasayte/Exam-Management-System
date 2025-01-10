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
using System.Windows.Forms.DataVisualization.Charting;
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
            <p>This document is generated and secured. Unauthorized alterations are prohibited.</p>
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
        private void chartview()
        {
            // Assuming schoolManagementSystemDataSet.Exam is a DataTable
            chart1.DataSource = schoolManagementSystemDataSet.Exam;

            // Set the X and Y value members to the appropriate column names
            chart1.Series["Result"].AxisLabel = "student_id";  // Assuming "exam_id" is the X-axis
            chart1.Series["Result"].YValueMembers = "result";  // Assuming "result" is the Y-axis

            // Set the chart type (e.g., Column, Line, Bar, etc.)
            chart1.Series["Result"].ChartType = SeriesChartType.Column;

            // Bind the data to the chart
            chart1.DataBind();
        }

        // Event handler for form load
        private void printResult_Load(object sender, EventArgs e)
        {
         this.examTableAdapter.Fill(this.schoolManagementSystemDataSet.Exam);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            // Query to get finished exams
            SqlCommand cmd = new SqlCommand("SELECT exam_name,exam_id,Student_id,finished,result FROM Exam Where finished='Yes' ;", con);
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd);
            DataTable finishedexamstable = new DataTable();
            sqlDataAdapter1.Fill(finishedexamstable);
            dataGridView2.DataSource = finishedexamstable;
           chartview(); 
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

        private void chart1_Click(object sender, EventArgs e)
        {
        }

      

    }
}
