using OpenQA.Selenium;
using PracticalTask1.Config;

namespace PracticalTask1.Pages;

public sealed class HomePage(
    IWebDriver driver
    ) : BasePage(driver)
{
    private static readonly By CareersLink = By.LinkText("Careers");

    public HomePage Open()
    {
        Driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
        return this;
    }

    public HomePage AcceptCookies()
    {
        AcceptCookiesBanner();
        return this;
    }

    public CareersPage GoToCareers()
    {
        Click(CareersLink);
        return new CareersPage(Driver);
    }
}
