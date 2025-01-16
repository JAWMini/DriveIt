using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class LoginEndToEndTests
{
    private const string BaseUrl = "https://localhost:7100"; // lub inny port

    [Fact]
    public void Login_WithInvalidCredentials_ShowsError()
    {
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl($"{BaseUrl}/Account/Login");

        // Czekamy 5s na załadowanie strony
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Wypełniamy formularz NIEpoprawnymi danymi
        driver.FindElement(By.CssSelector("input[placeholder='name@example.com']"))
              .SendKeys("fakeuser@example.com");
        driver.FindElement(By.CssSelector("input[placeholder='password']"))
              .SendKeys("WrongPassword123");

        // Klikamy "Log in"
        var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        // Czekamy na pojawienie się komunikatu błędu (np. z klasą alert-danger)
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElements(By.ClassName("alert-danger")).Count > 0);

        // Sprawdzamy, czy komunikat o błędzie faktycznie mówi o niepoprawnym logowaniu
        var errorElement = driver.FindElement(By.ClassName("alert-danger"));
        var errorText = errorElement.Text;
        Assert.Contains("Invalid login attempt", errorText, StringComparison.OrdinalIgnoreCase);

        driver.Quit();
    }

    [Fact]
    public void Login_WithValidCredentials_RedirectsToHomePage()
    {
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl($"{BaseUrl}/Account/Login");

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Wypełniamy formularz poprawnymi danymi
        driver.FindElement(By.CssSelector("input[placeholder='name@example.com']"))
              .SendKeys("janekmachalski1@gmail.com");
        driver.FindElement(By.CssSelector("input[placeholder='password']"))
              .SendKeys("abcA123.");

        // Klikamy "Log in"
        var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        // Czekamy na przekierowanie — np. na stronę główną "/"
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        // Warunek: URL zmienia się na stronę główną lub zawiera jej fragment
        // jeśli strona główna to np. "/"
        wait.Until(d => d.Url == $"{BaseUrl}/");
        // Alternatywnie: wait.Until(d => d.Url.Contains("/")); 
        // zależy od tego, jak wygląda docelowy URL

        // Możemy sprawdzić czy pojawia się np. nagłówek "Lista samochodów"
        var heading = driver.FindElement(By.TagName("h1")).Text;
        Assert.Contains("Lista samochodów", heading, StringComparison.OrdinalIgnoreCase);

        driver.Quit();
    }
}