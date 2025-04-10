using Microsoft.EntityFrameworkCore;

namespace TaxiApi.Entities
{
    public class TaxiDbContext: DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=TaxiDb;Trusted_Connection=True;";
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(c => c.Plate)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Car>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Drivers)
                .WithMany(d => d.Cars)
                .UsingEntity(j => j.ToTable("CarDriver"));

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            //modelBuilder.Entity<Driver>()
            //    .HasOne(u => u.User)
            //    .WithOne(d => d.Driver)
            //    .HasForeignKey<Driver>(d => d.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
