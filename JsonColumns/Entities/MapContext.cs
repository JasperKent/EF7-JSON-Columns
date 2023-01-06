using Microsoft.EntityFrameworkCore;

namespace JsonColumns.Entities
{
    public class MapContext : DbContext
    {
        public MapContext(DbContextOptions<MapContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().OwnsOne(
                country => country.Shape,
                builder =>
                {
                    builder.ToJson();
                    builder.OwnsMany(shape => shape.Coordinates);
                }
            );
        }

        public DbSet<Country> Countries { get; set; }
    }
}
