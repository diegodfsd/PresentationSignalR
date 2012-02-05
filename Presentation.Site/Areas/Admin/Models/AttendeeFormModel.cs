using System;
using System.ComponentModel.DataAnnotations;

namespace Projector.Site.Areas.Admin.Models
{
    public class AttendeeFormModel
    {
        public string Title { get; set; }

        public Guid presentationId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required"),
         RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", 
                           ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        public bool Speaker { get; set; }

        public AttendeeFormModel()
        {
        }

        public AttendeeFormModel(Guid presentationId, string title)
        {
            this.presentationId = presentationId;
            this.Title = title;
        }
    }
}