using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Areas.Moderator.Models.Parts
{
    public class CreatePartFormModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int XCordinate { get; set; }

        [Required]
        public int YCordinate { get; set; }

        public DateTime Initialized { get; set; }
    }
}
