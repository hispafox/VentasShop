using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Excepciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.TestsUnitarios.Builders;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M4.3 — Gestión de excepciones. Que el código falle bien es una funcionalidad, y se testea: se prueba
/// el airbag provocando el choque a propósito y verificando que salta la excepción correcta. Aserciones
/// nativas de xUnit: Assert.Throws&lt;T&gt; (tipo exacto), Assert.ThrowsAny&lt;T&gt; (subtipos),
/// Assert.ThrowsAsync&lt;T&gt; (async, OJO al await) y Record.Exception (para "NO lanza nada").
/// Solo se testean las excepciones DE DOMINIO (las que el código lanza a propósito).
/// </summary>
public class ExcepcionesTests
{
    // === Assert.Throws<T>: la acción va en la lambda, dentro del Throws ===
    [Fact]
    public void Pagar_PedidoSinLineas_LanzaPedidoSinLineasException()
    {
        var pedido = new PedidoBuilder().SinLineas().Build();

        // Act + Assert se funden: xUnit ejecuta la lambda rodeada de try/catch
        Assert.Throws<PedidoSinLineasException>(() => pedido.Pagar());
    }

    // === Transición inválida: pedido con líneas pero en Borrador (no Confirmado) ===
    [Fact]
    public void Pagar_PedidoEnBorrador_LanzaTransicionPedidoInvalidaException()
    {
        var pedido = new PedidoBuilder().ConLinea(50m, 1).Build();   // queda en Borrador

        var ex = Assert.Throws<TransicionPedidoInvalidaException>(() => pedido.Pagar());

        // El mensaje es contrato suficiente aquí: comprobamos el fragmento, no el texto exacto
        Assert.Contains("pagar", ex.Message);
        Assert.Contains("Borrador", ex.Message);
    }

    // === Capturar la excepción para verificar ParamName y mensaje (Contains, no Equal) ===
    [Fact]
    public void Cantidad_ConCero_LanzaConParamNameYMensajeCorrectos()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Cantidad(0));

        Assert.Equal("valor", ex.ParamName);             // propiedad estructurada = contrato estable
        Assert.Contains("mayor que cero", ex.Message);   // fragmento del mensaje, no el texto entero
    }

    // === Otra excepción de dominio: no se pueden sumar importes de monedas distintas ===
    [Fact]
    public void Dinero_SumarMonedasDistintas_LanzaInvalidOperationException()
    {
        var euros   = new Dinero(10m, "EUR");
        var dolares = new Dinero(5m, "USD");

        var ex = Assert.Throws<InvalidOperationException>(() => _ = euros + dolares);

        Assert.Contains("monedas distintas", ex.Message);
    }

    // === Assert.ThrowsAny<T>: acepta el tipo y sus subtipos (aquí, cualquier ArgumentException) ===
    [Fact]
    public void Cantidad_ConNegativo_LanzaAlgunaArgumentException()
    {
        // ThrowsAny admitiría tambien una subclase (p. ej. ArgumentOutOfRangeException)
        Assert.ThrowsAny<ArgumentException>(() => new Cantidad(-1));
    }

    // === Record.Exception: afirmar que un código válido NO lanza nada ===
    [Fact]
    public void Pagar_PedidoConfirmadoValido_NoLanzaNada()
    {
        var pedido = new PedidoBuilder().ConLinea(50m, 1).Confirmado().Build();

        var excepcion = Record.Exception(() => pedido.Pagar());

        Assert.Null(excepcion);   // pagar un pedido válido NO dispara ningún airbag
    }

    // === Assert.ThrowsAsync<T>: excepciones en código asíncrono. OJO al await (ver el lab). ===
    // Ejemplo autocontenido: el dominio de VentasShop es síncrono; el async real (ServicioPedidos
    // con pasarela) llega en M5. Aquí se ilustra la FORMA con una operación async local que falla.
    [Fact]
    public async Task OperacionAsync_QueFalla_SeTesteaConThrowsAsync()
    {
        // SIN el await, este test pasaria SIEMPRE aunque la operacion no lanzara nada (falso positivo).
        await Assert.ThrowsAsync<InvalidOperationException>(OperacionAsyncQueFalla);
    }

    private static async Task OperacionAsyncQueFalla()
    {
        await Task.Delay(1);
        throw new InvalidOperationException("operacion asincrona rechazada");
    }
}
