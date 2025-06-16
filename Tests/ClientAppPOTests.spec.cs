using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using static Microsoft.Playwright.Assertions;
// using Xunit;
// using NUnit.Framework;



namespace PlaywrightCSharp.Tests
{
    public class ClientAppPOTest
    {
        private IBrowser? browser;
        private IPlaywright? playwright;


        public class TestData
{
    public required string username { get; set; }
    public required string password { get; set; }
    public required string productName { get; set; }
}


        public static IEnumerable<TestData> TestDataSet()
        {
            var jsonData = File.ReadAllText(@"Utils\PlaceOrderData.json");
            return JsonSerializer.Deserialize<List<TestData>>(jsonData) ?? new List<TestData>();
//             var json = File.ReadAllText("TestData/PlaceOrderData.json");
// var testDataList = JsonConvert.DeserializeObject<List<TestData>>(json);

        }

        [SetUp]
        public async Task Setup()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        }

        [TearDown]
    public async Task Teardown()
{
    if (browser != null)
    {
        await browser.CloseAsync();
    }
}



        [Test, TestCaseSource(nameof(TestDataSet))]
        public async Task ClientAppLogin(TestData data)
        {
            var context = await browser!.NewContextAsync();
            var page = await context.NewPageAsync();

            var poManager = new POManager(page);

            // Login Page
            var loginPage = poManager.GetLoginPage();
            await loginPage.GoToAsync();
            await loginPage.ValidLoginAsync(data.username, data.password);


            // Dashboard Page
            var dashboardPage = poManager.GetDashboardPage();
            await dashboardPage.SearchProductAddCartAsync(data.productName);
            await dashboardPage.NavigateToCartAsync();

           

            // Cart Page
            var cartPage = poManager.GetCartPage();
            await cartPage.ProductIsVisibleAsync(data.productName);
            await cartPage.GoToCheckoutAsync();

            

            // Place Order Page
            var placeOrderPage = poManager.GetPlaceOrderPage();
            await placeOrderPage.SearchCountryAndSelectAsync("ind", "India");
            var orderId = await placeOrderPage.PlaceOrderClickAsync();
            TestContext.WriteLine($"Order ID: {orderId}");

           

            // Order History Page
            await dashboardPage.NavigateToOrdersAsync();
            var ordersPage = poManager.GetOrdersHistoryPage();
            await ordersPage.SearchOrderAndSelectAsync(orderId);
            var orderIdDetails = await ordersPage.GetOrderIdAsync();

Assert.That(orderId, Does.Contain(orderIdDetails)); // ✅ correct NUnit assertion
            // Assert.IsTrue(orderId.Contains(orderIdDetails));



           
        }
    }
}
