using Microsoft.EntityFrameworkCore;
using MvcNetCoreEF2.Models;

namespace MvcNetCoreEF2.Data
{
    public class EnfermoContext : DbContext
    {
        public EnfermoContext(DbContextOptions<EnfermoContext> options)
            : base(options) { }

        public DbSet<Enfermo> Enfermos { get; set; }
    }
}
