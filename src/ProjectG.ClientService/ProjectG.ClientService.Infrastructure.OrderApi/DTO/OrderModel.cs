namespace ProjectG.ClientService.Infrastructure.OrderApi.DTO
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

        public string Status { get; set; }

        public IEnumerable<OrderPositionModel> OrderPositions { get; set; }

        public class OrderStatus
        {
            public const string Created = "CREATED";
            public const string WaitingForPayment = "WAITING_FOR_PAYMENT";
            public const string Paid = "PAID";
            public const string Sent = "SENT";
        }
    }
}