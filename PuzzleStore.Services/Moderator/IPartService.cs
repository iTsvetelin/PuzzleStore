using PuzzleStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleStore.Services.Moderator
{
    public interface IPartService
    {
        IEnumerable<Part> All();

        void Create(
            int puzzleId,
            string userId,
            int x,
            int y,
            DateTime initialized);

        void Destroy(int id);
    }
}
