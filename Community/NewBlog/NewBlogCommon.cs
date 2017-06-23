using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Win32.Dialogs;
using Community.ObjectRepo.NewBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Community.NewBlog
{
    class NewBlogCommon
    {
        public void CreateNewBlog(string navigateURL, Browser Br, Manager M, string BlogTitle, string BlogDescription, string Club, string BlogTag)
        {
            Br.NavigateTo(navigateURL);
            Thread.Sleep(7000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();

            CreateBlog objNewblog = new CreateBlog(M);
            //objNewblog.createEntry.Wait.ForExists(5000);
            Br.Actions.Click(objNewblog.createEntry);

            Thread.Sleep(5000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();

            //Adding title to blog
            Br.Actions.SetText(objNewblog.BlogTitle, BlogTitle);

            var x = new ArtOfTest.WebAii.Win32.Dialogs.FileUploadDialog(Br, @"C:\Images\918.jpg", DialogButton.OPEN);
            M.DialogMonitor.Start();
            M.DialogMonitor.AddDialog(x);

            HtmlInputFile choose = Br.Find.ById<HtmlInputFile>("fuPreviewImage");
            choose.Click();
            x.WaitUntilHandled(10000);


            Thread.Sleep(5000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();

            ArtOfTest.WebAii.Core.Browser t1_frame = Br.Frames[0];

            Element TextEditor = t1_frame.Find.ByXPath("/html/body");

            Br.Actions.SetText(TextEditor, BlogDescription);

            switch (Club.ToUpper())
            {
                case "CUSTOMER":
                    Br.Actions.Check(objNewblog.cbxBlogCustomer, true);
                    break;
                case "DEVELOPER":
                    Br.Actions.Check(objNewblog.cbxBlogDeveloper, true);
                    break;
                case "TECHNICAL":
                    Br.Actions.Check(objNewblog.cbxBlogTechnical, true);
                    break;
                default:
                    Br.Actions.Check(objNewblog.cbxBlogCustomer, true);
                    break;
            }

            Br.Actions.SetText(objNewblog.blogTags, BlogTag);
            Br.Actions.Click(objNewblog.blogPostButton);
            Thread.Sleep(7000);


        }

        public void addcomment(Browser Br, Manager M, string Comment)
        {
            CreateBlog objNewblog = new CreateBlog(M);

            Thread.Sleep(5000);
            M.ActiveBrowser.WaitUntilReady();
            M.ActiveBrowser.RefreshDomTree();

            M.ActiveBrowser.Actions.SetText(objNewblog.TPBlogComment, Comment);
            M.ActiveBrowser.Actions.Click(objNewblog.SendComment);
        }


        public void BlogValidation(Manager M, Browser Br, string Url, string BlogName, string blogCategory)
        {
            switch (blogCategory.ToUpper())
            {
                case "CUSTOMER":
                    Br.NavigateTo(Url + "/en/Customer/", true);
                    break;
                case "DEVELOPER":
                    Br.NavigateTo(Url + "/en/Developer/", true);
                    break;
                case "TECHNICAL":
                    Br.NavigateTo(Url + "/en/Technical/", true);
                    break;
            }

            Thread.Sleep(5000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();

            CreateBlog objNewblog = new CreateBlog(M);


            //Identify the whole activity feed and selecting the first entry div Class= "activity-text-wrapper"
            HtmlDiv FirstBlog = objNewblog.validateActivityFeed.As<HtmlDiv>();
            System.Collections.ObjectModel.ReadOnlyCollection<HtmlAnchor> links = FirstBlog.Find.AllByTagName<HtmlAnchor>("a");
            HtmlAnchor blogURL = FirstBlog.Find.AllByTagName<HtmlAnchor>("a")[1];
            blogURL.Click();



            Thread.Sleep(3000);
            Br.WaitUntilReady();
            Br.RefreshDomTree();

            //Identify the blog title
            Element ValidateName = objNewblog.validateBlogName;
            Assert.AreEqual(ValidateName.InnerText, BlogName, "BLOG VALIDATION FAILED");
        }


    }
}
