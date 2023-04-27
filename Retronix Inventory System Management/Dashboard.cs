using System;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Dashboard : Form
    {
        private Models.Dashboard model;

        public Dashboard()
        {
            InitializeComponent();
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();

            model = new Models.Dashboard();
            LoadData();
        }

        private void btnOkCustomDate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCustomDate_Click(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void DisableCustomDates()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }

        private void LoadData()
        {
            var refreshData = model.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            if (refreshData == true)
            {
                lblNumOrders.Text = model.NumOrders.ToString();
                lblTotalRevenue.Text = "$" + model.TotalRevenue.ToString();
                lblTotalProfit.Text = "$" + model.TotalProfit.ToString();

                lblNumCustomers.Text = model.NumCustomers.ToString();
                lblNumSuppliers.Text = model.NumSuppliers.ToString();
                lblNumProducts.Text = model.NumProducts.ToString();

                ChartGrossRevnue.DataSource = model.GrossRevenueList;
                ChartGrossRevnue.Series[0].XValueMember = "Date";
                ChartGrossRevnue.Series[0].YValueMembers = "TotalAmount";
                ChartGrossRevnue.DataBind();

                ChartTopProducts.DataSource = model.TopProductsList;
                ChartTopProducts.Series[0].XValueMember = "Key";
                ChartTopProducts.Series[0].YValueMembers = "Value";
                ChartTopProducts.DataBind();

                dgvUnderstock.DataSource = model.UnderstockList;
                dgvUnderstock.Columns[0].HeaderText = "Item";
                dgvUnderstock.Columns[1].HeaderText = "Units";
                Console.WriteLine("Loaded view :)");
            }
            else Console.WriteLine("View not loaded, same query");
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

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
