namespace ProjectG.BasketService.Api.Extensions
{
    using ProjectG.BasketService.Api.DTO;
    using ProjectG.BasketService.Core.Models;

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
