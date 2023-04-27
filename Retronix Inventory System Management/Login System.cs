using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Login_System : Form
    {
        public Login_System()
        {
            InitializeComponent();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Sign_Up sign_Up = new Sign_Up();
            sign_Up.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int LoginAttempt = 0;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";
                con.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("select count(*) from [UserTbl] where Username= '" + UsernameTextBox.Text + "' and Password= '" + PasswordTextBox.Text + "'", con);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    timer1.Start();
                    con.Close();
                }
                else if (UsernameTextBox.Text == "" && PasswordTextBox.Text == "")
                {
                    MessageBox.Show("Please enter username and Password", "Please enter username and Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoginAttempt++;
                }
                else if (UsernameTextBox.Text == "")
                {
                    MessageBox.Show("Please enter username", "Please enter username", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoginAttempt++;
                }
                else if (PasswordTextBox.Text == "")
                {
                    MessageBox.Show("Please enter Password", "Please enter Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LoginAttempt++;
                }
                else
                {
                    MessageBox.Show("wrong username and Password", "wrong username and Password, access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (SqlException excep) // handling errors that my occur
            {

                MessageBox.Show(excep.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);

            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                MessageBox.Show("Login successful", "Login successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                PasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextBox.PasswordChar = '*';
            }
        }
    }
}
