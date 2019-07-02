namespace ProjectG.BasketService.WriteApi.Extensions
{
    using ProjectG.BasketService.Core.Models;
    using ProjectG.BasketService.WriteApi.DTO;

    public static class BasketPositionCreationModelExtensions
    {
        public static BasketPosition ToBasketPosition(this BasketPositionCreationModel basketPositionCreationModel)
        {
            return new BasketPosition
            {
                CustomerId = basketPositionCreationModel.CustomerId,
                ProductId = basketPositionCreationModel.ProductId,
                Price = basketPositionCreationModel.Price,
                Quantity = basketPositionCreationModel.Quantity
            };
        }
    }
}
