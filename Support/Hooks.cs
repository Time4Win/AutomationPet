using Microsoft.Playwright;
using Reqnroll.BoDi;

namespace AutomationExercise.Tests.Support;

[Binding]
public class Hooks
{
    private static IPlaywright _playwright;
    private static IBrowser _browser;

    private readonly IObjectContainer _container;
    private IBrowserContext _browserContext;
    private IPage _page;

    public Hooks(ScenarioContext scenarioContext, IObjectContainer container)
    {
        _container = container;
    }

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        Program.Main(["install"]);
        _playwright = await Playwright.CreateAsync();

        // Launch Chromium instance
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 50       // Slows down operations for easier debugging
        });
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _browserContext = await _browser.NewContextAsync();

        _page = await _browserContext.NewPageAsync();
        _container.RegisterInstanceAs(_page);

        await OpenAutomationExerciseWebSite();
    }

    private async Task OpenAutomationExerciseWebSite()
    {
        await _page.GotoAsync("https://automationexercise.com/");
        
        var consentButton = _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Consent" });
        if (await consentButton.CountAsync() > 0 && await consentButton.IsVisibleAsync())
        {
            await consentButton.ClickAsync();
        }
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_page != null)
        {
            await _page.CloseAsync();
        }
        if (_browserContext != null)
        {
            await _browserContext.CloseAsync();
        }
    }

    [AfterTestRun]
    public static async Task AfterTestRun()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
        _playwright?.Dispose();
    }
}