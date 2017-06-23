using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Issuecenter
{
    class ObjIssueCenter
    {
        private Manager _manager;

        public ObjIssueCenter(Manager m)
        {
            _manager = m;
        }

        public Element SearchTextBox { get { return _manager.ActiveBrowser.Find.ById("txtSearch"); } }
        public Element SearchButton { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='pnlFullWidthPageContainer']/div[2]/div[2]/div/input[2]"); } }
        public Element SearchTable { get { return _manager.ActiveBrowser.Find.ById("searchTbl"); } }
        public Element HitsCount { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='searchResultSummary']"); } }
        public Element SearchInresult { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='searchTbl_filter']/label/input']"); } }
        public Element TrackSlider { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='ctl00_plhFullWidthContentArea_ucBugInfoTfs_upComment']/div[2]/div/div[3]/div/label"); } }
        public Element TrackValidator { get { return _manager.ActiveBrowser.Find.ById("trackMessage"); } }
        public Element SubmitComment { get { return _manager.ActiveBrowser.Find.ById("btnSubmitComment"); } }
        public Element OpenNewTab { get { return _manager.ActiveBrowser.Find.ById("openIcon"); } }
        public Element Comments { get { return _manager.ActiveBrowser.Find.ById("comments"); } }
        public Element ReleseNotesDiv { get { return _manager.ActiveBrowser.Find.ById("releaseNotesSummary"); } }
        public Element TopVotedDiv { get { return _manager.ActiveBrowser.Find.ById("tblReleaseNotes_wrapper"); } }

    }
}
