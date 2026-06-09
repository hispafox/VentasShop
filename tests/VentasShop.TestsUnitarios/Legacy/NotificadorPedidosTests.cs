using AwesomeAssertions;
using VentasShop.TestsUnitarios.Dobles;   // RelojFijo, LoggerEspia (M5.1)

namespace VentasShop.TestsUnitarios.Legacy;

/// <summary>
/// M8.1 — La recompensa del refactor: una vez abiertas las costuras del <see cref="NotificadorPedidos"/>
/// (inyección de dependencias), la clase que era INTESTABLE ya se deja testear con dobles. Reutilizamos
/// RelojFijo y LoggerEspia (M5.1) para las costuras del reloj y del log; el acceso a datos y el correo
/// los doblamos aquí con dos fakes mínimos.
/// </summary>
public class NotificadorPedidosTests
{
    // Doble del acceso a datos: devuelve un pedido fijo, sin tocar ninguna base de datos.
    private sealed class AccesoPedidosFalso(PedidoLegacy pedido) : IAccesoPedidos
    {
        public PedidoLegacy? Cargar(Guid pedidoId) => pedido;
    }

    // Doble del enviador de correo: no manda nada; apunta lo que se le pidió enviar (spy).
    private sealed class EnviadorCorreoFalso : IEnviadorCorreo
    {
        public List<(string Destinatario, string Asunto)> Enviados { get; } = [];
        public void Enviar(string destinatario, string asunto) => Enviados.Add((destinatario, asunto));
    }

    private static readonly PedidoLegacy Pedido = new(Guid.NewGuid(), "ana@ejemplo.test");

    [Fact]
    public void NotificarEnvio_DeDia_EnviaElCorreoAlCliente()
    {
        var correo = new EnviadorCorreoFalso();
        var sut = new NotificadorPedidos(
            new AccesoPedidosFalso(Pedido),
            new RelojFijo(new DateTimeOffset(2026, 6, 15, 10, 0, 0, TimeSpan.Zero)),   // 10:00
            correo,
            new LoggerEspia<NotificadorPedidos>());

        sut.NotificarEnvio(Pedido.Id);

        correo.Enviados.Should().ContainSingle()
            .Which.Destinatario.Should().Be("ana@ejemplo.test");
    }

    [Fact]
    public void NotificarEnvio_DeNoche_NoEnviaCorreo()
    {
        var correo = new EnviadorCorreoFalso();
        var sut = new NotificadorPedidos(
            new AccesoPedidosFalso(Pedido),
            new RelojFijo(new DateTimeOffset(2026, 6, 15, 23, 0, 0, TimeSpan.Zero)),   // 23:00
            correo,
            new LoggerEspia<NotificadorPedidos>());

        sut.NotificarEnvio(Pedido.Id);

        correo.Enviados.Should().BeEmpty();
    }

    [Fact]
    public void NotificarEnvio_Siempre_DejaUnApunteEnElLog()
    {
        var logger = new LoggerEspia<NotificadorPedidos>();
        var sut = new NotificadorPedidos(
            new AccesoPedidosFalso(Pedido),
            new RelojFijo(new DateTimeOffset(2026, 6, 15, 23, 0, 0, TimeSpan.Zero)),
            new EnviadorCorreoFalso(),
            logger);

        sut.NotificarEnvio(Pedido.Id);

        logger.Apuntes.Should().ContainSingle();
    }
}
