using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.Entidades;

namespace VentasShop.TestsUnitarios.Dobles;

/// <summary>
/// FAKE de <see cref="IRepositorioPedidos"/> hecho a mano (M5.1): una implementación de verdad, pero
/// reducida, sobre un diccionario en memoria en vez de una base de datos. Guarda y recupera igual que
/// el repositorio real, sin la lentitud ni la infraestructura. Es el doble que de verdad funciona.
/// </summary>
/// <remarks>
/// Lleva además un contador <see cref="VecesGuardado"/>: así el mismo objeto sirve también de spy
/// cuando un test quiere verificar la interacción ("se llamó a Guardar una vez"). El tipo de doble lo
/// decide el test, no la dependencia (M5.1). Un fake como este no se monta con librería; se escribe a
/// mano una vez y se reutiliza, como los Builders de M3.3.
/// </remarks>
public sealed class RepositorioPedidosEnMemoria : IRepositorioPedidos
{
    private readonly Dictionary<Guid, Pedido> _pedidos = [];

    /// <summary>Cuántas veces se ha llamado a <see cref="Guardar"/> (para verificación de interacción).</summary>
    public int VecesGuardado { get; private set; }

    public Pedido? ObtenerPorId(Guid id) => _pedidos.GetValueOrDefault(id);

    // En memoria el grafo del pedido (Lineas, Cliente) ya está cargado: no hay nada que "incluir".
    public Pedido? ObtenerConLineas(Guid id) => _pedidos.GetValueOrDefault(id);

    public IReadOnlyList<Pedido> ObtenerPorCliente(Guid clienteId) =>
        _pedidos.Values.Where(p => p.Cliente.Id == clienteId).ToList();

    public void Agregar(Pedido pedido) => _pedidos[pedido.Id] = pedido;

    public void Guardar(Pedido pedido)
    {
        _pedidos[pedido.Id] = pedido;
        VecesGuardado++;
    }

    public void Eliminar(Pedido pedido) => _pedidos.Remove(pedido.Id);
}
