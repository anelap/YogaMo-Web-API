using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IProductService
    {
        List<Model.Product> Get(ProductSearchRequest request);

        Model.Product Get(int id);

        List<Model.Product> GetAvailableProducts();

        Model.Product Update(int id, ProductInsertRequest request);

        Model.Product Insert(ProductInsertRequest request);

        void Delete(int id);


    }
}
