using Microsoft.EntityFrameworkCore;

namespace Lumia.DataContext
{
    public class LumiaDbContext : DbContext
    {
        public LumiaDbContext(DbContextOptions<LumiaDbContext> options) : base(options) { }


    }
}
