using Microsoft.EntityFrameworkCore;
using Uprise.Repository.Uprise.Models;

namespace Uprise.Repository.Uprise;

public class UpriseDbContext : DbContext
{
    public const string SCHEMA = "uprise";

    public UpriseDbContext(DbContextOptions<UpriseDbContext> options)
        : base(options)
    {
        // https://www.npgsql.org/doc/types/basic.html
        // In versions prior to 6.0 (or when Npgsql.EnableLegacyTimestampBehavior is enabled),
        // reading a timestamp with time zone returns a Local DateTime instead of Utc.
        // See the breaking change note for more info.

        // In versions prior to 6.0 (or when Npgsql.EnableLegacyTimestampBehavior is enabled),
        // reading a timestamp with time zone as a DateTimeOffset returns a local offset based
        // on the timezone of the server where Npgsql is running.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SCHEMA);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
}
