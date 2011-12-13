using System.ComponentModel.DataAnnotations;
using System.Web;
using Projector.Site.Models;

namespace Projector.Site.Areas.Admin.Models
{
    public class PresentationFormViewModel
    {
        public PresentationFormViewModel()
        {
        }

        public PresentationFormViewModel(Presentation presentation)
        {
            Title = presentation.Title;
            Description = presentation.Description;
        }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; private set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; private set; }
        [Required(ErrorMessage = "Presentation file is required.")]
        public HttpPostedFileBase PresentationFile { get; set; }
    }
}