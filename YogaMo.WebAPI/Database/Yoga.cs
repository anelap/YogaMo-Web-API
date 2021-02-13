using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Yoga
    {
        public Yoga()
        {
            YogaClass = new HashSet<YogaClass>();
        }

        public int YogaId { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }
        public string Description { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<YogaClass> YogaClass { get; set; }
    }
}
