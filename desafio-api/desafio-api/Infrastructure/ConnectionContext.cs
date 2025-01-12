using desafio_api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_api.Infrastructure;

public class ConnectionContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseNpgsql("Server=localhost;" +
                                    "Port=5435;" +
                                    "Database=postgres;" +
                                    "Username=desafio;" +
                                    "Password=desafio");
    
    public DbSet<CategoryModel> Categories { get; set; }
}