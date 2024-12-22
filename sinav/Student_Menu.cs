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
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace sinav
{
    public partial class Student_Menu : Form
    {
        SqlConnection con;
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        int student_id;
        public Student_Menu(int id)
        {
            InitializeComponent();
            student_id = id;
        }

        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Login a = new Student_Login();
            a.Show();
            Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Exams_Load(object sender, EventArgs e)
        {
           con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT exam_name,exam_id,finished FROM Exam1 Where finished='F' and Student_id= "+student_id, con);
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Exam1 Where finished='T' and Student_id=  "+student_id , con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd1);
            DataTable notfinishedexams = new DataTable();
            DataTable finishedexamstable = new DataTable();
            sqlDataAdapter.Fill(notfinishedexams);
            sqlDataAdapter1.Fill(finishedexamstable);
            dataGridView1.DataSource=notfinishedexams;
            dataGridView2.DataSource=finishedexamstable;
            con.Close();
        }

       

        private void Exams_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                int examid = Int32.Parse(textBox4.Text);


                char finished = 'F';

               
                string cmd = "SELECT COUNT(1) FROM Exam1 WHERE finished = @finished AND exam_id = @exam_id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand com = new SqlCommand(cmd, connection))
                {
                 
                    com.Parameters.AddWithValue("@finished", finished);
                    com.Parameters.AddWithValue("@exam_id", examid);

                    connection.Open();
                    int count = Convert.ToInt32(com.ExecuteScalar());
                    connection.Close();
                    if (count > 0)
                    {
                        
                        Student_Exam student_Menu = new Student_Exam(examid, student_id);
                        student_Menu.Show();
                        Hide();
                    }
                    else
                    {
                
                        MessageBox.Show("No Exams for you right no", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
              
                MessageBox.Show(ex.Message);
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            Student_Login a = new Student_Login();
            a.Show();
            Hide();
        }

        private void operatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveExamResultAsPDF();
        }
        private void SaveExamResultAsPDF()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Please enter an Exam ID in the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox4.Text, out int examId))
                {
                    MessageBox.Show("Invalid Exam ID. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    Title = "Save Exam Result as PDF",
                    FileName = "ExamResult.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    iTextSharp.text.Document document = new iTextSharp.text.Document();
                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    // Define custom fonts
                    iTextSharp.text.Font resultFont = iTextSharp.text.FontFactory.GetFont("CascadiaCode", 32f, iTextSharp.text.BaseColor.GREEN);
                    iTextSharp.text.Font boldFont = iTextSharp.text.FontFactory.GetFont("CascadiaCode", 12f);
                    iTextSharp.text.Font blackBoldFont = iTextSharp.text.FontFactory.GetFont("CascadiaCode", 12f, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font normalFont = iTextSharp.text.FontFactory.GetFont("CascadiaCode", 12f, iTextSharp.text.BaseColor.RED);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand resultCmd = new SqlCommand(
                            "SELECT result, finished FROM Exam1 WHERE exam_id = @exam_id",
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
                                    document.Add(new iTextSharp.text.Paragraph($"                     Result: {resultValue}", resultFont));
                                }
                                else
                                {
                                    MessageBox.Show("Result not available or invalid for this exam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    resultReader.Close();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("The exam is not finished yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                resultReader.Close();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No exam found with the provided Exam ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            resultReader.Close();
                            return;
                        }

                        resultReader.Close();

                        SqlCommand examDetailsCmd = new SqlCommand(
                            "SELECT s.name, s.nick_name, e.exam_name, e.q1, e.a1, e.q2, e.a2, e.q3, e.a3, e.q4, e.a4, e.q5, e.a5 " +
                            "FROM Exam1 e " +
                            "INNER JOIN Students s ON e.student_id = s.id " +
                            "WHERE e.exam_id = @exam_id", connection);
                        examDetailsCmd.Parameters.AddWithValue("@exam_id", examId);

                        SqlDataReader examDetailsReader = examDetailsCmd.ExecuteReader();

                        if (examDetailsReader.Read())
                        {
                            string studentName = examDetailsReader["name"].ToString();
                            string studentNickname = examDetailsReader["nick_name"].ToString();
                            string examName = examDetailsReader["exam_name"].ToString();

                            document.Add(new iTextSharp.text.Paragraph($"Student: {studentName} ({studentNickname})", blackBoldFont));
                            document.Add(new iTextSharp.text.Paragraph($"Exam Name: {examName}", blackBoldFont));
                            document.Add(new iTextSharp.text.Paragraph("\n"));

                            for (int i = 1; i <= 5; i++)
                            {
                                string question = examDetailsReader[$"q{i}"].ToString();
                                string answer = examDetailsReader[$"a{i}"].ToString();

                                document.Add(new iTextSharp.text.Paragraph($"Question {i}: {question}", boldFont));
                                document.Add(new iTextSharp.text.Paragraph($"Answer {i}: {answer}", normalFont));
                                document.Add(new iTextSharp.text.Paragraph("\n"));
                            }
                        }
                        else
                        {
                            MessageBox.Show("No details found for the exam with the provided Exam ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            examDetailsReader.Close();
                            return;
                        }

                        examDetailsReader.Close();
                    }

                    document.Close();
                    MessageBox.Show("PDF with exam result and details has been saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
