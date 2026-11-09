# Practical Task 1 – Web Elements and Locators

**Test case:** the user can search for a position based on criteria.
**Verified:** 2026-11-09, Selenium WebDriver + xUnit, Chrome 157. Result: **2/2 passed** (Java, Python).

## Project structure (Page Object)

| File | Contents |
|------|----------|
| `Pages/BasePage.cs` | Shared waits and clicks, the cookie banner (row 1), window switching |
| `Pages/HomePage.cs` | epam.com: open the site, "Careers" link (row 2) |
| `Pages/CareersPage.cs` | "Start your search here" (row 3) |
| `Pages/JobSearchPage.cs` | careers.epam.com/en/jobs: the search form, filters, results (rows 4–12) |
| `Pages/VacancyPage.cs` | Vacancy page text (row 13) |
| `Drivers/DriverFactory.cs` | ChromeDriver setup (`HEADLESS=1` support) |
| `Drivers/Diagnostics.cs` | Saves HTML, a screenshot and the URL when a test fails |
| `Tests/CareerSearchTests.cs` | The test itself |

## Locators table

| # | Task step | Element on the site | Locator (type) |
|---|-----------|---------------------|----------------|
| 1 | Click "Accept all" in the cookie banner | "Accept All" button (same ID on epam.com and careers.epam.com) | **ID:** `onetrust-accept-btn-handler` |
| 2 | Find the "Carriers" link and click it | "Careers" link | **LinkText:** `Careers` |
| 3 | Click "Start your search here" | "Start your search here" button (CSS shows the text in uppercase) | **PartialLinkText:** `START YOUR SEARCH` |
| 4 | *(helper element)* | Search form on careers.epam.com/en/jobs | **CSS:** `form[aria-label='form']` |
| 5 | Select a value in the "Location" field | Country dropdown | **ClassName:** `dropdown__input` (inside the search form) |
| 6 | Select "All Locations" (test parameter) | "All available countries" option | **XPath with operator:** `//div[contains(@class,'dropdown__option') and normalize-space(.)='{location}']` |
| 7 | Type a programming language in "Keywords" (test parameter) | "Search by Role or Keyword" input | **Name:** `search` |
| 8 | Select the "Remote" option | "Remote" checkbox in the Workplace type filter | **XPath with axes:** `//input[@name='vacancy_type-Remote']/following-sibling::label` |
| 9 | Click "Search" | "SEARCH" button | **XPath (relative):** `//button[@name='submit_search_box_button']` |
| 10 | *(results list)* | Job cards | **CSS:** `div[class^='List_list'] > div` |
| 11 | Find the latest element in the results | Last job card | **CSS with pseudo-class:** `div[class^='List_list'] > div:last-child` |
| 12 | Click "View and apply" | Title link of the last card (opens the vacancy) | **XPath (relative, from the card):** `.//a[@data-testid='job-card-link']` |
| 13 | Check that the language is on the page | Vacancy page `careers.epam.com/en/vacancy/...` | **TagName:** `body` (its text must contain the language) |

## Required locator types

| Required type | Row(s) |
|---------------|--------|
| ID | 1 |
| Name | 7 |
| ClassName | 5 |
| TagName | 13 |
| LinkText | 2 |
| PartialLinkText | 3 |
| CSS (with a pseudo-class) | 4, 10, 11 (`:last-child`) |
| XPath (relative path) | 9, 12 |
| XPath with an operator | 6 (`and`) |
| XPath with axes | 8 (`following-sibling::`) |

## Test parameters

| Parameter | Values used |
|-----------|-------------|
| `location` | `All available countries` |
| `language` | `Java`, `Python` |

## Differences between the task text and the live site

| Task says | Live site (Nov 2026) | How the test handles it |
|-----------|----------------------|-------------------------|
| "Carriers" link | Link text is **Careers** | `By.LinkText("Careers")` |
| "Start your search here" | Rendered in uppercase by CSS. Selenium matches the rendered text | `By.PartialLinkText("START YOUR SEARCH")` |
| Search form on epam.com | The button opens **careers.epam.com/en/jobs**, a separate site with its own cookie banner | The cookie banner is accepted again if it appears |
| "All Locations" | The option is named **All available countries** | Test parameter value |
| "View and apply" button | Cards have a title link (opens the vacancy) and an **Apply** button that needs a login | The last card's title link opens the vacancy page |
| Remote option | A filter checkbox. Results re-render after Search and can swallow an early click | The test retries the click until the URL has `vacancy_type=remote` |

## Test run

```
dotnet test
  Passed UserCanSearchPositionByCriteria(location: "All available countries", language: "Python") [9 s]
  Passed UserCanSearchPositionByCriteria(location: "All available countries", language: "Java")   [10 s]
Passed! - Failed: 0, Passed: 2, Skipped: 0, Total: 2
```

Run in a normal (headed) browser. With `HEADLESS=1`, epam.com currently shows a Cloudflare "Just a moment..." check and the test cannot start. If a run fails, the HTML, a screenshot and the URL are saved to `bin/Debug/net10.0/diagnostics/`.
