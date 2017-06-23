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
using Community.ObjectRepo.EditModeCommon;
using Community.ObjectRepo.Standerdpage;
using System.Windows.Forms;

namespace Community.StanderdPage
{
    /// <summary>
    /// Summary description for Standerd
    /// </summary>
    [TestClass]
    public class Standerd : BaseTest
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
        string _Language;
        string _Club;
        string _Heading;
        string _Name;
        string _BodyText;
        string _Introduction;
        string _TopContent;
        string _BottomContent;

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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\StanderdPage.csv", "StanderdPage#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\StanderdPage.csv")]
        public void TestMethod_StanderdPage()
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

            //Navigate to customer
            myManager.ActiveBrowser.NavigateTo(_Url + "/customer/");
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            Thread.Sleep(5000);

            ObjEditMode editmode = new ObjEditMode(myManager);


            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();
            //Click On episerverButton
            HtmlUnorderedList Epilink = editmode.Episerverlink.As<HtmlUnorderedList>();
            Epilink.MouseClick();
            //it takes more than 15seconds to load the edit mode
            Thread.Sleep(30000);
            myManager.ActiveBrowser.Refresh();
            Thread.Sleep(25000);

            //Click on left sub tree
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan PagesTree = editmode.PagesTree.As<HtmlSpan>();
            PagesTree.MouseClick();


            //Click on leftmenu anchor 
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan PagesTreeAnchor = editmode.PagesTreePin.As<HtmlSpan>();
            PagesTreeAnchor.MouseClick();


            //-------------------------------------------------------------------------------------------------------------------------------------

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
            HtmlTableCell newPage = editmode.CustomerNewPage.As<HtmlTableCell>(); ;
            newPage.MouseClick();


            //Add a name to the page
            Thread.Sleep(3000);
            myManager.ActiveBrowser.RefreshDomTree();
            ObjStanderd standerd = new ObjStanderd(myManager);
            HtmlInputText PageName = standerd.PageName.As<HtmlInputText>();
            PageName.ScrollToVisible();
            PageName.MouseHover();
            PageName.MouseClick();
            PageName.Text = _Name;

            //Find The Community List
            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlDiv CommunityArea = myManager.ActiveBrowser.Find.ById("dijit__KeyNavContainer_1").As<HtmlDiv>();
            HtmlUnorderedList CommunityPages = CommunityArea.Find.AllByTagName("ul")[0].As<HtmlUnorderedList>();
            System.Collections.ObjectModel.ReadOnlyCollection<HtmlListItem> CommunityItems = CommunityPages.Find.AllByTagName<HtmlListItem>("li");


