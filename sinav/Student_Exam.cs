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

namespace sinav
{
    public partial class Student_Exam : Form
    {
        int Studenid;
        int examid;
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Student_Exam(int examId,int studentId)
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
            Student_Menu a =new Student_Menu(0);
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
                        if (reader.Read()) // Reading the first row
                        {
                            for (int i = 0; i < labels.Length; i++)
                            {
                                labels[i].Text = reader[i].ToString(); // Assign each question to the corresponding label
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error or show a message to the user)
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void FillAnswers()
        {

            string a1 = richTextBox1.Text
            , a2 = richTextBox2.Text
            , a3 = richTextBox3.Text
            , a4 = richTextBox4.Text
            , a5 = richTextBox5.Text;



            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();


               
                string comnd = "UPDATE Exam1 SET finished=@f, a1 = @a1, a2 = @a2, a3 = @a3, a4 = @a4, a5 = @a5 WHERE student_id = "+Studenid+" AND exam_id = "+examid+";";
                using (SqlCommand cmd = new SqlCommand(comnd, con))
                {

                    cmd.Parameters.AddWithValue("@a1", a1);
                    cmd.Parameters.AddWithValue("@a2", a2);
                    cmd.Parameters.AddWithValue("@a3", a3);
                    cmd.Parameters.AddWithValue("@a4", a4);
                    cmd.Parameters.AddWithValue("@a5", a5);
                    cmd.Parameters.AddWithValue("@f", "T");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("inserted successfully.");
                }
                con.Close();
                Student_Menu exams = new Student_Menu(Studenid);
                exams.Show();
                Hide();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Login a =new Student_Login();
            a.Show();
            Hide();
        }

        private void Student_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillAnswers();
        }
    }
}
