using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<SelectListItem> AllRoles { get; set; }
    }
}
