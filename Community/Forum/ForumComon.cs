using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using Community.ObjectRepo.TechnicalForum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Community.Forum
{
    class ForumComon
    {
        public static void NavigatetoForum(Browser br, Manager M, string BaseUrl, string ForumURL)
        {
            br.NavigateTo(BaseUrl + ForumURL, true);

            Thread.Sleep(5000);
            br.WaitUntilReady();
            br.RefreshDomTree();
        

            TechForum objforum = new TechForum(M);
            HtmlSpan CreateTopic = objforum.CreateTopicButton.As<HtmlSpan>();
            Assert.AreEqual(CreateTopic.IsEnabled, true, "CreateTopicButton is not visible");
        }

        public static void CreateNewForum(Browser br, Manager M, string Subject, string Messege, string Tags)
        {
            br.WaitUntilReady();
            br.RefreshDomTree();
            br.Window.SetFocus();

            Thread.Sleep(3000);
            TechForum forum = new TechForum(M);
            HtmlSpan CreateTopicbutton = forum.CreateTopicButton.As<HtmlSpan>();
            CreateTopicbutton.ScrollToVisible();
            CreateTopicbutton.MouseHover();
            CreateTopicbutton.MouseClick();

            Thread.Sleep(3000);
            br.WaitUntilReady();
            br.RefreshDomTree();

            //EnterSubject
            br.Actions.SetText(forum.TopicSubject, Subject);

            Thread.Sleep(3000);
            br.WaitUntilReady();
            br.RefreshDomTree();
            //enterMessege
            ArtOfTest.WebAii.Core.Browser t1_frame = br.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");

            br.Actions.SetText(TextEditor, Messege);
            //Check on email checkbox
            br.Actions.Check(forum.SendEmail, true);
            //Enter tags
            br.Actions.SetText(forum.Tags, Tags);
            //click on post button
            br.Actions.Click(forum.PostButton);
        }

        public static void ReplyForum(Browser br, Manager M, string reply)
        {
            br.WaitUntilReady();
            br.RefreshDomTree();

            TechForum forum = new TechForum(M);

            br.Actions.Click(forum.ReplyButton);

            Thread.Sleep(2000);
            br.WaitUntilReady();
            br.RefreshDomTree();


            ArtOfTest.WebAii.Core.Browser t1_frame = br.Frames[0];
            Element TextEditor = t1_frame.Find.ByXPath("/html/body");
            br.Actions.SetText(TextEditor, reply);

            //click on post button
            br.Actions.Click(forum.postReply);
        }
    }
}
