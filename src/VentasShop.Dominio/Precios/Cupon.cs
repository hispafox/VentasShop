namespace VentasShop.Dominio.Precios;

/// <summary>
/// Cupón de descuento (BR-06). Su validez depende de la fecha, por eso los casos que lo usan
/// necesitan un reloj inyectable (<see cref="Abstracciones.IReloj"/>) para ser repetibles.
/// </summary>
public sealed class Cupon
{
    public string Codigo { get; }
    public DateTimeOffset ExpiraEn { get; }
    public decimal TasaExtra { get; }

    public Cupon(string codigo, DateTimeOffset expiraEn, decimal tasaExtra = 0.10m)
    {
        Codigo = codigo;
        ExpiraEn = expiraEn;
        TasaExtra = tasaExtra;
    }

    public bool EsValidoEn(DateTimeOffset ahora) => ahora <= ExpiraEn;
}
