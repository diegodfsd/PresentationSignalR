using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projector.Site.Areas.Admin.Controllers
{
    public class AttendeesController : Controller
    {
        //
        // GET: /Admin/Attendees/

        public ActionResult Index()
        {
            return View();
        }
    }
}
