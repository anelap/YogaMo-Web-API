using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemService _service;
        private readonly IMapper _mapper;
        public OrderItemsController(IOrderItemService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Model.OrderItem>> Get([FromQuery] OrderItemSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public ActionResult<Model.OrderItem> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public Model.OrderItem Insert(OrderItemInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut]
        public ActionResult<Model.OrderItem> Update(int id, OrderItemInsertRequest request)
        {
            var order = _service.Get(id);
            if (order == null) return NotFound("Order not found");
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldOrderItem = _service.Get(id);

            if (oldOrderItem == null) return NotFound("Order Item not found");

            _service.Delete(id);

            return Ok(oldOrderItem);
        }
    }
}
