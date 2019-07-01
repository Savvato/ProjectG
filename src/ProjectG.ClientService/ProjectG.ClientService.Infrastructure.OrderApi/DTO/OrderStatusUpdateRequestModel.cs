namespace ProjectG.ClientService.Infrastructure.OrderApi.DTO
{
    public class OrderStatusUpdateRequestModel
    {
        public long OrderId { get; set; }

        public string Status { get; set; }
    }
}