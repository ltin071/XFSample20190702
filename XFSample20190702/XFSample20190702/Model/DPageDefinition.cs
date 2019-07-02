using System;
using System.Collections.Generic;

namespace XFSample20190702.Model
{
    public class DPageDefinition
    {
        public string title;
        public string icon;
        public Type pageClass;
        public List<DPageDefinition> subPages;

        public DPageDefinition(string title, string icon, Type pageClass = null, List<DPageDefinition> subPages = null)
        {
            this.title = title;
            this.icon = icon;
            this.pageClass = pageClass;
            this.subPages = subPages;
        }
    }
}
