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
using Community.ObjectRepo.Search;
using System.Threading;
using System.Text.RegularExpressions;
using Community.Utilities;

namespace Community.Search
{
    /// <summary>
    /// Summary description for Search
    /// </summary>
    [TestClass]
    public class Search : BaseWpfTest
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
        string _SearchString;


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
            mySettings.Web.DefaultBrowser = BrowserType.FireFox;
            myManager = new Manager(mySettings);
            myManager.Start();
            myManager.LaunchNewBrowser();

            //
            // Place any additional initialization here
            //

        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Data\\Search.csv", "Search#csv", DataAccessMethod.Sequential), DeploymentItem("Data\\Search.csv")]
        public void TestMethod_Search()
        {
            ReadData();

            //Login to the system
            myManager.ActiveBrowser.NavigateTo(_Url);
            CommonFunctions.HandleSpashScreen(myManager, myManager.ActiveBrowser);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            CommonFunctions.Login(myManager, myManager.ActiveBrowser, _Uname, _Password);
            Thread.Sleep(7000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            //Search using search text
            ObjSearch search = new ObjSearch(myManager);
            myManager.ActiveBrowser.Actions.Click(search.Searchicon);

            HtmlInputText searchtext = search.Searchtextbox.As<HtmlInputText>();
            searchtext.Text = _SearchString;
            myManager.ActiveBrowser.Actions.Click(search.Searchbutton);
            Thread.Sleep(5000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            SearchVerification();

        }

        public void SearchVerification()
        {
            Thread.Sleep(2000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            ObjSearch search = new ObjSearch(myManager);

            HtmlUnorderedList list = search.SearchFilters.As<HtmlUnorderedList>();

            foreach (HtmlListItem item in list.AllItems)
            {
                string text = item.InnerText;
                Match match = Regex.Match(text, @"(.*)(\([0-9]*\))", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var data = Regex.Match(text, @"\d+").Value; //filter the count
                    string lableName = match.Groups[1].Value; //filter the name
                    int val;
                    if (int.TryParse(data, out val))
                    {
                        if (val > 0)
                        {
                            if (lableName != "All")
                            {
                                lableName = ReturnNames(lableName.ToUpper());
                                HtmlInputCheckBox checkbox = item.Find.AllByTagName("input")[0].As<HtmlInputCheckBox>();
                                checkbox.MouseClick();
                                SelectFirstSearchResult(lableName);

                            }
                        }
                    }
                }

            }
        }

        public void SelectFirstSearchResult(string type)
        {
            string FirstSearchLink;


            Thread.Sleep(2000);
            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();

            ObjSearch search = new ObjSearch(myManager);

            HtmlUnorderedList SearchResult = search.ResultGri.As<HtmlUnorderedList>();
            HtmlListItem firstsearchResult = SearchResult.Items[0];
            HtmlAnchor firstsearchResultlink = firstsearchResult.Find.AllByTagName("a")[0].As<HtmlAnchor>();
            FirstSearchLink = firstsearchResultlink.InnerText;

            foreach (HtmlListItem ResultlistItem in SearchResult.Items)
            {
                HtmlDiv Lidiv = ResultlistItem.Find.AllByTagName("div")[0].As<HtmlDiv>();
                string classAttribute = Lidiv.Attributes[0].Value;
                Assert.AreEqual(classAttribute.ToLower(), type.ToLower(), "clicked on " + type + " but displays" + classAttribute);
            }

            if (FirstSearchLink.ToUpper().Contains(_SearchString.ToUpper()))
            {
                if (type != "Training") // if the browser opens training it opnes PDFs
                {
                    firstsearchResultlink.MouseClick();
                    myManager.ActiveBrowser.WaitUntilReady();
                    myManager.ActiveBrowser.RefreshDomTree();
                    Thread.Sleep(5000);

                    Assert.IsTrue(myManager.ActiveBrowser.ContainsText(_SearchString));
                }


            }

            myManager.ActiveBrowser.WaitUntilReady();
            myManager.ActiveBrowser.RefreshDomTree();
            myManager.ActiveBrowser.GoBack();
            Thread.Sleep(5000);

            myManager.ActiveBrowser.Actions.Click(search.AllSourcebutton);
            Thread.Sleep(2000);
        }

        public string ReturnNames(string labelname)
        {
            string TypeName;

            TypeName = labelname.Trim();

            switch (TypeName)
            {
                case "ARTICLES":
                    TypeName = "Article";
                    break;
                case "BLOGS":
                    TypeName = "Blog";
                    break;
                case "FAQ":
                    TypeName = "FAQ";
                    break;
                case "FORUMS":
                    TypeName = "Forum";
                    break;
                case "HELP":
                    TypeName = "Help file";
                    break;
                case "MEMBERS":
                    TypeName = "Member";
                    break;
                case "RESOURCES":
                    TypeName = "Resource";
                    break;
                case "SIM":
                    TypeName = "SIM";
                    break;
                case "TECH. DOCUMENTATION":
                    TypeName = "TechDoc";
                    break;
                case "TRAINING":
                    TypeName = "Training";
                    break;
                case "VIDEOS":
                    TypeName = "Video";
                    break;

            }
            return TypeName;
        }

        public void ReadData()
        {

            _Url = TestContext.DataRow["url"].ToString();
            _Uname = TestContext.DataRow["Username"].ToString();
            _Password = TestContext.DataRow["Password"].ToString();
            _SearchString = TestContext.DataRow["SearchSrting"].ToString();
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
