using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int? ProductId { get; set; }
        public int? ClientId { get; set; }
        public int Rating1 { get; set; }

        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
    }
}
