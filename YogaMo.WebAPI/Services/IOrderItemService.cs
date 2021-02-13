using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IOrderItemService
    {
        List<Model.OrderItem> Get(OrderItemSearchRequest request);
        Model.OrderItem Get(int id);
        Model.OrderItem Insert(OrderItemInsertRequest request);
        Model.OrderItem Update(int id, OrderItemInsertRequest request);
        void Delete(int id);

    }
}
