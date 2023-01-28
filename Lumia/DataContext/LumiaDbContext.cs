using Lumia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lumia.DataContext
{
    public class LumiaDbContext : IdentityDbContext
    {
        public LumiaDbContext(DbContextOptions<LumiaDbContext> options) : base(options) { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
