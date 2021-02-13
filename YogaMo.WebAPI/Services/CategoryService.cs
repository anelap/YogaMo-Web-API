using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public CategoryService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.Category.Find(id);

            _context.Category.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.Category> Get()
        {
            var list = _context.Category.ToList();

            return _mapper.Map<List<Model.Category>>(list);
        }

        public Model.Category Get(int id)
        {
            var entity = _context.Category.Find(id);

            return _mapper.Map<Model.Category>(entity);
        }

        public Model.Category Insert(string name)
        {
            var entity = new Database.Category();
            entity.Name = name;

            _context.Category.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Category>(entity);
           
        }

        public Model.Category Update(int id, string name)
        {
            var entity = _context.Category.Find(id);

            if (entity == null) throw new UserException("Category not found");

            entity.Name = name;

            _context.SaveChanges();

            return _mapper.Map<Model.Category>(entity);
        }
    }
}
