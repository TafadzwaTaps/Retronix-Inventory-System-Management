using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Suppliers : Form
    {
        public Suppliers()
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";
            if (CompanyTextbox.Text == "" || ContactTextbox.Text == "" || ContactTitleTextBox.Text == "" || CityTextBox.Text == "" ||CountryTextbox.Text == "" || PhoneTextbox.Text == "" || SupplierCodeTextBox.Text == "")
            {
                MessageBox.Show("missing information", "Important text", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (PhoneTextbox.Text.Length < 10)
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
                        string Query = "INSERT INTO [UserTbl] VALUES('" + CompanyTextbox.Text + "' , '" + ContactTextbox.Text + "','" + ContactTitleTextBox.Text + "','" + CityTextBox.Text + "','" + CountryTextbox.Text + "','" + PhoneTextbox.Text + "','" + SupplierCodeTextBox.Text + "','" + NotesTextbox.Text + "')";
                        SqlCommand command = new SqlCommand(Query, con);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Account created successfully", "Account created successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        CompanyTextbox.Clear();
                        ContactTextbox.Clear();
                        ContactTitleTextBox.Clear();
                        CityTextBox.Clear();
                        CountryTextbox.Clear();
                        PhoneTextbox.Clear();
                        SupplierCodeTextBox.Clear();
                        NotesTextbox.Clear();
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
                    string str = "Update Supplier set CompanyName='" +CompanyTextbox.Text + "',ContactName ='" + ContactTextbox.Text + "',ContactTitle ='" + ContactTitleTextBox.Text + "',City ='" + CityTextBox.Text + "',Country ='" + CountryTextbox.Text + "',Phone ='" + PhoneTextbox.Text + "',SupplierCode ='" + SupplierCodeTextBox.Text + "',Notes ='" + NotesTextbox.Text + "' Where CompanyName='" + CompanyTextbox.Text + "'";
                    SqlCommand command = new SqlCommand(str, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("" + CompanyTextbox.Text + "'s Details have been Updated Successfully.. ", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (CompanyTextbox.Text == "")
                try
                {
                    if (MessageBox.Show("Do you want to delete the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string str = "DELETE FROM Supplier WHERE CompanyName = '" + CompanyTextbox.Text + "'";
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
                    string SqlData = "Select * from Supplier WHERE CompanyName LIKE '%" + SearchTextBox.Text + "%'";
                    SqlCommand cmd = new SqlCommand(SqlData, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Company has been found", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CompanyTextbox.Text = dr.GetValue(1).ToString();
                        ContactTextbox.Text = dr.GetValue(2).ToString();
                        ContactTitleTextBox.Text = dr.GetValue(3).ToString();
                        CityTextBox.Text = dr.GetValue(4).ToString();
                        CountryTextbox.Text= dr.GetValue(5).ToString();
                        PhoneTextbox.Text = dr.GetValue(6).ToString();
                        SupplierCodeTextBox.Text = dr.GetValue(7).ToString();
                        NotesTextbox.Text = dr.GetValue(8).ToString();

                    }
                    else
                    {
                        MessageBox.Show("Sorry, This company, " + SearchTextBox.Text + " is not available.", "Important Messgae", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SearchTextBox.Text = "";
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555"))
                {
                    string str = "SELECT * FROM Supplier";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    SupplierDataGridView.DataSource = new BindingSource(dt, null);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupplierDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                DataGridViewRow row = SupplierDataGridView.Rows[indexRow];
                CompanyTextbox.Text = row.Cells[0].Value.ToString();
                ContactTextbox.Text = row.Cells[1].Value.ToString();
                ContactTitleTextBox.Text = row.Cells[2].Value.ToString();
                CityTextBox.Text = row.Cells[3].Value.ToString();
                CountryTextbox.Text = row.Cells[4].Value.ToString();
                PhoneTextbox.Text = row.Cells[5].Value.ToString();
                SupplierCodeTextBox.Text = row.Cells[6].Value.ToString();
                NotesTextbox.Text = row.Cells[7].Value.ToString();
            }
        }
    }
}
