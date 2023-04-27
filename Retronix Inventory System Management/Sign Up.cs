using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Sign_Up : Form
    {
        public Sign_Up()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Login_System login = new Login_System();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";
            if (UsernameTextbox.Text == "" || FullnameTextBox.Text == "" || PasswordTextBox.Text == "" || PhoneNumberTextBox.Text == "")
            {
                MessageBox.Show("missing information","Important text",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (PhoneNumberTextBox.Text.Length<9)
            {
                MessageBox.Show("please enter a valid phone number");
            }
            else
            {
                try
                {
                    con.Open();
                    if (MessageBox.Show("Are you sure you want to submit this data, please check once more before confirming", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Query = "INSERT INTO [UserTbl] VALUES('" + UsernameTextbox.Text + "' , '" + FullnameTextBox.Text + "','" + PasswordTextBox.Text + "','" + PhoneNumberTextBox.Text + "','" + pictureBox1.Image + "','" + RoleComboBox.SelectedItem.ToString() + "')";
                        SqlCommand command = new SqlCommand(Query, con);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Account created successfully", "Account created successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        UsernameTextbox.Clear();
                        PasswordTextBox.Clear();
                        PhoneNumberTextBox.Clear();
                        FullnameTextBox.Clear();         
                        Hide();
                        Login_System login = new Login_System();
                        login.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
            }
        }
    }
}
