using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Instructor
    {
        public Instructor()
        {
            Yoga = new HashSet<Yoga>();
        }

        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public bool? Status { get; set; }
        public string Title { get; set; }
        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Yoga> Yoga { get; set; }
    }
}
