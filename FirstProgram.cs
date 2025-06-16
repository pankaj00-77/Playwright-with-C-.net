// using Microsoft.Playwright;

// class Program
// {
//     public static async Task Main()
//     {
//         using var playwright = await Playwright.CreateAsync();
//         var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//         {
//             Headless = false
//         });
//         var page = await browser.NewPageAsync();
//         await page.GotoAsync("https://google.com");
//         Console.WriteLine("✅ Launched successfully!");
//     }
// }
