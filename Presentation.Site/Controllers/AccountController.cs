using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Projector.Site.Models;
using Projector.Site.Repositories.Contract;

namespace Projector.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<Presentation> presentations;

        public AccountController()
        {
            presentations = new Repository<Presentation>();
        }

        public ActionResult LogOn()
        {
            return View(new LogOnFormModel());
        }

        [HttpPost]
        public ActionResult LogOn(LogOnFormModel logOnFormModel, string permanent, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var presentation = presentations
                    .FindOne(p => p.Permanent == "Ruby31");
                var attendee = (presentation ?? new Presentation()).Attendees.SingleOrDefault(a => a.Email == logOnFormModel.Email && a.Password == logOnFormModel.Password);

                if(attendee == null)
                {
                    return View(logOnFormModel);
                }

                var ticket = new FormsAuthenticationTicket(1, attendee.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false, attendee.Email);
                var encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return Redirect(returnUrl);
            }
            return View(logOnFormModel);
        }
    }
}
