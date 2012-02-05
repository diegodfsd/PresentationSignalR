using System.Collections.Concurrent;
using SignalR.Hubs;

namespace Projector.Site.SignalR
{
    public class ServerHub : Hub
    {
        private readonly ConcurrentDictionary<string, int> presentationsState = new ConcurrentDictionary<string, int>();

        public void SignIn(string presentationId, string attendeeId)
        {
            Caller.PresentationId = presentationId;
            Caller.AttendeeId = attendeeId;
            Caller.CurrentSlide = presentationsState.GetOrAdd(presentationId, 0);

            AddToGroup(presentationId);
        }

        public void GoTo(int slideIndex)
        {
            string presentationId = Caller.PresentationId;
            presentationsState.AddOrUpdate(presentationId, slideIndex, (k,v) => slideIndex);
            Clients[presentationId].Show(slideIndex);
        }

        public void Send(string presentationId, string attendeeId, string message)
        {
            Clients[presentationId].ReceiveMessage(attendeeId, message);
        }
    }
}