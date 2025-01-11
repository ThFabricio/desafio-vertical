using Microsoft.EntityFrameworkCore;

namespace desafio_api.Infrastructure;

public class ConnectionContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseNpgsql("Server=localhost;" +
                                    "Port=5435;" +
                                    "Database=desafio;" +
                                    "Username=desafio;" +
                                    "Password=desafio");
}