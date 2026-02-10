using Microsoft.EntityFrameworkCore;
using MvcNetCoreEF2.Models;

namespace MvcNetCoreEF2.Data
{
    public class DeptContext : DbContext
    {
        public DeptContext(DbContextOptions<DeptContext> options)
            : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }
    }
}
