using PuzzleStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Models.Puzzles
{
    public class AddPuzzleViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50,MinimumLength =2)]
        public string Title { get; set; }

        [Required]
        public PuzzleDifficulty Difficulty { get; set; }

        [Display(Name = "Parts Count")]
        [Required]
        [Range(0, 24000)]
        public int PartsCount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [StringLength(2000,MinimumLength =10)]
        public string ImageUrl { get; set; }

        [Display(Name ="Parts Material")]
        [Required]
        public PartsMaterial PartsMaterial { get; set; }

        [Required]
        [StringLength(1000, MinimumLength =10, ErrorMessage = "Description must be added between 10 and 1000 symbols. Ex: The puzzle has small parts and it's not recomended for children bellow 3 years.")]
        public string Description { get; set; }
    }
}
