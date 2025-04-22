namespace SignalPlus.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.Models;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //public DbSet<User> Users { get; set; }

        public DbSet<Signal> Signals { get; set; }

        public DbSet<SignalImage> SignalImages { get; set; }
    }
}
