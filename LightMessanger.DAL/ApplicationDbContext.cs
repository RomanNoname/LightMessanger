using LightMessanger.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> ChatGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
             .HasOne(g => g.UserGenerated)
             .WithMany()
             .HasForeignKey(g => g.UserGeneratedId);

            modelBuilder.Entity<Group>()
             .HasMany(g => g.Users)
             .WithMany(g => g.Groups);

            modelBuilder.Entity<Message>().
                HasOne(u => u.Reciever).WithMany(u=>u.Messages).HasForeignKey(u=>u.RecieverUserId);

            modelBuilder.Entity<User>().HasMany(u => u.CreatedGroups).WithOne();
           


            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Group>().HasIndex(p => p.Name).IsUnique();

        }
    }
}