using Microsoft.Playwright;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace PlaywrightCSharp.PageObjects
{
    public class CartPage
    {
        private readonly IPage _page;
        private readonly ILocator _productCard;
        private readonly ILocator _checkout;

        public CartPage(IPage page)
        {
            _page = page;
            _productCard = _page.Locator("div li");
            _checkout = _page.Locator("text=Checkout");
        }

        public async Task ProductIsVisibleAsync(string productName)
        {
            await _productCard.First.WaitForAsync();
            bool isVisible = await _page.Locator($"h3:has-text('{productName}')").IsVisibleAsync();
            if (!isVisible)
            {
                throw new Exception($"❌ Product '{productName}' not visible in the cart.");
            }
        }

        public async Task GoToCheckoutAsync()
        {
            await _checkout.ClickAsync();
        }
    }
}