            foreach (HtmlListItem item in CommunityItems)
            {
                if (item.InnerText.Contains("[Community] Standard Page"))
                {
                    item.MouseClick();
                    Thread.Sleep(2000);
                    myManager.ActiveBrowser.RefreshDomTree();
                    EditStanderPage(_Name,_TopContent);
                    Thread.Sleep(20000);
                    //ValidateStanderPag(_Heading);
                    break;
                }
            }

        }

        public void EditStanderPage(string Name, string TopContent)
        {
            //Click On the Edit Button
            Thread.Sleep(20000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan EditButton = myManager.ActiveBrowser.Find.ByExpression("id=#dijit_form_ToggleButton_.", "title=All Properties").As<HtmlSpan>();
            HtmlSpan EditButtonSpan = EditButton.Find.AllByTagName("span")[0].As<HtmlSpan>();
            EditButtonSpan.ScrollToVisible();
            EditButtonSpan.MouseHover();
            EditButtonSpan.MouseClick();
            Thread.Sleep(7000);


            //Click on the rightTogglebar to get blocks
            ObjEditMode editmode = new ObjEditMode(myManager);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan righttogglebar = editmode.RightPane.As<HtmlSpan>();
            righttogglebar.MouseClick();
            
            //click on the pin icon
            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan PagesTreePin = editmode.RightSideBLockPin.As<HtmlSpan>();
            PagesTreePin.MouseClick();

            //Search for the top block
            HtmlInputText Searchforblocks = editmode.RightPaneSearchText.As<HtmlInputText>();
            Searchforblocks.Text = TopContent;
            Searchforblocks.MouseClick();
            myManager.Desktop.Mouse.Click(MouseClickType.LeftClick, Searchforblocks.GetRectangle());
            myManager.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);


            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();
            //Selected dev
            HtmlDiv Kbanner = myManager.ActiveBrowser.Find.ById("dgrid_4-row-16206").As<HtmlDiv>();

            HtmlDiv topcontentarea = editmode.topcontentarea.As<HtmlDiv>();

            var des = new System.Drawing.Point();
            des.X = topcontentarea.ScrollTop + 50;
            des.Y = topcontentarea.ScrollLeft + 50;

            Kbanner.DragTo(topcontentarea, ArtOfTest.Common.OffsetReference.TopCenter, des);

            //Add other details
            ObjStanderd standerdpage = new ObjStanderd(myManager);

            HtmlInputText Heading = standerdpage.Heading.As<HtmlInputText>();
            HtmlTextArea Introduction = standerdpage.Introduction.As<HtmlTextArea>();

            //adding just the heading wont add the text to the contorl 
            Heading.ScrollToVisible();
            Heading.MouseHover();
            Heading.MouseClick();
            Heading.Text = _Heading;

            Introduction.ScrollToVisible();
            Introduction.MouseHover();
            Introduction.MouseClick();
            Introduction.Text = _Introduction;

            ArtOfTest.WebAii.Core.Browser t1_frame = myManager.ActiveBrowser.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");
            myManager.ActiveBrowser.Actions.SetText(TextEditor, _BodyText);

            //Publish
            //Click on publish
            HtmlSpan PublishButton = editmode.PublishDropdown.As<HtmlSpan>();
            PublishButton.ScrollToVisible();
            PublishButton.MouseHover();
            PublishButton.MouseClick();
            Thread.Sleep(2000);

            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan Publish = editmode.PublishButton.As<HtmlSpan>();
            Publish.MouseHover();
            Publish.MouseClick();
            Thread.Sleep(10000);

            myManager.ActiveBrowser.Refresh();
            //Click on options dropdown
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlSpan OptionsButton = editmode.OptionsButton.As<HtmlSpan>();
            OptionsButton.ScrollToVisible();
            OptionsButton.MouseHover();
            OptionsButton.MouseClick();


            //Click on the URL
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlDiv OptionsDropdown = editmode.OptionsDropdown.As<HtmlDiv>();
            HtmlAnchor pagelink = OptionsDropdown.Find.AllByTagName<HtmlAnchor>("a")[0];
            pagelink.MouseClick();
            Thread.Sleep(2000);


            ValidatePage(_Heading);
            
        }

        private void ValidatePage(string Heading)
        {
            string pageHeading;

            Thread.Sleep(5000);
            myManager.ActiveBrowser.Window.SetFocus();
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            ObjStanderd standerdpage = new ObjStanderd(myManager);
            Element Headingverification = standerdpage.Headingverification;
            pageHeading = Headingverification.InnerText;

            Assert.AreEqual(pageHeading, Heading, "Headings dont match");

        }

        public void ReadData()
        {

            _Url = TestContext.DataRow["url"].ToString();
            _Language = TestContext.DataRow["Language"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _Club = TestContext.DataRow["Club"].ToString();
            _Heading = TestContext.DataRow["Heading"].ToString();
            _Name = TestContext.DataRow["Name"].ToString();
            _BodyText = TestContext.DataRow["BodyText"].ToString();
            _Introduction = TestContext.DataRow["Introduction"].ToString();
            _TopContent = TestContext.DataRow["TopContentblock"].ToString();
            _BottomContent = TestContext.DataRow["BottomContent"].ToString();
            
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
