using PuzzleStore.Data;
using PuzzleStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleStore.Services.Implementations
{
    public class GuideService : IGuideService
    {
        private readonly PuzzleStoreDbContext db;

        public GuideService(PuzzleStoreDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Guide> All()
            => this.db
                .Guides
                .ToList();

        public void Create(string title, string content, string realationUrl)
        {
            var guide = new Guide
            {
                Title = title,
                Content = content,
                RelationUrl = realationUrl
            };

            this.db.Add(guide);
            this.db.SaveChanges();
        }

        public Guide Details(int id)
            => this.db
                .Guides
                .Find(id);
    }
}
