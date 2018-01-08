using PuzzleStore.Data.Models;
using PuzzleStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleStore.Services
{
    public interface IPuzzleService
    {
        void Create(
            string manufacturer,
            string title,
            PuzzleDifficulty difficulty,
            int partsCount,
            decimal price,
            string imageUrl,
            PartsMaterial partsMaterial,
            string description,
            string userId
            );

        void Edit(
            int Id,
            string manufacturer,
            string title,
            PuzzleDifficulty difficulty,
            int partsCount,
            decimal price,
            string imageUrl,
            PartsMaterial partsMaterial,
            string description);

        void Destroy(int id);

        Puzzle FindById(int id);
        
        IEnumerable<Puzzle> All();
    }
}
