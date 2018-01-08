using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuzzleStore.Services;
using PuzzleStore.Web.Models.Guides;

namespace PuzzleStore.Web.Controllers
{
    public class GuidesController : Controller
    {
        private readonly IGuideService guides;

        public GuidesController(IGuideService guides)
        {
            this.guides = guides;
        }

        public IActionResult Details(int id)
        {
            var guide = this.guides
                .Details(id);

            if(guide.Content == null)
            {
                return NotFound();
            }

            var result = new GuideDetailsViewModel
            {
                Title = guide.Title,
                Content = guide.Content
            };

            return View(result);
        }

        public IActionResult Create()
            => View();

        [HttpPost]
        public IActionResult Create(CreateGuideFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.guides.Create(model.Title, model.Content);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var result = this.guides
                .All();

            var model = new DisplayGuidesViewModel
            {
                OnSite = new List<OnSiteGuides>(),
                Related = new List<RelatedGuides>()
            };
           

            foreach(var guide in result)
            {
                if(guide.Content == null)
                {
                    if(guide.RelationUrl == null)
                    {
                        continue;
                    }
                    var guideMap = new RelatedGuides
                    {
                        Title = guide.Title,
                        Url = guide.RelationUrl
                    };
                    model.Related.Add(guideMap);
                }
                else
                {
                    var guideMap = new OnSiteGuides
                    {
                        Id = guide.Id,
                        Title = guide.Title,
                        Content = guide.Content
                    };
                    model.OnSite.Add(guideMap);
                }
                
            }

            return View(model);
        }
    }
}