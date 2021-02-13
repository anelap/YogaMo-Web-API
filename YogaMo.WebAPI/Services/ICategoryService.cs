using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Services
{
    public interface ICategoryService
    {
        List<Model.Category> Get();
        Model.Category Get(int id);
        Model.Category Insert(string name);
        Model.Category Update(int id, string name);
        void Delete(int id);

    }
}
