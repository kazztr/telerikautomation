using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;

using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Community.Utilities;
using System.Threading;
using Community.ObjectRepo.Article;
using Community.ObjectRepo.EditModeCommon;

namespace Community.Article
{
    /// <summary>
    /// Summary description for Article
    /// </summary>
    [TestClass]
    public class Article : BaseTest
    {

        #region [Setup / TearDown]

        private TestContext testContextInstance = null;
        /// <summary>
        ///Gets or sets the VS test context which provides
        ///information about and functionality for the
        ///current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        //Initialize Settings and the Manager
        private Settings mySettings;
        private Manager myManager;

        string _Url;
        string _Uname;
        string _Password;
        string _PageName;
        string _Heading;
        string _Clubs;
        string _Introduction;
        string _Description;
        IList<string> _ClubsList;

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }


        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            #region WebAii Initialization

            // Initializes WebAii manager to be used by the test case.
            // If a WebAii configuration section exists, settings will be
            // loaded from it. Otherwise, will create a default settings
            // object with system defaults.
            //
            // Note: We are passing in a delegate to the VisualStudio
            // testContext.WriteLine() method in addition to the Visual Studio
            // TestLogs directory as our log location. This way any logging
            // done from WebAii (i.e. Manager.Log.WriteLine()) is
            // automatically logged to the VisualStudio test log and
            // the WebAii log file is placed in the same location as VS logs.
            //
            // If you do not care about unifying the log, then you can simply
            // initialize the test by calling Initialize() with no parameters;
            // that will cause the log location to be picked up from the config
            // file if it exists or will use the default system settings (C:\WebAiiLog\)
            // You can also use Initialize(LogLocation) to set a specific log
            // location for this test.

