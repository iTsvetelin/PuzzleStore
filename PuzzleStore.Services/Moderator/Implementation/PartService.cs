using System;
using System.Collections.Generic;
using System.Text;
using PuzzleStore.Data.Models;
using PuzzleStore.Data;
using System.Linq;
using PuzzleStore.Services.Admin;
using AutoMapper.QueryableExtensions;

namespace PuzzleStore.Services.Moderator.Implementation
{
    public class PartService : IPartService
    {
        private readonly PuzzleStoreDbContext db;

        public PartService(PuzzleStoreDbContext db,
            IPuzzleService puzzles,
            IAdminUserService users)
        {
            this.db = db;
        }

        public IEnumerable<Part> All()
            => this.db
            .Parts
            .ToList();

        public void Create(
            int puzzleId,
            string userId,
            int x,
            int y,
            DateTime initialized)
        {
           
            var part = new Part
            {
                PuzzleId = puzzleId,
                UserId = userId,
                XCordinate = x,
                YCordinate = y,
                Initialized = initialized
            };

            this.db.Add(part);
            this.db.SaveChanges();
        }

        public void Destroy(int id)
        {
            var part = this
                .db
                .Parts
                .Find(id);

            this.db.Remove(part);
            this.db.SaveChanges();

        }
    }
}
