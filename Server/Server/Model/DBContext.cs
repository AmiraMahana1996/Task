using Microsoft.EntityFrameworkCore;

namespace Server.Model
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> users { get; set; }
    }
}
