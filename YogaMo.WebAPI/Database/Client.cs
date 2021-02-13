using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Client
    {
        public Client()
        {
            Order = new HashSet<Order>();
            Rating = new HashSet<Rating>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public int? CityId { get; set; }
        public byte[] ProfilePicture { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }

    }
}

