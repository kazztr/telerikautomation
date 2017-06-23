using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ObjectRepo.TechnicalForum
{
    class TechForum
    {
        private Manager _manager;

        public TechForum(Manager m)
        {
            _manager = m;
        }

        public Element CreateTopicButton { get { return _manager.ActiveBrowser.Find.ById("btnCreateTopicbtn btn-primary"); } }
        public Element TopicSubject { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucNewTopic_puNewTopic_txtSubject"); } }
        public Element SendEmail { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucNewTopic_puNewTopic_chkSubscribe"); } }
        public Element Tags { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucNewTopic_puNewTopic_txtTags"); } }
        public Element PostButton { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucNewTopic_puNewTopic_btnPostTopic"); } }
        public Element ReplyButton { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucTopic_tbTopic_btnReply"); } }
        public Element postReply { get { return _manager.ActiveBrowser.Find.ById("ctl00_plhMainContentArea_plhLeftContentArea_ucTopic_tbTopic_ucNewReply_cpNewReply_btnPost"); } }

    }
}
