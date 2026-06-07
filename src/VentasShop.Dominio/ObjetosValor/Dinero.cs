namespace VentasShop.Dominio.ObjetosValor;

/// <summary>
/// Objeto de valor de importe monetario. Igualdad por valor (mismo importe y misma moneda).
/// No permite mezclar monedas: sumar importes de monedas distintas lanza excepción.
/// </summary>
public readonly struct Dinero : IEquatable<Dinero>
{
    public decimal Importe { get; }
    public string Moneda { get; }

    public Dinero(decimal importe, string moneda)
    {
        Importe = importe;
        Moneda = moneda;
    }

    public static Dinero operator +(Dinero a, Dinero b)
    {
        if (a.Moneda != b.Moneda)
            throw new InvalidOperationException("No se pueden sumar importes de monedas distintas.");
        return new Dinero(a.Importe + b.Importe, a.Moneda);
    }

    public static Dinero operator *(Dinero dinero, int factor) =>
        new(dinero.Importe * factor, dinero.Moneda);

    public bool Equals(Dinero otro) => Importe == otro.Importe && Moneda == otro.Moneda;
    public override bool Equals(object? obj) => obj is Dinero otro && Equals(otro);
    public override int GetHashCode() => HashCode.Combine(Importe, Moneda);
    public static bool operator ==(Dinero a, Dinero b) => a.Equals(b);
    public static bool operator !=(Dinero a, Dinero b) => !a.Equals(b);
    public override string ToString() => $"{Importe:0.00} {Moneda}";
}
