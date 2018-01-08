using PuzzleStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleStore.Services
{
    public interface IGuideService
    {
        IEnumerable<Guide> All();

        Guide Details(int id);

        void Create(string title, string content = null, string relationUrl = null);

        void Delete(int id);
    }
}
