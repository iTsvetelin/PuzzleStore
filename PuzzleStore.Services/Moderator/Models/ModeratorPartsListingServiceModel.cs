using PuzzleStore.Common.Mapping;
using PuzzleStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleStore.Services.Moderator.Models
{
    public class ModeratorPartsListingServiceModel
    {
        public int Id { get; set; }

        public int XCordinate { get; set; }
        
        public int YCordinate { get; set; }

        public DateTime Initialized { get; set; }

        public Puzzle Puzzle { get; set; }
        
        public User User { get; set; }
    }
}
