﻿using System;
using System.Linq;
using System.Web.Mvc;
using Projector.Site.Areas.Admin.Models;
using Projector.Site.Commands;
using Projector.Site.Models;
using Projector.Site.Repositories.Contract;

namespace Projector.Site.Areas.Admin.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly IRepository<Presentation> presentations;
        private readonly ICommandHandler<Presentation> commandHandler;

        public PresentationsController()
        {
            presentations = new Repository<Presentation>();
            commandHandler = new CommandHandler<Presentation>();
        }


        public ActionResult Index()
        {
            return View(presentations.All());
        }

        public ActionResult Create()
        {
            return View(new PresentationFormViewModel());
        }

        [HttpPost]
        public ActionResult Create(PresentationFormViewModel presentationForm)
        {
            //TODO:preciso remover a pasta /slides do git
            if(ModelState.IsValid)
            {
                var presentation = new Presentation
                                       {
                                           Title = presentationForm.Title,
                                           Description = presentationForm.Description
                                       };

                presentation.QtdSlides = commandHandler
                    .Add(new FilePresentationCommand(presentationForm.PresentationFile))
                    .Add(new ConvertPresentationCommand())
                    .Process(presentation).Last();

                presentations.Add(presentation);

                return RedirectToAction("Details", new{ id = presentation.Id });
            }
            return View(presentationForm);
        }

        public ActionResult Details(Guid id)
        {
            var presentation = presentations.FindOne(p => p.Id == id);
            return View(presentation);
        }
    }
}
