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
using Community.ObjectRepo.Login;
using Microsoft.VisualStudio.TestTools.UITesting;
using Community.ObjectRepo.NewBlog;

namespace Community.NewBlog
{
    /// <summary>
    /// Summary description for NewBlogCustomer
    /// </summary>
    [TestClass]
    public class NewBlogCustomer : BaseTest
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
        string _remember;
        string _Club;
        string _BlogTitle;
        string _BlogDescription;
        string _BlogTag;
        string _BlogComment;

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

            mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.InternetExplorer;
            myManager = new Manager(mySettings);
            myManager.Start();
            myManager.LaunchNewBrowser();

            //SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            //
            // Place any additional initialization here
            //

        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Blogs.csv", "Blogs#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Blogs.csv")]
        public void TestMethod_BlogCustomer()
        {
            //Read the datasheet
            ReadData();

            //Login to the system
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);

            //Navigate to blogs
            string navigateURL;
            navigateURL = _Url + "/home-blogs/";
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            //AddNewBlog
            NewBlogCommon objNewBlog = new NewBlogCommon();
            objNewBlog.CreateNewBlog(navigateURL, myManager.ActiveBrowser, myManager, _BlogTitle, _BlogDescription, _Club, _BlogTag);

            //Validate the blog in Activity stream
            CommonFunctions.ValideActivityStream(myManager, myManager.ActiveBrowser, _Url, _BlogTitle, _Club);
            //Validate the blog in blog page
            CommonFunctions.ValidateBlog(myManager, _BlogTitle);

            //addcomment
            objNewBlog.addcomment(myManager.ActiveBrowser, myManager, _BlogComment);
            //validate comment
            CommonFunctions.ActivityStreamCommentsValidator(myManager, myManager.ActiveBrowser, _Url, _BlogComment, _Club);

           //Logout
            CommonFunctions.Logout(myManager, myManager.ActiveBrowser);
            ObjLoginLanguage login = new ObjLoginLanguage(myManager);
            login.loginLink.Wait.ForExists(5000);

            //myManager.Dispose();
        }

        public void ReadData()
        {
            _Url = TestContext.DataRow["url"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _remember = TestContext.DataRow["Rememberme"].ToString();
            _Club = "Customer";
            _BlogTitle = TestContext.DataRow["Title"].ToString();
            _BlogDescription = TestContext.DataRow["Description"].ToString();
            _BlogTag = TestContext.DataRow["Tags"].ToString();
            _BlogTitle = "Customer" + _BlogTitle;
            _BlogTitle = CommonFunctions.GenarateRandom(_BlogTitle);
            _BlogComment = TestContext.DataRow["Comment"].ToString();
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
