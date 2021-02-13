using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;

        public ProductService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.Product.Find(id);

            _context.Product.Remove(entity);
            _context.SaveChanges();
        }

        List<Model.Product> IProductService.GetAvailableProducts()
        {
            List<Database.Product> list = _context.Product.Where(x => x.Status == true).ToList();

            foreach(var item in list)
            {
                item.Photo = null;
            }

            return _mapper.Map<List<Model.Product>>(list);
        }
        public List<Model.Product> Get(ProductSearchRequest request)
        {
            var query = _context.Product.Include(x=>x.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Name))
            {
                query = query.Where(x => x.Name.ToUpper().Contains(request.Name.ToUpper()));
            }
            if (request?.CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId);
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Product>>(list);
        }

        public Model.Product Get(int id)
        {
            var entity = _context.Product.Find(id);
            if (entity == null) throw new UserException("Product not found");

            return _mapper.Map<Model.Product>(entity);
        }

        public Model.Product Insert(ProductInsertRequest request)
        {
            var entity = _mapper.Map<Database.Product>(request);

            _context.Product.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Product>(entity);
        }

        public Model.Product Update(int id, ProductInsertRequest request)
        {
            var entity = _context.Product.Find(id);
            if (entity == null) throw new UserException("Product not found");

            Mapper.Map<ProductInsertRequest, Database.Product>(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Product>(entity);
        }

    }
}
