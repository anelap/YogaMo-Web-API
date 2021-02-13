using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _service;
        private readonly IMapper _mapper;

        public InstructorsController(IInstructorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Model.Instructor>> Get([FromQuery]InstructorsSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public Model.Instructor GetById(int id)
        {
            return _service.Get(id);
        }

        [Authorize(AuthenticationSchemes = "BasicAuthentication", Roles = "Instructor")]
        [HttpPost]
        public ActionResult<Model.Instructor> Insert(InstructorsInsertRequest request)
        {
            return _service.Insert(request);
        }

        [Authorize(AuthenticationSchemes = "BasicAuthentication", Roles = "Instructor")]
        [HttpPut("{id}")]
        public Model.Instructor Update(int id, InstructorsUpdateRequest request)
        {
            return _service.Update(id, request);
        }

        [Authorize(AuthenticationSchemes = "BasicAuthentication", Roles = "Instructor")]
        [HttpGet("MyProfile")]
        public Model.Instructor MyProfile()
        {
            return _service.MyProfile();
        }
       
    }
}
