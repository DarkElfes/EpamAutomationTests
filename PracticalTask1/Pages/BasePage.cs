using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PracticalTask1.Pages;

public abstract class BasePage
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(20);
    private static readonly TimeSpan CookieBannerTimeout = TimeSpan.FromSeconds(5);

    // Same cookie banner (OneTrust) on epam.com and careers.epam.com
    private static readonly By AcceptCookiesButton = By.Id("onetrust-accept-btn-handler");

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, DefaultTimeout);
        Wait.IgnoreExceptionTypes(
            typeof(NoSuchElementException),
            typeof(StaleElementReferenceException),
            typeof(ElementNotInteractableException));
    }

    protected IWebDriver Driver { get; }

    protected WebDriverWait Wait { get; }

    protected void AcceptCookiesBanner() => Click(AcceptCookiesButton);

    protected void AcceptCookiesIfShown()
    {
        try
        {
            var shortWait = new WebDriverWait(Driver, CookieBannerTimeout);
            var button = shortWait.Until(d => d.FindElements(AcceptCookiesButton).FirstOrDefault(e => e.Displayed));
            button?.Click();
        }
        catch (WebDriverTimeoutException)
        {
            // Banner not shown - nothing to accept
        }
    }

    protected IWebElement WaitVisible(By by) =>
        Wait.Until(d =>
        {
            var element = d.FindElement(by);
            return element.Displayed ? element : null;
        });

    protected void Click(By by) => Click(() => Driver.FindElement(by));

    protected void Click(Func<IWebElement> findElement) =>
        Wait.Until(_ =>
        {
            findElement().Click();
            return true;
        });

    protected bool WaitFor(Func<bool> condition, TimeSpan timeout)
    {
        try
        {
            return new WebDriverWait(Driver, timeout).Until(_ => condition());
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
}
