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
    public partial class Teacher_Login : Form
    {
     
        public Teacher_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student_Login a =new Student_Login();
            a.Show();
            Hide();
          
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            Teacher_AddQ a=new Teacher_AddQ();
            a.Show();
            Hide();
        }
    }
}
