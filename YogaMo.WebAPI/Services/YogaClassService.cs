using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class YogaClassService : IYogaClassService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public YogaClassService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.YogaClass> Get(YogaClassSearchRequest request)
        {
            var query = _context.YogaClass.AsQueryable();

            query = query.Include(x => x.Yoga.Instructor);

            if(request.YogaId != 0)
            {
                query = query.Where(x => x.YogaId == request.YogaId);
            }
            if(!string.IsNullOrEmpty(request.Day))
            {
                query = query.Where(x => x.Day == request.Day);
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.YogaClass>>(list);
        }

        public Model.YogaClass Get(int id)
        {
            var entity = _context.YogaClass.Find(id);
            return _mapper.Map<Model.YogaClass>(entity);
        }

        public Model.YogaClass Insert(YogaClassInsertRequest request)
        {
            var entity = _mapper.Map<Database.YogaClass>(request);

            var yoga = _context.Yoga.Find(request.YogaId);

            if(yoga == null)
            {
                throw new UserException("Yoga not found");
            }

            entity.Yoga = yoga;

            _context.YogaClass.Add(entity);
            _context.SaveChanges();


            return _mapper.Map<Model.YogaClass>(entity);
        }

        public Model.YogaClass Update(int id, YogaClassInsertRequest request)
        {
            var entity = _context.YogaClass.Find(id);

            if (entity == null) throw new UserException("Yoga class does not exist");

            var yoga = _context.Yoga.Find(request.YogaId);

            if (yoga == null)
            {
                throw new UserException("Yoga not found");
            }

            entity.Yoga = yoga;

            Mapper.Map<YogaClassInsertRequest, Database.YogaClass>(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.YogaClass>(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.YogaClass.Find(id);

            _context.YogaClass.Remove(entity);
            _context.SaveChanges();
        }
    }
}
