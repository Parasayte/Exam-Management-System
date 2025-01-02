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
namespace Exam_management_system
{
    public partial class Add_announcements : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        DateTime date ;
        public Add_announcements()
        {
            InitializeComponent();
        }

        private void Add_announcement(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string announcement = richTextBox1.Text.Trim();
                try
                {
                    sqlConnection.Open();

                    string assignExamQuery = "INSERT INTO announcements1 (announcement, Date) VALUES (@ann, @date)";
                    SqlCommand assignExamCommand = new SqlCommand(assignExamQuery, sqlConnection);
                    assignExamCommand.Parameters.AddWithValue("@ann", announcement);
                    assignExamCommand.Parameters.AddWithValue("@date", DateTime.Now); 

                    assignExamCommand.ExecuteNonQuery();

                    MessageBox.Show("Announcement added successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            richTextBox1.Clear();
            ShowDataTable();
        }


        private void ShowDataTable()
        {
            
            string com = "SELECT * FROM announcements1 ;";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(com, connectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void Add_announcement_Load(object sender, EventArgs e)
        {
            ShowDataTable();
        }

        private void Delete_annoucement(object sender, EventArgs e)
        {
            int announcementId = Convert.ToInt32(textBox1.Text.Trim());

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    
                    string deleteQuery = "DELETE FROM announcements1 WHERE announcements_id = @id";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                    deleteCommand.Parameters.AddWithValue("@id", announcementId);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Announcement deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No announcement found with the provided ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            ShowDataTable();
        }
        private void UpdateAnnouncement(int announcementId, string newAnnouncement)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string updateQuery = "UPDATE announcements1 SET announcement = @newAnnouncement, Date = @date WHERE announcements_id = @id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                    updateCommand.Parameters.AddWithValue("@id", announcementId);
                    updateCommand.Parameters.AddWithValue("@newAnnouncement", newAnnouncement);
                    updateCommand.Parameters.AddWithValue("@date", DateTime.Now); // Update with the current date and time

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Announcement updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No announcement found with the provided ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            ShowDataTable();
        }


        private void Update_announcement(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int announcementId))
            {
                string newAnnouncement = richTextBox1.Text.Trim();
                if (!string.IsNullOrEmpty(newAnnouncement))
                {
                    UpdateAnnouncement(announcementId, newAnnouncement);
                }
                else
                {
                    MessageBox.Show("Please enter a new announcement.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid announcement ID.");
            }
        }

        private void Search_announcement(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int announcementId))
            {
                string announcement = GetAnnouncementById(announcementId);

                richTextBox1.Text = announcement;
            }
            else
            {
                richTextBox1.Clear();
            }
        }
        private string GetAnnouncementById(int announcementId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string fetchQuery = "SELECT announcement, Date FROM announcements1 WHERE announcements_id = @id";
                    SqlCommand fetchCommand = new SqlCommand(fetchQuery, sqlConnection);
                    fetchCommand.Parameters.AddWithValue("@id", announcementId);

                    using (SqlDataReader reader = fetchCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string announcement = reader["announcement"].ToString();
                            string date = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd HH:mm:ss");
                            return $"{announcement} (Last updated: {date})";
                        }
                        else
                        {
                          
                            return string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return string.Empty;
                }
            }
        }

        private void addExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_exams addExam = new Add_exams();
            addExam.Show();
            Hide();
        }

        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_results readStudentsNotes = new Add_results();
            readStudentsNotes.Show();
            Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_students addSt = new Add_students();
            addSt.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void Add_announcement_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Delete_time_passed_annoucement(object sender, EventArgs e)
        {
            date = DateTime.Now;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand($"Delete  FROM announcements1 WHERE Date <'{date}';", sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("If there are Old announcements, will be deleteded.");
            sqlConnection.Close();
            ShowDataTable();

        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Group_chat group_Chat = new Group_chat(0);
            group_Chat.Show();
            Hide();
        }
    }
}
    