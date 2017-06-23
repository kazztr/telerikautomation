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
using Community.ObjectRepo.NewBlog;

namespace Community.NewBlog
{
    /// <summary>
    /// Summary description for NewBlogParliment
    /// </summary>
    [TestClass]
    public class NewBlogParliment : BaseTest
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
        string _remember;
        string _Club;
        string _BlogTitle;
        string _BlogDescription;
        string _BlogTag;
        string _Subsidiary;
        string _areas;
        bool _draft;
        bool _publish;
        string _displayUntill;
        bool _FrontPageNews;
        string _TPBlogComment;


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

            #endregion

            //
            // Place any additional initialization here
            //

        }


        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Blogparliment.csv", "Blogparliment#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Blogparliment.csv")]
        public void TestMethod_BlogParliament()
        {
            string navigateURL;
            ReadData();

            //Login to the system ---------------------
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);
            //----------- End reagon

            //Navigate to parliament blog -----------------------
            navigateURL = _Url + "/the-parliament/";
            myManager.ActiveBrowser.NavigateTo(navigateURL);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            //----------- End reagon

            //Add new Blog
            CreateBlog objNewblog = new CreateBlog(myManager);
            myManager.ActiveBrowser.Actions.Click(objNewblog.TPnewblog);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            myManager.ActiveBrowser.Actions.SetText(objNewblog.TPBlogTitle, _BlogTitle);

            //Add a picture
            var x = new ArtOfTest.WebAii.Win32.Dialogs.FileUploadDialog(myManager.ActiveBrowser, @"C:\Images\918.jpg", DialogButton.OPEN);
            myManager.DialogMonitor.Start();
            myManager.DialogMonitor.AddDialog(x);
            HtmlInputFile choose = myManager.ActiveBrowser.Find.ById<HtmlInputFile>("fuPreviewImage");
            choose.Click();
            x.WaitUntilHandled(10000);
            //Sometimes the frame takes little time to show up
            Thread.Sleep(5000);
            //----------- End reagon


            ArtOfTest.WebAii.Core.Browser t1_frame = myManager.ActiveBrowser.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");
            myManager.ActiveBrowser.Actions.SetText(TextEditor, _BlogDescription);

            //This functionality is temporalyremoved will be implemented in the future
            //--------------------------------------------------------------------------
            //myManager.ActiveBrowser.Actions.Check(objNewblog.TPfrontpagenews, _FrontPageNews);
            //myManager.ActiveBrowser.Actions.Click(objNewblog.TPdisplayuntill);
            //myManager.ActiveBrowser.Actions.SetText(objNewblog.TPdisplayuntill, _displayUntill);
            //myManager.ActiveBrowser.Actions.Click(objNewblog.TPdisplayuntill);
            //--------------------------------------------------------------------------

            //Selecting the pulish Subsidiary
            if (_Subsidiary != "")
            {
                selectSubsidiary(_Subsidiary);
            }
            //----------- End reagon

            //Selecting the pulish Subsidiary
            if (_areas != "")
            {
                selectArea(_areas);
            }
            Thread.Sleep(5000);
            //----------- End reagon


            //Publish or Draft
            if (_publish == true)
            {
                myManager.ActiveBrowser.Actions.Click(objNewblog.saveandPublish);
            }
            else if (_draft == true)
            {
                myManager.ActiveBrowser.Actions.Click(objNewblog.saveDraft);
            }
            else
            {
                myManager.ActiveBrowser.Actions.Click(objNewblog.saveandPublish);
            }

            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            //----------- End reagon


            TPBlogArchiveValidation(_BlogTitle);
            TPBlogvalidationCarasoel(_BlogTitle);

            Thread.Sleep(5000);
            CommonFunctions.ValideActivityStream(myManager, myManager.ActiveBrowser, _Url, _BlogTitle, "PARLIAMENT");

            //after the activity stream validation the test screen will be on the added blog

            //Adding a Comment to the blog

            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            myManager.ActiveBrowser.Actions.SetText(objNewblog.TPBlogComment, _TPBlogComment);
            myManager.ActiveBrowser.Actions.Click(objNewblog.TPBlogCommentPostButton);

            CommonFunctions.ActivityStreamCommentsValidator(myManager, myManager.ActiveBrowser, _Url, _TPBlogComment, "PARLIAMENT");

        }

        public void TPBlogvalidationCarasoel(string BlogTitle)
        {
            string firstblogTile;

            myManager.ActiveBrowser.NavigateTo(_Url + "/The-Parliament/");
            Thread.Sleep(6000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            CreateBlog objNewblog = new CreateBlog(myManager);
            firstblogTile = objNewblog.TPinfocaroselFirstpost.InnerText;
            Assert.AreEqual(firstblogTile, BlogTitle, "BLOG VALIDATION FAILED");

        }

        public void TPBlogArchiveValidation(string BlogTitle)
        {
            string Blogname;

            myManager.ActiveBrowser.NavigateTo(_Url + "/The-Parliament/");
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            CreateBlog objNewblog = new CreateBlog(myManager);
            HtmlAnchor BlogArchiveButton = objNewblog.TPBlogArchiveButton.As<HtmlAnchor>();
            BlogArchiveButton.Click(true);

            Thread.Sleep(10000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            Blogname = objNewblog.TPBlogArchiveFirstBlog.InnerText;
            Assert.AreEqual(Blogname, BlogTitle, "BLOG VALIDATION FAILED FROM BLOG ARCHIVE");
        }

        public void TPBlogvalidationActivityStream(string BlogTitle)
        {
            Thread.Sleep(7000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            CreateBlog objNewblog = new CreateBlog(myManager);

            //Identify the whole activity feed
            HtmlDiv FirstBlog = objNewblog.TPActivityStream.As<HtmlDiv>();
            System.Collections.ObjectModel.ReadOnlyCollection<HtmlAnchor> links = FirstBlog.Find.AllByTagName<HtmlAnchor>("a");
            HtmlAnchor blogURL = FirstBlog.Find.AllByTagName<HtmlAnchor>("a")[1];
            blogURL.Click();

            Thread.Sleep(6000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            //Identify the blog title
            Element ValidateName = objNewblog.TPBlogTitleArchive;
            Assert.AreEqual(ValidateName.InnerText, BlogTitle, "BLOG VALIDATION FAILED");
        }

        public void selectArea(string Subsidiary)
        {
            CreateBlog objNewblog = new CreateBlog(myManager);

            switch (Subsidiary.ToUpper())
            {
                case "ALL":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.areaAll, true);
                    break;
                case "CONSULTANCY AND SUPPORT":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.areaConsultancy, true);
                    break;
                case "DEVELOPMENT":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.areaDevelopment, true);
                    break;
                case "FINANCEANDADMINISTRATION":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.areaFinance, true);
                    break;
                case "MARKETING":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.areaMarketing, true);
                    break;
                case "SALES":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.areaSales, true);
                    break;
            }
        }

        public void selectSubsidiary(string Subsidiary)
        {
            CreateBlog objNewblog = new CreateBlog(myManager);

            switch (Subsidiary.ToUpper())
            {
                case "SUPERLAND":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiarySuperland, true);
                    break;
                case "AS":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryAS, true);
                    break;
                case "BENELUX":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryBenelux, true);
                    break;
                case "CAESAR":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryCaesar, true);
                    break;
                case "CLOUD":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryCloud, true);
                    break;
                case "DENMARK":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryDenmark, true);
                    break;
                case "GERMANY":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryGermany, true);
                    break;
                case "LITHUANIA":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryLithuania, true);
                    break;
                case "NORWAY":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryNorway, true);
                    break;
                case "SRILANKA":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiarySL, true);
                    break;
                case "SWEDEN":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiarySweden, true);
                    break;
                case "SWITZERLAND":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiarySwitzerland, true);
                    break;
                case "UNITEDKINGDOM":
                    myManager.ActiveBrowser.Actions.Check(objNewblog.subsidiaryUK, true);
                    break;
            }
        }

        public void ReadData()
        {
            string draftstring;
            string draftpublish;
            string frontpagenews;

            _Url = TestContext.DataRow["url"].ToString();
            _Language = TestContext.DataRow["Language"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _remember = TestContext.DataRow["Rememberme"].ToString();
            _Club = TestContext.DataRow["Club"].ToString();
            _BlogTitle = TestContext.DataRow["Title"].ToString();
            _BlogDescription = TestContext.DataRow["Description"].ToString();
            _BlogTag = TestContext.DataRow["Tags"].ToString();
            _Subsidiary = TestContext.DataRow["Subsidiary"].ToString();
            _areas = TestContext.DataRow["areas"].ToString();
            draftstring = TestContext.DataRow["Draft"].ToString();
            draftpublish = TestContext.DataRow["Publish"].ToString();
            frontpagenews = TestContext.DataRow["FrontPageNews"].ToString();
            _displayUntill = TestContext.DataRow["Displayuntill"].ToString();
            _TPBlogComment = TestContext.DataRow["BlogComment"].ToString();



            _BlogTitle = CommonFunctions.GenarateRandom(_BlogTitle);

            if (draftstring != "")
            {
                _draft = Convert.ToBoolean(draftstring);
            }

            if (draftpublish != "")
            {
                _publish = Convert.ToBoolean(draftpublish);
            }

            if (frontpagenews != "")
            {
                _FrontPageNews = Convert.ToBoolean(draftpublish);
            }
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
