using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using Projector.Site.Models;

namespace Projector.Site.Commands
{
    public class ConvertPresentationCommand : ICommand<Presentation>
    {
        public int Execute(Presentation model)
        {
            var directory = GetSlidesDirectory(model.Id);
            var command = new StringBuilder();
            command.AppendFormat("cd {0} && ", directory)
                .Append("gswin32c -q ")
                .Append("-sDEVICE=pngalpha ")
                .Append("-dNOPAUSE ")
                .Append("-dBATCH ")
                .AppendFormat("-sOutputFile=\"slide%d.png\" \"{0}.pdf\" ", model.Id)
                .Append("&& dir /b | find /c \".png\"");

            return Run(command.ToString());
        }

        private int Run(string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c " + command);
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            Process process = Process.Start(processStartInfo);
            StreamReader result = process.StandardOutput;

            while (!process.HasExited)
            { }

            return Convert.ToInt32(result.ReadToEnd());
        }

        private string GetSlidesDirectory(Guid id)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/Slides"), id.ToString());
        }
    }
}