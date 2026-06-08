using Microsoft.Extensions.Logging;
using VentasShop.Aplicacion;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.TestsUnitarios.Builders;
using VentasShop.TestsUnitarios.Dobles;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M5.1 — Test doubles, vistos por dentro. Aquí se testea el <see cref="ServicioPedidos"/> (que depende
/// de repositorio, pasarela, reloj y registro) sustituyendo cada dependencia por un doble HECHO A MANO,
/// sin ninguna librería: <see cref="RelojFijo"/> (stub), <see cref="RepositorioPedidosEnMemoria"/> (fake),
/// <see cref="PasarelaPagoFalsa"/> (stub + spy) y <see cref="LoggerEspia{T}"/> (spy). El submódulo es
/// conceptual: la sintaxis con NSubstitute que ahorra todo este montaje es M5.2; las aserciones fluidas,
/// M5.3. Aquí se ve la verificación de ESTADO (cómo queda el pedido) y la de INTERACCIÓN (qué se llamó).
/// </summary>
public class DoblesArtesanalesTests
{
    private static readonly DateTimeOffset InstanteFijo = new(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Pagar_CuandoLaPasarelaAcepta_MarcaElPedidoComoPagadoYLoGuarda()
    {
        // Arrange: un pedido confirmado dentro del repositorio fake, y un doble por dependencia.
        var pedido = new PedidoBuilder().Confirmado().Build();
        var repositorio = new RepositorioPedidosEnMemoria();
        repositorio.Agregar(pedido);
        var pasarela = new PasarelaPagoFalsa(exito: true);
        var reloj = new RelojFijo(InstanteFijo);            // stub: hora determinista
        var registro = new LoggerEspia<ServicioPedidos>();  // spy: captura la auditoría
        var servicio = new ServicioPedidos(repositorio, pasarela, reloj, registro);

        // Act
        servicio.Pagar(pedido.Id);

        // Assert de ESTADO: el resultado que importa es que el pedido quedó pagado.
        Assert.Equal(EstadoPedido.Pagado, pedido.Estado);
        // Assert de INTERACCIÓN: se intentó cobrar una vez y el pedido se guardó.
        Assert.Equal(1, pasarela.VecesCobrado);
        Assert.Equal(1, repositorio.VecesGuardado);
        // El spy del log confirma el apunte de auditoría (BR-10).
        Assert.Contains(registro.Apuntes, apunte => apunte.Nivel == LogLevel.Information);
    }

    [Fact]
    public void Pagar_CuandoLaPasarelaRechaza_NoAvanzaNiGuardaYRegistraElAviso()
    {
        // Arrange: misma escena, pero la pasarela (stub) está fijada para rechazar el cobro (BR-09).
        var pedido = new PedidoBuilder().Confirmado().Build();
        var repositorio = new RepositorioPedidosEnMemoria();
        repositorio.Agregar(pedido);
        var pasarela = new PasarelaPagoFalsa(exito: false);
        var registro = new LoggerEspia<ServicioPedidos>();
        var servicio = new ServicioPedidos(repositorio, pasarela, new RelojFijo(InstanteFijo), registro);

        // Act
        servicio.Pagar(pedido.Id);

        // Assert de ESTADO: el pedido NO avanzó, se queda confirmado.
        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
        // Assert de INTERACCIÓN: se intentó cobrar una vez, pero no se guardó nada.
        Assert.Equal(1, pasarela.VecesCobrado);
        Assert.Equal(0, repositorio.VecesGuardado);
        // El spy del log confirma que quedó el aviso del rechazo (Warning).
        Assert.Contains(registro.Apuntes, apunte => apunte.Nivel == LogLevel.Warning);
    }

    [Fact]
    public void Pagar_CobraElTotalDelPedido_LoVerificaElSpyDeLaPasarela()
    {
        // El stub-spy de la pasarela guarda el importe con el que se le llamó: así se verifica
        // que el servicio cobró exactamente el total del pedido, sin tocar una pasarela real.
        var pedido = new PedidoBuilder().ConLinea(50m, 2).Confirmado().Build();
        var repositorio = new RepositorioPedidosEnMemoria();
        repositorio.Agregar(pedido);
        var pasarela = new PasarelaPagoFalsa(exito: true);
        var servicio = new ServicioPedidos(repositorio, pasarela, new RelojFijo(InstanteFijo), new LoggerEspia<ServicioPedidos>());

        servicio.Pagar(pedido.Id);

        Assert.Equal(pedido.CalcularTotal(), pasarela.UltimoImporte);
    }

    [Fact]
    public void Pagar_PedidoInexistente_LanzaSinTocarLaPasarela()
    {
        // El fake está vacío: ObtenerPorId devuelve null y el servicio lanza antes de cobrar.
        var repositorio = new RepositorioPedidosEnMemoria();
        var pasarela = new PasarelaPagoFalsa(exito: true);
        var servicio = new ServicioPedidos(repositorio, pasarela, new RelojFijo(InstanteFijo), new LoggerEspia<ServicioPedidos>());

        Assert.Throws<InvalidOperationException>(() => servicio.Pagar(Guid.NewGuid()));
        Assert.Equal(0, pasarela.VecesCobrado); // ni se intentó cobrar
    }
}
