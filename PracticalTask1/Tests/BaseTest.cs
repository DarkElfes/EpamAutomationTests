using OpenQA.Selenium;
using PracticalTask1.Config;
using PracticalTask1.Utils;

namespace PracticalTask1.Tests;

public abstract class BaseTest : IDisposable
{
    protected IWebDriver Driver;
    private bool disposedValue;

    protected BaseTest()
    {
        Driver = DriverFactory.Create(ConfigReader.GetActiveBrowser());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing && Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            disposedValue = true;
        }
    }

    ~BaseTest()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
