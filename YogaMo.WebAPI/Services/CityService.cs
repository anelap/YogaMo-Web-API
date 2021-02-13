using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class CityService : ICityService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public CityService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.City.Find(id);

            _context.City.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.City> Get(Model.Requests.CitiesSearchRequest request)
        {
            var list = _context.City
                .Where(x=>x.CountryId == request.CountryId || request.CountryId == 0)
                .ToList();

            return _mapper.Map<List<Model.City>>(list);
        }

        public Model.City Get(int id)
        {
            var entity = _context.City.Find(id);

            return _mapper.Map<Model.City>(entity);
        }

        public Model.City Insert(string name)
        {
            var entity = new Database.City();
            entity.Name = name;

            _context.City.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.City>(entity);

        }

        public Model.City Update(int id, string name)
        {
            var entity = _context.City.Find(id);

            if (entity == null) throw new UserException("City not found");

            entity.Name = name;

            _context.SaveChanges();

            return _mapper.Map<Model.City>(entity);
        }
    }
}
