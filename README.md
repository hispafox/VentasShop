# VentasShop · M6.4 — Testing de repositorios y acceso a datos

> Rama `module-06.4/repositorios`. Checkpoint del curso **TESTNET**. Cierra el Módulo 6: el patrón
> Repository como mostrador delante de la base de datos, el CRUD completo contra SQLite in-memory y la
> estrategia de aislamiento entre tests.

## Qué hay en esta rama

- **Repo enriquecido** (`IRepositorioPedidos` + `RepositorioPedidos`): además de `ObtenerPorId`/`Agregar`/`Guardar`,
  ahora `ObtenerConLineas(Guid)` (con `Include` de líneas y cliente), `ObtenerPorCliente(Guid)` y `Eliminar(Pedido)`.
- **Borrado en cascada** del pedido a sus líneas en `ContextoVentasShop` (`OnDelete(DeleteBehavior.Cascade)`):
  SQLite, motor real, exige la integridad referencial; el provider in-memory no se habría quejado.
- **`tests/.../RepositorioPedidosCrudTests.cs`** — 5 tests de integración (CRUD completo) contra SQLite in-memory:
  - `Agregar_GuardaElPedidoConSusLineas_YSePuedeRecuperar` — guardar + recuperar con `ObtenerConLineas`.
  - `ObtenerPorId_UsaFind_YNoCargaLasLineas` — el contraste: `Find` trae el pedido pero **no** las líneas.
  - `Guardar_PersisteElCambioDeEstado` — actualizar (Confirmar) y comprobar que persiste.
  - `Eliminar_QuitaElPedidoDeLaBase` — borrar (cascade a las líneas).
  - `ObtenerPorCliente_DevuelveSoloLosPedidosDeEseCliente` — consulta filtrada por cliente.
- El doble a mano `RepositorioPedidosEnMemoria` (M5.1) se amplía con los 3 métodos nuevos para seguir compilando.
- **[`MANUAL.md`](MANUAL.md)** + **[`material/labs/M6.4-crud-y-aislamiento.md`](material/labs/M6.4-crud-y-aislamiento.md)** + **[`material/tarjetas/M6.4-repositorios.md`](material/tarjetas/M6.4-repositorios.md)**.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 73/73
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11 (3 in-memory + 3 SQLite + 5 CRUD)
```

## La estrategia de aislamiento de esta rama

Una conexión SQLite in-memory **por test** (el constructor abre, `Dispose` cierra). Como cada conexión
es una base distinta, cada test arranca con una base limpia y propia: la independencia (M1.3) sale sola,
sin limpiar tablas a mano. Es la más simple de las tres estrategias del submódulo (recrear / limpiar con
Respawn / transacción con rollback).

## El contraste que enseña el módulo

`ObtenerPorId` usa `Find`: trae el pedido pero **no** carga sus navegaciones. `ObtenerConLineas` lleva
`Include`. El test `ObtenerPorId_UsaFind_YNoCargaLasLineas` lo deja a la vista: si lees por la vía sin
`Include` y esperas líneas, vendrán vacías — y solo un test de integración real lo caza (M6.1).

## Dónde estás en el curso

… → `module-06.2/ef-core-inmemory` → `module-06.3/sqlite-in-memory` → **`module-06.4/repositorios`** ← estás aquí (cierra M6) → `module-07.1/...` → …

## Notas

- Código y material **en castellano**, síncrono. Proyecto **neutro**: sin nombres de cliente.
- Motor de integración: **SQLite in-memory** (M6.3), sin servidores ni instalaciones externas.
