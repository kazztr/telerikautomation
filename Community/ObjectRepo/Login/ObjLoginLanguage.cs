using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Login
{
    class ObjLoginLanguage
    {
        private Manager _manager;

        public ObjLoginLanguage(Manager m)
        {
            _manager = m;
        }

        public Element divSplashScreen { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[1]/div/div[1]"); } }

        public Element btnGotit { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='lbClose']"); } }

        //public Element languageSelector { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/form/div[3]/div/div[2]/div[1]"); } }

        public Element languageSelector { get { return _manager.ActiveBrowser.Find.ByXPath("/html/body/form/div[3]/div/div[2]/div[1]/ul"); } }

        public Element username { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_username"); } }

        public Element password { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_password"); } }

        public Element loginbutton { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_btnLogin"); } }

        public Element loginUnameSuperID { get { return _manager.ActiveBrowser.Find.ById("Username"); } }
        public Element loginPasswordSuperID { get { return _manager.ActiveBrowser.Find.ById("Password"); } }
        public Element loginButtonSuperID { get { return _manager.ActiveBrowser.Find.ById("loginButton"); } }


        public Element loginLink { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_lvHeaderLogin_LinkButton1"); } }

        public Element logoutlink { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_lvHeaderLogin_lbLogOut"); } }


        public Element lanDnask { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_rpLaguageSelection_ctl01_btnLanguage"); } }
        public Element lanDeutsch { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_rpLaguageSelection_ctl02_btnLanguage"); } }
        public Element lanDutch { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_rpLaguageSelection_ctl03_btnLanguage"); } }
        public Element lanEnglish { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_rpLaguageSelection_ctl04_btnLanguage"); } }
        public Element lanNorsk { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_rpLaguageSelection_ctl05_btnLanguage"); } }
        public Element lanSvenska { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_rpLaguageSelection_ctl06_btnLanguage"); } }

        public Element labelLoginHeadder { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_hostDiv_puGlobalLogin']/div/div/div/div/h2"); } }
        public Element labelUname { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_lblUsername"); } }
        public Element labelPwd { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_lblPassword"); } }
        public Element labelRemember { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_pnlLogin']/div[3]/span"); } }
        public Element labelforgot { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_forgot_password"); } }
        public Element labellogin { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_ucCommunityGlobalLogin_puGlobalLogin_tbGlobalLogin_relateLogin_btnLogin"); } }

        public Element signin { get { return _manager.ActiveBrowser.Find.ById("ctl00_PageHeader1_lvHeaderLogin_LinkButton1"); } }

        public Element Episerverbutton { get { return _manager.ActiveBrowser.Find.ById("epi-quickNavigator"); } }

    }
}
