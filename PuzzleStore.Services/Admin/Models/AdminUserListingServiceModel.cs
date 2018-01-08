using PuzzleStore.Common.Mapping;
using PuzzleStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleStore.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
