
using Microsoft.EntityFrameworkCore;

namespace Apsen{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<Cliente> Clientes { get; set;}
    }
}