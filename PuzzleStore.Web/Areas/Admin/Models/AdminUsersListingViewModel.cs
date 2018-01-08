using Microsoft.AspNetCore.Mvc.Rendering;
using PuzzleStore.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleStore.Web.Areas.Admin.Models
{
    public class AdminUsersListingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
