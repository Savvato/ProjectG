namespace ProjectG.OrderService.Core.Models
{
    public enum OrderStatus
    {
        Created = 0,
        Verified = 1,
        WaitingForPayment = 2,
        Paid = 3,
        Sent = 4
    }
}