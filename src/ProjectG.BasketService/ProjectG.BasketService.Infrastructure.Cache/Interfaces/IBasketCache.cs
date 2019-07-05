namespace ProjectG.BasketService.Infrastructure.Cache.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.BasketService.Core.Models;

    public interface IBasketCache
    {
        Task<IEnumerable<BasketPosition>> Get(long customerId);

        Task Set(long customerId, IEnumerable<BasketPosition> basket);

        Task Remove(long customerId);
    }
}