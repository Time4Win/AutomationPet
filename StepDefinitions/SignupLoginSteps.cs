using AutomationExercise.Tests.Contexts;
using AutomationExercise.Tests.Pages;
using Bogus;
using FluentAssertions;
using Microsoft.Playwright;

namespace AutomationExercise.Tests.StepDefinitions
{
    [Binding]
    public class SignupLoginSteps
    {
        private readonly IPage _page;
        private readonly LoginPage _loginPage;
        private readonly SignupPage _signupPage;
        private readonly RegistrationContext _context;
        private readonly Faker _faker;

        public SignupLoginSteps(IPage page, LoginPage loginPage, SignupPage signupPage, RegistrationContext context)
        {
            _page = page;
            _loginPage = loginPage;
            _signupPage = signupPage;
            _context = context;
            _faker = new Faker();
        }

        [Then(@"I'm navigated to '(.*)' page")]
        public async Task ThenIAmNavigatedToPage(string pageName)
        {
            pageName = pageName.ToLowerInvariant();
            
            await _page.WaitForURLAsync($"**/{pageName}");

            IBasePage page = pageName switch
            {
                "login" => _loginPage,
                "signup" => _signupPage,
                _ => throw new ArgumentOutOfRangeException($"Page name '{pageName}' is not supported in this step")
            };

            var isOnPage = await page.IsOnThisPageAsync();
            isOnPage.Should().BeTrue($"because the user should be navigated to the '{pageName}' page");
        }

        [When(@"I enter new unique signup user info")]
        public async Task WhenIEnterNewUniqueSignupUserInfo()
        {
            _context.FirstName = _faker.Name.FirstName();
            _context.UniqueName = $"{_context.FirstName}_{_faker.Random.Number(100, 999)}";
            _context.LastName = _faker.Name.LastName();
            _context.Email = _faker.Internet.Email(_context.FirstName, _context.LastName);
            
            await _loginPage.SignupNameInput.FillAsync(_context.UniqueName);
            await _loginPage.SignupEmailInput.FillAsync(_context.Email);
        }

        [When(@"I Press on Signup button")]
        public async Task WhenIPressOnSignupButton()
        {
            await _loginPage.SignupButton.ClickAsync();
        }

        [Then(@"Unique name and email showed filled up in Account Information")]
        public async Task ThenUniqueNameAndEmailShowedFilledUpInAccountInformation()
        {
            var actualName = await _signupPage.UniqueName.InputValueAsync();
            var actualEmail = await _signupPage.EmailInput.InputValueAsync();

            actualName.Should().Be(_context.UniqueName, "because the unique name should be auto-filled in the Account Info form");
            actualEmail.Should().Be(_context.Email, "because the unique email should be auto-filled in the Account Info form");
        }

        [When(@"I enter new password in Account Information")]
        public async Task WhenIEnterNewPasswordInAccountInformation()
        {
            _context.Password = _faker.Internet.Password(8);
           
            var passwordInput = _page.Locator("[data-qa='password']");
            await passwordInput.FillAsync(_context.Password);

            Console.WriteLine($"[TEST DATA] Unique Name: {_context.UniqueName}");
            Console.WriteLine($"[TEST DATA] Email: {_context.Email}");
            Console.WriteLine($"[TEST DATA] Password: {_context.Password}");
        }

        [When(@"I enter address information")]
        public async Task WhenIEnterAddressInformation()
        {
            var address = _faker.Address.StreetAddress();
            var randomCountry = _faker.PickRandom(SignupPage.AvailableCountries);
            var state = _faker.Address.State();
            var city = _faker.Address.City();
            var zipcode = _faker.Address.ZipCode();
            var mobileNumber = _faker.Phone.PhoneNumber();

            await _signupPage.FirstNameInput.FillAsync(_context.FirstName);
            await _signupPage.LastNameInput.FillAsync(_context.LastName);
            await _signupPage.AddressInput.FillAsync(address);
            await _signupPage.CountrySelect.SelectOptionAsync(randomCountry);
            await _signupPage.StateInput.FillAsync(state);
            await _signupPage.CityInput.FillAsync(city);
            await _signupPage.ZipcodeInputs.FillAsync(zipcode);
            await _signupPage.MobileNumberInput.FillAsync(mobileNumber);
        }

        [When(@"I click create account")]
        public async Task WhenIClickCreateAccount()
        {
            await _signupPage.CreateAccountButton.ClickAsync();
        }

        [Then(@"I should see the '(.*)' confirmation message")]
        public async Task ThenIShouldSeeTheConfirmationMessage(string expectedMessage)
        {
            await _signupPage.AccountCreatedHeader.WaitForAsync();
            var headerText = await _signupPage.AccountCreatedHeader.InnerTextAsync();

            headerText.Should().Contain(expectedMessage);
        }
    }
}