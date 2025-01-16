using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Xunit;

public class RegisterPageTests
{
    private const string BaseUrl = "https://localhost:7100";


    [Fact]
    public void CanRegisterAccountOrShowErrorForExistingAccount()
    {
        // Setup Selenium WebDriver
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl($"{BaseUrl}/Account/Register");

        // Wait for page to load
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Fill out the registration form
        driver.FindElement(By.CssSelector("input[placeholder='First Name']")).SendKeys("Test");
        driver.FindElement(By.CssSelector("input[placeholder='Last Name']")).SendKeys("User");
        /*var dateField = driver.FindElement(By.CssSelector("input[placeholder='Date of Birth']"));
        dateField.SendKeys("01");
        dateField.SendKeys("01");
        dateField.SendKeys("1990");*/
       // driver.FindElement(By.CssSelector("input[placeholder='Driver License Year']")).SendKeys("2010");
        driver.FindElement(By.CssSelector("input[placeholder='name@example.com']")).SendKeys("janekmachalski1@gmail.com");
        driver.FindElements(By.CssSelector("input[placeholder='password']"))
      .First()
      .SendKeys("StrongP@ssw0rd");

        driver.FindElements(By.CssSelector("input[placeholder='password']"))
              .Last()
              .SendKeys("StrongP@ssw0rd");

        Thread.Sleep(5000);

        // Submit the form
        var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        submitButton.Click();

        Thread.Sleep(5000);

        // Wait for navigation or status message
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.Url.Contains("/Account/RegisterConfirmation") || d.FindElements(By.ClassName("text-danger")).Count > 0);

        // Check if redirected to the confirmation page
        if (driver.Url.Contains("/Account/RegisterConfirmation"))
        {
            // Verify confirmation page
            var confirmationHeader = driver.FindElement(By.TagName("h1")).Text;
            Assert.Equal("Register confirmation", confirmationHeader);

            var confirmationMessage = driver.FindElement(By.TagName("p")).Text;
            Assert.Contains("Please check your email to confirm your account.", confirmationMessage);
        }
        else
        {
            // Verify error message
            var errorMessages = driver.FindElements(By.ClassName("text-danger"));
            Assert.Contains(errorMessages, e => e.Text.Contains("already exists", StringComparison.OrdinalIgnoreCase));
        }

        // Close the browser
        driver.Quit();
    }

}