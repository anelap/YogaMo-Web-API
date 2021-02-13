using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : Controller
    {
        private readonly ICityService _service;
        public CitiesController(ICityService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<Model.City>> Get([FromQuery]CitiesSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public ActionResult<Model.City> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.City> Insert(string name)
        {
            return _service.Insert(name);
        }

        [HttpPut]
        public ActionResult<Model.City> Update(int id, string name)
        {
            return _service.Update(id, name);
        }

        [HttpDelete("{id}")]
        public ActionResult<Model.City> Delete(int id)
        {
            var oldCity = _service.Get(id);
            if (oldCity == null) return NotFound("City not found");

            _service.Delete(id);

            return oldCity;
        }
    }
}
