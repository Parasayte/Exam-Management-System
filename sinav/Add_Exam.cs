﻿using System;
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
    public partial class Add_Exam : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        public Add_Exam()
        {
            InitializeComponent();
        }

        private void addquestions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

      

        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Read_Answers readStudentsNotes = new Read_Answers();
            readStudentsNotes.Show();
            Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Student addSt = new Add_Student();
            addSt.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_Login teacher_Login = new Teacher_Login();
            teacher_Login.Show();
            Hide();
        }
        private void ClearTextbox()
        {
           richTextBox3.Text = null;
          
            richTextBox2.Text = null;
            richTextBox4.Text = null;
            richTextBox5.Text = null;
            richTextBox6.Text = null;
            richTextBox7.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime lastdate=dateTimePicker1.Value;
            DateTime now = DateTime.Now;
            if (lastdate < now)
            {
               MessageBox.Show("Last date must be greater than now","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            int time = Int32.Parse(numericUpDown1.Value.ToString());
            if (time < 1)
            {

                MessageBox.Show("Time must be greater than 1 minutes","Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string q1 = richTextBox2.Text.Trim();
                string q2 = richTextBox3.Text.Trim();
                string q3 = richTextBox6.Text.Trim();
                string q4 = richTextBox4.Text.Trim();
                string q5 = richTextBox5.Text.Trim();
                string name = richTextBox7.Text.Trim();
            


                try
                {
                    sqlConnection.Open();

                    string assignExamQuery = @"
                INSERT INTO Exam1 (q1, q2, q3, q4, q5, exam_name, finished, Student_id,Time,lastDate)
                SELECT @q1, @q2, @q3, @q4, @q5, @exam_name, @finished, id ,@Time,@lastDate
                FROM Students;";

                    SqlCommand assignExamCommand = new SqlCommand(assignExamQuery, sqlConnection);
                    assignExamCommand.Parameters.AddWithValue("@q1", q1);
                    assignExamCommand.Parameters.AddWithValue("@q2", q2);
                    assignExamCommand.Parameters.AddWithValue("@q3", q3);
                    assignExamCommand.Parameters.AddWithValue("@q4", q4);
                    assignExamCommand.Parameters.AddWithValue("@q5", q5);
                    assignExamCommand.Parameters.AddWithValue("@exam_name", name);
                    assignExamCommand.Parameters.AddWithValue("@finished", "F");
                    assignExamCommand.Parameters.AddWithValue("@Time", time);
                    assignExamCommand.Parameters.AddWithValue("@lastDate", lastdate);
                    assignExamCommand.ExecuteNonQuery();

                    MessageBox.Show("Exam its for all students");
                    ClearTextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            BrigExamsData();
        }






        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddExam_Load(object sender, EventArgs e)
        {
            BrigExamsData();
        }
        private void BrigExamsData()
        {
            string com = "SELECT * FROM Exam1 ;";
        SqlDataAdapter  sqlDataAdapter = new SqlDataAdapter(com, connectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string examName = richTextBox7.Text;

               

                string com = "DELETE FROM Exam1 WHERE exam_name = @exam_name";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(com, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@exam_name", examName);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                MessageBox.Show("Exam deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }

            BrigExamsData();
            ClearTextbox();
        }

   

        private void addAnnouncementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_announcement addAnnouncements = new Add_announcement();
            addAnnouncements.Show();
            Hide();
        }
    }
};