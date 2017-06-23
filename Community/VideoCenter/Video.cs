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
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using ArtOfTest.WebAii.Wpf;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Community.Utilities;
using System.Threading;
using Community.ObjectRepo.VideoCenter;

namespace Community.VideoCenter
{
    /// <summary>
    /// Summary description for Video
    /// </summary>
    [TestClass]
    public class Video : BaseWpfTest
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
        string _VideoTitle;
        string _VideoDescription;
        string _VideoTag;
        string _VideoLanguage;
        string _VideoComment;


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

            Initialize(this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.WaitCheckInterval = 10000;

            // Now call Initialize again with your updated settings object
            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            */

            // Set the current test method. This is needed for WebAii to discover
            // its custom TestAttributes set on methods and classes.
            // This method should always exist in [TestInitialize()] method.
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            mySettings = new Settings();
            mySettings.Web.DefaultBrowser = BrowserType.InternetExplorer;
            myManager = new Manager(mySettings);
            myManager.Start();
            myManager.LaunchNewBrowser();

            //
            // Place any additional initialization here
            //

        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Video.csv", "Video#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Video.csv")]
        public void TestMethod_Video()
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

            //Navigate to Video Center-----------------------
            navigateURL = _Url + "/video-center/#/";
            myManager.ActiveBrowser.NavigateTo(navigateURL);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            //----------- End reagon

            //Add New Video
            ObjVideo objNewVideo = new ObjVideo(myManager);
            myManager.ActiveBrowser.Actions.Click(objNewVideo.UploadButton);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();


            //ClcikOn UploadVideoDialog
            //Add a Video
            var x = new ArtOfTest.WebAii.Win32.Dialogs.FileUploadDialog(myManager.ActiveBrowser, @"C:\Images\BMW.mp4", DialogButton.OPEN);
            myManager.DialogMonitor.Start();
            myManager.DialogMonitor.AddDialog(x);
            HtmlInputFile choose = myManager.ActiveBrowser.Find.ById<HtmlInputFile>("fuVideo");
            choose.Click();
            x.WaitUntilHandled(10000);
            //Sometimes the frame takes little time to show up
            Thread.Sleep(5000);
            //----------- End reagon

            //ClcikOn UploadImageDialog
            //Add a picture
            var xi = new ArtOfTest.WebAii.Win32.Dialogs.FileUploadDialog(myManager.ActiveBrowser, @"C:\Images\918.jpg", DialogButton.OPEN);
            myManager.DialogMonitor.Start();
            myManager.DialogMonitor.AddDialog(xi);
            HtmlInputFile chooseImage = myManager.ActiveBrowser.Find.ById<HtmlInputFile>("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_fuPreviewFrame");
            chooseImage.Click();
            xi.WaitUntilHandled(10000);
            //Sometimes the frame takes little time to show up
            Thread.Sleep(5000);
            //----------- End reagon

            //Addigng description
            ArtOfTest.WebAii.Core.Browser t1_frame = myManager.ActiveBrowser.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");
            myManager.ActiveBrowser.Actions.SetText(TextEditor, _VideoDescription);


            myManager.ActiveBrowser.Actions.SetText(objNewVideo.VIdeoTitle, _VideoTitle);

            switch (_Club.ToUpper())
            {
                case "CUSTOMER":
                    myManager.ActiveBrowser.Actions.Check(objNewVideo.CustomerCheckBox, true);
                    break;
                case "DEVELOPER":
                    myManager.ActiveBrowser.Actions.Check(objNewVideo.DeveloperCheckBox, true);
                    break;
                case "TECHNICAL":
                    myManager.ActiveBrowser.Actions.Check(objNewVideo.TechnicalCheckBox, true);
                    break;
               case "PARLIAMENT":
                    myManager.ActiveBrowser.Actions.Check(objNewVideo.ParliamentCheckBox, true);
                    break;
                default:
                    myManager.ActiveBrowser.Actions.Check(objNewVideo.CustomerCheckBox, true);
                    break;
            }

            myManager.ActiveBrowser.Actions.SelectDropDown(objNewVideo.LanguageDropdown, _VideoLanguage);

            myManager.ActiveBrowser.Actions.SetText(objNewVideo.VideoTag, _VideoTag);

            myManager.ActiveBrowser.Actions.Click(objNewVideo.Save);

            Thread.Sleep(5000);

            //Validate Activity stream
            CommonFunctions.ValideActivityStream(myManager, myManager.ActiveBrowser, _Url, _VideoTitle, _Club);

            Assert.AreEqual(VerifcationPlayVideo(), true, "Video is not playable");

            PostComment(_VideoComment);

            //VideoOtherValidations(_VideoTitle);
        }

