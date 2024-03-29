﻿using System;
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
                "Admin/Presentations/{presentationid}/Attendees/{action}",
                new { controller = "Attendees", action = "Index", presentationid = Guid.Empty },
                new { presentationid = @"[\w-]+" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
