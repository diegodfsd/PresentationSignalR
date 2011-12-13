using System;
using System.Collections.Generic;
using System.Linq;

namespace Projector.Site.Models
{
    public class Presentation
    {
        private readonly IList<Attendee> attendees;

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int QtdSlides { get; set; }
        public IEnumerable<Attendee> Attendees
        {
            get { return attendees; }
        }

        public Presentation()
        {
            Id = Guid.NewGuid();
            attendees = new List<Attendee>();
        }

        public void AddAttendee(Attendee attendee)
        {
            if (attendees.Any(attendee.Equals))
            {
                attendees.Add(attendee);
            }
        }
    }
}