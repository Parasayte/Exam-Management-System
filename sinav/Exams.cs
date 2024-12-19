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
        public Exams()
        {
            InitializeComponent();
        }



















        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Result a=new Result();
            a.Show();
            Hide();
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
            SqlCommand cmd = new SqlCommand("SELECT exam_name,exam_id,finished FROM Exams  ", con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource=dt;
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
