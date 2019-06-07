
namespace ProjectG.DocumentService.Infrastructure.Db
{
    using Microsoft.EntityFrameworkCore;

    using ProjectG.DocumentService.Core.Models;

    public abstract class BaseDocumentDbContext : DbContext
    {
        public DbSet<InspectionCertificateRequest> InspectionCertificateRequests { get; set; }

        protected BaseDocumentDbContext(DbContextOptions<BaseDocumentDbContext> options) : base(options)
        {
        }
    }
}
