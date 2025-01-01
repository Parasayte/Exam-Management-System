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
    public partial class Add_results : Form
    {
        string con = "Server=.; Database=dddd; Integrated Security=True";
        SqlDataAdapter sqlDataAdapter;
        public Add_results()
        {
            InitializeComponent();
          
        }

        private void ReadStudentsNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
           Application.Exit();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_login a = new Teacher_login();
            a.Show();
            Hide();
        }

        private void ReadStudentsNotes_Load(object sender, EventArgs e)
        {
            BrigExamsData();

        }

        private void addExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_exams addExam=new Add_exams();
            addExam.Show();
            Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_students addSt = new Add_students();
            addSt.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }
        private void BrigExamsData()
        {
            string com = "SELECT * FROM Exam1;";
            sqlDataAdapter = new SqlDataAdapter(com, con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            
        }
        private string selectedStudentId; 

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
             
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                
                selectedStudentId = selectedRow.Cells["Student_id"].Value.ToString(); 

                
            }
        }

        private void Add_student_result(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell == null)
                {
                    MessageBox.Show("Please select the result in the DataGridView.");
                    return;
                }

                // Get the selected row's Exam_id and Result
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];

                string examId = selectedRow.Cells["Exam_id"].Value?.ToString(); // Replace "Exam_id" with your actual column name
                string result = selectedRow.Cells["Result"].Value?.ToString(); // Replace "Result" with your actual column name

                if (string.IsNullOrEmpty(examId))
                {
                    MessageBox.Show("No Exam ID found in the selected row.");
                    return;
                }

                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("No result found in the selected row.");
                    return;
                }

                string updateQuery = "UPDATE Exam1 SET finished = 'T', result = @Result WHERE exam_id = @Exam_id";

                using (SqlConnection conn = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Exam_id", examId);
                        cmd.Parameters.AddWithValue("@Result", result);

                        conn.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Finished column and result updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No matching exam found to update in the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes to the database: " + ex.Message);
            }

            BrigExamsData();
        }






        private void addAnnouncementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_announcements addAnnouncements = new Add_announcements();
            addAnnouncements.Show();
            Hide();
        }

        private void gdataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                var studentId = selectedRow.Cells["Student_id"].Value; 
            }
        }

        private void Search_student_exam_paper(object sender, EventArgs e)
        {
            var searchTerm = searchtextbox.Text.ToLower();

           
            string filterExpression = $"Exam_name LIKE '%{searchTerm}%'";

         
            if (int.TryParse(searchTerm, out int parsedSearchTerm))
            {
                filterExpression += $" OR Student_id = {parsedSearchTerm} OR Exam_id = {parsedSearchTerm}";
            }

   
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = filterExpression;
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group_chat group_Chat = new Group_chat(0);
            group_Chat.Show();
            Hide();
        }
    }
}
