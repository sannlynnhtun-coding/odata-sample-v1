namespace DotNet8WebApi.ODataSample.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlogModel> Blogs { get; set; }
}