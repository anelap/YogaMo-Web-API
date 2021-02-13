using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YogasController : Controller
    {
        private readonly IYogaService _service;
        private readonly IMapper _mapper;

        public YogasController(IYogaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

      

        [HttpGet]
        public ActionResult<List<Model.Yoga>> Get([FromQuery] YogaSearchRequest request)
        {
            return _service.Get(request);
        }


        [HttpGet("{id}")]
        public Model.Yoga GetById(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.Yoga> Insert(YogaInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.Yoga Update(int id, YogaInsertRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldYoga = _service.Get(id);
            if (oldYoga == null) return NotFound("Yoga not found");

            _service.Delete(id);

            return Ok(oldYoga);
        }
    }
}
