using Microsoft.Extensions.Logging;

namespace VentasShop.TestsUnitarios.Dobles;

/// <summary>
/// SPY de <see cref="ILogger{T}"/> hecho a mano (M5.1): deja que el código registre con normalidad y,
/// además, guarda cada apunte (nivel + mensaje) en <see cref="Apuntes"/> para que el test verifique
/// después la auditoría (BR-10: al pagar se registra un apunte; un pago rechazado deja un aviso).
/// </summary>
/// <remarks>
/// Cuando un test NO mira el log, este mismo hueco se podría rellenar con un DUMMY
/// (<c>NullLogger&lt;T&gt;.Instance</c>): un relleno que cumple el parámetro obligatorio sin hacer nada.
/// Spy cuando se verifica el log; dummy cuando solo estorba.
/// </remarks>
public sealed class LoggerEspia<T> : ILogger<T>
{
    public List<(LogLevel Nivel, string Mensaje)> Apuntes { get; } = [];

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
        => Apuntes.Add((logLevel, formatter(state, exception)));
}
