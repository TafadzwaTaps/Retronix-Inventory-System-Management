using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
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

        private void HomeIconButton_Click(object sender, EventArgs e)
        {
            Hide();
            Dashboard home = new Dashboard();
            home.Show();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
           using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555"))
{
    if (string.IsNullOrWhiteSpace(ProductTextbox.Text) || string.IsNullOrWhiteSpace(SupplierTextBox.Text) || string.IsNullOrWhiteSpace(UnitPriceTextbox.Text) || string.IsNullOrWhiteSpace(PackageTextbox.Text) || string.IsNullOrWhiteSpace(StockTextbox.Text) || string.IsNullOrWhiteSpace(SupplierCodeTextbox.Text))
    {
        MessageBox.Show("Missing information", "Important text", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    else
    {
        try
        {
            con.Open();
            if (MessageBox.Show("Are you sure you want to submit this data? Please check once more before confirming", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "INSERT INTO [Product] VALUES(@Product, @Supplier, @UnitPrice, @Package, @Stock, @SupplierCode, @IsDiscontinued)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Product", ProductTextbox.Text);
                    command.Parameters.AddWithValue("@Supplier", SupplierTextBox.Text);
                    command.Parameters.AddWithValue("@UnitPrice", UnitPriceTextbox.Text);
                    command.Parameters.AddWithValue("@Package", PackageTextbox.Text);
                    command.Parameters.AddWithValue("@Stock", StockTextbox.Text);
                    command.Parameters.AddWithValue("@SupplierCode", SupplierCodeTextbox.Text);
                    command.Parameters.AddWithValue("@IsDiscontinued", IsDiscontinuedTextBox.Text);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Data successfully recorded", "Data successfully recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductTextbox.Clear();
                UnitPriceTextbox.Clear();
                StockTextbox.Clear();
                IsDiscontinuedTextBox.Clear();
                SupplierCodeTextbox.Clear();
                PackageTextbox.Clear();
                SupplierTextBox.Clear();
                Hide();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
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
                    string str = "Update Product set ProductName='" + ProductTextbox.Text + "',UnitPrice ='" + SupplierTextBox.Text + "',UnitPrice ='" + UnitPriceTextbox.Text + "',DateOfBirth ='" + "',Package ='" + PackageTextbox.Text + "',Stock ='" + StockTextbox.Text + "',SupplierCode ='" + SupplierCodeTextbox.Text + "',IsDiscontinued ='" + IsDiscontinuedTextBox.Text + "' Where ProductId='" + ProductIDTextbox.Text + "'";
                    SqlCommand command = new SqlCommand(str, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("" + ProductTextbox.Text + "'s Details have been Updated Successfully.. ", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (ProductTextbox.Text == "")
                try
                {
                    if (MessageBox.Show("Do you want to delete the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string str = "DELETE FROM Product WHERE id = '" + ProductIDTextbox.Text + "'";
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
                    string SqlData = "Select * from Product WHERE ProductName LIKE '%" + SearchTextBox.Text + "%'";
                    SqlCommand cmd = new SqlCommand(SqlData, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Product has been found", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       ProductIDTextbox.Text = dr.GetValue(0).ToString();
                        ProductTextbox.Text = dr.GetValue(1).ToString();
                        SupplierTextBox.Text = dr.GetValue(2).ToString();
                        UnitPriceTextbox.Text = dr.GetValue(3).ToString();
                        PackageTextbox.Text = dr.GetValue(4).ToString();
                        StockTextbox.Text = dr.GetValue(5).ToString();
                        SupplierCodeTextbox.Text = dr.GetValue(6).ToString();
                        IsDiscontinuedTextBox.Text = dr.GetValue(7).ToString();

                    }
                    else
                    {
                        MessageBox.Show("Sorry, This product, " + SearchTextBox.Text + " is not available.", "Important Messgae", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SearchTextBox.Text = "";
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555"))
                {
                    string str = "SELECT * FROM Product";
                    SqlCommand cmd = new SqlCommand(str, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ProductsDataGridView.DataSource = new BindingSource(dt, null);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                DataGridViewRow row = ProductsDataGridView.Rows[indexRow];
                ProductIDTextbox.Text = row.Cells[0].Value.ToString();
                ProductTextbox.Text = row.Cells[1].Value.ToString();
                SupplierTextBox.Text = row.Cells[2].Value.ToString();
                UnitPriceTextbox.Text = row.Cells[3].Value.ToString();
                PackageTextbox.Text = row.Cells[4].Value.ToString();
                StockTextbox.Text = row.Cells[5].Value.ToString();
                SupplierCodeTextbox.Text = row.Cells[6].Value.ToString();
                IsDiscontinuedTextBox.Text = row.Cells[7].Value.ToString();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ProductIDTextbox.Clear();
            ProductTextbox.Clear();
            SupplierTextBox.Clear();
            UnitPriceTextbox.Clear();
            PackageTextbox.Clear();
            StockTextbox.Clear();
            SupplierCodeTextbox.Clear();
            IsDiscontinuedTextBox.Clear();

        }
    }
}
