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
            CreateSlidesDirectory(model.Id);
            var fileName = string.Concat(model.Permanent, Path.GetExtension(httpPostedFile.FileName));
            httpPostedFile.SaveAs(GetFullPath(model.Id, fileName));
            return 0;
        }

        private void CreateSlidesDirectory(Guid presentationId)
        {
            var directory = GetSlidesDirectory(presentationId);
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private string GetSlidesDirectory(Guid presentationId)
        {
            var directory = HttpContext.Current.Server.MapPath(root);
            return Path.Combine(directory, presentationId.ToString());
        }

        private string GetFullPath(Guid presentationId, string fileName)
        {
            return Path.Combine(GetSlidesDirectory(presentationId), fileName);
        }
    }
}