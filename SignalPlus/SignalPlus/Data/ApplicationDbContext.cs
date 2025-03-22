namespace SignalPlus.Data
{
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // public DbSet<User> Users { get; set; }
        public DbSet<Signal> Signals { get; set; }
    }
}
