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
    }
}
