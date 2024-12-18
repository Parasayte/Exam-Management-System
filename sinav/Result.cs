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
    public partial class Result : Form
    {
        SqlConnection con;
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        public Result()
        {
            InitializeComponent();
        }

        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exams a = new Exams();
            a.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Login a =new Student_Login();
            a.Show();
            Hide();
        }

        private void Result_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT exam_name,exam_id,finished,q1,a1,q2,a2,q3,a3,q4,a4,q5,a5,result FROM Exams  ", con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Result_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

        }
    }
}
