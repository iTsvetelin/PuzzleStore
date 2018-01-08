using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PuzzleStore.Data.Models
{
    public class Guide
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 5)]
        public string Title { get; set; }
        
        public string RelationUrl { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Content { get; set; }
    }
}
