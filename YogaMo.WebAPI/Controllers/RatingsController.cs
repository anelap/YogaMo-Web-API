using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : Controller
    {
        private readonly IRatingService _service;
        private readonly IMapper _mapper;


        public RatingsController(IRatingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Model.Rating>> Get([FromQuery] RatingSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public ActionResult<Model.Rating> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpGet("{productId}/{clientId}")]
        public ActionResult<Model.Rating> GetByProductAndClient(int productId, int clientId)
        {
            return _service.GetByProductAndClient(productId, clientId);
        }

        [HttpPut]
        public ActionResult<Model.Rating> Update(int id, RatingInsertRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpPost]
        public ActionResult<Model.Rating> Insert(RatingInsertRequest request)
        {
            return _service.Insert(request);
        }
        
        [HttpPost("RateProduct")]
        public ActionResult<Model.Rating> RateProduct(RatingInsertRequest request)
        {
            return _service.RateProduct(request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldRating = _service.Get(id);
            if (oldRating == null) return NotFound("Rating not found");

            _service.Delete(id);

            return Ok(oldRating);
        }
    }
}
