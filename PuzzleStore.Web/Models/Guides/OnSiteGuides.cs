﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Models.Guides
{
    public class OnSiteGuides : GuideViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
