using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sinav
{
    public partial class Loading_page : Form
    {
        public Loading_page()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Width += 11;
            if (panel1.Width == 101) {
                Thread.Sleep(700);
                panel1.Width =153;
                Thread.Sleep(800);
                
            }
            if (panel1.Width > 632)
            {

                timer1.Stop();
                Login_menu login_Menu = new Login_menu();
                login_Menu.Show();
                Hide();
            }
        }

        private void Close_program(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Close_program(sender, e);

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Close_program(sender, e);

        }

        private void Loading_page_Load(object sender, EventArgs e)
        {

        }
    }
}
