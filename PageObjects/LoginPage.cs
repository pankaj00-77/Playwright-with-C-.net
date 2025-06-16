using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightCSharp.PageObjects
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _signInButton;
        private readonly ILocator _userName;
        private readonly ILocator _password;

        public LoginPage(IPage page)
        {
            _page = page;
            _signInButton = _page.Locator("[value='Login']");
            _userName = _page.Locator("#userEmail");
            _password = _page.Locator("#userPassword");
        }

        public async Task GoToAsync()
        {
            await _page.GotoAsync("https://rahulshettyacademy.com/client");
        }

        public async Task ValidLoginAsync(string username, string password)
        {
            await _userName.FillAsync(username);
            await _password.FillAsync(password);
            await _signInButton.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); // waits until page settles
        }
    }
}
