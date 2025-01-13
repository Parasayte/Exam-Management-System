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
                    sw.WriteLine("body { font-family: 'Times New Roman', serif; background-color: #f9f9f9; margin: 0; padding: 20px; }");
                    sw.WriteLine("h1 { color: #000; text-align: center; font-size: 24px; margin-bottom: 20px; }");
                    sw.WriteLine("table { width: 100%; border-collapse: collapse; margin: 20px 0; background-color: #fff; border: 1px solid #000; }");
                    sw.WriteLine("th, td { padding: 12px; text-align: left; border-bottom: 1px solid #000; }");
                    sw.WriteLine("th { background-color: #f0f0f0; color: #000; font-weight: bold; }");
                    sw.WriteLine("tr:hover { background-color: #f5f5f5; }");
                    sw.WriteLine(".header { text-align: center; margin-bottom: 20px; }");
                    sw.WriteLine(".header img { width: 80px; height: 80px; }");
                    sw.WriteLine(".muhur { text-align: center; margin-top: 20px; }");
                    sw.WriteLine(".muhur img { width: 100px; height: 100px; opacity: 0.8; }");
                    sw.WriteLine(".signature { text-align: right; margin-top: 40px; font-style: italic; }");
                    sw.WriteLine("</style>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");

                    // Add a random icon or logo at the top
                    sw.WriteLine("<div class='header'>");
                    sw.WriteLine("<img src='https://w7.pngwing.com/pngs/1009/824/png-transparent-seal-document-rubber-stamp-technical-standard-seal-animals-photography-technical-standard.png' alt='Official Icon'>");
                    sw.WriteLine("</div>");

                    // Add the title
                    sw.WriteLine("<h1>" + role + " List</h1>");

                    // Write the table
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

                    // Add a mühür (official seal)
                    sw.WriteLine("<div class='muhur'>");
                    sw.WriteLine("<img src='https://w7.pngwing.com/pngs/1009/824/png-transparent-seal-document-rubber-stamp-technical-standard-seal-animals-photography-technical-standard.png' alt='Mühür'>");
                    sw.WriteLine("</div>");

                    // Add a signature section
                    sw.WriteLine("<div class='signature'>");
                    sw.WriteLine("<p>Signature: ________________________</p>");
                    sw.WriteLine("<p>Date: ________________________</p>");
                    sw.WriteLine("</div>");

                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                }

                MessageBox.Show(role + " list saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}