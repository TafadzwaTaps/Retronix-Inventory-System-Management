using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retronix_Inventory_System_Management.Models;


namespace Retronix_Inventory_Management_System_Unit_Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
         public void TestDashboard()
        {
            //Arrange
            var dashboard = new Dashboard();

            //Act
            dashboard.SetDateRange("2021-01-01", "2022-01-05");
            dashboard.LoadData();

            //Assert
            Assert.AreEqual(1, dashboard.NumCustomers);
            Assert.AreEqual(2, dashboard.NumSuppliers);
            Assert.AreEqual(5, dashboard.NumProducts);
            Assert.AreEqual(3, dashboard.NumOrders);
            Assert.AreEqual(4, dashboard.TopProductsList.Count);
            Assert.AreEqual(0, dashboard.UnderstockList.Count);
            Assert.AreEqual(4, dashboard.GrossRevenueList.Count);
            Assert.AreEqual(17350m, dashboard.TotalRevenue);
            Assert.AreEqual(3470m, dashboard.TotalProfit);
        }
    }
}
