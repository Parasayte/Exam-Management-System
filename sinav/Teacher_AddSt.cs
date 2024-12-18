using System.Windows.Forms;
using System.Data.SqlClient;
namespace sinav
{
    public partial class Teacher_AddSt : Form
    {
        
        
        public Teacher_AddSt()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void Teacher_AddSt_Load(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
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
                    MessageBox.Show($"{rowsAffected} row(s) inserted successfully.");
                }
                con.Close();
                ClearTextbox();
            }
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
    }
}