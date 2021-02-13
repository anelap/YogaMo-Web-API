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
    //   [ApiExplorerSettings(IgnoreApi = true)]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Model.Product>> Get([FromQuery] ProductSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public Model.Product Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet("GetAvaibleProducts")]
        public ActionResult<List<Model.Product>> GetAvailableProducts()
        {
            return _service.GetAvailableProducts();
        }

        [HttpPost]
        public ActionResult<Model.Product> Insert(ProductInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.Product Update(int id, ProductInsertRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldProduct = _service.Get(id);
            if (oldProduct == null) return NotFound("Product not found");

            _service.Delete(id);

            return Ok(oldProduct);
        }

    }
}
