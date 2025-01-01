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
    public partial class Add_new_directory : Form
    {
        int student_id;
        public Add_new_directory(int id)
        {
            InitializeComponent();
            student_id = id;
          
        }

        private void AddNewDirectory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Add_newDirectory(object sender, EventArgs e)
        {
            Directories_menu directories = new Directories_menu($"D:\\Program Files\\{student_id}");
            directories.CreatnewFile(richTextBox1.Text);
            Hide();
        }

      

        private void Cancel(object sender, EventArgs e)
        {
            Directories_menu directories = new Directories_menu($"D:\\Program Files\\{student_id}");
            directories.Show();
            Hide();
        }
    }
}
