using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Customers : Form
    {
        public Customers()
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

        private void EditButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555");
            con.Open();
            try
            {
                if (MessageBox.Show("Do you want to update the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string str = "Update Customer set FirstName='" + FirstNameTextbox.Text + "',LastName ='" + LastNameTextbox.Text + "',City ='" + CityTextbox.Text + "',Country ='" + CountryTextbox.Text + "',PhoneNumber ='" + PhoneNumberTextBox.Text + "',Address ='" + AddressTextBox.Text + "',BillingAddress ='" + AddressTextBox.Text + "' Where CustomerID='" + CustomerIdTextbox.Text + "'";
                    SqlCommand command = new SqlCommand(str, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("" + LastNameTextbox.Text + "'s Details have been Updated Successfully.. ", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (CustomerIdTextbox.Text == "")
                try
                {
                    if (MessageBox.Show("Do you want to delete the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string str = "DELETE FROM Customer WHERE CustomerID = '" + CustomerIdTextbox.Text + "'";
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";
            if (FirstNameTextbox.Text == "" || LastNameTextbox.Text == "" || CityTextbox.Text == "" || CountryTextbox.Text == "" || PhoneNumberTextBox.Text == "" || AddressTextBox.Text == "")
            {
                MessageBox.Show("missing information", "Important text", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (PhoneNumberTextBox.Text.Length>10)
            {
                MessageBox.Show("please enter a phone number");
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
                        string Query = "INSERT INTO [Customer] VALUES('" + CustomerIdTextbox.Text + "' , '" + FirstNameTextbox.Text + "' , '" + LastNameTextbox.Text + "','" + CityTextbox.Text + "','" + CountryTextbox.Text + "','" + PhoneNumberTextBox.Text + "','" + AddressTextBox.Text + "','" + AddressTextBox.Text + "')";
                        SqlCommand command = new SqlCommand(Query, con);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data successfully recorded", "Data successfully recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        FirstNameTextbox.Clear();
                        LastNameTextbox.Clear();
                        CityTextbox.Clear();
                        CountryTextbox.Clear();
                        PhoneNumberTextBox.Clear();
                        AddressTextBox.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555"))
                {
                    string str = "SELECT * FROM Customer";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    CustomerDataGridView.DataSource = new BindingSource(dt, null);
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
                    string SqlData = "Select * from Customer WHERE FirstName LIKE '%" + SearchTextBox.Text + "%'";
                    SqlCommand cmd = new SqlCommand(SqlData, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Customer has been found", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CustomerIdTextbox.Text = dr.GetValue(0).ToString();
                        FirstNameTextbox.Text = dr.GetValue(1).ToString();
                        LastNameTextbox.Text = dr.GetValue(2).ToString();
                        AddressTextBox.Text = dr.GetValue(3).ToString();
                        CityTextbox.Text = dr.GetValue(4).ToString();
                        CountryTextbox.Text = dr.GetValue(5).ToString();
                        PhoneNumberTextBox.Text = dr.GetValue(6).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Sorry, This Customer, " + SearchTextBox.Text + " is not available.", "Important Messgae", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SearchTextBox.Text = "";
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555"))
                {
                    string str = "SELECT * FROM Customer";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    CustomerDataGridView.DataSource = new BindingSource(dt, null);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CustomerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                DataGridViewRow row = CustomerDataGridView.Rows[indexRow];
                CustomerIdTextbox.Text = row.Cells[0].Value.ToString();
                FirstNameTextbox.Text = row.Cells[1].Value.ToString();
                LastNameTextbox.Text = row.Cells[2].Value.ToString();
                CityTextbox.Text = row.Cells[3].Value.ToString();
                CountryTextbox.Text = row.Cells[4].Value.ToString();
                PhoneNumberTextBox.Text = row.Cells[5].Value.ToString();
                AddressTextBox.Text = row.Cells[6].Value.ToString();
            }
        }
    }
}
