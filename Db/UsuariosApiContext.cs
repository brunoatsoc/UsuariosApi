using Microsoft.EntityFrameworkCore;
using UsuariosApi.Users;

namespace UsuariosApi.Db;

public class UsuariosApiContext : DbContext {
    public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>().HasKey(u => u.User_Id);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = "Server=localhost;Database=UsuariosApi;User=root;Password=1003200039";

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging().EnableDetailedErrors();
    }
}