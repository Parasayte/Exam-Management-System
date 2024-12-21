using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Data;
namespace sinav
{
    public partial class Add_Student : Form
    {

        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Add_Student()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void Teacher_AddSt_Load(object sender, System.EventArgs e)
        {
            BrigStudentData();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            
          
        }
        private void ClearTextbox()
        {
            textBox1.Text=null;
            textBox2.Text = null;
           textBox4.Text=null;
            textBox5.Text=null;
           textBox6.Text=null;
        }

        private void Teacher_AddSt_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Student_Login a = new Student_Login();
            a.Show();
            Hide();
        }

        private void addExamToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Add_Exam addExam = new Add_Exam();
            addExam.Show();
            Hide();
        }
        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Read_Answers readStudentsNotes = new Read_Answers();
            readStudentsNotes.Show();
            Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Student addSt = new Add_Student();
            addSt.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_Login teacher_Login = new Teacher_Login();
            teacher_Login.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string nickn = textBox2.Text;
            string birthd = textBox4.Text;
            string password = textBox5.Text;
            string gmail = textBox6.Text;


            string constr = "Server=.; Database=dddd; Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(constr))
            {

                con.Open();


                string query = "INSERT INTO Students (name, nick_name, birth_date, password, gmail) " +
                               "VALUES (@name, @nick_name, @birth_date, @password, @gmail)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@nick_name", nickn);
                    cmd.Parameters.AddWithValue("@birth_date", birthd);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gmail", gmail);


                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("inserted successfully.");
                }
                con.Close();
                ClearTextbox();
            }
            BrigStudentData();
        }

      
        private void BrigStudentData()
        {
            string com = "SELECT*FROM Students;";
            SqlDataAdapter dt = new SqlDataAdapter(com, connectionString);
            DataTable dataTable = new DataTable();
            dt.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string id=textBox3.Text;
            string com = "DELETE FROM Students WHERE id = "+id;
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(com,connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            BrigStudentData();
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            Teacher_Login teacher_Login = new Teacher_Login();
            teacher_Login.Show();
            Hide();
        }

        private void readNotesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Read_Answers readStudentsNotes = new Read_Answers();
            readStudentsNotes.Show();
            Hide();
        }
    }
}