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
    public partial class Exams : Form
    {
        public Exams()
        {
            InitializeComponent();
        }

        private void reuslutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Result a=new Result();
            a.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Login a = new Student_Login();
            a.Show();
            Hide();
        }
    }
}
