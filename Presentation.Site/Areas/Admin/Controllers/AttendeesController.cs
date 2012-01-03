using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projector.Site.Models;
using Projector.Site.Repositories.Contract;

namespace Projector.Site.Areas.Admin.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly IRepository<Presentation> presentations;

        public AttendeesController()
        {
            presentations = new Repository<Presentation>();
        }

        public ActionResult Index(Guid presentationid)
        {
            var presentation = presentations.FindOne(p => p.Id == presentationid);
            return View(presentation);
        }
    }
}
