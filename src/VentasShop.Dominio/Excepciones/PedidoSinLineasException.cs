namespace VentasShop.Dominio.Excepciones;

/// <summary>BR-07: no se puede operar (confirmar/pagar) un pedido sin líneas.</summary>
public sealed class PedidoSinLineasException : Exception
{
    public PedidoSinLineasException()
        : base("No se puede operar sobre un pedido sin líneas.") { }

    public PedidoSinLineasException(string mensaje) : base(mensaje) { }
}
