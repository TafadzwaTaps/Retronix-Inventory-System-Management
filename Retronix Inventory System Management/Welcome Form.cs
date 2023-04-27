using System;
using System.Windows.Forms;

namespace Retronix_Inventory_System_Management
{
    public partial class Welcome_Form : Form
    {
        public Welcome_Form()
        {
            InitializeComponent();
        }

        private void ProceedButton_Click(object sender, EventArgs e)
        {
            Hide();
            Login_System login = new Login_System();
            login.Show();
        }
    }
}
