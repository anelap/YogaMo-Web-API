using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
