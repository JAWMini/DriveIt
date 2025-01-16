using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

public class HomePageEndToEndTests
{
    private const string BaseUrl = "https://localhost:7100";

    [Fact]
    public void CanLoadHomePageAndDisplayCars()
    {
        // Setup Selenium WebDriver
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl(BaseUrl);

        // Wait for page to load
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Verify page title
        var title = driver.Title;
        Assert.Equal("Home", title);

        // Verify page header
        var header = driver.FindElement(By.TagName("h1")).Text;
        Assert.Equal("Lista samochodów", header);

        // Verify the grid exists
        var grid = driver.FindElement(By.ClassName("table"));
        Assert.NotNull(grid);

        // Verify at least one car is displayed
        var rows = grid.FindElements(By.TagName("tr"));
        Assert.True(rows.Count > 1, "Powinna byæ przynajmniej jedna linia w tabeli (samochód).");

        // Close the browser
        driver.Quit();
    }

    [Fact]
    public void CanApplyFiltersAndSeeResults()
    {
        // Setup Selenium WebDriver
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl(BaseUrl);

        // Wait for page to load
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Verify filter section exists
        var brandFilter = driver.FindElement(By.XPath("//h4[text()='Marka i model']"));
        Assert.NotNull(brandFilter);

        // Expand brand filter
        brandFilter.Click();

        // Select a brand checkbox
        var brandCheckbox = driver.FindElement(By.XPath("//input[@type='checkbox' and @id='Toyota']"));
        brandCheckbox.Click();

        // Wait for grid to refresh
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

        // Verify grid displays filtered results
        var grid = driver.FindElement(By.ClassName("table"));
        var rows = grid.FindElements(By.TagName("tr"));
        Assert.True(rows.Count > 1, "Powinna byæ przynajmniej jedna linia w tabeli po zastosowaniu filtra.");

        // Close the browser
        driver.Quit();
    }
}