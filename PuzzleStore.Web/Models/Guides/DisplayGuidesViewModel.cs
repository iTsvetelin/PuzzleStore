using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Models.Guides
{
    public class DisplayGuidesViewModel
    {
        public IList<OnSiteGuides> OnSite { get; set; }

        public IList<RelatedGuides> Related { get; set; }
    }
}
