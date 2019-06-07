namespace ProjectG.DocumentService.Infrastructure.Db
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class ReadOnlyDocumentDbContext : BaseDocumentDbContext
    {
        public ReadOnlyDocumentDbContext(DbContextOptions<BaseDocumentDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new InvalidOperationException("This context is read-only");
        }
    }
}
