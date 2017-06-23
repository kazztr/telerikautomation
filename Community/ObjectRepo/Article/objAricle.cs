using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Article
{
    class objAricle
    {
        private Manager _manager;

        public objAricle(Manager m)
        {
            _manager = m;
        }


        public Element ToggleNavigationPane { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[2]/div[2]/div/div[6]/div[2]/div/div/div[1]/span[1]/span[1]/span/span/span[1]"); } }
        public Element CustomerLink { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[2]/div[2]/div/div[4]/div/div/div[2]/div/div/div/table/tbody/tr/td/div[1]/div[3]/div[1]/div[2]/div[2]/div/div[2]/div[2]/div/div[2]/div/div[2]/div[8]/div[1]/span/span[4]/span[1]"); } }
        public Element ArticlePreviewImageButton { get { return _manager.ActiveBrowser.Find.ById("dijit_form_Button_22_label"); } }
        public Element ArticleHeading { get { return _manager.ActiveBrowser.Find.ById("dijit_form_ValidationTextBox_4"); } }
        public Element ArticleCustomerCheckbox { get { return _manager.ActiveBrowser.Find.ById("dijit_form_CheckBox_1"); } }
        public Element ArticleDeveloperCheckbox { get { return _manager.ActiveBrowser.Find.ById("dijit_form_CheckBox_2"); } }
        public Element ArticleTechnicalCheckbox { get { return _manager.ActiveBrowser.Find.ById("dijit_form_CheckBox_3"); } }
        public Element ArticleParlimentCheckbox { get { return _manager.ActiveBrowser.Find.ById("dijit_form_CheckBox_4"); } }

        //Select Image
        public Element ForallHomeDiv { get { return _manager.ActiveBrowser.Find.ById("uniqName_94_305"); } }
        public Element PageName { get { return _manager.ActiveBrowser.Find.ById("dijit_form_ValidationTextBox_3"); } }
        public Element Forallsites { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[41]/div[2]/div[3]/div/div/div[2]/div/div/div/div[2]/a/span/span[1]"); } }
        public Element SelectedImage { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[41]/div[2]/div[3]/div/div/div[2]/div/div/div/div[2]/div/div[22]/a/span/span[6]"); } }

        public Element CreateButton { get { return _manager.ActiveBrowser.Find.ById("dijit_form_Button_20_label"); } }


        public Element ArticleHeding { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/form/div[4]/div[3]/div/div/div/h1"); } }
        public Element ArticleEditButton { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[2]/div[2]/div/div[6]/div[1]/div/div/div/div/div[1]/div/div[3]/div/span[1]/span/span/span[1]"); } }
        //use reguler expressions to catch dynamic objects
        public HtmlInputText WrittenbyTextbox { get { return _manager.ActiveBrowser.Find.ByExpression<HtmlInputText>("id=~dijit_form_ValidationTextBox_"); } }

        public Element Introduction { get { return _manager.ActiveBrowser.Find.ByExpression("name=introduction", "tagname=textarea"); } }

        public Element publishDropdown { get { return _manager.ActiveBrowser.Find.ById("dijit_form_DropDownButton_14_label"); } }

        public Element publishButton { get { return _manager.ActiveBrowser.Find.ByExpression("id=~dijit_form_Button_6", "tagname=span"); } }

        public Element ArticlePageHeading { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='aspnetForm']/div[4]/div[3]/div/div/div/h1"); } }


        public Element OpenTree { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='uniqName_48_57']/span[1]"); } }
        public Element CustomerPlusIcon { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='uniqName_0_26']/div[1]/span/span[4]/span[1]"); } }







    }
}
