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
    public partial class Exams : Form
    {
        SqlConnection con;
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        int student_id;
        public Exams(int id)
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student_Login a = new Student_Login();
            a.Show();
            Hide();
        }
    }
}
