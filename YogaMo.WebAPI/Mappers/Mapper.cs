using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;

namespace YogaMo.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Database.Instructor, Model.Instructor>().ReverseMap();
            CreateMap<Database.Instructor, InstructorsInsertRequest>().ReverseMap();
            CreateMap<Database.Instructor, InstructorsUpdateRequest>().ReverseMap();

            CreateMap<Database.Client, Model.Client>().ReverseMap();
            CreateMap<Database.Client, ClientInsertRequest>().ReverseMap();
            CreateMap<Database.Client, ClientUpdateRequest>().ReverseMap();

            CreateMap<Database.Yoga, Model.Yoga>().ReverseMap();
            CreateMap<Database.Yoga, YogaInsertRequest>().ReverseMap();

            CreateMap<Database.YogaClass, Model.YogaClass>().ReverseMap();
            CreateMap<Database.YogaClass, YogaClassInsertRequest>().ReverseMap();

            CreateMap<Database.Product, Model.Product>().ReverseMap();
            CreateMap<Database.Product, ProductInsertRequest>().ReverseMap();

            CreateMap<Database.Rating, Model.Rating>().ReverseMap();
            CreateMap<Database.Rating, RatingInsertRequest>().ReverseMap();

            CreateMap<Database.Category, Model.Category>().ReverseMap();

            CreateMap<Database.Order, Model.Order>().ReverseMap();
            CreateMap<Database.Order, OrderInsertRequest>().ReverseMap();

            CreateMap<Database.OrderItem, Model.OrderItem>().ReverseMap();
            CreateMap<Database.OrderItem, OrderItemInsertRequest>().ReverseMap();


            CreateMap<Database.City, Model.City>().ReverseMap();
            CreateMap<Database.Country, Model.Country>().ReverseMap();


        }
    }
}
