using final_work_x.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace final_work_x.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ManufactureEntity> Manufactures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarEntity>(e =>
            {
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);

                e.Property(c => c.Year).IsRequired();

                e.Property(c => c.Volume).IsRequired();

                e.Property(c => c.Price).IsRequired();
            });

            builder.Entity<ManufactureEntity>(e =>
            {
                e.Property(m => m.Name).IsRequired();
            });

            builder.Entity<CarEntity>().HasOne(c => c.Manufacture).WithMany(m => m.Cars).HasForeignKey(c => c.ManufactureId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
