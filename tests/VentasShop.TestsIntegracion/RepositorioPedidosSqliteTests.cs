using AwesomeAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Infraestructura.Persistencia;

namespace VentasShop.TestsIntegracion;

/// <summary>
/// M6.3 — Tests de INTEGRACIÓN contra SQLite in-memory: un motor relacional de verdad en RAM, sin
/// instalar ni levantar nada. A diferencia del provider in-memory de M6.2, SQLite SÍ
/// refuerza el índice único, las claves foráneas y las transacciones. Es el coche de verdad en el
/// circuito de pruebas: motor real, aunque no sea tu motor de producción exacto (dialecto).
///
/// Clave: la base en memoria vive mientras la conexión esté ABIERTA. Por eso abrimos la conexión en el
/// constructor, la mantenemos viva durante el test y la cerramos en Dispose (con ella se borra la base).
/// A UseSqlite se le pasa el OBJETO conexión, no una cadena, para que todos los contextos compartan base.
/// </summary>
public class RepositorioPedidosSqliteTests : IDisposable
{
    private readonly SqliteConnection _conexion;

    public RepositorioPedidosSqliteTests()
    {
        // xUnit crea una instancia de la clase por cada test → una base nueva, limpia y propia por test.
        _conexion = new SqliteConnection("Filename=:memory:");
        _conexion.Open();   // la base existe mientras esta conexión siga abierta
        using var contexto = NuevoContexto();
        contexto.Database.EnsureCreated();   // crea el esquema desde el modelo (con el índice único)
    }

    private ContextoVentasShop NuevoContexto() =>
        new(new DbContextOptionsBuilder<ContextoVentasShop>()
            .UseSqlite(_conexion)   // el objeto conexión abierto, no una cadena de texto
            .Options);

    public void Dispose() => _conexion.Dispose();   // cierra la conexión → se borra la base en memoria

    private static Producto ProductoCon(string codigo, decimal precio) => new()
    {
        Codigo = codigo,
        Nombre = "Producto " + codigo,
        PrecioUnitario = new Dinero(precio, "EUR"),
        UnidadesStock = 100
    };

    [Fact]
    public void GuardarPedido_DespuesSePuedeRecuperarConSusLineas()
    {
        var cliente = new Cliente { Nombre = "Ana", Tipo = TipoCliente.Vip };
        var pedido = new Pedido(cliente);
        pedido.AgregarLinea(ProductoCon("TECL-01", 50m), new Cantidad(2));

        using (var contexto = NuevoContexto())
            new RepositorioPedidos(contexto).Agregar(pedido);   // Agregar guarda internamente

        // Leemos en un contexto NUEVO (misma conexión, misma base) para no leer de la caché de EF;
        // con Include para traer las líneas, que Find por sí solo no carga.
        using var lectura = NuevoContexto();
        var recuperado = lectura.Pedidos
            .Include(p => p.Lineas)
            .SingleOrDefault(p => p.Id == pedido.Id);

        recuperado.Should().NotBeNull();
        recuperado!.Lineas.Should().HaveCount(1);
    }

    [Fact]
    public void Consulta_PedidosDeUnCliente_DevuelveSoloLosSuyos()
    {
        var ana = new Cliente { Nombre = "Ana", Tipo = TipoCliente.Vip };
        var beto = new Cliente { Nombre = "Beto", Tipo = TipoCliente.Estandar };

        using (var contexto = NuevoContexto())
        {
            var repositorio = new RepositorioPedidos(contexto);

            var pedidoAna1 = new Pedido(ana);
            pedidoAna1.AgregarLinea(ProductoCon("PROD-A", 30m), new Cantidad(1));
            var pedidoAna2 = new Pedido(ana);
            pedidoAna2.AgregarLinea(ProductoCon("PROD-B", 40m), new Cantidad(2));
            var pedidoBeto = new Pedido(beto);
            pedidoBeto.AgregarLinea(ProductoCon("PROD-C", 20m), new Cantidad(1));

            repositorio.Agregar(pedidoAna1);
            repositorio.Agregar(pedidoAna2);
            repositorio.Agregar(pedidoBeto);
        }

        using var lectura = NuevoContexto();
        var pedidosDeAna = lectura.Pedidos
            .Include(p => p.Cliente)
            .Where(p => p.Cliente.Id == ana.Id)
            .ToList();

        pedidosDeAna.Should().HaveCount(2);
    }

    [Fact]
    public void Sqlite_RefuerzaElIndiceUnico_DosProductosConElMismoCodigoLanzan()
    {
        using var contexto = NuevoContexto();
        contexto.Productos.Add(ProductoCon("ABC", 50m));
        contexto.Productos.Add(ProductoCon("ABC", 20m));   // mismo Codigo → viola el índice único

        // ¡Ahora SÍ salta! SQLite es un motor relacional de verdad: el índice único existe en la base,
        // el segundo producto con código repetido lo viola, y SaveChanges lanza la excepción.
        // Es justo el test que el provider in-memory de M6.2 dejaba pasar.
        Action guardar = () => contexto.SaveChanges();

        guardar.Should().Throw<DbUpdateException>();
    }
}
