using Microsoft.Playwright;
using PlaywrightCSharp.PageObjects;

namespace PlaywrightCSharp
{
    // Page Object Manager: Central place to manage all page objects
    public class POManager
    {
        private readonly IPage _page;
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;
        private readonly CartPage _cartPage;
        private readonly PlaceOrderPage _placeOrderPage;
        private readonly OrdersHistoryPage _ordersHistoryPage;

        public POManager(IPage page)
        {
            _page = page;

            // Instantiate all page objects using the shared IPage instance
            _loginPage = new LoginPage(_page);
            _dashboardPage = new DashboardPage(_page);
            _cartPage = new CartPage(_page);
            _placeOrderPage = new PlaceOrderPage(_page);
            _ordersHistoryPage = new OrdersHistoryPage(_page);
        }

        public LoginPage GetLoginPage()
        {
            return _loginPage;
        }

        public DashboardPage GetDashboardPage()
        {
            return _dashboardPage;
        }

        public CartPage GetCartPage()
        {
            return _cartPage;
        }

        public PlaceOrderPage GetPlaceOrderPage()
        {
            return _placeOrderPage;
        }

        public OrdersHistoryPage GetOrdersHistoryPage()
        {
            return _ordersHistoryPage;
        }
    }
}
