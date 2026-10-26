using AutomationExercise.Tests.Pages;

namespace AutomationExercise.Tests.StepDefinitions;

[Binding]
public class HomeSteps
{
    private readonly HomePage _homePage ;
  
    public HomeSteps(HomePage homePage)
    {
        _homePage = homePage;
    }

    [When(@"I click on Signup\/Login link")]
    public async Task ClickOnSignupLoginLink()
    {
        await _homePage.SignupLoginLink.ClickAsync();
    }
}