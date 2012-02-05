using System.Security.Principal;
using System.Web.Security;

namespace Projector.Site.Models
{
    public class PresentationIdentity : IIdentity
    {
        private readonly FormsAuthenticationTicket ticket;

        public PresentationIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        public string Name
        {
            get { return ticket.Name; }
        }

        public string AuthenticationType
        {
            get { return "Presentation"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string UserName
        {
            get { return ticket.UserData;  }
        }
    }
}