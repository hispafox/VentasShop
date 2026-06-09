using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Infraestructura.Persistencia;

/// <summary>
/// Contexto de EF Core del proyecto (Módulo 6). Mapea las entidades y convierte los objetos de
/// valor (Dinero, Cantidad) a columnas simples. La configuración fina y los tests de integración
/// se trabajan en M6.
/// </summary>
public class ContextoVentasShop : DbContext
{
    public ContextoVentasShop(DbContextOptions<ContextoVentasShop> opciones) : base(opciones) { }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        var conversorDinero = new ValueConverter<Dinero, string>(
            d => d.Importe.ToString(CultureInfo.InvariantCulture) + "|" + d.Moneda,
            s => new Dinero(
                decimal.Parse(s.Split('|')[0], CultureInfo.InvariantCulture),
                s.Split('|')[1]));

        var conversorCantidad = new ValueConverter<Cantidad, int>(
            c => c.Valor,
            v => new Cantidad(v));

        modelo.Entity<Cliente>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Nombre).IsRequired();
        });

        modelo.Entity<Producto>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Codigo).IsRequired();
            b.HasIndex(p => p.Codigo).IsUnique();   // no dos productos con el mismo codigo (M6.2/M6.3)
            b.Property(p => p.Nombre).IsRequired();
            b.Property(p => p.PrecioUnitario).HasConversion(conversorDinero);
        });

        modelo.Entity<Pedido>(b =>
        {
            b.HasKey(p => p.Id);
            b.HasOne(p => p.Cliente).WithMany().IsRequired();
            b.Property(p => p.Estado).HasConversion<string>();
            var navLineas = b.Metadata.FindNavigation(nameof(Pedido.Lineas))!;
            navLineas.SetPropertyAccessMode(PropertyAccessMode.Field);
            b.HasMany(p => p.Lineas).WithOne().HasForeignKey("PedidoId");
        });

        modelo.Entity<LineaPedido>(b =>
        {
            b.HasKey(l => l.Id);
            b.HasOne(l => l.Producto).WithMany().IsRequired();
            b.Property(l => l.Cantidad).HasConversion(conversorCantidad);
            b.Ignore(l => l.Subtotal);
        });
    }
}
