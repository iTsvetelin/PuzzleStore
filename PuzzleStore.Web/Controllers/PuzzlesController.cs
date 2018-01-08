using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PuzzleStore.Web.Models.Puzzles;
using PuzzleStore.Services;
using Microsoft.AspNetCore.Identity;
using PuzzleStore.Data.Models;
using PuzzleStore.Web.Infrastructure;

namespace PuzzleStore.Web.Controllers
{
    public class PuzzlesController : Controller
    {
        private readonly UserManager<User> userManager; 
        private readonly IPuzzleService puzzles;

        public PuzzlesController(
            UserManager<User> userManager,
            IPuzzleService puzzles)
        {
            this.userManager = userManager;
            this.puzzles = puzzles;
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddPuzzleViewModel puzzleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(puzzleModel);
            }

            this.puzzles.Create(
                puzzleModel.Manufacturer,
                puzzleModel.Title,
                puzzleModel.Difficulty,
                puzzleModel.PartsCount,
                puzzleModel.Price,
                puzzleModel.ImageUrl,
                puzzleModel.PartsMaterial,
                puzzleModel.Description,
                this.userManager.GetUserId(User)
                );

            return RedirectToAction(nameof(DisplayAll));
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult Edit(int Id)
        {
            var puzzleModel = this.puzzles.FindById(Id);

            var puzzleViewModel = new AddPuzzleViewModel
            {
                Manufacturer = puzzleModel.Manufacturer,
                Title = puzzleModel.Title,
                Difficulty = puzzleModel.Difficulty,
                PartsCount = puzzleModel.PartsCount,
                Price = puzzleModel.Price,
                ImageUrl = puzzleModel.ImageUrl,
                PartsMaterial = puzzleModel.PartsMaterial,
                Description = puzzleModel.Description
                
            };

            return this.View(puzzleViewModel);
            //forma s modela
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpPost]
        public IActionResult Edit(int Id, AddPuzzleViewModel puzzleModel)
        {
            this.puzzles.Edit(
                Id,
                puzzleModel.Manufacturer,
                puzzleModel.Title,
                puzzleModel.Difficulty,
                puzzleModel.PartsCount,
                puzzleModel.Price,
                puzzleModel.ImageUrl,
                puzzleModel.PartsMaterial,
                puzzleModel.Description
                );

            return RedirectToAction(nameof(DisplayAll));
        }

        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult Destroy(int id)
        {
            TempData["puzleId"] = id;
            return this.View();
        }

        [Authorize(Roles = "Administrator,Moderator")]
        [HttpPost]
        public IActionResult Destroy()
        {
            this.puzzles.Destroy(int.Parse(TempData["puzleId"].ToString()));

            return RedirectToAction(nameof(DisplayAll));
        }

        public IActionResult DisplayAll()
        {
            var result = this
                .puzzles
                .All()
                .Select(p => new DisplayPuzzlesViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    PartsCount = p.PartsCount,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .ToList();

            return this.View(result);
        }
    }
}