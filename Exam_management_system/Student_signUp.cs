using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Data;
namespace Exam_management_system
{
    public partial class Student_signUp : Form
    {

        string connectionString = "Server=.; Database=dddd; Integrated Security=True;";

        public Student_signUp()
        {
            InitializeComponent();
            string birthd = textBox4.Text;
          
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void Teacher_AddSt_Load(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {


        }
        private void ClearTextbox()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }

        private void Teacher_AddSt_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        private void addExamToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Add_exams addExam = new Add_exams();
            addExam.Show();
            Hide();
        }
        private void readNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_results readStudentsNotes = new Add_results();
            readStudentsNotes.Show();
            Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_students addSt = new Add_students();
            addSt.Show();
            Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teacher_login teacher_Login = new Teacher_login();
            teacher_Login.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Sign_up(object sender, EventArgs e)
        {
            string birthd = dateTimePicker4.Value.ToString("yyyy-MM-dd");
            string name = textBox1.Text;
            string nickn = textBox2.Text;
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
                    MessageBox.Show($@"Welcome to the system {name}","Congrat",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                con.Close();
                Go_back_to_loginPage(sender, e);
            }
        }


       

    
     
      
     
        private void Student_signUp_Load(object sender, EventArgs e)
        {

        }

        private void Go_back_to_loginPage(object sender, EventArgs e)
        {
            
            Student_login a = new Student_login();
            a.Show();
            Hide();
        }

        private void Student_signUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}