using PuzzleStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PuzzleStore.Data.Models
{
    public class Puzzle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        public PuzzleDifficulty Difficulty { get; set; }

        [Required]
        [Range(0,24000)]
        public int PartsCount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string ImageUrl { get; set; }

        [Required]
        public PartsMaterial PartsMaterial { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Part> Parts { get; set; } = new List<Part>();
    }
}
