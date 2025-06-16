using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightCSharp.PageObjects
{
    public class OrdersHistoryPage
    {
        private readonly IPage _page;
        private readonly ILocator _ordersTable;
        private readonly ILocator _rows;
        private readonly ILocator _orderIdDetails;

        public OrdersHistoryPage(IPage page)
        {
            _page = page;
            _ordersTable = _page.Locator("tbody");
            _rows = _page.Locator("tbody tr");
            _orderIdDetails = _page.Locator(".col-text");
        }

        public async Task SearchOrderAndSelectAsync(string orderId)
        {
            await _ordersTable.WaitForAsync();

            int count = await _rows.CountAsync();
            for (int i = 0; i < count; i++)
            {
               String? rowOrderId = await _rows.Nth(i).Locator("th").TextContentAsync();
if (!string.IsNullOrEmpty(rowOrderId) && orderId.Contains(rowOrderId))
{
    await _rows.Nth(i).Locator("button").First.ClickAsync();
    break;
}
            }
        }

        public async Task<string?> GetOrderIdAsync()
        {
            return await _orderIdDetails.TextContentAsync();
        }
    }
}
// // ...existing code...
// String? rowOrderId = await _rows.Nth(i).Locator("th").TextContentAsync();
// if (!string.IsNullOrEmpty(rowOrderId) && orderId.Contains(rowOrderId))
// {
//     await _rows.Nth(i).Locator("button").First.ClickAsync();
//     break;
// }
// // ...existing code...