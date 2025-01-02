using System;
using System.Drawing;
using System.Media;
using System.Reflection.Emit;
using System.Windows.Forms;
using sinav.Properties;
using sinav.Properties;

namespace sinav
{
    public partial class Time_controler_app : Form
    {
        int min;
        int sec ;
        bool isPause ;
        bool isBreak ;
        int rounds=1;
        int student_id;
        Mini_game a = new Mini_game();
        public Time_controler_app(int id)
        {
            InitializeComponent();
            student_id = id;
        }

        private void StartTimer()
        {
            timer1.Start();
        }

      
        private void UpdateScreen()
        {
            progressBar2.Value = sec;

            progressBar1.Value = min * 60 + sec;
            labelmin.Text = min.ToString("00");
            labelsec.Text = sec.ToString("00");
        }

     

        private void Start_pomodoro(object sender, EventArgs e)
        {

            if (isBreak)
            {
                min = 5;
              
            }
            else
            {
                min = 25;
                HideButton();
            }

            sec = 0;
            progressBar1.Maximum = min * 60;
            progressBar2.Value = sec;

            StartTimer();
        }

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
                        min = 15;
                        ShowButton();
                        label5.Text = "Long Break";
                    }
                    else
                    {
                        ShowButton();
                        min = 5;
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

        private void Stop_Pause_time(object sender, EventArgs e)
        {
            if (!isPause)
            {
                a.Hide();
                HideButton();
                label3.Image =sinav.Properties.Resources.Hopstarter_Button_Button_Pause_72__1_;
                timer1.Stop();
                isPause = true;
            }
            else
            {
                ShowButton();
                label3.Image = Resources.Hopstarter_Button_Button_Play_72;
                timer1.Start();
                isPause = false;
            }
        }

        private void Pomodoro_Load(object sender, EventArgs e)
        {

        }
        private void ShowButton()
        {
           button2.Visible = true;
            button2.Enabled = true;
        }
        private void HideButton()
        {
            button2.Visible = false;
            button2.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString("HH:mm");
        }

        private void Pomodoro_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();        }

        private void Open_login_page(object sender, EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        private void Go_back(object sender, EventArgs e)
        {
            if(student_id == -1)
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

        private void Open_minigame(object sender, EventArgs e)
        {
          
            a.Show();
        }
    }
}
