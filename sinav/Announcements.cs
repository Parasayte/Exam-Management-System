using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using sinav.Properties;

namespace sinav
{
    public partial class Announcements : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
        int studentId;

        FlowLayoutPanel flowLayoutPanel;

        public Announcements(int id)
        {
            InitializeComponent();
            studentId = id;
            flowLayoutPanel = new FlowLayoutPanel
            {
                AutoScroll = true,
                AutoScrollMargin = new Size(10, 10),
                Location = new Point(20, 20),
                Size = new Size(Width - 40, Height - 60),
                Padding = new Padding(10),
                BackColor = Color.FromArgb(35, 30, 30),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };


            Controls.Add(flowLayoutPanel);
        }

        private void Announcements_Load(object sender, EventArgs e)
        {
            ShowAnnouncementsAsMessages();
        }

        private void ShowAnnouncementsAsMessages()
        {
            flowLayoutPanel.Controls.Clear();
            string query = "SELECT announcement, Date FROM announcements1;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string announcement = reader["announcement"].ToString();
                        string date = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd");

                        Panel messagePanel = new Panel
                        {
                            AutoSize = true,
                            BackColor = Color.Transparent,
                            Margin = new Padding(5),
                            Padding = new Padding(5),
                            BorderStyle = BorderStyle.None,
                            MaximumSize = new Size(flowLayoutPanel.Width - 40, 0)
                        };

                        Label imageLabel = new Label
                        {
                            Size = new Size(40, 40),
                            Image = Resources.Hopstarter_Sleek_Xp_Basic_Chat_32,
                            BackColor = Color.Transparent,
                            Margin = new Padding(5)
                        };

                        Label messageBubble = new Label
                        {
                            Text = $"{announcement}\n\n\n\nDate: {date}",
                            AutoSize = true,
                            BackColor = Color.FromArgb(45, 45, 48), 
                            ForeColor = Color.White,              
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(10),
                            Margin = new Padding(5),
                            MaximumSize = new Size(flowLayoutPanel.Width - 100, 0)
                        };

                        messagePanel.Controls.Add(imageLabel);
                        messagePanel.Controls.Add(messageBubble);

                        imageLabel.Location = new Point(5, 5);
                        messageBubble.Location = new Point(imageLabel.Width + 10, 5);

                        flowLayoutPanel.Controls.Add(messagePanel);
                    }
                }
            }
        }


        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAnnouncementsAsMessages();
        }

        private void Announcements_FormClosing(object sender, FormClosingEventArgs e)
        {
           Application.Exit();
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
