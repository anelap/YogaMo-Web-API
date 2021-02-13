using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public OrderService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool ConfirmOrder(PaymentIntentCreateRequest request)
        {
            var Order = _context.Order.Where(x => x.OrderId == request.OrderId).FirstOrDefault();
            if (Order == null) throw new UserException("Order not found");

            var paymentIntents = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = Convert.ToInt32(Order.TotalPrice * 100),
                Currency = "usd",
                Confirm = true,
                PaymentMethod = request.PaymentMethodId,
                Metadata = new Dictionary<string, string>()
            };
            options.Metadata.Add("OrderId", request.OrderId.ToString());

            var paymentIntent = paymentIntents.Create(options);
            if (paymentIntent == null)
                return false;

            foreach (var charge in paymentIntent.Charges)
            {
                if (charge.Paid == true)
                {
                    Order.OrderStatus = Database.OrderStatus.Confirmed;
                    _context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public void Delete(int id)
        {
            var entity = _context.Order.Find(id);
            var items = _context.OrderItem.Where(x => x.OrderId == id).ToList();
            foreach (var item in items)
            {
                _context.OrderItem.Remove(item);
            }
            _context.Order.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.Order> Get(OrderSearchRequest request)
        {
            int OrderStatusInt = request.OrderStatus != null ? (int)request.OrderStatus.Value : 0;
            var OrderStatusEnum = (Database.OrderStatus)OrderStatusInt;

            var list = _context.Order
                .Include(x => x.Client)
                .Where(x => request.OrderStatus == null || x.OrderStatus == OrderStatusEnum)
                .OrderByDescending(x => x.Date)
                .ToList();

            var listModel = _mapper.Map<List<Model.Order>>(list);

            foreach (var item in listModel)
            {
                item.Client.ProfilePicture = null;
            }

            return listModel;
        }

        public Model.Order Get(int id)
        {
            var entity = _context.Order.Find(id);

            if (entity == null) throw new UserException("Order not found");

            return _mapper.Map<Model.Order>(entity);
        }

        public List<Model.Order> GetByClient(int id)
        {
            var list = _context.Order.Where(x => x.ClientId == id).OrderByDescending(d => d.Date).ToList();

            return _mapper.Map<List<Model.Order>>(list); //provjeriti samo kako vraca ove podatke... da li treba jos neki model se napraviti
        }

        public List<Model.Order> GetByDate(int day, int month, int year)
        {
            var query = _context.Order.AsQueryable();
            if (day == 0 || month == 0 || year == 0)
            {
                DateTime today = DateTime.Now;
                query = query.Where(x => x.Date.Year == today.Year && x.Date.Month == today.Month && x.Date.Day == today.Day);
                var list = query.ToList();
                return _mapper.Map<List<Model.Order>>(list);
            }
            else
            {
                query = query.Where(x => x.Date.Year == year && x.Date.Month == month && x.Date.Day == day);
                var list = query.ToList();
                return _mapper.Map<List<Model.Order>>(list);
            }
        }

        public Model.Order Insert(OrderInsertRequest request)
        {
            var entity = _mapper.Map<Database.Order>(request);

            var client = _context.Client.Where(x => x.ClientId == request.ClientId).FirstOrDefault();
            if (client == null) throw new UserException("Client not found");
            entity.Client = client;

            _context.Order.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Order>(entity);
        }

        public Model.Order Update(int id, OrderInsertRequest request)
        {
            var entity = _context.Order.Find(id);

            var client = _context.Client.Where(x => x.ClientId == request.ClientId).FirstOrDefault();
            if (client == null) throw new UserException("Client not found");
            entity.Client = client;

            Mapper.Map<OrderInsertRequest, Database.Order>(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Order>(entity);
        }

    }
}
