namespace VentasShop.Dominio.Abstracciones;

/// <summary>
/// Reloj inyectable. Permite que los casos que dependen de la fecha (cupones, BR-06)
/// sean repetibles: el test fija "ahora" en vez de leer el reloj del sistema.
/// </summary>
public interface IReloj
{
    DateTimeOffset Ahora { get; }
}
