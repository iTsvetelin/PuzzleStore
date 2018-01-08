using System;
using System.Collections.Generic;
using System.Text;
using PuzzleStore.Data.Models.Enums;
using PuzzleStore.Data;
using PuzzleStore.Data.Models;
using System.Linq;

namespace PuzzleStore.Services.Implementations
{
    public class PuzzleService : IPuzzleService
    {
        private readonly PuzzleStoreDbContext db;

        public PuzzleService(PuzzleStoreDbContext db)
        {
            this.db = db;
        }

       
        public void Create(
            string manufacturer, 
            string title, 
            PuzzleDifficulty difficulty, 
            int partsCount, 
            decimal price, 
            string imageUrl, 
            PartsMaterial partsMaterial, 
            string description,
            string userId
            )
        {
            var puzzle = new Puzzle
            {
                Manufacturer = manufacturer,
                Title = title,
                Difficulty = difficulty,
                PartsCount = partsCount,
                Price = price,
                ImageUrl = imageUrl,
                PartsMaterial = partsMaterial,
                Description = description,
                UserId = userId
            };

            this.db.Add(puzzle);
            this.db.SaveChanges();
        }

        public void Edit(
            int Id,
            string manufacturer,
            string title,
            PuzzleDifficulty difficulty,
            int partsCount,
            decimal price,
            string imageUrl,
            PartsMaterial partsMaterial,
            string description)
        {
            var puzzle = this.FindById(Id);

            puzzle.Manufacturer = manufacturer;
            puzzle.Title = title;
            puzzle.Difficulty = difficulty;
            puzzle.PartsCount = partsCount;
            puzzle.ImageUrl = imageUrl;
            puzzle.PartsMaterial = partsMaterial;
            puzzle.Description = description;

            db.SaveChanges();

        }

        public void Destroy(int id)
        {
            var puzzle = this.FindById(id);

            this.db.Remove(puzzle);
            this.db.SaveChanges();

        }

        public Puzzle FindById(int id)
            => this
            .db
            .Puzzles
            .Find(id);

        public IEnumerable<Puzzle> All()
            => this
            .db
            .Puzzles
            .OrderBy(p => p.Id)
            .ToList();
    }
}
