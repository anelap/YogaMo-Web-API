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
    public class YogaClassesController : Controller
    {
        private readonly IYogaClassService _service;
        private readonly IMapper _mapper;
        public YogaClassesController(IYogaClassService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Model.YogaClass>> Get([FromQuery]YogaClassSearchRequest request)
        {
            return _service.Get(request);
        }


        [HttpGet("{id}")]
        public Model.YogaClass GetById(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.YogaClass> Insert(YogaClassInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.YogaClass Update(int id, YogaClassInsertRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldYogaClass = _service.Get(id);
            if (oldYogaClass == null) return NotFound("Yoga Class not found");

            _service.Delete(id);

            return Ok(oldYogaClass);
        }
    }
}
