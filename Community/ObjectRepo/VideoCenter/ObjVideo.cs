using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.VideoCenter
{
    class ObjVideo
    {
        private Manager _manager;

        public ObjVideo(Manager m)
        {
            _manager = m;
        }

        public Element UploadButton { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_rtSearch_btnNewVideo"); } }
        public Element VIdeoTitle { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_txtTitle"); } }
        public Element CustomerCheckBox { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_chkClub_0"); } }
        public Element DeveloperCheckBox { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_chkClub_1"); } }
        public Element TechnicalCheckBox { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_chkClub_2"); } }
        public Element ParliamentCheckBox { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_chkClub_3"); } }
        public Element LanguageDropdown { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_ddlLanguage"); } }
        public Element VideoTag { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_txtTags"); } }
        public Element Save { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucNewVideo_puNewVideo_btnUploadNewVideo"); } }
        public Element PostComment { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucVideoPlayer_btnNewVideoComment"); } }
        public Element CommentTextArea { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucVideoPlayer_txtComment"); } }
        public Element SendComment { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhFullWidthContentArea_ucVideoPlayer_btnAddComment"); } }
        public Element SearchField { get { return _manager.ActiveBrowser.Find.ById("searchfield"); } }
        public Element SearchButton { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='container']/div/div/div/div[1]/div[1]/button"); } }
        public Element FirstSearchResult { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='epiSearchResult']/ul/li[1]/h3/a"); } }
        public Element CountryDropdown { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='country - options']/div/a"); } }

        public Element SelectedCountry { get { return _manager.ActiveBrowser.Find.ByXPath(" //*[@id='country - options']/ul/li[5]/a/label"); } }


       





    }
}
