using AwesomeAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Infraestructura.Persistencia;

namespace VentasShop.TestsIntegracion;

/// <summary>
/// M6.4 — Testing del patrón Repository contra SQLite in-memory: el CRUD completo del RepositorioPedidos
/// (crear, leer, actualizar, borrar, consultar por cliente) y el contraste entre ObtenerPorId (Find, sin
/// navegaciones) y ObtenerConLineas (con Include). Cierra el Módulo 6.
///
/// ESTRATEGIA DE AISLAMIENTO: una conexión SQLite in-memory por test (constructor abre, Dispose cierra).
/// Como cada conexión es una base distinta, cada test arranca con una base limpia y propia: la
/// independencia (M1.3) sale sola, sin limpiar tablas a mano. Es la más simple de las tres estrategias
/// del submódulo (recrear / limpiar con Respawn / transacción con rollback).
/// </summary>
public class RepositorioPedidosCrudTests : IDisposable
{
    private readonly SqliteConnection _conexion;

    public RepositorioPedidosCrudTests()
    {
        _conexion = new SqliteConnection("Filename=:memory:");
        _conexion.Open();
        using var contexto = NuevoContexto();
        contexto.Database.EnsureCreated();
    }

    private ContextoVentasShop NuevoContexto() =>
        new(new DbContextOptionsBuilder<ContextoVentasShop>().UseSqlite(_conexion).Options);

    public void Dispose() => _conexion.Dispose();

    // Construcción directa (el PedidoBuilder vive en TestsUnitarios y no se referencia aquí; el deck lo
    // usa como forma de lectura, los tests de integración montan el pedido a mano, como en M6.2/M6.3).
    private static Producto ProductoCon(decimal precio) => new()
    {
        Codigo = "PROD-" + Guid.NewGuid().ToString()[..8],
        Nombre = "Producto",
        PrecioUnitario = new Dinero(precio, "EUR"),
        UnidadesStock = 100
    };

    private static Pedido PedidoCon(Cliente cliente, params (decimal precio, int cantidad)[] lineas)
    {
        var pedido = new Pedido(cliente);
        foreach (var (precio, cantidad) in lineas)
            pedido.AgregarLinea(ProductoCon(precio), new Cantidad(cantidad));
        return pedido;
    }

    private static Cliente ClienteVip() => new() { Nombre = "Ana", Tipo = TipoCliente.Vip };

    [Fact]
    public void Agregar_GuardaElPedidoConSusLineas_YSePuedeRecuperar()
    {
        var pedido = PedidoCon(ClienteVip(), (100m, 2), (30m, 1));

        using (var contexto = NuevoContexto())
            new RepositorioPedidos(contexto).Agregar(pedido);

        using (var contexto = NuevoContexto())
        {
            var recuperado = new RepositorioPedidos(contexto).ObtenerConLineas(pedido.Id);
            recuperado.Should().NotBeNull();
            recuperado!.Lineas.Should().HaveCount(2);
            recuperado.Cliente.Tipo.Should().Be(TipoCliente.Vip);
        }
    }

    [Fact]
    public void ObtenerPorId_UsaFind_YNoCargaLasLineas()
    {
        var pedido = PedidoCon(ClienteVip(), (100m, 2));
        using (var contexto = NuevoContexto())
            new RepositorioPedidos(contexto).Agregar(pedido);

        using (var contexto = NuevoContexto())
        {
            // ObtenerPorId usa Find: trae el pedido pero NO sus navegaciones.
            var recuperado = new RepositorioPedidos(contexto).ObtenerPorId(pedido.Id);
            recuperado.Should().NotBeNull();
            recuperado!.Lineas.Should().BeEmpty();   // las líneas NO se cargan sin Include
        }
    }

    [Fact]
    public void Guardar_PersisteElCambioDeEstado()
    {
        var pedido = PedidoCon(ClienteVip(), (50m, 1));   // nace en Borrador
        using (var contexto = NuevoContexto())
            new RepositorioPedidos(contexto).Agregar(pedido);

        using (var contexto = NuevoContexto())
        {
            var repositorio = new RepositorioPedidos(contexto);
            var recuperado = repositorio.ObtenerConLineas(pedido.Id)!;
            recuperado.Confirmar();
            repositorio.Guardar(recuperado);
        }

        using (var contexto = NuevoContexto())
        {
            var recuperado = new RepositorioPedidos(contexto).ObtenerPorId(pedido.Id);
            recuperado!.Estado.Should().Be(EstadoPedido.Confirmado);
        }
    }

    [Fact]
    public void Eliminar_QuitaElPedidoDeLaBase()
    {
        var pedido = PedidoCon(ClienteVip(), (50m, 1));
        using (var contexto = NuevoContexto())
            new RepositorioPedidos(contexto).Agregar(pedido);

        using (var contexto = NuevoContexto())
        {
            var repositorio = new RepositorioPedidos(contexto);
            repositorio.Eliminar(repositorio.ObtenerPorId(pedido.Id)!);
        }

        using (var contexto = NuevoContexto())
            new RepositorioPedidos(contexto).ObtenerPorId(pedido.Id).Should().BeNull();
    }

    [Fact]
    public void ObtenerPorCliente_DevuelveSoloLosPedidosDeEseCliente()
    {
        var ana = new Cliente { Nombre = "Ana", Tipo = TipoCliente.Vip };
        var beto = new Cliente { Nombre = "Beto", Tipo = TipoCliente.Estandar };

        using (var contexto = NuevoContexto())
        {
            var repositorio = new RepositorioPedidos(contexto);
            repositorio.Agregar(PedidoCon(ana, (30m, 1)));
            repositorio.Agregar(PedidoCon(ana, (40m, 2)));
            repositorio.Agregar(PedidoCon(beto, (20m, 1)));
        }

        using (var contexto = NuevoContexto())
        {
            var deAna = new RepositorioPedidos(contexto).ObtenerPorCliente(ana.Id);
            deAna.Should().HaveCount(2);
            deAna.Should().OnlyContain(p => p.Cliente.Id == ana.Id);
        }
    }
}
