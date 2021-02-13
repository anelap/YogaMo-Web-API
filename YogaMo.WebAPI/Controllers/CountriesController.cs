using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YogaMo.WebAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {
        private readonly ICountryService _service;
        public CountriesController(ICountryService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<Model.Country>> Get()
        {
            return _service.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Model.Country> Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.Country> Insert(string name)
        {
            return _service.Insert(name);
        }

        [HttpPut]
        public ActionResult<Model.Country> Update(int id, string name)
        {
            return _service.Update(id, name);
        }

        [HttpDelete("{id}")]
        public ActionResult<Model.Country> Delete(int id)
        {
            var oldCountry = _service.Get(id);
            if (oldCountry == null) return NotFound("Country not found");

            _service.Delete(id);

            return oldCountry;
        }
    }
}
