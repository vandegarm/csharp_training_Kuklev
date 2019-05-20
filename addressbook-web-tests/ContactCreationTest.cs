using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTest
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void ContactCreation()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToNewContactPage();

            ContactData contact = new ContactData
            {
                Firstname = "Vulfried",
                Middlename = "Kor",
                Lastname = "Tarren",
                Nickname = "Vulf",
                //Testphoto = "C:\Users\admin\Desktop\decka\Bebop.jpg",
                Title = "Captain",
                Company = "WolfBrigade",
                Address = "Mars",
                Home = "123-456-78",
                Mobile = "456-789-12",
                Work = "789-123-45",
                Fax = "890-123-456",
                Email = "vulfried@yandex.ru",
                Email2 = "vulfried1@yandex.ru",
                Email3 = "vulfried2@yandex.ru",
                Homepage = "vulfried.ru",
                Birthyear = "1984",
                Address2 = "Mars City",
                Phone2 = "147-852-39",
                Notes = "Freedom for mars"
            };

            EnterContactFieldValues(contact);
            Logout();

        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        private void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        private void GoToNewContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        private void EnterContactFieldValues(ContactData contact)
        {
            SendKeys(By.Name("firstname"), contact.Firstname);
            SendKeys(By.Name("middlename"), contact.Middlename);
            SendKeys(By.Name("lastname"), contact.Lastname);
            SendKeys(By.Name("nickname"), contact.Nickname);
            //SendKeys(By.Name("photo"), contact.Testphoto);
            SendKeys(By.Name("title"), contact.Title);
            SendKeys(By.Name("company"), contact.Company);
            SendKeys(By.Name("address"), contact.Address);
            SendKeys(By.Name("home"), contact.Home);
            SendKeys(By.Name("mobile"), contact.Mobile);
            SendKeys(By.Name("work"), contact.Work);
            SendKeys(By.Name("fax"), contact.Fax);
            SendKeys(By.Name("email"), contact.Email);
            SendKeys(By.Name("email2"), contact.Email2);
            SendKeys(By.Name("email3"), contact.Email3);
            SendKeys(By.Name("homepage"), contact.Homepage);
            SendKeys(By.Name("byear"), contact.Birthyear);

            driver.FindElement(By.Name("bday")).Click();
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText("6");
            driver.FindElement(By.CssSelector("option[value=\"6\"]")).Click();
            driver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText("November");
            driver.FindElement(By.CssSelector("option[value=\"November\"]")).Click();
            driver.FindElement(By.Name("new_group")).Click();
            new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText("NewTestDataName");
            driver.FindElement(By.CssSelector("option[value=\"41\"]")).Click();

            SendKeys(By.Name("address2"), contact.Address2);
            SendKeys(By.Name("phone2"), contact.Phone2);
            SendKeys(By.Name("notes"), contact.Notes);

            driver.FindElement(By.XPath("//input[21]")).Click();
        }

        private void SendKeys(By selector, string value)
        {
            driver.FindElement(selector).Clear();
            driver.FindElement(selector).SendKeys(value);
        }

        private void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
