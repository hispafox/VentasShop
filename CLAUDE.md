# VentasShop — instrucciones del repo

Proyecto-ejemplo del curso **TESTNET — Testing Automatizado en .NET**. Fuente de verdad ejecutable:
el código compila y los tests pasan, y los capítulos del curso enlazan aquí. Ficha completa en
[`README.md`](README.md). Spec del dominio en el vault del curso (`recursos/proyecto-ejemplo-spec.md`).

## Reglas duras (no negociables)

1. **Código EN CASTELLANO.** Identificadores, namespaces y nombres de proyecto en español
   (`Dinero`/`Importe`/`Moneda`, `Pedido`/`Pagar()`/`Estado`, `CalculadoraDescuentos`,
   `IRepositorioPedidos`, `ServicioPedidos`; proyectos `VentasShop.Dominio/.Aplicacion/...`).
   Identificadores en **ASCII** (sin ñ ni tildes: `AgregarLinea`, `Estandar`, `Lineas`). Strings y
   comentarios sí llevan acentos. El vocabulario propio del testing se mantiene (test, suite, mock,
   bug, fixture, flaky…).
2. **Neutro (R1).** Cero nombres ni datos de cliente. `NotificadorPedidos` es legacy neutro de
   enseñanza. El código real del cliente NUNCA entra aquí (solo se trabaja en el taller en directo).
3. **El código de producción vive en `starter` y no cambia entre ramas.** Lo que crece son los
   **tests**, submódulo a submódulo. Excepción: el legacy del Módulo 8.
4. **Stack y convención por módulo.** xUnit v3 + `Assert` nativo hasta M5.3; NSubstitute entra en
   M5.2; AwesomeAssertions en M5.3; cobertura (`Microsoft.Testing.Extensions.CodeCoverage`, sobre MTP)
   en M2.3 y a fondo (ReportGenerator, `dotnet-coverage`) en M7; EF Core InMemory en M6.2 y
   SQLite in-memory en M6.3 (motor relacional real, sin instalar ni levantar nada). Cada rama refleja la progresión: no metas una librería antes de su módulo.
   Coverlet (collector/msbuild) es de VSTest y NO integra con MTP: no se usa en este repo.
5. **Trazabilidad regla → test.** Cada test referencia su `BR-XX` (ver README).
6. **Prosa en castellano humano.** Los `MANUAL.md` y `README.md` de cada rama se redactan con el
   skill `escritura-humana` (en `.claude/skills/`) y en **«tú» singular**. Tras redactar, grep de
   lista negra.

## Estructura de ramas

`starter` → `module-MM.S/<slug>` (una por submódulo, acumulativas) → `main`; más
`module-MM.S-demo-<slug>` (una por demo, ramificada de su submódulo). Detalle en el README.

Cada rama de submódulo añade, sobre la anterior: sus **tests** + **`MANUAL.md`** (manual del alumno,
pedagógico — skill `manual-del-alumno`) + **`README.md`** (ficha técnica — skill
`documentador-ejemplos`).

## Skills disponibles (`.claude/skills/`)

- `escritura-humana` — voz humana en español; obligatorio antes de redactar prosa.
- `manual-del-alumno` — genera el `MANUAL.md` pedagógico de un ejemplo (plantilla de 13 secciones).
- `documentador-ejemplos` — genera el `README.md` técnico de un ejemplo.

## Cómo trabajar

- **Compila y testea antes de commitear:** `dotnet build VentasShop.slnx` y `dotnet test VentasShop.slnx`.
- **Construcción acumulativa:** rellena las ramas en orden de temario; cada submódulo parte de la
  anterior. Las ramas de demo parten de su submódulo padre.
- Plan de producción del repo: `~/.claude/plans/refactored-greeting-beaver.md` (en la máquina de Pedro).
