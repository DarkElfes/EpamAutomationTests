using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PracticalTask1.Drivers;

public static class DriverFactory
{
    public static IWebDriver CreateChrome()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-blink-features=AutomationControlled");
        if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }

        return new ChromeDriver(options);
    }
}
