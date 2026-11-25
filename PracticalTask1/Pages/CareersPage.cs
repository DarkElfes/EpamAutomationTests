using OpenQA.Selenium;

namespace PracticalTask1.Pages;

public sealed class CareersPage(
    IWebDriver driver
    ) : BasePage(driver)
{
    private static readonly By StartSearchButton = By.PartialLinkText("START YOUR SEARCH");

    /// <summary>Opens careers.epam.com/en/jobs (a separate site with its own cookie banner).</summary>
    public JobSearchPage StartSearch()
    {
        Click(StartSearchButton);
        AcceptCookiesIfShown();
        return new JobSearchPage(Driver);
    }
}
