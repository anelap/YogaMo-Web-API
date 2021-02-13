using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class OrderItemService : IOrderItemService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public OrderItemService(_150222Context context, IMapper mapper, IClientService _clientService)
        {
            _context = context;
            _mapper = mapper;
            this._clientService = _clientService;
        }

        public void Delete(int id)
        {
            var entity = _context.OrderItem.Find(id);

            _context.OrderItem.Remove(entity);
        }

        public List<Model.OrderItem> Get(OrderItemSearchRequest request)
        {
            var query = _context.OrderItem
                .Where(x=>x.OrderId == request.OrderId || request.OrderId == 0)
                .AsQueryable();

            var CurrentClient = _clientService.GetCurrentClient();
            if(CurrentClient != null)
            {
                query = query.Where(x => x.Order.ClientId == CurrentClient.ClientId);
            }

            query = query.Include(x => x.Product);

            var list = query.ToList();

            return _mapper.Map<List<Model.OrderItem>>(list);
        }

        public Model.OrderItem Get(int id)
        {
            var entity = _context.OrderItem.Find(id);

            return _mapper.Map<Model.OrderItem>(entity);
        }

        public Model.OrderItem Insert(OrderItemInsertRequest request)
        {
            var CurrentClient = _clientService.GetCurrentClient();

            var entity = _mapper.Map<Database.OrderItem>(request);

            var order = _context.Order.Find(request.OrderId);

            var product = _context.Product.Find(request.ProductId);

            if(order==null)
            {
                order = _context.Order.Where(x => x.ClientId == CurrentClient.ClientId && x.OrderStatus == Database.OrderStatus.New).FirstOrDefault();
                if(order != null)
                {
                    entity.OrderId = order.OrderId;
                }
                else
                {
                    order = new Database.Order
                    {
                        ClientId = CurrentClient.ClientId,
                        Date = DateTime.Now,
                        OrderStatus = Database.OrderStatus.New
                    };
                    _context.Order.Add(order);
                    _context.SaveChanges();

                    entity.OrderId = order.OrderId;
                }
            }
            if(product == null)
            {
                throw new UserException("Product not found");
            }

            entity.Product = product;
            entity.Price = product.Price;

            _context.OrderItem.Add(entity);
            _context.SaveChanges();

            var TotalPrice = _context.OrderItem.Where(x => x.OrderId == order.OrderId).Sum(x => x.Price * x.Quantity);
            order.TotalPrice = TotalPrice;
            _context.SaveChanges();

            return _mapper.Map<Model.OrderItem>(entity);
        }

        public Model.OrderItem Update(int id, OrderItemInsertRequest request)
        {
            var entity = _mapper.Map<Database.OrderItem>(request);

            var order = _context.Order.Find(request.OrderId);

            var product = _context.Product.Find(request.ProductId);

            if (order == null)
            {
                throw new UserException("Order not found");
            }
            if (product == null)
            {
                throw new UserException("Product not found");
            }

            entity.Order = order;
            entity.Product = product;

            Mapper.Map<OrderItemInsertRequest, Database.OrderItem>(request, entity);
            _context.SaveChanges();

            return _mapper.Map<Model.OrderItem>(entity);
        }
    }
}
