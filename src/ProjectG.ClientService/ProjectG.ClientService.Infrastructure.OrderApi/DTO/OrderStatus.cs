namespace ProjectG.ClientService.Infrastructure.OrderApi.DTO
{
    public enum OrderStatus
    {
        Created = 0,
        WaitingForPayment = 1,
        Paid = 2,
        Sent = 3
    }
}