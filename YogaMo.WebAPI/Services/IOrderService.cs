using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IOrderService
    {
        List<Model.Order> Get(OrderSearchRequest request);
        Model.Order Get(int id);
        List<Model.Order> GetByDate(int day, int month, int year);
        List<Model.Order> GetByClient(int id);
        Model.Order Insert(OrderInsertRequest request);
        Model.Order Update(int id, OrderInsertRequest request);
        void Delete(int id);
        bool ConfirmOrder(PaymentIntentCreateRequest request);
    }
}
