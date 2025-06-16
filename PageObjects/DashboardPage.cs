using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PlaywrightCSharp.PageObjects
{
    public class DashboardPage
    {
        private readonly IPage _page;
        private readonly ILocator _products;
        private readonly ILocator _productText;
        private readonly ILocator _cart;
        private readonly ILocator _orders;

        public DashboardPage(IPage page)
        {
            _page = page;
            _products = _page.Locator(".card-body");
            _productText = _page.Locator(".card-body b");
            _cart = _page.Locator("[routerlink*='cart']");
            _orders = _page.Locator("button[routerlink*='myorders']");
        }

        public async Task SearchProductAddCartAsync(string productName)
        {
            var titles = await _productText.AllTextContentsAsync();
            Console.WriteLine(string.Join(", ", titles)); // optional debug log

            int count = await _products.CountAsync();
            for (int i = 0; i < count; i++)
            {
                var title = await _products.Nth(i).Locator("b").TextContentAsync();
                if ((title?.Trim() ?? "") == productName)

                {
                    await _products.Nth(i).Locator("text= Add To Cart").ClickAsync();
                    break;
                }
            }
        }

        public async Task NavigateToCartAsync()
        {
            await _cart.ClickAsync();
        }

        public async Task NavigateToOrdersAsync()
        {
            await _orders.ClickAsync();
        }
    }
}
