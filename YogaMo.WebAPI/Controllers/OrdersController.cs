using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;


namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Model.Order>> Get([FromQuery] OrderSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public ActionResult<Model.Order> Get(int id)
        {
            return _service.Get(id);
        }


        [HttpGet("GetOrdersByDate/{day?}/{month?}/{year?}")]
        public ActionResult<List<Model.Order>> Get(int day, int month, int year)
        {
            return _service.GetByDate(day, month, year);
        }

        [HttpGet("GetOrderByClient/{id?}")]
        public ActionResult<List<Model.Order>> GetOrdersByClient(int id)
        {
            return _service.GetByClient(id);
        }

        [HttpPost]
        public Model.Order Insert(OrderInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public ActionResult<Model.Order> Update(int id, OrderInsertRequest request)
        {
            var order = _service.Get(id);
            if (order == null) return NotFound("Order not found");
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldOrder = _service.Get(id);

            if (oldOrder == null) return NotFound("Order not found");

            _service.Delete(id);

            return Ok(oldOrder);
        }

        [HttpPost("ConfirmOrder")]
        public bool ConfirmOrder(Model.Requests.PaymentIntentCreateRequest request)
        {
            return _service.ConfirmOrder(request);
        }

    }
}
