namespace ProjectG.ClientService.Infrastructure.DTO
{
    using System.Collections.Generic;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;

    public class CustomerDetailedModel
    {
        public CustomerModel Customer { get; set; }

        public IEnumerable<BasketPositionModel> Basket { get; set; }
    }
}