using Microsoft.EntityFrameworkCore;
using Bienes.Server.Models;

namespace Bienes.Server.Context;

public interface IMyDbContext
{

   DbSet<UsuarioRol> UsuariosRoles { get; set; }
    DbSet<Usuario> Usuarios { get; set; }  
     DbSet<Cliente> Clientes { set; get; }
     DbSet<Terreno> Listas { set; get; } 

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class MyDbContext : DbContext, IMyDbContext
{
    private readonly IConfiguration config;

    public MyDbContext(IConfiguration _config)
    {
        config = _config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("Bienes"));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    #region Tablas de mi base de datos
    public DbSet<UsuarioRol> UsuariosRoles { set; get; } = null!;
    public DbSet<Usuario> Usuarios { set; get; } = null!;
    public DbSet<Cliente> Clientes { set; get; } = null!;
    public DbSet<Terreno> Listas { set; get; } = null!;
    
    
    #endregion
}