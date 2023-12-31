using Microsoft.EntityFrameworkCore;
using GamesHub.Entities;
using GamesHub.Protocol.Response;

namespace ResultsNet.Data;

public class GamesHubContext : DbContext
{
    private readonly ILogger<GamesHubContext> _logger;
    protected readonly IConfiguration configuration;
    public DbSet<User> users {get; set;}
    public DbSet<GameHistory> gameHistory {get; set;}
    public GamesHubContext(ILogger<GamesHubContext> logger, IConfiguration configuration)
    {
        this._logger = logger;
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string connectionString = $"server={Environment.GetEnvironmentVariable("ASPNETCORE_DB_IP")}; port={Environment.GetEnvironmentVariable("ASPNETCORE_DB_PORT")}; " + 
            $"database={Environment.GetEnvironmentVariable("ASPNETCORE_DB_NAME")}; user=root; password='{Environment.GetEnvironmentVariable("ASPNETCORE_DB_PASSWORD")}'";
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //options.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>{
            entity.Property(u => u.Created).HasDefaultValueSql("(now())");
            entity.HasAlternateKey(u => u.Username);
        });
    }

    public async Task<bool> UserExists(string username)
    {
        bool exists = await users.AnyAsync(u => u.Username == username);
        return exists;
    }
}