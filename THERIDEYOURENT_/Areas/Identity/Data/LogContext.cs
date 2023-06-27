using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using THERIDEYOURENT_.Areas.Identity.Data;

namespace THERIDEYOURENT_.Areas.Identity.Data
{
    public class LogContext : IdentityDbContext<User>
    {

        public DbSet<User> Users { get; set; }



        public LogContext() { 
        
        
        }
        public LogContext(DbContextOptions<LogContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configure the database provider here
                optionsBuilder.UseSqlServer("your_connection_string");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfigaration());
        }
    }
    //public DbSet<User> Users { get; set; }


    public class ApplicationUserEntityConfigaration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.name).HasMaxLength(256);
            builder.Property(x => x.inspector).HasMaxLength(256);
        }
    }


}
