namespace ProjectG.ProductService.Infrastructure.OrderApi.DTO
{
    using System;
    using System.Collections.Generic;

    public class OrderModel
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime DateCreated { get; set; }

        public OrderStatus Status { get; set; }

        public IEnumerable<OrderPositionModel> OrderPositions { get; set; }
    }
}