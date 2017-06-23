using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Search
{
    class ObjSearch
    {
        private Manager _manager;

        public ObjSearch(Manager m)
        {
            _manager = m;
        }


        public Element Searchicon { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='aspnetForm']/div[3]/div[1]/div/nav/ul/li[6]/span[1]"); } }
        public Element Searchtextbox { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ctl00_tbSearch"); } }
        public Element Searchbutton { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ctl00_btnSearchV2"); } }
        public Element SearchFilters { get { return _manager.ActiveBrowser.Find.ById("resourcecentersourcefacetlist"); } }
        public Element ResultGri { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='epiSearchResult']/ul"); } }
        public Element AllSourcebutton { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='resourcecentersourcefacetlist']/li[1]/label/input"); } }
    }
}
