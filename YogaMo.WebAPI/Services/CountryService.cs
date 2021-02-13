using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public CountryService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.Country.Find(id);

            _context.Country.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.Country> Get()
        {
            var list = _context.Country.ToList();

            return _mapper.Map<List<Model.Country>>(list);
        }

        public Model.Country Get(int id)
        {
            var entity = _context.Country.Find(id);

            return _mapper.Map<Model.Country>(entity);
        }

        public Model.Country Insert(string name)
        {
            var entity = new Database.Country();
            entity.Name = name;

            _context.Country.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Country>(entity);

        }

        public Model.Country Update(int id, string name)
        {
            var entity = _context.Country.Find(id);

            if (entity == null) throw new UserException("Country not found");

            entity.Name = name;

            _context.SaveChanges();

            return _mapper.Map<Model.Country>(entity);
        }
    }
}
