using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IRecommenderService
    {
        List<Model.Product> Get(int ProductIdToExclude);
    }
}
