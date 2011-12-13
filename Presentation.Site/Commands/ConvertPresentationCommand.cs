using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Projector.Site.Models;

namespace Projector.Site.Commands
{
    public class ConvertPresentationCommand : ICommand<Presentation>
    {
        public int Execute(Presentation model)
        {
            var directory = GetSlidesDirectory(model.Id);
            var command = GetCommand(directory, model.Permanent);

            return Run(command);
        }

        private string GetCommand(string directory, string permanent)
        {
            var commands = new[]
                               {
                                   "cd " + directory,
                                   "gswin32c -q -sDEVICE=pngalpha -dNOPAUSE -dBATCH -sOutputFile=\"slide%d.png\" \""+ permanent +"\".pdf",
                                   "dir /b | find /c \".png\""
                               };

            return string.Join(" && ", commands);
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

            var output = result.ReadToEnd();
            return Count(output);
        }

        private int Count(string output)
        {
            return Convert.ToInt32(Regex.Replace(output, @"[^(\d)]+/g", ""));
        }

        private string GetSlidesDirectory(Guid id)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/Slides"), id.ToString());
        }
    }
}