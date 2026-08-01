using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticketora.Domain.Entities;
using Ticketora.Persistence.Identity;

namespace Ticketora.Persistence.Context
{
    public class TicketoraContext : IdentityDbContext<ApplicationUser>
    {
        public TicketoraContext()
        {
        }

        public TicketoraContext(DbContextOptions<TicketoraContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-9DQS8R7\\MSSQLSERVER01;initial Catalog=TicketOraDb;integrated security=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .HasIndex(x => x.TicketNumber)
                .IsUnique();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
