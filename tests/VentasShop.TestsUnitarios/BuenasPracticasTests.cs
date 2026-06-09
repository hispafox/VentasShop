using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Precios;
using VentasShop.TestsUnitarios.Builders;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M7.3 · Buenas prácticas consolidadas — la suite-JARDÍN (el "después" del lab).
///
/// Estos tests modelan los hábitos del submódulo sobre VentasShop: un assert CONCEPTUAL por test
/// (un comportamiento, una razón para el rojo), simplicidad (ni una línea de lógica: parametrización
/// con [Theory] en vez de un for), e independencia (cada test se fabrica sus datos con el builder de
/// M3.3, sin estado compartido). El "antes" (la suite-jungla: FuncionaTodo, for-en-test, variable
/// estática compartida, nombre mudo, test de feature muerta) está en material/labs/M7.3-jungla-a-jardin.md.
/// </summary>
public class BuenasPracticasTests
{
    // Un assert CONCEPTUAL por test: el FuncionaTodo se separa en un comportamiento por test, y el
    // nombre canta qué se rompió si se pone rojo.
    [Fact]
    public void Pagar_DePedidoConfirmado_DejaElPedidoEnPagado()
    {
        var pedido = new PedidoBuilder().ConLinea(50m, 2).Confirmado().Build();
        pedido.Pagar();
        Assert.Equal(EstadoPedido.Pagado, pedido.Estado);
    }

    [Fact]
    public void CalcularTotal_DeDosUnidadesA50_Devuelve100()
    {
        var pedido = new PedidoBuilder().ConLinea(50m, 2).Confirmado().Build();
        Assert.Equal(100m, pedido.CalcularTotal().Importe);
    }

    // Simplicidad: en vez de un for recorriendo casos dentro del test (lógica que puede fallar y
    // mensajes confusos), una [Theory] — cada caso es una ejecución independiente. Los números
    // "mágicos" con significado se dejan a la vista: son lo que hace el test legible.
    [Theory]
    [InlineData(500, TipoCliente.Estandar, 0.10)]   // BR-03: pedido grande
    [InlineData(100, TipoCliente.Estandar, 0.05)]   // BR-02: pedido medio
    [InlineData(500, TipoCliente.Vip, 0.15)]        // BR-03 + BR-04 + BR-05: tope
    public void CalcularTasaDescuento_SegunImporteYCliente_DevuelveLaTasaEsperada(
        decimal total, TipoCliente tipo, double tasaEsperada)
    {
        var tasa = new CalculadoraDescuentos().CalcularTasaDescuento(total, tipo);
        Assert.Equal((decimal)tasaEsperada, tasa);
    }

    // Independencia: este test no comparte nada con los de arriba; fabrica su propio pedido y no deja
    // rastro. El orden de ejecución no cambia su resultado (FIRST, M1.3).
    [Fact]
    public void Confirmar_DePedidoConLineas_DejaElPedidoEnConfirmado()
    {
        var pedido = new PedidoBuilder().ConLinea(20m, 1).Build();   // nace en Borrador
        pedido.Confirmar();
        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
    }
}
