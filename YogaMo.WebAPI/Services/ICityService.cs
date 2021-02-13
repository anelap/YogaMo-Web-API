using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Services
{
    public interface ICityService
    {
        List<Model.City> Get(Model.Requests.CitiesSearchRequest request);
        Model.City Get(int id);
        Model.City Insert(string name);
        Model.City Update(int id, string name);
        void Delete(int id);

    }
}
