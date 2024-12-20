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
    public partial class Student_Menu : Form
    {
        public Student_Menu(int id)
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exams a =new Exams(0);
            a.Show();
            Hide();
        }

        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Login a =new Student_Login();
            a.Show();
            Hide();
        }

        private void Student_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
