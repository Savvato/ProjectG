namespace ProjectG.DocumentService.Infrastructure.Db
{
    using Microsoft.EntityFrameworkCore;

    public class DocumentDbContext : BaseDocumentDbContext
    {
        public DocumentDbContext(DbContextOptions<BaseDocumentDbContext> options) : base(options)
        {
        }
    }
}
