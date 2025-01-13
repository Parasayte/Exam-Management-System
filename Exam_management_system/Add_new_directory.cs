using System;
using System.IO;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Add_new_directory : Form
    {
        private readonly int _studentId;
        private readonly string _directoryPath;

        public Add_new_directory(int studentId, string directoryPath)
        {
            InitializeComponent();
            _studentId = studentId;
            _directoryPath = directoryPath?.Trim() ?? string.Empty;
        }

        private void AddNewDirectory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void BtnCreateDirectory_Click(object sender, EventArgs e)
        {
            CreateNewDirectory();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            NavigateToMenu();
        }

        private void NavigateToMenu()
        {
            try
            {
                string baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Public Notes", "-1");
                string teacherNotesPath = Path.Combine(baseDirectory, "Teachers Notes", "-1");
                string adminNotesPath = Path.Combine(baseDirectory, "Admins Notes", "-1");
                string studentNotesBasePath = Path.Combine(baseDirectory, "Students Notes");

                // Normalize paths for comparison
                string normalizedDirectoryPath = Path.GetFullPath(_directoryPath);
                string normalizedTeacherNotesPath = Path.GetFullPath(teacherNotesPath);
                string normalizedAdminNotesPath = Path.GetFullPath(adminNotesPath);
                string normalizedStudentNotesBasePath = Path.GetFullPath(studentNotesBasePath);

                if (normalizedDirectoryPath.Equals(normalizedTeacherNotesPath, StringComparison.OrdinalIgnoreCase) ||
                    normalizedDirectoryPath.Equals(normalizedAdminNotesPath, StringComparison.OrdinalIgnoreCase) ||
                    normalizedDirectoryPath.StartsWith(normalizedStudentNotesBasePath, StringComparison.OrdinalIgnoreCase))
                {
                    OpenDirectoriesMenu(normalizedDirectoryPath);
                }
                else
                {
                    MessageBox.Show("Invalid path. Cannot determine user type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while navigating: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDirectoriesMenu(string path)
        {
            try
            {
                var menuForm = new Directories_menu(path);
                menuForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the menu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateNewDirectory()
        {
                  Directory.CreateDirectory(_directoryPath+$"\\{richTextBox1.Text}");
                    MessageBox.Show("Directory created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OpenDirectoriesMenu(_directoryPath);
                               
            
        }
    }
}