using System;
using System.Windows.Forms;

namespace sinav
{
    public partial class Student_Login : Form
    {
        public Student_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Teacher_Login a =new Teacher_Login();
            a.Show();
            Hide();
        }
    }
}