using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Model.Category>> Get()
        {
            return _service.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Model.Category>Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.Category> Insert(string name)
        {
            return _service.Insert(name);
        }

        [HttpPut]
        public ActionResult<Model.Category> Update(int id, string name)
        {
            return _service.Update(id, name);
        }

        [HttpDelete("{id}")]
        public ActionResult<Model.Category> Delete (int id)
        {
            var oldCategory = _service.Get(id);
            if (oldCategory == null) return NotFound("Category not found");

            _service.Delete(id);

            return oldCategory;
        }
    }
}
