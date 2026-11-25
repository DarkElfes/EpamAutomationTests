using OpenQA.Selenium;

namespace PracticalTask1.Drivers;

public static class Diagnostics
{
    /// <summary>Saves the page HTML, a screenshot and the URL to bin/.../diagnostics. Best-effort.</summary>
    public static void Save(IWebDriver driver, string name)
    {
        try
        {
            var dir = Path.Combine(AppContext.BaseDirectory, "diagnostics");
            Directory.CreateDirectory(dir);
            var safeName = string.Concat(name.Split(Path.GetInvalidFileNameChars()));
            File.WriteAllText(Path.Combine(dir, $"{safeName}.html"), driver.PageSource);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(dir, $"{safeName}.png"));
            File.WriteAllText(Path.Combine(dir, $"{safeName}.url.txt"), driver.Url);
        }
        catch (Exception ex) when (ex is WebDriverException or IOException or UnauthorizedAccessException)
        {
            // Diagnostics must never hide the original test failure
        }
    }
}
