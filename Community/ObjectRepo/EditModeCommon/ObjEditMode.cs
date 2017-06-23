using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.EditModeCommon
{
    class ObjEditMode
    {
        private Manager _manager;

        public ObjEditMode(Manager m)
        {
            _manager = m;
        }

        public Element Episerverlink { get { return _manager.ActiveBrowser.Find.ById("epi-quickNavigator"); } }
        public Element PagesTree { get { return _manager.ActiveBrowser.Find.ByAttributes("class=dijitReset dijitInline dijitIcon epi-iconTree"); } }
        public Element PagesTreePin { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='dijit_form_ToggleButton_6']/span[1]"); } }
        public Element RightSideBLock { get { return _manager.ActiveBrowser.Find.ById("uniqName_52_60"); } }
        public Element RightSideBLockPin { get { return _manager.ActiveBrowser.Find.ByExpression("id=#dijit_form_ToggleButton_..", "title=Pin"); } }
        public Element PublishDropdown { get { return _manager.ActiveBrowser.Find.ByExpression("id=^dijit_form_DropDownButton_", "innertext=Publish?"); } }
        public Element PublishButton { get { return _manager.ActiveBrowser.Find.ByExpression("id=^dijit_form_Button", "innertext=Publish"); } }
        public Element CustomerNewDropdown { get { return _manager.ActiveBrowser.Find.ByAttributes("class=epi-extraIcon epi-pt-contextMenu epi-iconContextMenu"); } }
        public Element CustomerNewPage { get { return _manager.ActiveBrowser.Find.ByExpression("id=#uniqName_.._..._text", "innertext=New Page"); } }
        public Element RightPane { get { return _manager.ActiveBrowser.Find.ByExpression("id=#uniqName_.._..", "title=Toggle assets pane"); } }
        public Element RightPaneSearchText { get { return _manager.ActiveBrowser.Find.ByExpression("id=#uniqName_.._.", "role=textbox"); } }
        //public Element topcontentarea { get { return _manager.ActiveBrowser.Find.ByExpression("id=#uniqName_..._.", "class=dijitInline epi-content-area-editor", "tabindex=-1", "role=presentation"); } }
        public Element topcontentarea { get { return _manager.ActiveBrowser.Find.ByAttributes("class=dijitInline epi-content-area-editor", "tabindex=-1", "role=presentation"); } }
        public Element OptionsButton { get { return _manager.ActiveBrowser.Find.ByExpression("id=#dijit_form_DropDownButton_._label", "class=dijitReset dijitInline dijitButtonText", "innertext=Options"); } }
        public Element OptionsDropdown { get { return _manager.ActiveBrowser.Find.ByExpression("id=#uniqName_.._.", "role=menu"); } }

    }
}
