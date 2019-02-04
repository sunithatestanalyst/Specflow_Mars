using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using SpecflowPages;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;
using static SpecflowPages.CommonMethods;


namespace SpecflowTests.AcceptanceTest
{
    [Binding]
    public class SpecFlowFeature1Steps : Utils.Start
    {
        //Background

        [Given(@"i am in Profile page")]
        public void GivenIAmInProfilePage()
        {
            //Wait
            Thread.Sleep(1500);

            // Click on Profile tab
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[1]/div/a[2]")).Click();
        }



        //Scenario 1
        [Given(@"I clicked on  Add New button under language page")]
        public void GivenIClickedOnAddNewButtonUnderLanguagePage()
        {
            //Start the Reports
            CommonMethods.ExtentReports();
            Thread.Sleep(1000);
            CommonMethods.test = CommonMethods.extent.StartTest("Add a Language");

            try
            {
                //Check 'Add new' is present
                bool status = CommonMethods.ElementVisible(Driver.driver, "XPath", "//div[@class='ui bottom attached tab segment active tooltip-target']//div[contains(@class,'ui teal button')][contains(text(),'Add New')]");

                if (status)
                {
                    // Add New
                    IWebElement AddNew = Driver.driver.FindElement(By.XPath("//div[@class='ui bottom attached tab segment active tooltip-target']//div[contains(@class,'ui teal button')][contains(text(),'Add New')]"));
                    //Click on Add New button
                    AddNew.Click();
                }
                else
                {
                    CommonMethods.test.Log(LogStatus.Fail, "Add New Button Not Visible, Already maximum languages added");
                }

            }
            catch(Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Error in clicking Add New button"+ e.Message);
            }
          

           
        }

        [When(@"I add a language(.*) and its level(.*)")]
        public void WhenIAddALanguageAndItsLevel(string lang,string level)
        {
            try
            {
                ScenarioContext.Current["lang"] = lang;
                //Enter Language
                Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[1]/input")).SendKeys(lang);

                //Click on Language Level
                Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[2]/select")).Click();

                //Choose the language level

                IWebElement leveldropdown = Driver.driver.FindElement(By.XPath("//select[@name='level']"));
                SelectElement select = new SelectElement(leveldropdown);
                select.SelectByText(level);


                //Click on Add button
                Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[3]/input[1]")).Click();

            }
            catch(Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Error in Adding language "+ e.Message);
            }

        }

        [Then(@"that language should be displayed under language listings")]
        public void ThenThatLanguageShouldBeDisplayedUnderLanguageListings()
        {
            try
            {
                

                Thread.Sleep(1000);
                var lang = ScenarioContext.Current["lang"];
                string ExpectedLanguage = lang.ToString();
                string ActualLanguage = Driver.driver.FindElement(By.XPath("//td[contains(text(),'" + ExpectedLanguage + "')]")).Text; 
                Thread.Sleep(500);
                if (ExpectedLanguage == ActualLanguage)
                {
                    CommonMethods.test.Log(LogStatus.Pass, "Test Passed, Added a Language Successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageAdded");
                }

                else
                    CommonMethods.test.Log(LogStatus.Fail, "Test Failed");

            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed"+ e.Message);
            }


        }

