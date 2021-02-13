using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IYogaClassService
    {
        List<Model.YogaClass> Get(YogaClassSearchRequest request);
        Model.YogaClass Get(int id);
        Model.YogaClass Insert(YogaClassInsertRequest request);
        Model.YogaClass Update(int id, YogaClassInsertRequest request);

        void Delete(int id);
    }
}
