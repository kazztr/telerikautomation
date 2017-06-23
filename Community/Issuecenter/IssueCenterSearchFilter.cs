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
using Community.ObjectRepo.Issuecenter;

namespace Community.Issuecenter
{
    /// <summary>
    /// Summary description for IssueCenter
    /// </summary>
    [TestClass]
    public class IssueCenterSearchFilter : BaseTest
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
        string _SearchText;
        string _SearchInResult;
        string _Comment;


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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\IssueCenter.csv", "IssueCenter#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\IssueCenter.csv")]
        public void TestMethod_IssueCenterSearchFilter()
        {
            string HitscountString;
            int hitsCount;
            //Read the datasheet
            ReadData();

            //Login to the system
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);

            //Navigate to IssueCenter
            string navigateURL;
            navigateURL = _Url + "/bugs-wishes/";
            myManager.ActiveBrowser.NavigateTo(navigateURL);
            Thread.Sleep(5000);


            //Search For a bug
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            ObjIssueCenter issueCenter = new ObjIssueCenter(myManager);
            HtmlInputText SearchText =  issueCenter.SearchTextBox.As<HtmlInputText>();
            HtmlInputButton SearchButton = issueCenter.SearchButton.As<HtmlInputButton>();
            SearchText.Text = _SearchText;
            SearchButton.MouseClick();
            //It takes sometime to load the results
            Thread.Sleep(7000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            HtmlTable ResultSet = issueCenter.SearchTable.As<HtmlTable>();


            Element HitsCount = issueCenter.HitsCount;
            //HtmlDiv HitsCount = ResultSet.Find.ById("searchResultSummary").As<HtmlDiv>();

            HitscountString = HitsCount.InnerText;
            hitsCount = CommonFunctions.ExtractNumberFromSrting(HitscountString);

            //Verify the number of rows are actually displyed in the table 
            Assert.AreEqual(hitsCount, ResultSet.BodyRows.Count);

            Thread.Sleep(2000);
            myManager.ActiveBrowser.RefreshDomTree();

            //verify search in rersult 
            HtmlDiv div = myManager.ActiveBrowser.Find.ById("searchTbl_filter").As<HtmlDiv>();
            HtmlInputSearch searchinResult = div.ChildNodes[0].ChildNodes[1].As<HtmlInputSearch>();
            searchinResult.Text = _SearchInResult;
            myManager.Desktop.Mouse.Click(MouseClickType.LeftClick, searchinResult.GetRectangle());
            myManager.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);
            Thread.Sleep(5000);
            ResultSet.Refresh();
            myManager.ActiveBrowser.RefreshDomTree();

            string result = ResultSet.Rows[0].Cells[0].InnerText;

            //Verify the searched sesult from the grid
            Assert.AreEqual(result, _SearchInResult);

            //open the popup
            ResultSet.Rows[0].MouseClick(MouseClickType.LeftDoubleClick);

            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            //Click on track This issue
            Element TrackSLider = issueCenter.TrackSlider;
            if (TrackSLider!= null)
            {
                Thread.Sleep(2000);
                myManager.ActiveBrowser.Actions.Click(TrackSLider);

                Thread.Sleep(3000);
                myManager.ActiveBrowser.WaitUntilReady();
                myManager.ActiveBrowser.RefreshDomTree();

                HtmlDiv trackMessage = issueCenter.TrackValidator.As<HtmlDiv>();
                string trackMessageext = trackMessage.InnerText;
                Assert.IsTrue(trackMessageext.Contains("Added to your tracking list."));

                //remove from the tracking list
                Thread.Sleep(2000);
                myManager.ActiveBrowser.Actions.Click(TrackSLider);
            }
            //Add a comment
            ArtOfTest.WebAii.Core.Browser t1_frame = myManager.ActiveBrowser.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");
            myManager.ActiveBrowser.Actions.SetText(TextEditor, _Comment);

            HtmlAnchor Submitbutton = issueCenter.SubmitComment.As<HtmlAnchor>();
            Submitbutton.ScrollToVisible();
            Submitbutton.MouseClick();

            //Click on open in a new tab
            HtmlAnchor OpenNewtab = issueCenter.OpenNewTab.As<HtmlAnchor>();
            OpenNewtab.MouseClick();

            Thread.Sleep(5000);
            myManager.ActiveBrowser.Refresh();
            //Validate Comments in the page
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.Window.SetFocus();

            HtmlDiv Comments = issueCenter.Comments.As<HtmlDiv>();
            Assert.IsTrue(Comments.InnerText.Contains(_Comment));

        }

        private void ReadData()
        {
            _Url = TestContext.DataRow["url"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _SearchText = TestContext.DataRow["SearchText"].ToString();
            _SearchInResult = TestContext.DataRow["Searchinresult"].ToString();
            _Comment = TestContext.DataRow["Comment"].ToString();
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
