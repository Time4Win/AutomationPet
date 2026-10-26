using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    public ILocator SignupLoginLink => _page.Locator("a[href='/login']");
}