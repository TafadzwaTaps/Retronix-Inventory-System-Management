using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXing;

namespace Retronix_Inventory_System_Management
{
    public partial class Products : Form
    {
        private const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Inventory Database;Integrated Security=False;User Id=sa;Password=qqq555";

        public Products()
        {
            InitializeComponent();
        }

        private void NavigateTo(Form form)
        {
            Hide();
            form.Show();
        }

        private void UserIconButton_Click(object sender, EventArgs e) => NavigateTo(new Users());
        private void CustomerIconButton_Click(object sender, EventArgs e) => NavigateTo(new Customers());
        private void OrderIconButton_Click(object sender, EventArgs e) => NavigateTo(new Orders());
        private void ProductsIconButton_Click(object sender, EventArgs e) => NavigateTo(new Products());
        private void SupplierrIconButton_Click(object sender, EventArgs e) => NavigateTo(new Suppliers());
        private void LogoutIconButton_Click(object sender, EventArgs e) => Application.Exit();
        private void HomeIconButton_Click(object sender, EventArgs e) => NavigateTo(new Dashboard());

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsAnyInputMissing())
            {
                MessageBox.Show("Missing information", "Important text", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to submit this data? Please check once more before confirming", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "INSERT INTO [Product] (ProductName, SupplierId, UnitPrice, Package, Stock, SupplierCode, IsDiscontinued) VALUES (@ProductName, @SupplierId, @UnitPrice, @Package, @Stock, @SupplierCode, @IsDiscontinued)";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@ProductName", ProductTextbox.Text);
                            command.Parameters.AddWithValue("@SupplierId", SupplierTextBox.Text);
                            command.Parameters.AddWithValue("@UnitPrice", UnitPriceTextbox.Text);
                            command.Parameters.AddWithValue("@Package", PackageTextbox.Text);
                            command.Parameters.AddWithValue("@Stock", StockTextbox.Text);
                            command.Parameters.AddWithValue("@SupplierCode", SupplierCodeTextbox.Text);
                            command.Parameters.AddWithValue("@IsDiscontinued", IsDiscontinuedTextBox.Text);
                            command.ExecuteNonQuery();
                        }
                    }
                    ClearInputFields();
                    MessageBox.Show("Data successfully recorded", "Data successfully recorded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductIDTextbox.Text))
            {
                MessageBox.Show("Please select a product to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Do you want to update the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "UPDATE Product SET ProductName=@ProductName, Supplier=@Supplier, UnitPrice=@UnitPrice, Package=@Package, Stock=@Stock, SupplierCode=@SupplierCode, IsDiscontinued=@IsDiscontinued WHERE ProductId=@ProductId";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@ProductId", ProductIDTextbox.Text);
                            command.Parameters.AddWithValue("@ProductName", ProductTextbox.Text);
                            command.Parameters.AddWithValue("@SupplierId", SupplierTextBox.Text);
                            command.Parameters.AddWithValue("@UnitPrice", UnitPriceTextbox.Text);
                            command.Parameters.AddWithValue("@Package", PackageTextbox.Text);
                            command.Parameters.AddWithValue("@Stock", StockTextbox.Text);
                            command.Parameters.AddWithValue("@SupplierCode", SupplierCodeTextbox.Text);
                            command.Parameters.AddWithValue("@IsDiscontinued", IsDiscontinuedTextBox.Text);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Product details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductIDTextbox.Text))
            {
                MessageBox.Show("Please select a product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Do you want to delete the data", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "DELETE FROM Product WHERE ProductId=@ProductId";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@ProductId", ProductIDTextbox.Text);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Product record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                MessageBox.Show("Please enter data to perform search operation", "Important message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Product WHERE ProductName LIKE @SearchText";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@SearchText", "%" + SearchTextBox.Text + "%");
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ProductIDTextbox.Text = dr["ProductId"].ToString();
                                ProductTextbox.Text = dr["ProductName"].ToString();
                                SupplierTextBox.Text = dr["SupplierId"].ToString();
                                UnitPriceTextbox.Text = dr["UnitPrice"].ToString();
                                PackageTextbox.Text = dr["Package"].ToString();
                                StockTextbox.Text = dr["Stock"].ToString();
                                SupplierCodeTextbox.Text = dr["SupplierCode"].ToString();
                                IsDiscontinuedTextBox.Text = dr["IsDiscontinued"].ToString();
                                MessageBox.Show("Product found.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Sorry, the product '{SearchTextBox.Text}' is not available.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Product";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        ProductsDataGridView.DataSource = dt;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ProductsDataGridView.Rows[e.RowIndex];
                ProductIDTextbox.Text = row.Cells["ProductId"].Value.ToString();
                ProductTextbox.Text = row.Cells["ProductName"].Value.ToString();
                SupplierTextBox.Text = row.Cells["SupplierId"].Value.ToString();
                UnitPriceTextbox.Text = row.Cells["UnitPrice"].Value.ToString();
                PackageTextbox.Text = row.Cells["Package"].Value.ToString();
                StockTextbox.Text = row.Cells["Stock"].Value.ToString();
                SupplierCodeTextbox.Text = row.Cells["SupplierCode"].Value.ToString();
                IsDiscontinuedTextBox.Text = row.Cells["IsDiscontinued"].Value.ToString();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private bool IsAnyInputMissing()
        {
            return string.IsNullOrWhiteSpace(ProductTextbox.Text) ||
                   string.IsNullOrWhiteSpace(SupplierTextBox.Text) ||
                   string.IsNullOrWhiteSpace(UnitPriceTextbox.Text) ||
                   string.IsNullOrWhiteSpace(PackageTextbox.Text) ||
                   string.IsNullOrWhiteSpace(StockTextbox.Text) ||
                   string.IsNullOrWhiteSpace(SupplierCodeTextbox.Text);
        }

        private void ClearInputFields()
        {
            ProductIDTextbox.Clear();
            ProductTextbox.Clear();
            SupplierTextBox.Clear();
            UnitPriceTextbox.Clear();
            PackageTextbox.Clear();
            StockTextbox.Clear();
            SupplierCodeTextbox.Clear();
            IsDiscontinuedTextBox.Clear();
            SearchTextBox.Clear();
        }

        private void GenerateAndSaveQRCodes()
        {

            string qrCodeFolderPath = @"C:\Users\manix\source\repos\Retronix Inventory System Management\Retronix Inventory System Management\Retronix Inventory System Management\QrCodeImages";

            // Ensure the directory exists
            if (!Directory.Exists(qrCodeFolderPath))
            {
                Directory.CreateDirectory(qrCodeFolderPath);
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT ProductId, ProductName FROM Product"; // Adjust query as needed
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int productId = reader.GetInt32(0);
                    string productName = reader.GetString(1);
                    string qrCodeText = $"{productId}:{productName}"; // Unique identifier for QR code

                    Bitmap qrCodeImage = GenerateQRCode(qrCodeText);

                    // Save the QR code image with productId as the filename
                    string qrCodeFilePath = Path.Combine(qrCodeFolderPath, $"{productId}_qr.png");
                    qrCodeImage.Save(qrCodeFilePath);

                    // Optionally, update the product record with the QR code file path
                    UpdateProductQRCode(productId, qrCodeFilePath);
                }
            }
        }

        private Bitmap GenerateQRCode(string text)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 200,
                    Height = 200
                }
            };
            return writer.Write(text);
        }

        private void UpdateProductQRCode(int productId, string qrCodePath)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string updateQuery = "UPDATE Product SET Barcode = @Barcode WHERE ProductId = @ProductId";
                SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                updateCommand.Parameters.AddWithValue("@Barcode", qrCodePath);
                updateCommand.Parameters.AddWithValue("@ProductId", productId);
                updateCommand.ExecuteNonQuery();
            }
        }


        private void ScanBtn_Click(object sender, EventArgs e)
        {
            var scanner = new BarcodeReader();

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var bitmap = (Bitmap)Image.FromFile(dialog.FileName);
                    var result = scanner.Decode(bitmap);
                    if (result != null)
                    {
                        // Display the scanned result in the appropriate TextBox
                        ProductTextbox.Text = result.Text; // or whichever TextBox you want
                        MessageBox.Show("Scanned: " + result.Text);
                    }
                    else
                    {
                        MessageBox.Show("No QR code/barcode found.");
                    }
                }
            }
        }

        private void LoadProductById(int productId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Product WHERE ProductId = @ProductId";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@ProductId", productId);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ProductIDTextbox.Text = dr["ProductId"].ToString();
                        ProductTextbox.Text = dr["ProductName"].ToString();
                        SupplierTextBox.Text = dr["Supplier"].ToString();
                        UnitPriceTextbox.Text = dr["UnitPrice"].ToString();
                        PackageTextbox.Text = dr["Package"].ToString();
                        StockTextbox.Text = dr["Stock"].ToString();
                        SupplierCodeTextbox.Text = dr["SupplierCode"].ToString();
                        IsDiscontinuedTextBox.Text = dr["IsDiscontinued"].ToString();
                    }
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
