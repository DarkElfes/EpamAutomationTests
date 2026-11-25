using OpenQA.Selenium;

namespace PracticalTask1.Pages;

public sealed class HomePage(
    IWebDriver driver
    ) : BasePage(driver)
{
    private const string Url = "https://www.epam.com/";

    private static readonly By CareersLink = By.LinkText("Careers");


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
