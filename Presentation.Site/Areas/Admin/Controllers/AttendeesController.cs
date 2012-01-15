using System;
using System.Web.Mvc;
using Projector.Site.Areas.Admin.Models;
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

        public ActionResult Add(Guid presentationid)
        {
            var presentation = presentations.FindOne(p => p.Id == presentationid);
            var attendeeFormModel = new AttendeeFormModel(presentation.Id, presentation.Title);
            
            return View(attendeeFormModel);
        }

        [HttpPost]
        public ActionResult Add(AttendeeFormModel attendeeFormModel)
        {
            if (ModelState.IsValid)
            {
                var presentation = presentations.FindOne(p => p.Id == attendeeFormModel.presentationId);
                var attendee = new Attendee
                                   {
                                       Name = attendeeFormModel.Name,
                                       Email = attendeeFormModel.Email,
                                       Password = attendeeFormModel.Password,
                                       Speaker = attendeeFormModel.Speaker
                                   };

                presentation.AddAttendee(attendee);
                presentations.Update(presentation);

                return RedirectToAction("Details", "Presentations", new { id = presentation.Id });
            }

            return View(attendeeFormModel);
        }
    }
}
