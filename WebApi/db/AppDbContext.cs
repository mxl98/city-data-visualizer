using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents the context for the database
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of <see cref="AppDbContext"/> with the given configuration. 
    /// </summary>
    /// <param name="options">The configuration specifying the database provider and the connection string.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<PiscineModel> PiscineModels { get; set; }
}