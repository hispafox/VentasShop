using VentasShop.Dominio.Abstracciones;

namespace VentasShop.TestsUnitarios.Dobles;

/// <summary>
/// STUB de <see cref="IReloj"/> hecho a mano (M5.1): devuelve una fecha y hora fijas, las que el
/// test le da al construirlo. Su único papel es alimentar al SUT con un "ahora" controlado para que
/// el test sea determinista (BR-06). No se verifica nada sobre él: es entrada controlada, nada más.
/// En M5.2 este mismo doble se crea con una línea de NSubstitute; aquí se ve por dentro.
/// </summary>
public sealed class RelojFijo(DateTimeOffset ahora) : IReloj
{
    public DateTimeOffset Ahora { get; } = ahora;
}
