using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using PracticalTask1.Config;

namespace PracticalTask1.Utils;

internal static class DriverFactory
{
    public static IWebDriver Create(BrowserProfile profile) =>
        profile.Type.ToLowerInvariant() switch
        {
            "chrome" => CreateChrome(profile),
            "edge" => CreateEdge(profile),
            "firefox" => CreateFirefox(profile),
            _ => throw new ArgumentException($"Unsupported browser type: {profile.Type}")
        };

    private static ChromeDriver CreateChrome(BrowserProfile profile)
    {
        var options = new ChromeOptions();
        options.AddArguments(profile.Arguments);
        return new ChromeDriver(options);
    }

    private static EdgeDriver CreateEdge(BrowserProfile profile)
    {
        var options = new EdgeOptions();
        options.AddArguments(profile.Arguments);
        return new EdgeDriver(options);
    }

    private static FirefoxDriver CreateFirefox(BrowserProfile profile)
    {
        var options = new FirefoxOptions();
        options.AddArguments(profile.Arguments);
        return new FirefoxDriver(options);
    }

}
