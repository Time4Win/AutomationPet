using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class SignupPage : IBasePage
{
    private readonly IPage _page;

    public SignupPage(IPage page)
    {
        _page = page;
    }

    public static readonly string[] AvailableCountries =
    [
        "India",
        "United States",
        "Canada",
        "Australia",
        "Israel",
        "New Zealand",
        "Singapore"
    ];

    public ILocator UniqueName => _page.Locator("[data-qa='name']");
    public ILocator EmailInput => _page.Locator("[data-qa='email']");
    public ILocator PasswordInput => _page.Locator("[data-qa='password']");
    public ILocator FirstNameInput => _page.Locator("[data-qa='first_name']");
    public ILocator LastNameInput => _page.Locator("[data-qa='last_name']");
    public ILocator AddressInput => _page.Locator("[data-qa='address']");
    public ILocator CountrySelect => _page.Locator("[data-qa='country']");
    public ILocator StateInput => _page.Locator("[data-qa='state']");
    public ILocator CityInput => _page.Locator("[data-qa='city']");
    public ILocator ZipcodeInputs => _page.Locator("[data-qa='zipcode']");
    public ILocator MobileNumberInput => _page.Locator("[data-qa='mobile_number']");
    public ILocator CreateAccountButton => _page.Locator("[data-qa='create-account']");
    public ILocator AccountCreatedHeader => _page.Locator("[data-qa='account-created']");
    
    public async Task<bool> IsOnThisPageAsync()
    {
        var headerLocator = _page.Locator("h2:has-text('Enter Account Information')");

        return await headerLocator.IsVisibleAsync();
    }
}