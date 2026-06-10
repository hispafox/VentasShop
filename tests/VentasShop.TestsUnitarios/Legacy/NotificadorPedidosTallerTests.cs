using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using VentasShop.Dominio.Abstracciones;   // IReloj

namespace VentasShop.TestsUnitarios.Legacy;

/// <summary>
/// M8.3 — El taller: los mismos comportamientos del <see cref="NotificadorPedidos"/> que en M8.1, pero
/// escritos como en el partido de verdad, con dobles de NSubstitute (M5.2) en vez de fakes a mano. Es el
/// curso entero junto en veinte líneas: reloj fijo (M7.2), dobles (M5.2), verificación de interacción
/// (Received/DidNotReceive), naming que canta (M3.2), AAA (M3.1), el borde de las 22:00 (M2.2) y un
/// comportamiento por test (M7.3). En la sala se hace esto mismo sobre código real del cliente (R1: ese
/// código nunca entra aquí); este ejemplo es neutro para poder publicarlo.
/// </summary>
public class NotificadorPedidosTallerTests
{
    private static (NotificadorPedidos sut, IEnviadorCorreo correo) Montar(int hora)
    {
        var id = Guid.NewGuid();
        var acceso = Substitute.For<IAccesoPedidos>();
        acceso.Cargar(Arg.Any<Guid>()).Returns(new PedidoLegacy(id, "ana@ejemplo.test"));
        var reloj = Substitute.For<IReloj>();
        reloj.Ahora.Returns(new DateTimeOffset(2026, 6, 6, hora, 0, 0, TimeSpan.Zero));
        var correo = Substitute.For<IEnviadorCorreo>();
        var sut = new NotificadorPedidos(acceso, reloj, correo, Substitute.For<ILogger<NotificadorPedidos>>());
        return (sut, correo);
    }

    [Fact]
    public void NotificarEnvio_DeDia_EnviaElCorreoAlCliente()
    {
        var (sut, correo) = Montar(hora: 10);   // 10:00, de día

        sut.NotificarEnvio(Guid.NewGuid());

        correo.Received(1).Enviar("ana@ejemplo.test", Arg.Any<string>());
    }

    [Fact]
    public void NotificarEnvio_DeNoche_NoEnviaCorreo()   // el borde de las 22:00 (M2.2)
    {
        var (sut, correo) = Montar(hora: 23);   // 23:00, de noche

        sut.NotificarEnvio(Guid.NewGuid());

        correo.DidNotReceive().Enviar(Arg.Any<string>(), Arg.Any<string>());
    }

    // Nota: la verificación del apunte en el log (auditoría) ya está en M8.1 con el LoggerEspia hecho a
    // mano (NotificadorPedidosTests) — verificar ILogger<T>.Log con NSubstitute es enrevesado por la firma
    // genérica. Es justo el caso del Módulo 5.1: a veces un spy a mano es más claro que el mock con librería.
}
