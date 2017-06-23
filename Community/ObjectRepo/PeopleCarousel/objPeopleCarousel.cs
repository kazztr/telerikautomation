using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.PeopleCarousel
{
    class objPeopleCarousel
    {
        private Manager _manager;

        public objPeopleCarousel(Manager m)
        {
            _manager = m;
        }

        public Element PeopleArchivelink { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='aboutEmployee']/div[2]/div/div[3]/div[2]/a"); } }
        public Element findPeople { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='aboutEmployee']/div[2]/div/div[3]/div[1]/input"); } }
        public Element firstSearchResult { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[1]/div/div/div/div[1]/div/div/div[1]"); } }

        //MembersPage
        public Element MembersSearch { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_txtSearchQuery"); } }
        //SearchButton
        public Element buttonSearch { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_btnSearchMembers"); } }

        //First Name
        public Element FirstEmployeeSearchResult { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucMembers_lvMembers_ctrl0_ctl01_hlMyPage"); } }


        //People archive search
        public Element ArchiveSearchField { get { return _manager.ActiveBrowser.Find.ById("searchfield"); } }
        //Search button
        public Element ArchiveSearchButton { get { return _manager.ActiveBrowser.Find.ById("searchfield"); } }

    }
}
