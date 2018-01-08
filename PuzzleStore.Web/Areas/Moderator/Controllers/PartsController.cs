using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuzzleStore.Web.Areas.Moderator.Models.Parts;
using PuzzleStore.Services.Moderator;
using PuzzleStore.Data.Models;
using PuzzleStore.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PuzzleStore.Services;

namespace PuzzleStore.Web.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    [Authorize(Roles = GlobalConstants.ModeratorRole)]
    public class PartsController : Controller
    {
        private readonly IPartService parts;
        private readonly IPuzzleService puzzles;
        private readonly UserManager<User> userManager;

        public PartsController(IPartService parts,
            IPuzzleService puzzles,
            UserManager<User> userManager)
        {
            this.parts = parts;
            this.puzzles = puzzles;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var result = new List<ListAllViewModel>();

            var parts = this
                .parts
                .All();
            foreach(var part in parts)
            {
                var puzzle = this.puzzles.FindById(part.PuzzleId);
                var item = new ListAllViewModel
                {
                    Id = part.Id,
                    PuzzleFullName = $"{puzzle.Manufacturer} - {puzzle.Title}",
                    DaysWentBy = (DateTime.UtcNow - part.Initialized).Days
                };
                result.Add(item);
            }

            return View(result);
        }

        public IActionResult Create(int Id)
        {
            TempData["puzzleId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePartFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var puzzle = this.puzzles.FindById(int.Parse(TempData["puzzleId"].ToString()));
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (puzzle == null || user == null)
            {
                return NotFound();
            }

            this.parts.Create(
                puzzle.Id,
                user.Id,
                model.XCordinate,
                model.YCordinate,
                model.Initialized
                );

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Destroy(int id)
        {
            TempData["partId"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult Destroy()
        {
            this.parts.Destroy(int.Parse(TempData["partId"].ToString()));

            return RedirectToAction(nameof(Index));
        }
    }
}