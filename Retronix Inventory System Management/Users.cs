using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void HomeIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Dashboard home = new Dashboard();
            home.Show();
        }

        private void UserIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Users users = new Users();
            users.Show();
        }

        private void CustomerIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Customers customers = new Customers();
            customers.Show();
        }

        private void OrderIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Orders orders = new Orders();
            orders.Show();

        }

        private void ProductsIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Products products = new Products();
            products.Show();
        }

        private void SupplierrIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Suppliers suppliers = new Suppliers();
            suppliers.Show();
        }

        private void LogoutIconButton_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";
            if (UserTextBox.Text == "" || FullNameTextbox.Text == "" || PasswordTextBox.Text == "" || PhoneNumberTextBox.Text == "")
            {
                MessageBox.Show("missing information", "Important text", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (PhoneNumberTextBox.Text.Length < 9)
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
                        string Query = "INSERT INTO [UserTbl] VALUES('" + UserIdTextBox.Text + "' , '" + UserTextBox.Text + "','" + FullNameTextbox.Text + "','" + PasswordTextBox.Text + "','" + PhoneNumberTextBox.Text + "','" + RoleComboBox.SelectedItem.ToString() + "')";
                        SqlCommand command = new SqlCommand(Query, con);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Account created successfully", "Account created successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        UserTextBox.Clear();
                        PasswordTextBox.Clear();
                        PhoneNumberTextBox.Clear();
                        FullNameTextbox.Clear();
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

        private void EditButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555");
            con.Open();
            try
            {
                if (MessageBox.Show("Do you want to update the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string str = "Update UserTbl set UserName='" + UserTextBox.Text + "',Fullname ='" + FullNameTextbox.Text + "',Password ='" + PasswordTextBox.Text + "',PhoneNumber ='" + PhoneNumberTextBox.Text + "',UserRole ='" + RoleComboBox.SelectedItem.ToString() + "' Where UserId='" + UserIdTextBox.Text + "'";
                    SqlCommand command = new SqlCommand(str, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("" + UserTextBox.Text + "'s Details have been Updated Successfully.. ", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555");
            con.Open();
            if (UserIdTextBox.Text == "")
                try
                {
                    if (MessageBox.Show("Do you want to delete the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string str = "DELETE FROM UserTbl WHERE UserID = '" + UserIdTextBox.Text + "'";
                        SqlCommand cmd = new SqlCommand(str, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Account Information Record Delete Successfully", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private void Users_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555"))
                {
                    string str = "SELECT * FROM UserTbl";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    UserDataGridView.DataSource = new BindingSource(dt, null);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=\"Inventory Database\";Integrated Security=True");
            con.Open();
            if (SearchTextBox.Text == "")
            {
                MessageBox.Show("please enter data to perform search operation", "Important message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    string SqlData = "Select * from UserTbl WHERE Username LIKE '%" + SearchTextBox.Text + "%'";
                    SqlCommand cmd = new SqlCommand(SqlData, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("User has been found", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserIdTextBox.Text = dr.GetValue(0).ToString();
                        FullNameTextbox.Text = dr.GetValue(1).ToString();
                        UserTextBox.Text = dr.GetValue(2).ToString();
                        PasswordTextBox.Text = dr.GetValue(3).ToString();
                        PhoneNumberTextBox.Text = dr.GetValue(4).ToString();
                        RoleComboBox.Text = dr.GetValue(5).ToString();

                    }
                    else
                    {
                        MessageBox.Show("Sorry, This user, " + SearchTextBox.Text + " is not available.", "Important Messgae", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SearchTextBox.Text = "";
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UserDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                DataGridViewRow row = UserDataGridView.Rows[indexRow];
                UserIdTextBox.Text = row.Cells[0].Value.ToString();
                FullNameTextbox.Text = row.Cells[1].Value.ToString();
                UserTextBox.Text = row.Cells[2].Value.ToString();
                PasswordTextBox.Text = row.Cells[3].Value.ToString();
                PhoneNumberTextBox.Text = row.Cells[4].Value.ToString();
                RoleComboBox.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
