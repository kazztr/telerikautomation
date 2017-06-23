using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Event
{
    class objEvent
    {
        private Manager _manager;

        public objEvent(Manager m)
        {
            _manager = m;
        }

        public Element NewEvent { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_pnlNewEventTop"); } }
        public Element EventName { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtTitle"); } }
        public Element EventOrganizer { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtArrenger"); } }
        public Element EventLocation { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtLocation"); } }
        public Element Language { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_ddlLanguage"); } }
        public Element Startdate { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtStartDate"); } }
        public Element StartCal { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_imgCalendar"); } }



        public Element StartTime { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtStartTime"); } }
        public Element EndDate { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtEndDate"); } }
        public Element EndTime { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtEndTime"); } }
        public Element NoRegistration { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_chbNoRegistration"); } }
        public Element RegistrationStartDate { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtRegistrationStartDate"); } }
        public Element RegistrationStartTime { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtRegistrationStartTime"); } }
        public Element RegistrationEndDate { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtRegistrationEndDate"); } }
        public Element RegistrationEndTime { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtRegistrationEndTime"); } }
        public Element MaxRegistrations { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtIntroduction"); } }
        public Element Introduction { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtMaxNumRegistrations"); } }
        public Element EmailFrom { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtEmailFrom"); } }
        public Element EmailTo { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtEmailTo"); } }
        public Element Subject { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_txtEmailSubject"); } }
        public Element Save { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainCenterArea_ctl00_ctl00_ctl01_ctl00_ucNewGlobalEvent_puNewGlobalEvent_tbNewGlobalEvent_btnPostClubEvent"); } }
       



    }
}
