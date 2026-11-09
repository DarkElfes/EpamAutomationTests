using OpenQA.Selenium;

namespace PracticalTask1.Pages;

public sealed class CareersPage : BasePage
{
    // The text is rendered in uppercase by CSS, and Selenium matches the rendered text
    private static readonly By StartSearchButton = By.PartialLinkText("START YOUR SEARCH");    // PartialLinkText

    public CareersPage(IWebDriver driver) : base(driver)
    {
    }

    /// <summary>Opens careers.epam.com/en/jobs (a separate site with its own cookie banner).</summary>
    public JobSearchPage StartSearch()
    {
        Click(StartSearchButton);
        SwitchToNewestWindow();
        AcceptCookiesIfShown();
        return new JobSearchPage(Driver);
    }
}
