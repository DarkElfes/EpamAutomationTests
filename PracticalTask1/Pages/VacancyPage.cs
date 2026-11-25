using OpenQA.Selenium;
using PracticalTask1.Config;

namespace PracticalTask1.Pages;

public sealed class VacancyPage : BasePage
{
    private static readonly By PageBody = By.TagName("body");    // TagName

    public VacancyPage(IWebDriver driver) : base(driver)
    {
        Wait.Until(d => d.Url.Contains("/vacancy/"));
    }

    public string BodyText => Driver.FindElement(PageBody).Text;

    public bool WaitForText(string text) =>
        WaitFor(() => BodyText.Contains(text, StringComparison.OrdinalIgnoreCase), TimeSpan.FromSeconds(ConfigReader.TimeoutSeconds));
}
