using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.NewBlog
{
    class CreateBlog
    {

        private Manager _manager;

        public CreateBlog(Manager m)
        {
            _manager = m;
        }


        public Element createEntry { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ctl01_ctl00_ctl01_ctl00_btnCreateEntry"); } }

        public Element BlogTitle { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_txtTitle"); } }

        public Element cbxBlogCustomer { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_cblCategories_0"); } }

        public Element cbxBlogDeveloper { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_cblCategories_1"); } }

        public Element cbxBlogTechnical { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_cblCategories_2"); } }

        public Element blogTags { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_txtTags"); } }

        public Element blogPostButton { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_btnPostBlogEntry"); } }

        public Element validateActivityFeed { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/form/div[5]/div/div/div[5]/div[2]/div/div/div/div/div/div[2]/div/div[1]/div[2]/div"); } }

        public Element validateCustomerActivityFeed { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/form/div[5]/div/div/div[5]/div[2]/div/div/div/div/div/div[2]/div/div[1]/div[2]/div"); } }

        public Element validateBlogName { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='pnlFullWidthPageContainer']/div[2]/div[1]/div/div[1]/h1"); } }

        public Element TPnewblog { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ctl00_ctl00_ctl03_ctl00_btnCreateEntry"); } }
        /// <summary>
        /// ////////////////////////////////////change the _ after the screeenshot fuctionality is working
        /// </summary>
        public Element TPBlogTitle { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_txtTitle"); } }

        public Element TPfrontpagenews { get { return _manager.ActiveBrowser.Find.ById("ctl00_plMainTopArea_ctl00_ctl00_ctl02_ctl00_ucNewParliamentBlogEntry_puNewBlogEntry_chkIsFrontPageNews"); } }

        public Element TPdisplayuntill { get { return _manager.ActiveBrowser.Find.ById("ctl00_plMainTopArea_ctl00_ctl00_ctl02_ctl00_ucNewParliamentBlogEntry_puNewBlogEntry_txtDisplayUntil"); } }



        //Subsidary checkboxes in paliment blog
        public Element subsidiarySuperland { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_0"); } }
        public Element subsidiaryAS { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_1"); } }
        public Element subsidiaryBenelux { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_2"); } }
        public Element subsidiaryCaesar { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_3"); } }
        public Element subsidiaryCloud { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_4"); } }
        public Element subsidiaryDenmark { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_5"); } }
        public Element subsidiaryGermany { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_6"); } }
        public Element subsidiaryLithuania { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_7"); } }
        public Element subsidiaryNorway { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_8"); } }
        public Element subsidiarySL { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_9"); } }
        public Element subsidiarySweden { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_10"); } }
        public Element subsidiarySwitzerland { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_11"); } }
        public Element subsidiaryUK { get { return _manager.ActiveBrowser.Find.ById("cblSubsidiaries_12"); } }


        //IntrestAreas
        public Element areaAll { get { return _manager.ActiveBrowser.Find.ById("cblAreas_0"); } }
        public Element areaConsultancy { get { return _manager.ActiveBrowser.Find.ById("cblAreas_0"); } }
        public Element areaDevelopment { get { return _manager.ActiveBrowser.Find.ById("cblAreas_0"); } }
        public Element areaFinance { get { return _manager.ActiveBrowser.Find.ById("cblAreas_0"); } }
        public Element areaMarketing { get { return _manager.ActiveBrowser.Find.ById("cblAreas_0"); } }
        public Element areaSales { get { return _manager.ActiveBrowser.Find.ById("cblAreas_0"); } }

        //Buttons save and draft
        public Element saveDraft { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_btnSaveBlogEntry"); } }
        public Element saveandPublish { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucEditCommunityBlog_btnPostBlogEntry"); } }

        //PostComemnt button
        public Element PostComment { get { return _manager.ActiveBrowser.Find.ById("//*[@id='communityBlogComment']/div[2]/div/div[1]/textarea"); } }
        //Comment area funtionality chnaged
        //public Element Commentiframe { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_tbBlogEntries_ucBlogEntry_puComment_txtCommentContent_ifr"); } }

        //send comment
        public Element SendComment { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucCommunityBlogEntry_ucCommunityBlogComment_btnAddComment"); } }

        

        //TP infocarasoel first post
        public Element TPinfocaroselFirstpost { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='ctl00_plhFullWidthContentArea_ctl00_ctl00_ctl03_ctl00_pnlCarousel']/div/div[1]/div/div/a[1]/span[1]/span"); } }


        //TP blog close button
        public Element TPblogclose { get { return _manager.ActiveBrowser.Find.ById("cboxClose"); } }

        //TP activity stream
        public Element TPActivityStream { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='feed']/div/div[1]/div[2]/div"); } }

        //TPBlog archive first blog
        public Element TPBlogArchiveFirstBlog { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='foo']/div[1]/a/div/span[1]/span"); } }

        //TPBlog archive button
        public Element TPBlogArchiveButton { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ctl00_ctl00_ctl03_ctl00_hylArchive"); } }

        //TP Blog Heading
        public Element TPBlogTitleArchive { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='foo']/div[1]/a/span[1]"); } }

        //TP blog comment
        public Element TPBlogComment { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='communityBlogComment']/div[2]/div/div[1]/textarea"); } }

        //TP Blog Add comment
        public Element TPBlogCommentPostButton { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucCommunityBlogEntry_ucCommunityBlogComment_btnAddComment"); } }

        //TP Blog Add comment
        public Element TPBlogCommentVerify { get { return _manager.ActiveBrowser.Find.ById("//*[@id='communityBlogCommentList']/div[2]/div[1]/div[1]/div[2]"); } }


    }
}
