using PracticalTask1.Drivers;
using PracticalTask1.Pages;

namespace PracticalTask1.Tests;

public sealed class CareerSearchTests : BaseTest
{
    [Theory]
    [InlineData("All available countries", "Java")]
    [InlineData("All available countries", "Python")]
    public void UserCanSearchPositionByCriteria(string location, string language)
    {
        try
        {
            HomePage homePage = new HomePage(Driver)
                .Open()
                .AcceptCookies();

            CareersPage careersPage = homePage.GoToCareers();

            JobSearchPage jobSearchPage = careersPage.StartSearch();
            jobSearchPage
                .SelectLocation(location)
                .EnterKeyword(language)
                .Search()
                .SelectRemote();

            Assert.True(jobSearchPage.IsRemoteSelected, "The 'Remote' filter is not selected.");

            VacancyPage vacancyPage = jobSearchPage.OpenLastResult();

            Assert.True(vacancyPage.WaitForText(language), $"The vacancy page does not mention '{language}'.");
        }
        catch
        {
            Diagnostics.Save(Driver, $"{location}_{language}");
            Dispose();
            throw;
        }
    }
}
