using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Exam_management_system.Properties;

namespace Exam_management_system
{
    public partial class Announcements_list : Form
    {
        // Connection string to the database
        string connectionString = "Server=.; Database=SchoolManagementSystem; Integrated Security=True;";
        int studentId;

        FlowLayoutPanel flowLayoutPanel;

        public Announcements_list(int id)
        {
            InitializeComponent();
            studentId = id;
            // Initialize and configure the FlowLayoutPanel
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
            // Load and display announcements when the form loads
            Print_announcements();
        }

        private void Print_announcements()
        {
            flowLayoutPanel.Controls.Clear();
            string query = "SELECT announcement, Date FROM Announcements;";
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

                        // Create a panel for each announcement
                        Panel messagePanel = new Panel
                        {
                            AutoSize = true,
                            BackColor = Color.Transparent,
                            Margin = new Padding(5),
                            Padding = new Padding(5),
                            BorderStyle = BorderStyle.None,
                            MaximumSize = new Size(flowLayoutPanel.Width - 40, 0)
                        };

                        // Create a label for the image
                        Label imageLabel = new Label
                        {
                            Size = new Size(40, 40),
                            Image = Resources.Hopstarter_Sleek_Xp_Basic_Chat_32,
                            BackColor = Color.Transparent,
                            Margin = new Padding(5)
                        };

                        // Create a label for the announcement text
                        Label messageBubble = new Label
                        {
                            Text = $"{announcement}\n\n\n\nDate: {date}",
                            AutoSize = true,
                            BackColor = Color.FromArgb(45, 45, 48),
                            ForeColor = Color.YellowGreen,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(10),
                            Margin = new Padding(5),
                            Font = new Font("Arial", 12, FontStyle.Bold),
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
            // Refresh the announcements list
            Print_announcements();
        }

        private void Announcements_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Exit the application when the form is closing
            Application.Exit();
        }

        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Navigate to the student menu
            Student_menu student_Menu = new Student_menu(studentId);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(studentId == -1)
            {
                // Log out and show the Admin menu
                Admin_menu admin_Menu = new Admin_menu();
                admin_Menu.Show();
                Hide();
            }
            else
            {
                // Log out and show the student menu
                Student_login student_Login = new Student_login();
                student_Login.Show();
            }
          
      
        }
    }
}
