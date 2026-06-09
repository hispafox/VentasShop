# VentasShop · M8.1 — Diagnóstico del proyecto

> Rama `module-08.1/diagnostico`. Checkpoint del curso **TESTNET**. Abre el Módulo 8: el salto fuera del
> laboratorio. Un proyecto real llega sin tests y con código que no se deja testear; el primer trabajo es
> **diagnosticar** y abrir la primera costura, no escribir el test.

## Qué hay en esta rama

- **`tests/.../Legacy/NotificadorPedidos.cs`** — un **legacy NEUTRO inventado** (R1: jamás código de
  cliente), un mundo aparte de VentasShop. Aquí está la versión **ya refactorizada**: las cuatro
  dependencias clavadas convertidas en cuatro costuras por inyección de dependencias
  (`IAccesoPedidos`, `IReloj`, `IEnviadorCorreo`, `ILogger<>`). La lógica es idéntica al original; solo
  cambia de dónde vienen las piezas.
- **`tests/.../Legacy/NotificadorPedidosTests.cs`** — la recompensa: la clase que era **intestable** ahora
  se testea con dobles (RelojFijo y LoggerEspia de M5.1 + dos fakes mínimos). 3 tests: notifica de día,
  no de noche, deja apunte en el log.
- **[`material/labs/M8.1-diagnostico-legacy.md`](material/labs/M8.1-diagnostico-legacy.md)** — el lab con
  la versión **«antes»** (los 4 clavos) y el diagnóstico + refactor mínimo.
- **[`material/tarjetas/M8.1-diagnostico.md`](material/tarjetas/M8.1-diagnostico.md)** — la chuleta.

## La idea

Un proyecto real no se parece a VentasShop (laboratorio). El trabajo, en orden:

1. **Triaje** (riesgo × consecuencia, M2.1 + el historial de Git como señal): empieza por lo crítico y lo
   que más se toca, no por la primera clase.
2. **Diagnóstico de testabilidad**: busca las **costuras** (*seam*, Feathers). Las dependencias clavadas
   (`new` de infraestructura, `DateTime.Now`, singletons estáticos) impiden meter un doble.
3. **Intervención mínima**: abre la primera costura introduciendo inyección de dependencias, **sin cambiar
   el comportamiento**. Cambias la estructura, no lo que hace.
4. Si el cambio da respeto, primero un **test de caracterización** (foto del comportamiento actual), luego
   el refactor de verdad.

## Por qué el legacy es un mundo aparte (y la versión «antes» no compila)

El `NotificadorPedidos` original tiene 4 clavos: `new RepositorioPedidosSql("Server=prod-db;...")`,
`DateTime.Now`, `new SmtpClient(...)` y `Logger.Instance`. Clavado a producción, no se puede ni compilar
en un test ni testear; por eso esa versión vive como **snippet en el lab**, no como código de esta rama. Lo
que sí compila y se testea es la versión refactorizada. Es legacy **neutro e inventado**: el `Cliente` de
VentasShop no tiene email, así que el ejemplo usa su propio `PedidoLegacy` para no forzar el dominio real.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 87 verdes + 1 skip
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11
```

## Dónde estás en el curso

… → `module-07.3/buenas-practicas` (cierra M7) → **`module-08.1/diagnostico`** ← estás aquí → `module-08.2/...` → …

## Notas

- Material **en castellano**. **R1**: legacy neutro inventado; el proyecto real del cliente solo en el
  taller en directo de M8.3, nunca en el material.
- Reutiliza `IReloj`/`RelojFijo` y `ILogger`/`LoggerEspia` (M5.1) como costuras del reloj y del log.
