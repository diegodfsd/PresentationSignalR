using System;
using System.IO;
using System.Web;
using Projector.Site.Models;

namespace Projector.Site.Commands
{
    public class FilePresentationCommand : ICommand<Presentation>
    {
        private const string root = "~/Slides";
        private readonly HttpPostedFileBase httpPostedFile;

        public FilePresentationCommand(HttpPostedFileBase httpPostedFile)
        {
            this.httpPostedFile = httpPostedFile;
        }

        public int Execute(Presentation model)
        {
            var fileName = string.Concat(model.Id, Path.GetExtension(httpPostedFile.FileName));
            httpPostedFile.SaveAs(GetSlidesDirectory(model.Id, fileName));
            return 0;
        }

        private string GetSlidesDirectory(Guid presentationId, string fileName)
        {
            var directory = HttpContext.Current.Server.MapPath(root);
            return Path.Combine(directory, presentationId.ToString(), fileName);
        }
    }
}