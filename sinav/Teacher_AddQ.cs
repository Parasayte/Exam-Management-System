using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace sinav
{
    public partial class Teacher_AddQ : Form
    {
        SqlConnection con;


        public Teacher_AddQ()
        {
            InitializeComponent();
            InitializeSqlConnection();

        }
        private void InitializeSqlConnection()
        {
            try
            {
                
                string connectionString = "Server=.; Database=dddd; Integrated Security=True;";
                con = new SqlConnection(connectionString);
                con.Open();
                MessageBox.Show("Database Connection Successful!");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Failed: " + ex.Message);
            }
        }

     
        private void operatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO exam (exam_id, q1, a1) VALUES (@exam_id, @q1, @a1)", con);

                // Add parameters for each column
                cmd.Parameters.AddWithValue("@exam_id", richTextBox1.Text); // Assuming you have a TextBox for exam_id
                cmd.Parameters.AddWithValue("@q1", richTextBox1.Text);       // For question 1
                cmd.Parameters.AddWithValue("@a1", richTextBox1.Text);       // For answer 1

                // Execute the command
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data inserted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}