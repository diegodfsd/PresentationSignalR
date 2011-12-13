using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Norm.Attributes;

namespace Projector.Site.Models
{
    //Reference:http://pt.gravatar.com/site/implement/images/

    public class Attendee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Speaker { get; set; }

        [MongoIgnore]
        public string GravatarUrl
        {
            get { return string.Format("http://www.gravatar.com/avatar/{0}?s=30d=mm", ComputeHash()); }
        }

        public Attendee()
        {
            Id = Guid.NewGuid();
        }

        private string ComputeHash()
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(Email));
            return data.Aggregate("", (hash, item) => hash += item.ToString("x2"));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object other)
        {
            var instance = other as Attendee;
            return instance != null && Email == instance.Email;
        }
    }
}