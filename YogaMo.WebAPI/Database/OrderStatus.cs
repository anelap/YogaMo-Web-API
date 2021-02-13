using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Database
{
    public enum OrderStatus
    {
        New,
        Processing,
        Confirmed,
        Completed,
        Canceled
    }
}
