using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exam_management_system
{
    public partial class Add_new_file : Form
    {
        public string fileName;
        string path;
        public Add_new_file(string path1)
        {
            InitializeComponent();
            path = path1;
        }


        private void Add_newFile(object sender, EventArgs e)
        {
            Files_menu m = new Files_menu(path);

            m.CreatnewFile(richTextBox1.Text, path);
            Hide();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Files_menu m = new Files_menu(path);
            m.Show();

            Hide();
        }
    }
}
