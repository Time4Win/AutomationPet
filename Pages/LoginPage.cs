using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class LoginPage : IBasePage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public ILocator SignupNameInput => _page.Locator("[data-qa='signup-name']");
    public ILocator SignupEmailInput => _page.Locator("[data-qa='signup-email']");
    public ILocator SignupButton => _page.Locator("[data-qa='signup-button']");

    public async Task<bool> IsOnThisPageAsync()
    {
        var loginHeader = _page.Locator("h2:has-text('Login to your account')");
        var signupHeader = _page.Locator("h2:has-text('New User Signup!')");

        return await Waiter.WaitForCondition(async () =>
            await loginHeader.IsVisibleAsync() && await signupHeader.IsVisibleAsync());
    }
}