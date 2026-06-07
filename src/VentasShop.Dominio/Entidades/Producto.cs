using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Dominio.Entidades;

/// <summary>Producto del catálogo.</summary>
public class Producto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public Dinero PrecioUnitario { get; set; }
    public int UnidadesStock { get; set; }
}
