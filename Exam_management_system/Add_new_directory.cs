using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Add_new_directory : Form
    {
        int student_id;
        string path;
        public Add_new_directory(int id,string path1)
        {
            InitializeComponent();
            student_id = id;
            path = path1;
        }

        private void AddNewDirectory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Add_newDirectory(object sender, EventArgs e)
        {

            Directories_menu directories = new Directories_menu(path);
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
