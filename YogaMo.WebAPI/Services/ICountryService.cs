using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Services
{
    public interface ICountryService
    {
        List<Model.Country> Get();
        Model.Country Get(int id);
        Model.Country Insert(string name);
        Model.Country Update(int id, string name);
        void Delete(int id);

    }
}
