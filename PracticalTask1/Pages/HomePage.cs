using OpenQA.Selenium;

namespace PracticalTask1.Pages;

public sealed class HomePage : BasePage
{
    private const string Url = "https://www.epam.com/";

    private static readonly By CareersLink = By.LinkText("Careers");    // LinkText

    public HomePage(IWebDriver driver) : base(driver)
    {
    }

    public HomePage Open()
    {
        Driver.Navigate().GoToUrl(Url);
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
