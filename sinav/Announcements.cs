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

    public partial class Announcements : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        int studentId;
        public Announcements(int id)
        {
            InitializeComponent();
            studentId = id;
        }

        private void Announcements_Load(object sender, EventArgs e)
        {
            ShowDataTable();

        }
        private void ShowDataTable()
        {

            string com = "SELECT announcement, Date FROM announcements1 ;";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(com, connectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDataTable();
        }

        private void Announcements_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }

        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Menu student_Menu = new Student_Menu(studentId);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Menu student_Menu = new Student_Menu(studentId);
            student_Menu.Show();
            Hide();
        }

  

       
    }
}
