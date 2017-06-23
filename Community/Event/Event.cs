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
using Community.ObjectRepo.Event;

namespace Community.Event
{
    /// <summary>
    /// Summary description for Event
    /// </summary>
    [TestClass]
    public class Event : BaseTest
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

        private Settings mySettings;
        private Manager myManager;

        string _Url;
        string _Uname;
        string _Password;
        string _Language;
        string _remember;
        string _EventName;
        string _EventIntroduction;
        string _EventDescription;
        string _EventOrganizer;
        string _EventLocation;
        string _EventLanguage;
        string _EventStartDate;
        string _EventStartTime;
        string _EventEndDate;
        string _EventEndTime;
        bool _EventNoRegistration;
        string _EventRegistrationStartDate;
        string _EventRegistrationStartTime;
        string _EventRegistrationEndDate;
        string _EventRegistrationEndTime;
        string _EventMaxRegistration;
        string _EventEmailTo;
        string _EventEmailFrom;
        string _EventEmailSubject;

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
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Event.csv", "Event#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Event.csv")]
        public void TestMethod_Events()
        {
            //Read From data sheet
            ReadData();
            string navigateURL;

            //Login to the system ---------------------
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);
            //----------- End reagon

            //Navigate to Events -----------------------
            navigateURL = _Url + "/Events/";
            myManager.ActiveBrowser.NavigateTo(navigateURL);
            Thread.Sleep(3000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            //----------- End reagion

            //Add New event
            objEvent Event = new objEvent(myManager);
            HtmlDiv Newevent = Event.NewEvent.As<HtmlDiv>();
            HtmlAnchor AddNew = Newevent.Find.AllByTagName("a")[0].As<HtmlAnchor>();
            AddNew.MouseClick();
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            myManager.ActiveBrowser.Actions.SetText(Event.EventName, _EventName);
            myManager.ActiveBrowser.Actions.SetText(Event.EventOrganizer, _EventOrganizer);
            myManager.ActiveBrowser.Actions.SetText(Event.EventLocation, _EventLocation);
            myManager.ActiveBrowser.Actions.SelectDropDown(Event.Language, _EventLanguage);

            HtmlInputText sttextbox = Event.Startdate.As<HtmlInputText>();
            sttextbox.MouseHover();
            sttextbox.ScrollToVisible();
            sttextbox.Text = "25/08/2016";

            HtmlSpan st = myManager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_lblUnlimited").As<HtmlSpan>();
            st.MouseClick();


            Thread.Sleep(5000);

        }

        public void ReadData()
        {
            string draftstring;
            string draftpublish;
            string frontpagenews;
            string NoRegistration;

            _Url = TestContext.DataRow["url"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _EventName = TestContext.DataRow["Name"].ToString();
            _EventIntroduction = TestContext.DataRow["Introduction"].ToString();
            _EventDescription = TestContext.DataRow["Organizer"].ToString();
            _EventOrganizer = TestContext.DataRow["Organizer"].ToString();
            _EventLocation = TestContext.DataRow["Location"].ToString();
            _EventLanguage = TestContext.DataRow["Language"].ToString();
            _EventStartDate = TestContext.DataRow["StartDate"].ToString();
            _EventStartTime = TestContext.DataRow["StartTime"].ToString();
            _EventEndDate = TestContext.DataRow["EndDate"].ToString();
            _EventEndTime = TestContext.DataRow["EndTime"].ToString();
            NoRegistration = TestContext.DataRow["NoRegistration"].ToString();
            _EventRegistrationStartDate = TestContext.DataRow["RegistrationStartDate"].ToString();
            _EventRegistrationStartTime = TestContext.DataRow["RegistrationStartTime"].ToString();
            _EventRegistrationEndDate = TestContext.DataRow["RegistrationEndDate"].ToString();
            _EventRegistrationEndTime = TestContext.DataRow["RegistrationEndTime"].ToString();
            _EventMaxRegistration = TestContext.DataRow["MaxRegistration"].ToString();
            _EventEmailTo = TestContext.DataRow["EmailToText"].ToString();
            _EventEmailFrom = TestContext.DataRow["EmailFromText"].ToString();
            _EventEmailSubject = TestContext.DataRow["EmailSubject"].ToString();
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
