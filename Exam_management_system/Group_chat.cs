﻿using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Exam_management_system.Properties;

namespace Exam_management_system
{
    public partial class Group_chat : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";
        int studentId;

        FlowLayoutPanel flowLayoutPanel;
        Label studentNameLabel;
        RichTextBox richTextBox1;
        Timer timer2;

        public Group_chat(int id) // Removed the `role` parameter
        {
            InitializeComponent();
            studentId = id;

            // Initialize and start timer
          

            // Initialize student name label
            studentNameLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                BackColor = Color.Transparent
            };
            Controls.Add(studentNameLabel);

            // Initialize flow layout panel for messages
            flowLayoutPanel = new FlowLayoutPanel
            {
                AutoScroll = true,
                Location = new Point(0, 50),
                Size = new Size(Width - 15, Height - 150),
                BackColor = Color.FromArgb(35, 30, 30),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };
            Controls.Add(flowLayoutPanel);

            // Initialize rich text box for message input
            richTextBox1 = new RichTextBox
            {
                Location = new Point(10, Height - 90),
                Size = new Size(Width - 120, 40),
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.Silver,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            Controls.Add(richTextBox1);

            // Initialize send label
            Label sendLabel = new Label
            {
                AutoSize = true,
                Image = Resources.Custom_Icon_Design_Pretty_Office_9_Email_send_32,
                Size = new Size(120, 120),
                BackColor = Color.FromArgb(35, 30, 30),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(20),
                Location = new Point(Width - 100, Height - 96),
                TextAlign = ContentAlignment.MiddleCenter
            };
            sendLabel.Click += Send_Message;
            Controls.Add(sendLabel);
        }

        // Show student name
        private void ShowUserName()
        {
            if (studentId == -1)
            {
                studentNameLabel.Text = "Admin";
            }
            else if (studentId == 0)
            {
                studentNameLabel.Text = "Teacher";
            }
            else
            {
                string query = "SELECT Name FROM Students WHERE student_Id = @Id;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", studentId);
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        studentNameLabel.Text = result?.ToString() ?? "Student";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading student name: " + ex.Message);
                    }
                }
            }
        }

        // Print group messages
        private void Print_group_messages()
        {
            Point scrollPosition = flowLayoutPanel.AutoScrollPosition;

            flowLayoutPanel.SuspendLayout();
            flowLayoutPanel.Controls.Clear();

            string query = @"
            SELECT Chat.Message, Chat.Date, Chat.Time, Chat.Student_id, Students.Name 
            FROM Chat 
            LEFT JOIN Students ON Chat.Student_id = Students.student_Id;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string announcement = reader["Message"].ToString();
                            int senderId = Convert.ToInt32(reader["Student_id"]);
                            string studentName = "";

                            if (senderId == -1)
                            {
                                studentName = "Admin";
                            }
                            else if (senderId == 0)
                            {
                                studentName = "Teacher";
                            }
                            else
                            {
                                studentName = reader["Name"] != DBNull.Value
                                    ? reader["Name"].ToString()
                                    : "Student";
                            }

                            string date = reader["Date"] != DBNull.Value
                                ? Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd")
                                : "No Date";
                            string time = reader["Time"] != DBNull.Value
                                ? TimeSpan.Parse(reader["Time"].ToString()).ToString(@"hh\:mm\:ss")
                                : "No Time";

                            bool isCurrentStudent = senderId == studentId;

                            Panel messagePanel = new Panel
                            {
                                AutoSize = true,
                                BackColor = Color.Transparent,
                                Margin = new Padding(5)
                            };

                            PictureBox pictureBox = new PictureBox
                            {
                                Size = new Size(30, 30),
                                SizeMode = PictureBoxSizeMode.Zoom,
                                Location = new Point(5, 5),
                                Image = senderId == -1
                                    ? Resources.Hopstarter_Scrap_Administrator_32
                                    : senderId == 0
                                        ? Resources.Hopstarter_Sleek_Xp_Basic_Office_Girl_32
                                        : Resources.Hopstarter_Sleek_Xp_Basic_Chat_32
                            };
                            messagePanel.Controls.Add(pictureBox);

                            Label messageBubble = new Label
                            {
                                Text = $" {studentName}\n\n****************************************************\n\n\n {announcement}\n\n\n****************************************************\n\nDate: {date}\nTime: {time}",
                                AutoSize = true,
                                Font = new Font("Arial", 9, FontStyle.Bold),
                                BackColor = Color.FromArgb(45, 45, 48),
                                ForeColor = senderId == -1
                                    ? Color.Firebrick
                                    : senderId == 0
                                        ? Color.Olive
                                        : (isCurrentStudent ? Color.LightGreen : Color.LightGreen),
                                BorderStyle = BorderStyle.FixedSingle,
                                Padding = new Padding(10),
                                MaximumSize = new Size(flowLayoutPanel.Width - 80, 0),
                                Location = new Point(40, 5),
                            };

                            if (isCurrentStudent)
                            {
                                messageBubble.BackColor = Color.FromArgb(36, 0, 8);
                                messageBubble.Location = new Point(530, 5);
                                pictureBox.Location = new Point(845, 5);
                                messageBubble.TextAlign = ContentAlignment.MiddleRight;
                            }

                            messagePanel.Controls.Add(messageBubble);
                            flowLayoutPanel.Controls.Add(messagePanel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading messages: " + ex.Message);
                }
            }

            flowLayoutPanel.ResumeLayout();
            flowLayoutPanel.AutoScrollPosition = new Point(0, flowLayoutPanel.VerticalScroll.Maximum);
        }

        // Send message
        private void Send_Message(object sender, EventArgs e)
        {
            string message = richTextBox1.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                string query = "INSERT INTO Chat (Message, Student_id, Date, Time) VALUES (@Message, @StudentId, @Date, @Time);";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Message", message);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@Time", currentTime);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        richTextBox1.Clear();
                        Print_group_messages();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending message: " + ex.Message);
                    }
                }
            }
        }

        // Load group chat
        private void Group_chat_Load(object sender, EventArgs e)
        {
            ShowUserName();
            Print_group_messages();
        }

        // Timer tick event
        private void timer1_Tick(object sender, EventArgs e)
        {
            Print_group_messages();
        }

        // Log out
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (studentId == -1)
            {
                Admin_menu adminMenu = new Admin_menu();
                adminMenu.Show();
                Hide();
            }
            else if (studentId == 0)
            {
               Teacher_menu teacher_Menu = new Teacher_menu();
                teacher_Menu.Show();
                Hide();
            }
            else
            {
                Student_menu studentMenu = new Student_menu(studentId);
                studentMenu.Show();
                Hide();
            }
        }

        // Form closing event
        private void Group_chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Refresh messages
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print_group_messages();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Handle menu item click
        }
    }
}