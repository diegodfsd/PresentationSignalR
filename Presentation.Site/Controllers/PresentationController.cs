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

        public ActionResult Show(string permanent)
        {
            var identity = (PresentationIdentity)User.Identity;
            var presenteation = presentations
                .FindOne(p => p.Permanent.Equals(permanent));
            
            var attendee = presenteation
                .Attendees
                .SingleOrDefault(a => a.Email.Equals(identity.UserName));

            dynamic presentationView = new ExpandoObject();
            presentationView.Presentation = presenteation;
            presentationView.Attendee = attendee;

            return View(presentationView);
        }
    }
}
