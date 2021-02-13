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
    public class RecommenderController : Controller
    {
        private readonly IRecommenderService _service;
        private readonly IMapper _mapper;

        public RecommenderController(IRecommenderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<List<Model.Product>> Get(int id)
        {
            return _service.Get(id);
        }

    }
}
