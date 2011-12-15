using System.Web.Mvc;

namespace Projector.Site.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_attendee",
                "Admin/Presentations/{presentationid}/Attendees/{action}/{id}",
                new { controller = "Attendees", presentationid = "", action = "Index", id = UrlParameter.Optional}
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
