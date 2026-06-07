namespace VentasShop.Dominio.ObjetosValor;

/// <summary>
/// Objeto de valor de cantidad de producto. Invariante (BR-08): siempre &gt; 0.
/// </summary>
public readonly struct Cantidad : IEquatable<Cantidad>
{
    public int Valor { get; }

    public Cantidad(int valor)
    {
        if (valor <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(valor));
        Valor = valor;
    }

    public bool Equals(Cantidad otra) => Valor == otra.Valor;
    public override bool Equals(object? obj) => obj is Cantidad c && Equals(c);
    public override int GetHashCode() => Valor.GetHashCode();
    public static bool operator ==(Cantidad a, Cantidad b) => a.Equals(b);
    public static bool operator !=(Cantidad a, Cantidad b) => !a.Equals(b);
    public override string ToString() => Valor.ToString();
}