        public void VideoOtherValidations(string Title)
        {
            myManager.ActiveBrowser.NavigateTo(_Url + "/video-center/#/");
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            ObjVideo objNewVideo = new ObjVideo(myManager);

            //Chnage filer to norsk
            HtmlDiv CountryDropDown = objNewVideo.CountryDropdown.As<HtmlDiv>();
            CountryDropDown.MouseClick();
            Thread.Sleep(2000);
            myManager.ActiveBrowser.RefreshDomTree();
            HtmlDiv SelectedCountry = objNewVideo.SelectedCountry.As<HtmlDiv>();
            SelectedCountry.MouseClick();

            //Searchfor the video
            myManager.ActiveBrowser.Actions.SetText(objNewVideo.SearchField, Title);
            myManager.ActiveBrowser.Actions.Click(objNewVideo.SearchButton);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.RefreshDomTree();

            //Verify the first result
            HtmlAnchor FirstSearchResult = objNewVideo.FirstSearchResult.As<HtmlAnchor>();
            Assert.AreEqual(Title, FirstSearchResult.InnerText);

        }


        public bool VerifcationPlayVideo()
        {
            bool Play = false;
            string state = "";

            //It takes sometime to load the video in to the frame
            Thread.Sleep(5000);

            myManager.ActiveBrowser.Actions.InvokeScript("jwplayer().play()");
            Thread.Sleep(5000);
            state = myManager.ActiveBrowser.Actions.InvokeScript("jwplayer().getState()");

            if (state == "PLAYING")
            {
                Play = true;
            }
            return Play;
        }

        public void PostComment(string Comment)
        {
            ObjVideo objNewVideo = new ObjVideo(myManager);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            myManager.ActiveBrowser.Actions.SetText(objNewVideo.CommentTextArea, Comment);
            myManager.ActiveBrowser.Actions.Click(objNewVideo.SendComment);

            CommonFunctions.ActivityStreamCommentsValidator(myManager, myManager.ActiveBrowser, _Url, _VideoComment, _Club);
        }


        public void ReadData()
        {

            _Url = TestContext.DataRow["url"].ToString();
            _Language = TestContext.DataRow["Language"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _remember = TestContext.DataRow["Rememberme"].ToString();
            _Club = TestContext.DataRow["Club"].ToString();
            _VideoTitle = TestContext.DataRow["Title"].ToString();
            _VideoDescription = TestContext.DataRow["Description"].ToString();
            _VideoTag = TestContext.DataRow["Tags"].ToString();
            _VideoLanguage = TestContext.DataRow["VideoLanguage"].ToString();
            _VideoComment = TestContext.DataRow["VideoComment"].ToString();
            _VideoTitle = CommonFunctions.GenarateRandom(_VideoTitle);


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

            Thread.Sleep(2000);

            #region WebAii CleanUp

            // Shuts down WebAii manager and closes all applications currently running
            // after each test.
            myManager.Dispose();
            this.CleanUp();

            #endregion
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // This will shut down all applications
            ShutDown();
        }

        #endregion
    }
}
