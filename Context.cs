using Conversion.Class;
using Microsoft.EntityFrameworkCore;

namespace Conversion
{
    public class Context : DbContext
    {
        public Context()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }


        public string DefaultConnection { get; set; }
        public DbSet<Converts> Transaction { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
