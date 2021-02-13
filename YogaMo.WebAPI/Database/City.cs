using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class City
    {
        public City()
        {
            Client = new HashSet<Client>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Client> Client { get; set; }
    }
}
