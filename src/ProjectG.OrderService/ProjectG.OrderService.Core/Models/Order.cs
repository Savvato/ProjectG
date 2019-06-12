using System;
using System.Collections.Generic;

namespace ProjectG.OrderService.Core.Models
{
    public class Order
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public DateTime DateCreated { get; set; }

        public OrderStatus Status { get; set; }


        public IEnumerable<OrderPosition> OrderPositions { get; set; }
    }
}
