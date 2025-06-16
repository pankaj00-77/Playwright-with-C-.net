using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

using static Microsoft.Playwright.Assertions;

namespace PlaywrightCSharp.PageObjects
{
    public class PlaceOrderPage
    {
        private readonly IPage _page;
        private readonly ILocator _countryInput;
        private readonly ILocator _dropdown;
        private readonly ILocator _emailId;
        private readonly ILocator _submit;
        private readonly ILocator _orderConfirmationText;
        private readonly ILocator _orderId;

        public PlaceOrderPage(IPage page)
        {
            _page = page;
            _countryInput = page.Locator("[placeholder*='Country']");
            _dropdown = page.Locator(".ta-results");
            _emailId = page.Locator(".user__name [type='text']").First;
            _submit = page.Locator(".action__submit");
            _orderConfirmationText = page.Locator(".hero-primary");
            _orderId = page.Locator(".em-spacer-1 .ng-star-inserted");
        }

        // public async Task SearchCountryAndSelectAsync(string countryCode, string countryName)
        // {
        //     Console.WriteLine($"{countryCode}, {countryName}");

        //   await _countryInput.FillAsync(countryCode);
        //     await _dropdown.WaitForAsync();
        //     var option = _dropdown.Locator("button.ta-item");
        //         await option.First.WaitForAsync(new() { State = WaitForSelectorState.Visible });


        //     int optionsCount = await option.CountAsync();

        //     for (int i = 0; i < optionsCount; i++)
        //     {
        //     string? text = await option.Nth(i).TextContentAsync();
        //         if ((text?.Trim().ToLower() ?? "") == countryName.ToLower())
        //         {
        //             await option.Nth(i).ClickAsync(); // ✅ Use the same locator
        //             break;
        //         }
        //     }

        // }

        public async Task SearchCountryAndSelectAsync(string countryCode, string countryName)
{
    Console.WriteLine($"{countryCode}, {countryName}");

            // Simulate typing with delay (like pressSequentially in JS)
            // foreach (char c in countryCode)
            // {
            //     await _countryInput.TypeAsync(c.ToString(), new() { Delay = 100 });
            // }
            await _countryInput.FocusAsync();
await _countryInput.PressSequentiallyAsync(countryCode);


            // Wait for the dropdown to appear
            await _dropdown.WaitForAsync();

    // Count the number of button items in the dropdown
    var options = _dropdown.Locator("button");
    int optionsCount = await options.CountAsync();

    for (int i = 0; i < optionsCount; i++)
    {
        var option = _dropdown.Locator("button.ta-item").Nth(i);
        string? text = await option.TextContentAsync();

        if ((text?.Trim().ToLower() ?? "") == countryName.ToLower())
        {
            await _dropdown.Locator("button").Nth(i).ClickAsync();
            break;
        }
    }
}


        public async Task VerifyEmailIdAsync(string username)
        {
            await Expect(_emailId).ToHaveTextAsync(username);
        }

        public async Task<string> PlaceOrderClickAsync()
    {
        await _submit.ClickAsync();
        await Assertions.Expect(_orderConfirmationText).ToContainTextAsync(" Thankyou for the order. ");
        
        return await _orderId.TextContentAsync() ?? string.Empty;
    }
    }
}