            // Pass in 'true' to recycle the browser between test methods
            Initialize(false, this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.Web.DefaultBrowser = BrowserType.FireFox;

            // Now call Initialize again with your updated settings object
            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            */

            // Set the current test method. This is needed for WebAii to discover
            // its custom TestAttributes set on methods and classes.
            // This method should always exist in [TestInitialize()] method.
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.FireFox;
            myManager = new Manager(mySettings);
            myManager.Start();
            myManager.LaunchNewBrowser();


        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Article.csv", "Article#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Article.csv")]
        public void TestMethod_Article()
        {
            ReadData();

            //Login
            //Login to the system
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);

            myManager.ActiveBrowser.NavigateTo(_Url + "/customer/");
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(5000);

            //Click On episerverButton
            HtmlUnorderedList Episerverlink = myManager.ActiveBrowser.Find.ById<HtmlUnorderedList>("epi-quickNavigator");
            Episerverlink.MouseClick();
            //it takes more than 15seconds to load the edit mode
            Thread.Sleep(30000);
            myManager.ActiveBrowser.Refresh();
            Thread.Sleep(25000);

            //Click on left sub tree
            ObjEditMode editmode = new ObjEditMode(myManager);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan PagesTree = editmode.PagesTree.As<HtmlSpan>();
            PagesTree.MouseClick();

            //Click on leftmenu anchor 
            
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan PagesTreePin = editmode.PagesTreePin.As<HtmlSpan>();
            PagesTreePin.MouseClick();

            //-----------------------------------------------------------------------------

            //CLick on customer Branch to create a new file
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan NewpageDropDown = editmode.CustomerNewDropdown.As<HtmlSpan>();
            NewpageDropDown.MouseClick();


            //Click On New Page
            Thread.Sleep(3000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            //Element newPage = editmode.CustomerNewPage;
            HtmlTableCell newPage = editmode.CustomerNewPage.As<HtmlTableCell>(); ;
            //myManager.ActiveBrowser.Actions.Click(newPage);
            newPage.MouseClick();


            //Find The Community List
            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlDiv CommunityArea = myManager.ActiveBrowser.Find.ById("dijit__KeyNavContainer_1").As<HtmlDiv>(); 
            HtmlUnorderedList CommunityPages = CommunityArea.Find.AllByTagName("ul")[0].As<HtmlUnorderedList>();
            System.Collections.ObjectModel.ReadOnlyCollection<HtmlListItem> CommunityItems = CommunityPages.Find.AllByTagName<HtmlListItem>("li");

            foreach (HtmlListItem item in CommunityItems)
            {
                if (item.InnerText.Contains("[Community] Article"))
                {
                    item.MouseClick();
                    Thread.Sleep(2000);
                    myManager.ActiveBrowser.RefreshDomTree();
                    CreateArticle(_Heading, _PageName, _ClubsList, _Introduction, _Description);
                    Thread.Sleep(20000);
                    ValidateArticle(_Heading);
                    break;
                }
            }
        }

        public void ValidateArticle(string Heading)
        {
            CommonFunctions.ValideActivityStream(myManager, myManager.ActiveBrowser, _Url, Heading, "CUSTOMER");
        }

        public void CreateArticle(string Heading, string Name, IList<string> ClubsList, string Introduction, string Description)
        {
            //name on the page
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlInputText NameOnPage = myManager.ActiveBrowser.Find.ById("dijit_form_ValidationTextBox_3").As<HtmlInputText>();
            //NameOnPage.ScrollToVisible();
            NameOnPage.MouseHover();
            NameOnPage.MouseClick();
            NameOnPage.Text = Name;


            //Select PreviewImage
            Thread.Sleep(2000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan PreviewImageButton = myManager.ActiveBrowser.Find.ByExpression("id=#dijit_form_Button_.._label").As<HtmlSpan>();
            PreviewImageButton.MouseClick();

            //TreeExpand
            Thread.Sleep(7000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan TreeExpand = myManager.ActiveBrowser.Find.ByXPath("/html/body/div[58]/div[2]/div[3]/div/div/div[2]/div/div/div/div[2]/a/span/span[1]").As<HtmlSpan>();
            TreeExpand.MouseClick();


            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan firstpng = myManager.ActiveBrowser.Find.ByXPath("/html/body/div[58]/div[2]/div[3]/div/div/div[2]/div/div/div/div[2]/div/div[10]/a/span/span[6]").As<HtmlSpan>();
            firstpng.MouseClick();

            Thread.Sleep(2000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan okbuttonPicture = myManager.ActiveBrowser.Find.ByExpression("id=#dijit_form_Button_.._label", "innertext=OK").As<HtmlSpan>();
            okbuttonPicture.MouseClick();

            //Add Heading
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlInputText HeadingtextBox = myManager.ActiveBrowser.Find.ById("dijit_form_ValidationTextBox_4").As<HtmlInputText>();
            HeadingtextBox.Text = Heading;

            //SelectClub
            objAricle article = new objAricle(myManager);

            HtmlInputCheckBox CustomerCheckbox = article.ArticleCustomerCheckbox.As<HtmlInputCheckBox>();
            HtmlInputCheckBox DeveloperCheckbox = article.ArticleDeveloperCheckbox.As<HtmlInputCheckBox>();
            HtmlInputCheckBox TechnicalCheckbox = article.ArticleTechnicalCheckbox.As<HtmlInputCheckBox>();
            HtmlInputCheckBox ParlimentCheckbox = article.ArticleCustomerCheckbox.As<HtmlInputCheckBox>();

            foreach (string listItem in ClubsList)
            {
                switch (listItem)
                {
                    case "Customer":
                        CustomerCheckbox.MouseClick();
                        break;
                    case "Developer":
                        DeveloperCheckbox.MouseClick();
                        break;
                    case "Technical":
                        TechnicalCheckbox.MouseClick();
                        break;
                    case "Parliment":
                        ParlimentCheckbox.MouseClick();
                        break;
                    default:
                        CustomerCheckbox.MouseClick();
                        break;
                }

            }

            //CLick On Create Button
            Thread.Sleep(2000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan CreateButton = myManager.ActiveBrowser.Find.ByExpression("id=#dijit_form_Button_._label", "innertext=Create").As<HtmlSpan>();
            CreateButton.MouseClick();
            Thread.Sleep(5000);


            //Click On the Edit Button
            Thread.Sleep(20000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan EditButton = myManager.ActiveBrowser.Find.ByExpression("id=#dijit_form_ToggleButton_.", "title=All Properties").As<HtmlSpan>();
            HtmlSpan EditButtonSpan = EditButton.Find.AllByTagName("span")[0].As<HtmlSpan>();
            EditButtonSpan.ScrollToVisible();
            EditButtonSpan.MouseHover();
            EditButtonSpan.MouseClick();
            Thread.Sleep(7000);

            //Adding Introduction
            //Add Heading
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlTextArea IntroductionField = myManager.ActiveBrowser.Find.ByExpression("id=#uniqName_..._.", "name=introduction").As<HtmlTextArea>();
            IntroductionField.MouseClick();
            IntroductionField.Text = Introduction;
            

            Thread.Sleep(5000); //Till the editor loads
            //Add Discription
            ArtOfTest.WebAii.Core.Browser t1_frame = myManager.ActiveBrowser.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");
            myManager.ActiveBrowser.Actions.SetText(TextEditor, Description);
            

            //Publish
            //Click on publish
            ObjEditMode editmode = new ObjEditMode(myManager);
            HtmlSpan PublishButton = editmode.PublishDropdown.As<HtmlSpan>();
            PublishButton.ScrollToVisible();
            PublishButton.MouseHover();
            PublishButton.MouseClick();
            Thread.Sleep(2000);

            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan Publish = editmode.PublishButton.As<HtmlSpan>();
            Publish.MouseClick();

        }

        public void ReadData()
        {

            _Url = TestContext.DataRow["url"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _PageName = TestContext.DataRow["PageName"].ToString();
            _Heading = TestContext.DataRow["Heading"].ToString();
            _Clubs = TestContext.DataRow["Clubs"].ToString();
            _Introduction = TestContext.DataRow["Introduction"].ToString();
            _Description = TestContext.DataRow["Description"].ToString();
            _Clubs = TestContext.DataRow["Clubs"].ToString();
            _ClubsList = _Clubs.Split(',').ToList<string>();
            _Heading = CommonFunctions.GenarateRandom(_Heading);
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

            //Screen_shot
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                System.Drawing.Image img = myManager.ActiveBrowser.Capture();
                string filename = string.Format("{0}_{1}.jpg", DateTime.Now.ToString("yyyyMMdd_HHmmsss"), TestContext.TestName);
                img.Save(@"C:\Images\Errors\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            #region WebAii CleanUp

            // Shuts down WebAii manager and closes all browsers currently running
            // after each test. This call is ignored if recycleBrowser is set
            myManager.Dispose();
            this.CleanUp();

            #endregion
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // This will shut down all browsers if
            // recycleBrowser is turned on. Else
            // will do nothing.
            ShutDown();
        }

        #endregion

    }
}
