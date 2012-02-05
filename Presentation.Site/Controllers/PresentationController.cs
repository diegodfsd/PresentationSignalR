using System;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Projector.Site.Models;
using Projector.Site.Repositories;
using Projector.Site.Repositories.Contract;

namespace Projector.Site.Controllers
{
    public class PresentationController : Controller
    {
        private readonly IRepository<Presentation> presentations;

        public PresentationController()
        {
            presentations = new Repository<Presentation>();
        }

        public ActionResult Index()
        {
            var all = presentations.All();

            return View(all);
        }

        [Authorize]
        public ActionResult Show(string permanent)
        {
            var identity = (PresentationIdentity)User.Identity;
            var presenteation = presentations
                .FindOne(p => p.Permanent == permanent);
            
            var attendee = presenteation
                .Attendees
                .SingleOrDefault(a => a.Email.Equals(identity.UserName));

            ViewBag.Presentation = presenteation;
            ViewBag.Attendee = attendee;

            return View();
        }
    }
}
