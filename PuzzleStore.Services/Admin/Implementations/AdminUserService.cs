using System;
using System.Collections.Generic;
using System.Text;
using PuzzleStore.Services.Admin.Models;
using PuzzleStore.Data;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PuzzleStore.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly PuzzleStoreDbContext db;

        public AdminUserService(PuzzleStoreDbContext db)
        {
            this.db = db; 
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
