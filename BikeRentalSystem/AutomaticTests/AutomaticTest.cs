using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomaticTests
{
    public class AutomaticTest : IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomaticTest()
        {
            _driver = new ChromeDriver();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void LoginTest()
        {
            _driver.Navigate().GoToUrl("https://localhost:7178/Identity/Account/Login");
            _driver.FindElement(By.Id("Input_Email")).SendKeys("user@example.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("User123!");
            _driver.FindElement(By.Id("login-submit")).Click();
            Assert.Equal("https://localhost:7178/", _driver.Url);
        }
        [Fact]
        public void ReservationTest()
        {
            LoginTest();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driver.FindElement(By.LinkText("Rent a vehicle")).Click();
            Assert.Equal("https://localhost:7178/Reservation/Create", _driver.Url);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            IWebElement pickUpDateField = _driver.FindElement(By.Id("PickupDate"));
            pickUpDateField.Clear();
            pickUpDateField.SendKeys("2023-06-12T10:00");
            IWebElement returnDateField = _driver.FindElement(By.Id("ReturnDate"));
            returnDateField.Clear();
            returnDateField.SendKeys("2023-06-14T10:00");
            IWebElement vehicleSelection = _driver.FindElement(By.Id("Vehicles"));
            SelectElement selectElement = new SelectElement(vehicleSelection);
            selectElement.SelectByText("Xiaomi - Hulajnoga - 35");
            IWebElement selectedOption = selectElement.SelectedOption;
            _driver.FindElement(By.CssSelector("button.btn-primary")).Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Assert.Equal("https://localhost:7178/Reservation/CreateReservation", _driver.Url);
        }
    }
}