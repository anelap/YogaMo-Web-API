using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IYogaService
    {
        List<Model.Yoga> Get(YogaSearchRequest request);
        Model.Yoga Get(int id);
        Model.Yoga Insert(YogaInsertRequest request);
        Model.Yoga Update(int id, YogaInsertRequest request);
        void Delete(int id);
    }
}
