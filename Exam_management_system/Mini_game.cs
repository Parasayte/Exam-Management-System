using System;
using System.Drawing;
using System.Windows.Forms;
using Exam_management_system.Properties;

namespace Exam_management_system
{
    public partial class Mini_game : Form
    {
        private string direction = "";
        private int step = 12; 
        private Rectangle bounds = new Rectangle(0, 0, 753, 420); 

        public Mini_game()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            StartGame();
        }

        private void StartGame()
        {
            CreateBarriers();

            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down: if (direction != "up") direction = "down"; break;
                case Keys.Up: if (direction != "down") direction = "up"; break;
                case Keys.Right: if (direction != "left") direction = "right"; break;
                case Keys.Left: if (direction != "right") direction = "left"; break;
            }
        }

        private void CreateBarriers()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, panel1.Width - 50);
                int y = random.Next(0, panel1.Height - 50);
                Label barrier = new Label
                {

                    Location = new Point(x, y),
                    Image = Resources.crate_1554455,
                    Tag = "bar",
                    Size = new Size(15, 15),
                    Text = ""

                };

                panel1.Controls.Add(barrier);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var currentLocation = label1.Location;
            var newLocation = currentLocation;

            switch (direction)
            {
                case "down": newLocation = new Point(currentLocation.X, currentLocation.Y + step); label1.Image = Resources.down; break;
                case "up": newLocation = new Point(currentLocation.X, currentLocation.Y - step); label1.Image = Resources.up2; break;
                case "right": newLocation = new Point(currentLocation.X + step, currentLocation.Y); label1.Image = Resources.right; break;
                case "left": newLocation = new Point(currentLocation.X - step, currentLocation.Y); label1.Image = Resources.left; break;
            }


            if (bounds.Contains(newLocation))
            {
                label1.Location = newLocation;
            }


            foreach (Control control in panel1.Controls)
            {
                if (control.Tag?.ToString() == "bar" && label1.Bounds.IntersectsWith(control.Bounds))
                {
                    label1.Location = new Point(0, 0);
                    timer1.Stop();
                    CreateBarriers();
                    ClearBarriers();
                    timer1.Start();
                    return;
                }
            }
        }
        private void ClearBarriers()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control.Tag?.ToString() == "bar")
                {
                    panel1.Controls.Remove(control);
                    control.Dispose(); 
                }
            }
        }

        private void miniGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Mini_game_Load(object sender, EventArgs e)
        {

        }
    }
}
