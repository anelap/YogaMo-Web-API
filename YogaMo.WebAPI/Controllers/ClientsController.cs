using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly IClientService _service;
        private readonly IMapper _mapper;

        public ClientsController(IClientService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Model.Client>> Get([FromQuery] ClientSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public Model.Client GetById(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.Client> Insert(ClientInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.Client Update(int id, ClientUpdateRequest request)
        {
            return _service.Update(id, request);
        }


        [HttpGet("MyProfile")]
        [Authorize(AuthenticationSchemes = "BasicAuthentication", Roles = "Client")]
        public Model.Client MyProfile()
        {
            return _service.MyProfile();
        }
    }
}
