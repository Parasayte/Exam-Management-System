using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Exam_management_system
{
    public partial class Users_list : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\ProjectModels\SchoolManagementSystem.mdf;Integrated Security=True;";

        public Users_list()
        {
            InitializeComponent();
        }

        private void Users_list_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin_menu admin_Menu = new Admin_menu();
            admin_Menu.Show();
            Hide();
        }

        private void Print_admins_data(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Admins", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void Print_teachers_data(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Teacher", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void Print_students_data(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Students", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void Users_list_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Get the current DataTable from the DataGridView
            DataTable dt = (DataTable)dataGridView1.DataSource;

            if (dt != null)
            {
                // Determine the role based on the current data
                string role = "Users";
                if (dataGridView1.DataSource is DataTable dataTable)
                {
                    if (dataTable.TableName == "Admins")
                        role = "Admins";
                    else if (dataTable.TableName == "Teacher")
                        role = "Teachers";
                    else if (dataTable.TableName == "Students")
                        role = "Students";
                }

                // Save the current list as an HTML file
                SaveDataTableToHtml(dt, role);
            }
            else
            {
                MessageBox.Show("No data to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaveDataTableToHtml(DataTable dt, string role)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HTML Files (*.html)|*.html";
            saveFileDialog.Title = "Save " + role + " List";
            saveFileDialog.FileName = role + "_List.html";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    // Write HTML and CSS
                    sw.WriteLine("<!DOCTYPE html>");
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("<title>" + role + " List</title>");
                    sw.WriteLine("<style>");
                    sw.WriteLine("body { font-family: Arial, sans-serif; }");
                    sw.WriteLine("h1 { color: #3053ff; text-align: center; }");
                    sw.WriteLine("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
                    sw.WriteLine("th, td { padding: 12px; text-align: left; border-bottom: 1px solid #ddd; }");
                    sw.WriteLine("th { background-color: #3053ff; color: white; }");
                    sw.WriteLine("tr:hover { background-color: #f5f5f5; }");
                    sw.WriteLine("</style>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<h1>" + role + " List</h1>");
                    sw.WriteLine("<table>");

                    // Write the header row
                    sw.WriteLine("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sw.WriteLine("<th>" + column.ColumnName + "</th>");
                    }
                    sw.WriteLine("</tr>");

                    // Write the data rows
                    foreach (DataRow row in dt.Rows)
                    {
                        sw.WriteLine("<tr>");
                        foreach (var item in row.ItemArray)
                        {
                            sw.WriteLine("<td>" + item.ToString() + "</td>");
                        }
                        sw.WriteLine("</tr>");
                    }

                    sw.WriteLine("</table>");
                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                }

                MessageBox.Show(role + " list saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}