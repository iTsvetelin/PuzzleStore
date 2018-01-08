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
        public string Title { get; set; }

        public string RelationUrl { get; set; }
        
        public string Content { get; set; }
    }
}
