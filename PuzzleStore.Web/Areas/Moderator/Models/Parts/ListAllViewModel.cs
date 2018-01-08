using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Areas.Moderator.Models.Parts
{
    public class ListAllViewModel
    {
        public int Id { get; set; }

        public string PuzzleFullName { get; set; }

        public string PartPosition { get; set; }

        public string UserName { get; set; }

        public int DaysWentBy { get; set; }
        
    }
}
