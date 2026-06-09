# VentasShop · M6.3 — Testing con SQLite in-memory

> Rama `module-06.3/sqlite-in-memory`. Checkpoint del curso **TESTNET**. Cierra la espina de M6.2: el
> provider in-memory de EF Core no refuerza el índice único. **SQLite in-memory** sí: es un motor
> relacional de verdad en RAM, sin instalar ni levantar nada. El coche de verdad en el circuito de pruebas.

## Qué hay en esta rama

- **`Microsoft.EntityFrameworkCore.Sqlite`** añadido a `tests/VentasShop.TestsIntegracion` (junto al
  provider in-memory de M6.2, que se conserva para el contraste).
- **`tests/.../RepositorioPedidosSqliteTests.cs`** — 3 tests de integración contra SQLite in-memory:
  - `GuardarPedido_DespuesSePuedeRecuperarConSusLineas` — guardar con `Agregar`, leer en contexto NUEVO con `Include`.
  - `Consulta_PedidosDeUnCliente_DevuelveSoloLosSuyos` — consulta filtrada por cliente.
  - `Sqlite_RefuerzaElIndiceUnico_DosProductosConElMismoCodigoLanzan` — **la recompensa**: el mismo
    escenario que en M6.2 dejaba pasar, ahora `SaveChanges` lanza `DbUpdateException`.
- Setup por test con **`IDisposable`**: la conexión `SqliteConnection("Filename=:memory:")` se abre en
  el constructor y se cierra en `Dispose` (con ella se borra la base). Cada test, su base limpia.
- **[`MANUAL.md`](MANUAL.md)** — la conexión que hay que mantener abierta, `EnsureCreated`, el límite del dialecto, la estrategia.
- **[`material/tarjetas/M6.3-sqlite-in-memory.md`](material/tarjetas/M6.3-sqlite-in-memory.md)** + **[`material/labs/M6.3-el-contraste.md`](material/labs/M6.3-el-contraste.md)**.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 73/73
dotnet test  tests/VentasShop.TestsIntegracion   # 6/6 (3 in-memory + 3 SQLite)
```

No hace falta ningún servidor ni instalación: SQLite in-memory va en RAM.

## El contraste con M6.2

El test de unicidad de M6.2 (`InMemory_NoRefuerzaElIndiceUnico_...`) pasa **porque** el provider
in-memory no valida el índice único. El de M6.3 (`Sqlite_RefuerzaElIndiceUnico_...`) pasa **porque**
SQLite sí lo refuerza y `SaveChanges` lanza `DbUpdateException`. El mismo escenario, los dos motores,
en una sola suite: el simulador y el coche de verdad.

## La clave técnica (que más despista)

- **La base vive mientras la conexión esté abierta.** Por eso se abre la `SqliteConnection` y se mantiene
  viva durante el test; a `UseSqlite` se le pasa el **objeto** conexión, no una cadena.
- **`EnsureCreated`** crea el esquema desde el modelo (con el índice único). No `MigrateAsync` (no hay migraciones).
- **Una conexión por test** = una base propia y limpia (independencia, M1.3).

## El límite honesto

SQLite es un motor relacional real, pero **no es tu motor de producción**: hay diferencias de dialecto
(tipos, funciones, ordenación de cadenas). Cubre el grueso de la integración sin montar nada; para el
dialecto exacto crítico, el juez es tu motor real (M6.4 y más allá).

## Dónde estás en el curso

… → `module-06.1/unit-vs-integracion` → `module-06.2/ef-core-inmemory` → **`module-06.3/sqlite-in-memory`** ← estás aquí → `module-06.4/...` → …

## Notas

- Código y material **en castellano**, síncrono (el repo es síncrono). Proyecto **neutro**: sin nombres de cliente.
- SQLite in-memory da fidelidad relacional sin servidores ni instalaciones externas.
