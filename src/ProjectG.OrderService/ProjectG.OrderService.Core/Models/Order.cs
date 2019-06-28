namespace ProjectG.OrderService.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string Surname { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public OrderStatusDetails StatusDetails { get; set; }

        public IEnumerable<OrderPosition> OrderPositions { get; set; }
    }
}