using Microsoft.EntityFrameworkCore;

namespace T2204M_Practice.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
