using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PuzzleStore.Data.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public List<Puzzle> Puzzles { get; set; } = new List<Puzzle>();

        public List<Part> Parts { get; set; } = new List<Part>();
    }
}
