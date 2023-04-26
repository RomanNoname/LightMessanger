using LightMessanger.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<UnreadMessages> UnreadMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
             .HasOne(g => g.UserGenerated)
             .WithMany(u=>u.CreatedGroups);
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups);
       
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Group>().HasIndex(p => p.Name).IsUnique();

        }
    }
}