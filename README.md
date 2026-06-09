# VentasShop · M6.2 — Testing con EF Core in-memory

> Rama `module-06.2/ef-core-inmemory`. Checkpoint del curso **TESTNET**. **Estrena el proyecto
> `VentasShop.TestsIntegracion`** con el provider in-memory de EF Core: rápido y sin Docker, pero no es un
> motor relacional. Aquí se ve, en una pantalla, qué **no** comprueba.

## Qué hay en esta rama

- **Cambio de dominio:** `Producto` estrena el campo `Codigo` (SKU), con **índice único** en
  `ContextoVentasShop` (`HasIndex(p => p.Codigo).IsUnique()`). Es la restricción que el in-memory no
  refuerza y que SQL Server real sí cazará (M6.3).
- **`Microsoft.EntityFrameworkCore.InMemory` + `AwesomeAssertions`** en `tests/VentasShop.TestsIntegracion`.
- **`tests/.../RepositorioPedidosInMemoryTests.cs`** — 3 tests de integración:
  - `GuardarPedido_DespuesSePuedeRecuperarConSusLineas` — guardar con `Agregar`, leer en contexto NUEVO con `Include`.
  - `Consulta_PedidosDeUnCliente_DevuelveSoloLosSuyos` — consulta filtrada por cliente.
  - `InMemory_NoRefuerzaElIndiceUnico_GuardaDosProductosConElMismoCodigo` — **documenta la limitación**:
    `SaveChanges` con código duplicado **no lanza** en in-memory (`Record.Exception` → `null`).
- **[`MANUAL.md`](MANUAL.md)** — el simulador, cómo se usa, qué no comprueba (con el matiz EF 6+), el ejemplo y cuándo sí.
- **[`material/tarjetas/M6.2-ef-in-memory.md`](material/tarjetas/M6.2-ef-in-memory.md)** + **[`material/labs/M6.2-in-memory-y-su-limite.md`](material/labs/M6.2-in-memory-y-su-limite.md)**.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 73/73
dotnet test  tests/VentasShop.TestsIntegracion   # 3/3 (NUEVO)
```

Los **unitarios** siguen en verde (73/73) y el proyecto de **integración** estrena 3 tests en verde. El
código de producción solo cambia para añadir `Producto.Codigo` + su índice único.

## El contraste con M6.3

El test `InMemory_NoRefuerzaElIndiceUnico_...` pasa **porque** el in-memory NO valida el índice único:
`Record.Exception(() => contexto.SaveChanges())` devuelve `null`. En M6.3, el mismo escenario contra SQL
Server real (Testcontainers) hará que `SaveChanges` lance `DbUpdateException`. Mismo código, distinto motor:
el simulador no es la carretera.

## Detalles que enseña

- **Contexto nuevo para leer** (no la caché de seguimiento de EF) — con el mismo nombre de base.
- **`Include`** para cargar las navegaciones (`Find` no las trae).
- **Nombre de base único por test** (`Guid`) = independencia.

## Dónde estás en el curso

… → `module-06.1/unit-vs-integracion` → **`module-06.2/ef-core-inmemory`** ← estás aquí → `module-06.3/testcontainers` → …

## Notas

- Código y material **en castellano**, síncrono (el repo es síncrono). Proyecto **neutro**: sin nombres de cliente.
- El equipo de EF desaconseja in-memory para lo relacional; se aborda de frente. SQLite in-memory es el punto medio; Testcontainers (M6.3) la fidelidad total.
