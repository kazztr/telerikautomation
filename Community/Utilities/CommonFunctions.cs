using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using Community.ObjectRepo.Activity;
using Community.ObjectRepo.Article;
using Community.ObjectRepo.Login;
using Community.ObjectRepo.NewBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Community.Utilities
{
    class CommonFunctions
    {
        public static void CaptureScreen(Browser br)
        {
            //ScreenCapture sc = new ScreenCapture();
            //// capture entire screen, and save it to a file
            //Image img = sc.CaptureScreen();
            //// display image in a Picture control named imageDisplay
            //br.Window.im  imageDisplay.Image = img;
            // capture this window, and save it
            br.ContentWindow.SetActive();
            br.ContentWindow.SetFocus();
            //sc.CaptureWindowToFile(br.ContentWindow.Handle, "C:\\temp2.gif", ImageFormat.Gif);
        }



        public static void HandleSpashScreen(Manager man, Browser br)
        {
            ObjLoginLanguage login = new ObjectRepo.Login.ObjLoginLanguage(man);
            //HandleSpashscreen
            Element HeaderDiv = login.btnGotit;
            if (HeaderDiv != null)
            {
                br.Actions.Click(HeaderDiv);
            }
            Thread.Sleep(5000);
            br.WaitUntilReady();
            br.RefreshDomTree();

        }

        public static void Login(Manager mngr, Browser br, string strUname, string strPassword)
        {
            ObjLoginLanguage objlog = new ObjLoginLanguage(mngr);

            br.Actions.Click(objlog.loginLink);
            Thread.Sleep(5000);
            br.WaitUntilReady();
            br.RefreshDomTree();

            //Login 
            br.Actions.SetText(objlog.username, strUname);
            br.Actions.SetText(objlog.password, strPassword);
            Thread.Sleep(5000);
            br.Actions.Click(objlog.loginbutton);

            //SuperIDlogin
            //br.Actions.SetText(objlog.loginUnameSuperID , strUname);
            //br.Actions.SetText(objlog.loginPasswordSuperID, strPassword);
            //br.Actions.Click(objlog.loginButtonSuperID);

            Thread.Sleep(5000);
            br.WaitUntilReady();
            br.RefreshDomTree();

            //Login Validation
            Thread.Sleep(10000);
            br.WaitUntilReady();
            br.RefreshDomTree();
            HtmlAnchor loginNotification = objlog.logoutlink.As<HtmlAnchor>();
            Assert.AreEqual(loginNotification.IsEnabled, true, "LOGIN FAILED");
        }

        public static string GenarateRandom(string strPrefix)
        {
            string strRandomText = "";

            Random random = new Random();
            int intNumber = random.Next(10000);
            string strNumber = intNumber.ToString();
            strRandomText = strNumber + " " + strPrefix;

            return strRandomText;
        }

        public static void ValideActivityStream(Manager M, Browser Br, string Url, string Name, string club)
        {
            string PostName;
            switch (club.ToUpper())
            {
                case "CUSTOMER":
                    Br.NavigateTo(Url + "/Customer/", true);
                    break;
                case "DEVELOPER":
                    Br.NavigateTo(Url + "/Developer/", true);
                    break;
                case "TECHNICAL":
                    Br.NavigateTo(Url + "/Technical/", true);
                    break;
                case "PARLIAMENT":
                    Br.NavigateTo(Url + "/The-Parliament/", true);
                    //It takes a lot of time to update parliament activity stream for some unknown reason
                    Thread.Sleep(50000);
                    break;
            }

            Br.Refresh();
            Thread.Sleep(10000); //Activity stream takes a lot of time to load the post
            //Br.WaitUntilReady();
            Br.RefreshDomTree();

            ActivityStream objActivity = new ActivityStream(M);


            //Identify the first post if the  activity feed and selecting the first entry div Class= "activity-text-wrapper"
            HtmlDiv FirstBlog = objActivity.validateActivityFeed.As<HtmlDiv>();
            System.Collections.ObjectModel.ReadOnlyCollection<HtmlAnchor> links = FirstBlog.Find.AllByTagName<HtmlAnchor>("a");
            HtmlAnchor blogURL = FirstBlog.Find.AllByTagName<HtmlAnchor>("a")[1];
            PostName = blogURL.InnerText;
            Assert.AreEqual(PostName, Name, "Activity stream validation failed");
            blogURL.Click();

            Thread.Sleep(3000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();
        }


        public static void ActivityStreamCommentsValidator(Manager M, Browser Br, string Url, string Comment, string club)
        {
            string PostName="";
            Thread.Sleep(5000);
            switch (club.ToUpper())
            {
                case "CUSTOMER":
                    Br.NavigateTo(Url + "/Customer/", true);
                    break;
                case "DEVELOPER":
                    Br.NavigateTo(Url + "/Developer/", true);
                    break;
                case "TECHNICAL":
                    Br.NavigateTo(Url + "/Technical/", true);
                    break;
                case "PARLIAMENT":
                    Br.NavigateTo(Url + "/The-Parliament/", true);
                    break;
            }
            //It takes atleast 1minute to load the comments to the activity stream
            Thread.Sleep(5000);
            Br.Refresh();
            Thread.Sleep(5000);
            Br.Refresh();
            Br.WaitUntilReady();
            Br.RefreshDomTree();

            ActivityStream objActivity = new ActivityStream(M);


            //Identify the first post if the  activity feed and selecting the first entry div Class= "activity-text-wrapper"
            HtmlDiv FirstBlog = objActivity.validateActivityFeed.As<HtmlDiv>();
            System.Collections.ObjectModel.ReadOnlyCollection<HtmlSpan> links = FirstBlog.Find.AllByTagName<HtmlSpan>("Span");
            HtmlSpan blogComment = FirstBlog.Find.AllByTagName<HtmlSpan>("Span")[4];
            PostName = blogComment.InnerText;
            //Assert.IsTrue(PostName.Contains(Comment), "Activity stream comment validation failed");

            Thread.Sleep(3000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();
        }

        public static void ValidateArticle(Manager M, Browser Br, string Heading)
        {
            string atriclePageHeading;

            objAricle article = new objAricle(M);

            Br.Window.SetFocus();
            Br.RefreshDomTree();

            atriclePageHeading = article.ArticlePageHeading.InnerText;
            Assert.AreEqual(atriclePageHeading, Heading, "Article Validation Failed from the page");
        }

        public static void ValidateBlog(Manager M, string BlogName)
        {
            CreateBlog objNewblog = new CreateBlog(M);
            Element ValidateName = objNewblog.validateBlogName;
            Assert.AreEqual(ValidateName.InnerText, BlogName, "BLOG VALIDATION FAILED");

        }
        public static void Logout(Manager mngr, Browser br)
        {
            ObjLoginLanguage objlog = new ObjLoginLanguage(mngr);
            br.Actions.Click(objlog.logoutlink);
        }

        public static int ExtractNumberFromSrting(string subjectString)
        {
            string resultString;
            int Number =0;

            resultString = Regex.Match(subjectString, @"\d+").Value;
            int.TryParse(resultString.Trim(), out Number);
            
            return Number;
        }



    }
}
