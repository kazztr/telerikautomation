using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Standerdpage
{
    class ObjStanderd
    {
        private Manager _manager;

        public ObjStanderd(Manager m)
        {
            _manager = m;
        }

        public Element PageName { get { return _manager.ActiveBrowser.Find.ById("dijit_form_ValidationTextBox_3"); } }
        public Element OkButoon { get { return _manager.ActiveBrowser.Find.ById("dijit_form_Button_24_label"); } }
        public Element Heading { get { return _manager.ActiveBrowser.Find.ByExpression("id=^dijit_form_ValidationTextBox_", "title=Heading"); } }
        public Element Introduction { get { return _manager.ActiveBrowser.Find.ByExpression("id=#uniqName_..._." , "title=Introduction"); } }
        public Element Headingverification { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='pnlFullWidthPageContainer']/div[2]/div[2]/h1"); } }


        

    }
}
