using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PuzzleStore.Data.Models
{
    public class Part
    {
        public int Id { get; set; }

        public int PuzzleId { get; set; }
        
        public Puzzle Puzzle { get; set; }

        [Required]
        public int XCordinate { get; set; }

        [Required]
        public int YCordinate { get; set; }

        public DateTime Initialized { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
