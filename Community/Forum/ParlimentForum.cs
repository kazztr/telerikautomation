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

namespace Community.Forum
{
    /// <summary>
    /// Summary description for ParlimentForum
    /// </summary>
    [TestClass]
    public class ParlimentForum : BaseTest
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


        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        //Initialize Settings and the Manager
        private Settings mySettings;
        private Manager myManager;

        string _Url;
        string _ForumUrl;
        string _Uname;
        string _Password;
        string _Subject;
        string _Messege;
        bool _SendEmail;
        string _Tags;
        string _reply;


        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            #region WebAii Initialization

            
            // Pass in 'true' to recycle the browser between test methods
            Initialize(false, this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.InternetExplorer;
            myManager = new Manager(mySettings);
            myManager.Start();
            myManager.LaunchNewBrowser();

        }


        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Forum.csv", "Forum#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Forum.csv")]
        public void TestMethod_ForumParliament()
        {
            //Read From data sheet
            ReadData();

            _Subject = _Subject + "  Parliament";

            //Login to the system
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);


            //Navigate to forum
            ForumComon.NavigatetoForum(myManager.ActiveBrowser, myManager, _Url, _ForumUrl);

            //Create new forum
            ForumComon.CreateNewForum(myManager.ActiveBrowser, myManager, _Subject, _Messege, _Tags);

            //Validate Activity Stream
            CommonFunctions.ValideActivityStream(myManager, myManager.ActiveBrowser, _Url, _Subject, "PARLIAMENT");

            //Reply to the forum post
            ForumComon.ReplyForum(myManager.ActiveBrowser, myManager, _reply);

        }

        public void ReadData()
        {
            string SendEmail;

            _Url = TestContext.DataRow["url"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _Subject = TestContext.DataRow["Subject"].ToString();
            _Messege = TestContext.DataRow["Messege"].ToString();
            SendEmail = TestContext.DataRow["SendEmail"].ToString();
            _Tags = TestContext.DataRow["Tags"].ToString();
            _SendEmail = Convert.ToBoolean(SendEmail);
            _Subject = CommonFunctions.GenarateRandom(_Subject);
            _reply = TestContext.DataRow["Reply"].ToString();
            _ForumUrl = TestContext.DataRow["ForumUrlParli"].ToString();
        }


        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

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
