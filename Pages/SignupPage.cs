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

    public ILocator UniqueName => _page.GetByTestId("name");
    public ILocator EmailInput => _page.GetByTestId("email");
    public ILocator PasswordInput => _page.GetByTestId("password");
    public ILocator FirstNameInput => _page.GetByTestId("first_name");
    public ILocator LastNameInput => _page.GetByTestId("last_name");
    public ILocator AddressInput => _page.GetByTestId("address");
    public ILocator CountrySelect => _page.GetByTestId("country");
    public ILocator StateInput => _page.GetByTestId("state");
    public ILocator CityInput => _page.GetByTestId("city");
    public ILocator ZipcodeInputs => _page.GetByTestId("zipcode");
    public ILocator MobileNumberInput => _page.GetByTestId("mobile_number");
    public ILocator CreateAccountButton => _page.GetByTestId("create-account");
    public ILocator AccountCreatedHeader => _page.GetByTestId("account-created");

    public async Task<bool> IsOnThisPageAsync()
    {
        var headerLocator = _page.Locator("h2:has-text('Enter Account Information')");

        return await Waiter.WaitForCondition(async () => await headerLocator.IsVisibleAsync());
    }
}