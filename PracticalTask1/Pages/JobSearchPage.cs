using OpenQA.Selenium;

namespace PracticalTask1.Pages;

public sealed class JobSearchPage : BasePage
{
    private const string RemoteFilterInUrl = "vacancy_type=remote";
    private static readonly TimeSpan FilterApplyTimeout = TimeSpan.FromSeconds(1);

    private static readonly By SearchForm = By.CssSelector("form[aria-label='form']");                        // CSS
    private static readonly By LocationField = By.ClassName("dropdown__input");                               // ClassName (inside SearchForm)
    private static readonly By KeywordInput = By.Name("search");                                              // Name
    private static readonly By RemoteCheckbox = By.Name("vacancy_type-Remote");                               // Name
    private static readonly By RemoteCheckboxLabel =
        By.XPath("//input[@name='vacancy_type-Remote']/following-sibling::label");                            // XPath with axes
    private static readonly By SearchButton = By.XPath("//button[@name='submit_search_box_button']");         // XPath (relative)
    private static readonly By ResultCards = By.CssSelector("div[class^='List_list'] > div");                 // CSS
    private static readonly By LastResultCard = By.CssSelector("div[class^='List_list'] > div:last-child");   // CSS with pseudo-class
    private static readonly By CardTitleLink = By.XPath(".//a[@data-testid='job-card-link']");               // XPath (relative, from the card)

    private string _keyword = string.Empty;

    public JobSearchPage(IWebDriver driver) : base(driver)
    {
    }

    public bool IsRemoteSelected => Driver.FindElement(RemoteCheckbox).Selected;

    private static By LocationOption(string location) =>
        By.XPath($"//div[contains(@class,'dropdown__option') and normalize-space(.)='{location}']");          // XPath with operator

    public JobSearchPage SelectLocation(string location)
    {
        var form = WaitVisible(SearchForm);
        Click(() => form.FindElement(LocationField));
        Click(LocationOption(location));
        return this;
    }

    public JobSearchPage EnterKeyword(string keyword)
    {
        var input = WaitVisible(KeywordInput);
        input.Clear();
        input.SendKeys(keyword);
        _keyword = keyword;
        return this;
    }

    public JobSearchPage Search()
    {
        Click(SearchButton);
        Wait.Until(d => d.Url.Contains($"search={_keyword}", StringComparison.OrdinalIgnoreCase));
        return this;
    }

    /// <summary>
    /// The list re-renders right after a search and can swallow an early click,
    /// so the click is retried until the filter appears in the URL.
    /// </summary>
    public JobSearchPage SelectRemote()
    {
        Wait.Until(_ =>
        {
            if (IsRemoteFilterApplied())
            {
                return true;
            }

            Click(RemoteCheckboxLabel);
            return WaitFor(IsRemoteFilterApplied, FilterApplyTimeout);
        });
        return this;
    }

    public VacancyPage OpenLastResult()
    {
        Wait.Until(d => d.FindElements(ResultCards).Count > 0);
        var lastCard = WaitVisible(LastResultCard);
        Click(() => lastCard.FindElement(CardTitleLink));
        SwitchToNewestWindow();
        return new VacancyPage(Driver);
    }

    private bool IsRemoteFilterApplied() =>
        Driver.Url.Contains(RemoteFilterInUrl, StringComparison.OrdinalIgnoreCase);
}
