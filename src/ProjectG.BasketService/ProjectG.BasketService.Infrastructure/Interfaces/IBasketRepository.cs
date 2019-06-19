namespace ProjectG.BasketService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.BasketService.Core.Models;

    public interface IBasketRepository
    {
        IQueryable<BasketPosition> Get();

        Task<BasketPosition> Get(long id);

        Task<IEnumerable<BasketPosition>> GetByCustomerId(long customerId);

        Task Add(BasketPosition basketPosition);

        Task<bool> Exists(BasketPosition basketPosition);
    }
}
