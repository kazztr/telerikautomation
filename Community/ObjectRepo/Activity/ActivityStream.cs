using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.Activity
{
    class ActivityStream
    {
        private Manager _manager;

        public ActivityStream(Manager m)
        {
            _manager = m;
        }

        public Element validateActivityFeed { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='feed']/div/div[1]/div[2]/div"); } }
        public Element validatePostName { get { return _manager.ActiveBrowser.Find.ByXPath("//*[@id='feed']/div/div[1]/div[2]/div/span[3]/span/a/span"); } }
    }
}
