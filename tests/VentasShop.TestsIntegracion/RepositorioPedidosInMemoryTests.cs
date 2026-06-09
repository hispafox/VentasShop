using AwesomeAssertions;
using Microsoft.EntityFrameworkCore;
using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Infraestructura.Persistencia;

namespace VentasShop.TestsIntegracion;

/// <summary>
/// M6.2 — Primeros tests de INTEGRACIÓN, con el provider in-memory de EF Core. Practican la lógica del
/// repositorio (guardar/recuperar/consultar) y, sobre todo, DEMUESTRAN una limitación: el in-memory no
/// refuerza el índice único. Es el simulador de conducción: vale para la lógica, no es la carretera.
/// El mismo escenario de unicidad, contra SQLite in-memory (un motor relacional de verdad), sí salta (M6.3).
/// </summary>
public class RepositorioPedidosInMemoryTests
{
    // Mismo nombre de base = mismo almacén (para guardar con un contexto y leer con otro);
    // Guid único por test = independencia entre tests (M1.3).
    private static ContextoVentasShop CrearContexto(string nombreBd) =>
        new(new DbContextOptionsBuilder<ContextoVentasShop>()
            .UseInMemoryDatabase(nombreBd)
            .Options);

    private static Pedido PedidoCon(Cliente cliente, string codigoProducto, int cantidad)
    {
        var pedido = new Pedido(cliente);
        var producto = new Producto
        {
            Codigo = codigoProducto,
            Nombre = "Producto " + codigoProducto,
            PrecioUnitario = new Dinero(50m, "EUR"),
            UnidadesStock = 100
        };
        pedido.AgregarLinea(producto, new Cantidad(cantidad));
        return pedido;
    }

    [Fact]
    public void GuardarPedido_DespuesSePuedeRecuperarConSusLineas()
    {
        var nombreBd = Guid.NewGuid().ToString();
        var pedido = PedidoCon(new Cliente { Nombre = "Ana", Tipo = TipoCliente.Vip }, "TECL-01", 2);

        using (var contexto = CrearContexto(nombreBd))
            new RepositorioPedidos(contexto).Agregar(pedido);   // Agregar guarda internamente

        // Leemos en un contexto NUEVO (misma base) para no leer de la caché de EF;
        // con Include para traer las líneas, que Find por sí solo no carga.
        using var lectura = CrearContexto(nombreBd);
        var recuperado = lectura.Pedidos
            .Include(p => p.Lineas)
            .SingleOrDefault(p => p.Id == pedido.Id);

        recuperado.Should().NotBeNull();
        recuperado!.Lineas.Should().HaveCount(1);
    }

    [Fact]
    public void Consulta_PedidosDeUnCliente_DevuelveSoloLosSuyos()
    {
        var nombreBd = Guid.NewGuid().ToString();
        var ana = new Cliente { Nombre = "Ana", Tipo = TipoCliente.Vip };
        var beto = new Cliente { Nombre = "Beto", Tipo = TipoCliente.Estandar };

        using (var contexto = CrearContexto(nombreBd))
        {
            var repositorio = new RepositorioPedidos(contexto);
            repositorio.Agregar(PedidoCon(ana, "PROD-A", 1));
            repositorio.Agregar(PedidoCon(ana, "PROD-B", 2));
            repositorio.Agregar(PedidoCon(beto, "PROD-C", 1));
        }

        using var lectura = CrearContexto(nombreBd);
        var pedidosDeAna = lectura.Pedidos
            .Include(p => p.Cliente)
            .Where(p => p.Cliente.Id == ana.Id)
            .ToList();

        pedidosDeAna.Should().HaveCount(2);
    }

    [Fact]
    public void InMemory_NoRefuerzaElIndiceUnico_GuardaDosProductosConElMismoCodigo()
    {
        using var contexto = CrearContexto(Guid.NewGuid().ToString());
        contexto.Productos.Add(new Producto { Codigo = "ABC", Nombre = "Teclado", PrecioUnitario = new Dinero(50m, "EUR"), UnidadesStock = 10 });
        contexto.Productos.Add(new Producto { Codigo = "ABC", Nombre = "Ratón",   PrecioUnitario = new Dinero(20m, "EUR"), UnidadesStock = 10 });

        // El provider in-memory NO refuerza el índice único: guarda los dos sin lanzar.
        // Contra SQLite in-memory (M6.3) este mismo SaveChanges lanzaría DbUpdateException.
        var excepcion = Record.Exception(() => contexto.SaveChanges());

        excepcion.Should().BeNull();   // documenta la limitación: el simulador no es la carretera
    }
}
