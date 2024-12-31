using System;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sinav
{
    public partial class Add_Teacher : Form
    {
        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Add_Teacher()
        {
            InitializeComponent();
            BrigStudentData();
        }

        private void BrigStudentData()
        {
            string com = "SELECT*FROM Teacher;";
            SqlDataAdapter dt = new SqlDataAdapter(com, connectionString);
            DataTable dataTable = new DataTable();
            dt.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            

            string name = textBox1.Text;
            string phone = textBox2.Text;
            DateTime birthd = dateTimePicker1.Value;
            string password = textBox5.Text;
            string gmail = textBox6.Text;


            string constr = "Server=.; Database=dddd; Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(constr))
            {

                con.Open();


                string query = "INSERT INTO Teacher (teacher_name, password, gmail, birthdate, phone_no) " +
                               "VALUES (@teacher_name, @password, @gmail, @birthdate, @phone_no)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@teacher_name", name);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gmail", gmail);
                    cmd.Parameters.AddWithValue("@birthdate", birthd);
                    cmd.Parameters.AddWithValue("@phone_no", phone);


                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher added");
                }
                con.Close();
                ClearTextbox();
            }
            BrigStudentData();
            BrigStudentData();
        }
        private void ClearTextbox()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            
            textBox5.Text = null;
            textBox6.Text = null;
        }

        private void Add_Teacher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

      
        private void examsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin_Menu adminMenu = new Admin_Menu();
            adminMenu.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            string com = "DELETE FROM Teacher WHERE teacher_name = \'"+name+"\'" ;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(com, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            BrigStudentData();
        }
    }
}
