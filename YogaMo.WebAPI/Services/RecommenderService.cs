using AutoMapper;
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
    public class RecommenderService : IRecommenderService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        private readonly int MinimumRating = 3;
        private readonly int NumberOfRecommendations = 3;
        public RecommenderService(_150222Context context, IMapper mapper, IClientService clientService)
        {
            _context = context;
            _mapper = mapper;
            this._clientService = clientService;
        }

        public List<Model.Product> Get(int ProductIdToExclude)
        {
            int ClientId = _clientService.GetCurrentClient().ClientId;
            if (ClientId != 0)
            {
                List<Database.Rating> ListOfRatings = _context.Rating.Where(x => x.ClientId == ClientId)
                    .Include(m => m.Product.Category)
                    .ToList();

                List<Database.Rating> ListOfPositiveRatings = ListOfRatings
                    .Where(x => x.Rating1 >= MinimumRating)
                    .ToList();

                if (ListOfPositiveRatings.Count() > 0)
                {
                    List<Database.Category> uniqueCategories = new List<Database.Category>();
                    foreach (var Rating in ListOfPositiveRatings)
                    {
                        bool add = true;
                        for (int i = 0; i < uniqueCategories.Count; i++)
                        {
                            if (Rating.Product.CategoryId == uniqueCategories[i].CategoryId)
                            {
                                add = false;
                            }
                        }

                        if (add)
                        {
                            uniqueCategories.Add(Rating.Product.Category);
                        }
                    }

                    List<Database.Product> ListOfRecommendedProducts = new List<Database.Product>();
                    foreach (var Category in uniqueCategories)
                    {
                        var ProductsInCategory = _context.Product
                            .Where(x => x.QuantityStock > 0)
                            .Where(x => x.ProductId != ProductIdToExclude)
                            .Where(n => n.CategoryId == Category.CategoryId)
                            .ToList();

                        foreach (var Product in ProductsInCategory)
                        {
                            bool add = true;
                            for (int i = 0; i < ListOfRecommendedProducts.Count; i++)
                            {
                                if (Product.ProductId == ListOfRecommendedProducts[i].ProductId)
                                {
                                    add = false;
                                }
                            }

                            foreach (var Rating in ListOfRatings)
                            {
                                if (Product.ProductId == Rating.ProductId)
                                {
                                    add = false;
                                }
                            }

                            if (add)
                            {
                                ListOfRecommendedProducts.Add(Product);
                            }
                        }
                    }


                    if (ListOfRecommendedProducts.Count > 0)
                    {
                        var list1 = _mapper.Map<List<Model.Product>>(ListOfRecommendedProducts);

                        foreach (var entity in list1)
                        {
                            entity.AverageRating = _context.Rating.Where(x => x.ProductId == entity.ProductId).Average(x => (double?)x.Rating1) ?? 0;
                        }

                        list1 = list1.OrderByDescending(x => x.AverageRating).Take(NumberOfRecommendations).ToList();

                        return list1;
                    }
                }
            }

            var ListOfAllProducts = _context.Product
                                        .Where(x => x.QuantityStock > 0)
                                        .Where(x => x.ProductId != ProductIdToExclude)
                                        .ToList();

            var list2 = _mapper.Map<List<Model.Product>>(ListOfAllProducts);

            foreach (var entity in list2)
            {
                entity.AverageRating = _context.Rating.Where(x => x.ProductId == entity.ProductId).Average(x => (double?)x.Rating1) ?? 0;
            }

            list2 = list2.OrderByDescending(x => x.AverageRating).Take(NumberOfRecommendations).ToList();

            return list2;
        }
    }
}
