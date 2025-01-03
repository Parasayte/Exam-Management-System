using System;
using System.Drawing;
using System.Media;
using System.Reflection.Emit;
using System.Windows.Forms;
using Exam_management_system.Properties;

namespace Exam_management_system
{
    public partial class Time_controler_app : Form
    {
        int min; // Minutes
        int sec; // Seconds
        bool isPause; // Pause state
        bool isBreak; // Break state
        int rounds = 1; // Number of rounds
        int student_id; // Student ID
        Mini_game a = new Mini_game(); // Mini game instance

        public Time_controler_app(int id)
        {
            InitializeComponent();
            student_id = id;
        }

        // Start the timer
        private void StartTimer()
        {
            timer1.Start();
        }

        // Update the screen with the current time
        private void UpdateScreen()
        {
            progressBar2.Value = sec;
            progressBar1.Value = min * 60 + sec;
            labelmin.Text = min.ToString("00");
            labelsec.Text = sec.ToString("00");
        }

        // Start the Pomodoro timer
        private void Start_pomodoro(object sender, EventArgs e)
        {
            if (isBreak)
            {
                min = 5; // Short break
            }
            else
            {
                min = 25; // Pomodoro session
                HideButton();
            }

            sec = 0;
            progressBar1.Maximum = min * 60;
            progressBar2.Value = sec;

            StartTimer();
        }

        // Timer tick event handler
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (sec == 0 && min > 0)
            {
                min--;
                sec = 59;
            }
            else if (sec > 0)
            {
                sec--;
            }

            UpdateScreen();

            if (min == 0 && sec == 0)
            {
                isBreak = !isBreak;

                if (isBreak)
                {
                    label1.Enabled = false;

                    if (rounds % 4 == 0 && rounds != 0)
                    {
                        min = 15; // Long break
                        ShowButton();
                        label5.Text = "Long Break";
                    }
                    else
                    {
                        ShowButton();
                        min = 5; // Short break
                        label5.Text = "Break";
                    }

                    sec = 0;
                    progressBar1.Maximum = min * 60;
                    label2.Text = rounds.ToString();
                }
                else
                {
                    a.Hide();
                    HideButton();
                    label1.Enabled = true;
                    label5.Text = "Pomodoro";
                    timer1.Stop();
                    rounds++;
                }
            }
        }

        // Stop or pause the timer
        private void Stop_Pause_time(object sender, EventArgs e)
        {
            if (!isPause)
            {
                a.Hide();
                HideButton();
                label3.Image = Exam_management_system.Properties.Resources.Hopstarter_Button_Button_Pause_72__1_;
                timer1.Stop();
                isPause = true;
            }
            else
            {
                ShowButton();
                label3.Image = Exam_management_system.Properties.Resources.Hopstarter_Button_Button_Play_72;
                timer1.Start();
                isPause = false;
            }
        }

        private void Pomodoro_Load(object sender, EventArgs e)
        {
            // Load event handler
        }

        // Show the button
        private void ShowButton()
        {
            button2.Visible = true;
            button2.Enabled = true;
        }

        // Hide the button
        private void HideButton()
        {
            button2.Visible = false;
            button2.Enabled = false;
        }

        // Timer tick event handler for updating the current time
        private void timer2_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString("HH:mm");
        }

        // Form closing event handler
        private void Pomodoro_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Open the login page
        private void Open_login_page(object sender, EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        // Go back to the previous menu
        private void Go_back(object sender, EventArgs e)
        {
            if (student_id == -1)
            {
                Student_login student = new Student_login();
                student.Show();
                Hide();
            }
            else
            {
                Student_menu a = new Student_menu(student_id);
                a.Show();
                Hide();
            }
        }

        // Open the mini game
        private void Open_minigame(object sender, EventArgs e)
        {
            a.Show();
        }
    }
}
