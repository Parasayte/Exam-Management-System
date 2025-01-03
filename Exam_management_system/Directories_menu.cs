using System;
using System.Collections.Generic;

using System.Drawing;
using System.IO;

using System.Threading.Tasks;
using System.Windows.Forms;
using Exam_management_system.Properties;
using Exam_management_system;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Exam_management_system
{
    public partial class Directories_menu : Form
    {
        // Fields
        bool selected;
        List<string> SelectedlabelList = new List<string>();
        List<Label> LabelLis = new List<Label>();
        string path1;
        int id;

        // Constructor
        public Directories_menu(string path)
        {
            InitializeComponent();
            path1 = path;
        }

        // Load event handler
        private void directories_Load(object sender, EventArgs e)
        {
            // Check if directory exists
            if (!Directory.Exists(path1))
            {
                MessageBox.Show(@"The Folder Not Found,The Folder Will Create", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Directory.CreateDirectory(path1);
                Showfilesonmenu(path1);
            }
            textBox1.Text = Path.GetFullPath(path1);
            Showfilesonmenu(path1);
            string path = Path.GetFileName(path1);
            id = int.Parse(path);
        }

        // Show files on menu
        public void Showfilesonmenu(string path)
        {
            int x = 10, y = 120;
            string[] files = Directory.GetDirectories(path);

            foreach (var file in files)
            {
                if (x % 810 == 0)
                {
                    x = 10;
                    y += 100;
                }
                Label label = new Label();
                label.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
                label.Location = new System.Drawing.Point(x, y);
                label.Name = Path.GetFileName(file);
                label.Tag = file;
                label.Size = new System.Drawing.Size(90, 93);
                label.TabIndex = 0;
                label.Text = Path.GetFileName(file);
                label.Image = Resources.D;
                label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.Controls.Add(label);
                LabelLis.Add(label);
                x += 100;

                // Add event handlers
                label.DoubleClick += new System.EventHandler(this.label_DoubleClick);
                label.Click += new System.EventHandler(this.label_Click);
            }
        }

        private bool isDoubleClick = false;

        // Single click event handler
        private void label_Click(object sender, EventArgs e)
        {
            if (!isDoubleClick)
            {
                Task.Delay(100).ContinueWith(_ =>
                {
                    if (!isDoubleClick)
                    {
                        this.Invoke(new Action(() => SingleClickHandler(sender, e)));
                    }
                    isDoubleClick = false;
                });
            }
        }

        // Double click event handler
        private void label_DoubleClick(object sender, EventArgs e)
        {
            isDoubleClick = true;
            DoubleClickHandler(sender, e);
        }

        // Handle single click
        private void SingleClickHandler(Object sender, EventArgs e)
        {
            Label clickedButton = sender as Label;

            if (clickedButton == null) return;

            if (SelectedlabelList.Contains(clickedButton.Tag.ToString()))
            {
                clickedButton.BackColor = SystemColors.Control;
                SelectedlabelList.Remove(clickedButton.Tag.ToString());
            }
            else
            {
                SelectedlabelList.Add(clickedButton.Tag.ToString());
                clickedButton.BackColor = Color.MediumPurple;
            }

            label2.Enabled = SelectedlabelList.Count > 0;
        }

        // Handle double click
        private void DoubleClickHandler(Object sender, EventArgs e)
        {
            Label clickedButton = sender as Label;
            if (clickedButton != null)
            {
                string buttonPath = clickedButton.Tag as string;
                Files_menu menu = new Files_menu(buttonPath);
                menu.Show();
                Hide();
            }
        }

        // Button click event handler
        private void button_Click(Object sender, EventArgs e)
        {
            Label clickedButton = sender as Label;
            if (clickedButton != null)
            {
                string buttonPath = clickedButton.Tag as string;
                string path = Path.GetFullPath(buttonPath);

                Files_menu menu = new Files_menu(path);
                menu.Show();
                Hide();
            }
        }

        // Form closing event handler
        private void directories_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Add new directory
        private void Add_new_directory(object sender, EventArgs e)
        {
            Add_new_directory addNewDirectory = new Add_new_directory(id);
            addNewDirectory.Show();
            Hide();
            Showfilesonmenu(path1);
        }

        // Create new file
        public void CreatnewFile(string name)
        {
            if (Directory.Exists($"{path1}\\{name}"))
            {
                MessageBox.Show(@"The name is taken ,Try Another One!", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Show();
            }
            else
            {
                Directory.CreateDirectory($"{path1}\\{name}");
                MessageBox.Show(@"The Folder Created Succefuly", "Succeful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                Show();
            }
        }

        // Exit menu item click event handler

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define base paths for different note types
            string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Trim();
            string teacherNotesPath = Path.Combine(baseDirectory, "Teachers Notes", "-1").Trim();
            string adminNotesPath = Path.Combine(baseDirectory, "Admins Notes", "-1").Trim();
            string studentNotesBasePath = Path.Combine(baseDirectory, "Students Notes").Trim();

            path1 = path1.Trim();
            if (path1 == teacherNotesPath)
            {
                // Open Teacher menu
                Add_results add_Results = new Add_results();
                add_Results.Show();
                Hide();
                return;
            }
            else if (path1 == adminNotesPath)
            {
                // Show the Admin menu form
                Admin_menu admin_Menu = new Admin_menu();
                admin_Menu.Show();
                Hide();
                return;
            }
            else if (path1.StartsWith(studentNotesBasePath))
            {
                try
                {
                    // Extract the directory name (student ID) from the path
                    string folderName = Path.GetFileName(path1);

                    if (int.TryParse(folderName.Trim(), out int studentId))
                    {
                        // Open Student Menu with the ID as a parameter
                        Student_menu student_Menu = new Student_menu(studentId);
                        student_Menu.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid directory name format. Unable to parse student ID.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions and notify the user
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Unknown path. Please check the directory.");
                Student_login student_Login = new Student_login();
                student_Login.Show();
                Hide();
            }
        }





        // Delete selected folders
        private void Delete_selected_folders(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"The Folder Will Delete, Are You Sure ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            foreach (string a in SelectedlabelList)
            {
                if (Directory.Exists(a))
                {
                    foreach (string file in Directory.GetFiles(a))
                    {
                        File.Delete(file);
                    }

                    foreach (string dir in Directory.GetDirectories(a))
                    {
                        Directory.Delete(dir, true);
                    }

                    Directory.Delete(a);
                }
            }

            MessageBox.Show(@"The Folders Deleted Succefuly", "Succeful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Hide();
            Directories_menu directories = new Directories_menu(path1);
            directories.Show();
        }

        // Select all folders
        private void Selectall_folders(object sender, EventArgs e)
        {
            label2.Enabled = true;

            if (selected)
            {
                label2.Enabled = true;
                selected = false;

                foreach (Label a in LabelLis)
                {
                    SelectedlabelList.Remove(a.Tag.ToString());
                    a.BackColor = SystemColors.Control;
                }
            }
            else
            {
                selected = true;

                foreach (Label a in LabelLis)
                {
                    SelectedlabelList.Add(a.Tag.ToString());
                    a.BackColor = Color.MediumPurple;
                }
            }

            label2.Enabled = SelectedlabelList.Count > 0;
        }

        // Label click event handler
        private void label4_Click(object sender, EventArgs e)
        {
            path1 = textBox1.Text;
            Directories_menu d = new Directories_menu(path1);
            d.Show();
            Hide();
        }
    }

}

