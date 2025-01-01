using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinav
{
    public partial class Admin_login : Form
    {
        public Admin_login()
        {
            InitializeComponent();
        }

        private void LogIn_as_admin(object sender, EventArgs e)
        {
            if (textBox1.Text == "selam")
            {
                Admin_menu a = new Admin_menu();
                a.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(@"Not correct , try again ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
