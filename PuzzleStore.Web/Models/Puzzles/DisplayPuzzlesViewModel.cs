using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Models.Puzzles
{
    public class DisplayPuzzlesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PartsCount { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string UserName { get; set; }
    }
}
