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
using System.Xml.Linq;
using Exam_management_system.Properties;
using Exam_management_system;

namespace Exam_management_system
{
    public partial class Files_menu : Form
    {
        string path; // Path to the directory
        bool selected; // Flag to check if all files are selected
        List<string> SelectedlabelList = new List<string>(); // List of selected file paths
        List<Label> LabelLis = new List<Label>(); // List of file labels

        public Files_menu(string path1)
        {
            InitializeComponent();
            path = path1;
            if (string.IsNullOrEmpty(label3.Text))
            {
                label3.Text = path;
            }
        }

        // Method to show files on the menu
        public void Showfilesonmenu()
        {
            Hide();
            Show();
            int x = 10, y = 120;
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
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
                label.Image = Resources.T;
                label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                this.Controls.Add(label);
                LabelLis.Add(label);
                x += 100;

                // Event handlers for click and double click
                label.DoubleClick += new System.EventHandler(this.label_DoubleClick);
                label.Click += new System.EventHandler(this.label_Click);
                label3.Text.Trim();
                label3.Text = Path.GetFullPath(path);
            }
        }

        private bool isDoubleClick = false; // Flag to check if it is a double click

        // Event handler for label click
        private void label_Click(object sender, EventArgs e)
        {
            if (!isDoubleClick)
            {
                Task.Delay(200).ContinueWith(_ =>
                {
                    if (!isDoubleClick)
                    {
                        this.Invoke(new Action(() => SingleClickHandler(sender, e)));
                    }
                    isDoubleClick = false;
                });
            }
        }

        // Event handler for label double click
        private void label_DoubleClick(object sender, EventArgs e)
        {
            isDoubleClick = true;
            DoubleClickHandler(sender, e);
        }

        // Handler for single click
        private void SingleClickHandler(Object sender, EventArgs e)
        {
            Label clickedButton = sender as Label;

            if (clickedButton == null) return;

            if (SelectedlabelList.Contains(clickedButton.Tag.ToString()))
            {
                label3.Text = Path.GetFullPath(path);
                clickedButton.BackColor = SystemColors.Control;
                SelectedlabelList.Remove(clickedButton.Tag.ToString());
            }
            else
            {
                label3.Text = clickedButton.Tag.ToString();
                SelectedlabelList.Add(clickedButton.Tag.ToString());
                clickedButton.BackColor = Color.MediumPurple;
            }

            label2.Enabled = SelectedlabelList.Count > 0;
        }

        // Handler for double click
        private void DoubleClickHandler(Object sender, EventArgs e)
        {
            Label clickedButton = sender as Label;
            if (clickedButton != null)
            {
                string buttonPath = clickedButton.Tag as string;
                Edit_notes form = new Edit_notes(buttonPath);
                form.Show();
                Hide();
            }
        }

        // Event handler for form load
        private void menu_Load(object sender, EventArgs e)
        {
            newNameToolStripMenuItem.Text = Path.GetFileName(path);
            fileNameToolStripMenuItem.Text = Path.GetFileName(path);
            Showfilesonmenu();
        }

        // Event handler for form closing
        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Event handler to add a new file
        private void Add_new_file(object sender, EventArgs e)
        {
            Add_new_file file = new Add_new_file(path);
            Hide();
            file.Show();
        }

        // Method to create a new file
        public void CreatnewFile(string name, string path1)
        {
            if (File.Exists($"{path1}\\{name}.txt"))
            {
                MessageBox.Show(@"The name is taken ,Try Another One!", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Show();
            }
            else
            {
                File.Create($"{path1}\\{name}.txt").Close();
                MessageBox.Show(@"The Files Created Succefuly", "Succeful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                Show();
            }
        }

        // Event handler to exit the menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directories_menu directories = new Directories_menu(Path.GetDirectoryName(path));
            directories.Show();
            Hide();
        }

        // Event handler to delete selected files
        private void Delete_selected_files(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"The Folder Will Delete, Are You Sure ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            foreach (string a in SelectedlabelList)
            {
                File.Delete($"{a}");
            }

            MessageBox.Show(@"The Files Deleted Succefuly", "Succeful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Hide();
            Files_menu menu = new Files_menu(path);
            menu.Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Empty event handler
        }

        // Event handler to select all files
        private void Selectall_files(object sender, EventArgs e)
        {
            label3.Text = path;
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

        // Event handler for label3 click
        private void label3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = label3.Text;
            ofd.ShowDialog();
        }

        // Event handler to rename a file
        private void Rename_file(object sender, EventArgs e)
        {
            string sourceFilePath = path;
            string destinationFilePath = Path.Combine(Path.GetDirectoryName(path), newNameToolStripMenuItem.Text);

            try
            {
                Directory.Move(sourceFilePath, destinationFilePath);
                path = destinationFilePath;
                fileNameToolStripMenuItem.Text = Path.GetFileName(path);
                MessageBox.Show("File renamed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
