using VentasShop.Dominio.Abstracciones;

namespace VentasShop.Infraestructura.Tiempo;

/// <summary>Reloj real: lee la hora del sistema. En los tests se sustituye por un reloj fijo.</summary>
public sealed class RelojSistema : IReloj
{
    public DateTimeOffset Ahora => DateTimeOffset.Now;
}
