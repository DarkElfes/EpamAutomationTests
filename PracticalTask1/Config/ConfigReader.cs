using Microsoft.Extensions.Configuration;

namespace PracticalTask1.Config;

internal static class ConfigReader
{
    private readonly static IConfiguration _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

    public static string BaseUrl =>
        _config["BaseUrl"] ?? throw new InvalidOperationException("BaseUrl is not configured.");

    public static string Browser =>
        _config["Browser"] ?? throw new InvalidOperationException("Browser is not configured.");

    public static short TimeoutSeconds =>
        short.Parse(_config["TimeoutSeconds"] ?? throw new InvalidOperationException("TimeoutSeconds is not configured."));

    public static BrowserProfile GetActiveBrowser() =>
        _config.GetSection("Browsers").GetSection(Browser).Get<BrowserProfile>()
            ?? throw new InvalidOperationException($"Browser profile '{Browser}' not found.");
}
