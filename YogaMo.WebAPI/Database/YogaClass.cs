using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class YogaClass
    {
        public int YogaClassId { get; set; }
        public int? YogaId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public virtual Yoga Yoga { get; set; }
    }
}