        // Scenario 2
        [Given(@"I clicked on Edit button of a language added (.*)")]
        public void GivenIClickedOnEditButtonOfALanguageAdded(string EditLanguage)
        {
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Edit a Language");
            try
            {
                for (int i = 1; i <= 4; i++)
                {
                    string Languageinlist = Driver.driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[2]/div[1]/div[2]/div[1]/table[1]/tbody[" + i + "]/tr[1]/td[1]")).Text;
                    if (Languageinlist == EditLanguage)
                    {
                        Driver.driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[2]/div[1]/div[2]/div[1]/table[1]/tbody[" + i + "]/tr[1]/td[3]/span[1]/i[1]")).Click();
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Error in editing language" + e.Message);
            }
            
         }


        [When(@"I update language(.*) and its level(.*)")]
        public void WhenIUpdateLanguageAndItsLevel(string Editlang, string langlevel)
        {

            try
            {
                //Enter Language
                IWebElement Language = Driver.driver.FindElement(By.XPath("//input[@placeholder='Add Language']"));
                Language.Clear();
                Language.SendKeys(Editlang);
                ScenarioContext.Current["Langua"] = Editlang;

                //Click on Language Level
                IWebElement LanguageLevel = Driver.driver.FindElement(By.XPath("//select[@name='level']"));
                SelectElement Level = new SelectElement(LanguageLevel);
                Level.SelectByText(langlevel);
                ScenarioContext.Current["level"] = langlevel;

                //Click on update button
                Driver.driver.FindElement(By.XPath("//input[@value='Update']")).Click();
            }
            catch(Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Language not found to edit", e.Message);
                SaveScreenShotClass.SaveScreenshot(Driver.driver, "Languagenotfound");
            }

        }
        [Then(@"Language is edited propertly and displayed under listings")]
        public void ThenLanguageIsEditedPropertlyAndDisplayedUnderListings()
        {
            try
            {
                //Start the Reports
                Thread.Sleep(1000);
                var Expected = ScenarioContext.Current["Langua"];
                string ExpectedValue = Expected.ToString();
                string ActualValue = Driver.driver.FindElement(By.XPath("//td[contains(text(),'"+ ExpectedValue + "')]")).Text;
                Thread.Sleep(500);
                if (ExpectedValue == ActualValue)
                {
                    CommonMethods.test.Log(LogStatus.Pass, "Test Passed, Language Edited Successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "Language Edited");
                }

                else
                    CommonMethods.test.Log(LogStatus.Fail, "Language edit Failed");


                var level = ScenarioContext.Current["level"];
                string ExpectedLanguageLevel = level.ToString();
                string ActualLanguageLevel = Driver.driver.FindElement(By.XPath("//td[contains(text(),'"+ ExpectedLanguageLevel +"')]")).Text;
                if(ExpectedLanguageLevel== ActualLanguageLevel)
                {
                    CommonMethods.test.Log(LogStatus.Pass, "Test Passed, Languagelevel Edited Successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguagelevelEdited");
                }
                else
                {
                    CommonMethods.test.Log(LogStatus.Fail, "Language level edit Failed");
                }
            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Language  edit Failed", e.Message);
            }

        }

        //scenario 3 delete a language
        [Given(@"I click on Delete button of a language(.*)")]
        public void GivenIClickOnDeleteButtonOfALanguage(string ToDelete)
        {
            CommonMethods.ExtentReports();
            CommonMethods.test = CommonMethods.extent.StartTest("Delete a  Language");
            try
            {
                ScenarioContext.Current["ToDelete"] = ToDelete;
                for (int j = 1; j <= 4; j++)
                {
                    string LanguageToDelete = Driver.driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[2]/div[1]/div[2]/div[1]/table[1]/tbody[" + j + "]/tr[1]/td[1]")).Text;
                    if (LanguageToDelete == ToDelete)
                    {
                        Driver.driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[2]/div[1]/div[2]/div[1]/table[1]/tbody[" + j + "]/tr[1]/td[3]/span[2]/i[1]")).Click();
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Language not found" + e.Message);
            }

        }

        [Then(@"language is deleted is from the listings")]
        public void ThenLanguageIsDeletedIsFromTheListings()
       {
            try
            {
                var langtodelete = ScenarioContext.Current["ToDelete"];
                string ExpectedLanguageDeleted = langtodelete.ToString();
                bool LanguageDeleted = CommonMethods.ElementVisible(Driver.driver, "XPath", "//td[contains(text(),'" + ExpectedLanguageDeleted + "')]");
                if (LanguageDeleted)
                {
                    CommonMethods.test.Log(LogStatus.Fail, "Language Delete Failed");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageDeleteFail");

                }

                else
                {
                    CommonMethods.test.Log(LogStatus.Pass, "Language Deleted successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageDeletedSuccessfully");
                }
            }
            catch(Exception e)
            {
               CommonMethods.test.Log(LogStatus.Fail, "Language Delete Failed"+e.Message);
            }
        }





    }
}
