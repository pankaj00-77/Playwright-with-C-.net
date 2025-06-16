// using Microsoft.Playwright;
// using NUnit.Framework;
// using System.Threading.Tasks;
// using PlaywrightCSharp.PageObjects;


// namespace PlaywrightCSharp.Tests
// {
//     public class ClientAppTests
//     {
//         private IBrowser browser;
//         private IBrowserContext context;
//         private IPage page;

//         [SetUp]
//         public async Task Setup()
//         {
//             var playwright = await Playwright.CreateAsync();
//             browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
//             context = await browser.NewContextAsync();
//             page = await context.NewPageAsync();
//         }

//         [TearDown]
//         public async Task Teardown()
//         {
//             await browser.CloseAsync();
//         }

//         [Test]
//         public async Task ClientAppLogin()
//         {
//             var poManager = new POManager(page);

//             // Login
//             var loginPage = poManager.GetLoginPage();
//             await loginPage.GoToAsync();
//             await loginPage.ValidLoginAsync("username", "password");

//             // Dashboard
//             var dashboardPage = poManager.GetDashboardPage();
//             await dashboardPage.SearchProductAddCartAsync("zara coat 3");
//             await dashboardPage.NavigateToCartAsync();

//             // Cart
//             var cartPage = poManager.GetCartPage();
//             await cartPage.ProductIsVisibleAsync("zara coat 3");
//             await cartPage.GoToCheckoutAsync();

//             // Place Order
//             var placeOrderPage = poManager.GetPlaceOrderPage();
//             await placeOrderPage.SearchCountryAndSelectAsync("ind", "India");
//             var orderId = await placeOrderPage.PlaceOrderClickAsync();

//             // Orders
//             await dashboardPage.NavigateToOrdersAsync();
//             var ordersPage = poManager.GetOrdersHistoryPage();
//             await ordersPage.SearchOrderAndSelectAsync(orderId);
//             var orderIdDetails = await ordersPage.GetOrderIdAsync();

//             Assert.IsTrue(orderId.Contains(orderIdDetails));
//         }
//     }
// }
