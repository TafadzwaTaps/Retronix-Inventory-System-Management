using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();
        private void InsertButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";
            if (OrderDateTextbox.Text == "" || OrderNumberTextBox.Text == "" || CustomerIdTextbox.Text == "" || TotalAmountTextbox.Text == "")
            {
                MessageBox.Show("missing information", "Important text", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    con.Open();
                    if (MessageBox.Show("Are you sure you want to submit this data, please check once more before confirming", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Query = "INSERT INTO [Order] VALUES('" + OrderIdTextBox.Text + "' , '" + OrderDateTextbox.Text + "' , '" + OrderNumberTextBox.Text + "','" + CustomerIdTextbox.Text + "','" + TotalAmountTextbox.Text + "')";
                        SqlCommand command = new SqlCommand(Query, con);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data successfully recorded", "Data successfully recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        OrderDateTextbox.Clear();
                        OrderNumberTextBox.Clear();
                        CustomerIdTextbox.Clear();
                        TotalAmountTextbox.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555");
            con.Open();
            if (OrderIdTextBox.Text == "")
                try
                {
                    if (MessageBox.Show("Do you want to delete the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string str = "DELETE FROM Order WHERE OrderId = '" + OrderIdTextBox.Text + "'";
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555");
            con.Open();
            try
            {
                if (MessageBox.Show("Do you want to update the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string str = "Update Order set OrderDate='" + OrderDateTextbox.Text + "',OrderNumber ='" + OrderNumberTextBox.Text + "',CustomerId ='" + CustomerIdTextbox.Text + "',TotalAmount ='" + TotalAmountTextbox.Text + "' Where OrderId='" + OrderIdTextBox.Text + "'";
                    SqlCommand command = new SqlCommand(str, con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("" + OrderNumberTextBox.Text + "'s Details have been Updated Successfully.. ", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            Hide() ;
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

        private void Orders_Load(object sender, EventArgs e)
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

            table.Columns.Add("num", typeof(int));
            table.Columns.Add("product", typeof(string));
            table.Columns.Add("qty", typeof(int));
            table.Columns.Add("uprice", typeof(decimal));
            table.Columns.Add("totprice", typeof(decimal));
        }

        int num = 0;
        int totprice, qty;
        string product;
        decimal uprice;


        int flag = 0;
        private void ProductsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                DataGridViewRow row = ProductsDataGridView.Rows[indexRow];
                product = row.Cells[1].Value.ToString();
                //qty = Convert.ToInt32(QtyTb.Text);
                uprice = Convert.ToDecimal(row.Cells[3].Value.ToString());
                //totprice = qty * uprice;
                flag = 1;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void AddToOrderButton_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
                MessageBox.Show("Enter The Number Of Products");
            else if (flag == 0)
                MessageBox.Show("Select The Product");
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * Convert.ToInt32(uprice);
                table.Rows.Add(num, product, qty, uprice, totprice);
                OrderDataGridView.DataSource = table;
                flag = 0;
            }
        }
    }
}
