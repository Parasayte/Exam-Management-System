using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Login_menu : Form
    {
        public Login_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student_login student_Login = new Student_login();
            student_Login.Show();
            Hide();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
           Admin_login admin_Login = new Admin_login();
            admin_Login.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void Login_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
