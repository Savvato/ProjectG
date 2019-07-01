namespace ProjectG.OrderService.WriteApi.DTO
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    using ProjectG.OrderService.Core.Models;

    public class OrderStatusUpdateEventModel
    {
        [Required]
        public long OrderId { get; set; }

        [Required]
        public string Status { get; set; }

        public OrderStatus MapStatusToEnum()
        {
            switch (this.Status)
            {
                case "CREATED": return OrderStatus.Created;
                case "WAITING_FOR_PAYMENT": return OrderStatus.WaitingForPayment;
                case "PAID": return OrderStatus.Paid;
                case "SENT": return OrderStatus.Sent;
                default: throw new InvalidDataException("Undefined order status");
            }
        }
    }
}