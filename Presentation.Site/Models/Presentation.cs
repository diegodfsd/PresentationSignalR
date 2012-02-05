using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Norm.Attributes;

namespace Projector.Site.Models
{
    public class Presentation
    {
        private readonly IList<Attendee> attendees;

        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public string Permanent { get; protected set; }
        [MongoIgnore]
        public Attendee Speaker
        {
            get
            { return attendees.SingleOrDefault(attendee => attendee.Speaker); }
        }
        public string Description { get; set; }
        public int QtdSlides { get; set; }
        public IEnumerable<Attendee> Attendees
        {
            get { return attendees; }
        }

        public Presentation()
        {}

        public Presentation(string title, string description)
            : this()
        {
            Title = title;
            Description = description;
            Permanent = Regex.Replace(Title, @"[^(\w|0-9)]+", "");
            Id =  Guid.NewGuid();
            attendees = new List<Attendee>();
        }

        public void AddAttendee(Attendee attendee)
        {
            if (!attendees.Any(attendee.Equals))
            {
                attendees.Add(attendee);
            }
        }

        public bool SpeakerIs(Guid attendeId)
        {
            var speaker = Speaker;

            return speaker != null 
                ? speaker.Speaker : false;
        }
    }
}