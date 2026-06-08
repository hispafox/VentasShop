# VentasShop · M2.3 — Métricas de cobertura de código

> Rama `module-02.3/cobertura`. Checkpoint del curso **TESTNET**. Aquí **mides la cobertura** de los
> tests que ya tienes, y aprendes a no dejarte engañar por el número. Cierra el Módulo 2.

## Qué hay en esta rama

- **Cobertura activada** en `tests/VentasShop.TestsUnitarios/`: el paquete
  `Microsoft.Testing.Extensions.CodeCoverage` (sobre MTP) en el `.csproj`. Ya puedes recoger cobertura
  en la misma ejecución de la suite.
- **`CoberturaFalsoPositivoTests.cs`** — el ejemplo estrella del submódulo: dos **anti-ejemplos a
  propósito** (un test sin aserción y otro con aserción débil) que pasan en verde y "cubren" código sin
  comprobar nada, más el **contraste** con una aserción de verdad. Es el falso positivo de cobertura.
- Los tests de M2.2 siguen aquí (`CalculadoraDescuentosTests.cs`, `PedidoEstadosTests.cs`,
  `CantidadTests.cs`): son los que mides.
- **[`MANUAL.md`](MANUAL.md)** — qué mide la cobertura, líneas/ramas/caminos, el 80%, el falso positivo
  y cómo medir en este repo.
- **[`material/tarjetas/M2.3-cobertura.md`](material/tarjetas/M2.3-cobertura.md)** — tarjeta de 1 página.
- **[`material/labs/M2.3-generar-cobertura.md`](material/labs/M2.3-generar-cobertura.md)** — lab:
  generar cobertura, leer el % de ramas y vivir el falso positivo.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (25/25). Para recoger cobertura con información de ramas:

```bash
dotnet test tests/VentasShop.TestsUnitarios -- --coverage --coverage-output-format cobertura
```

Deja un `.cobertura.xml` en `TestResults/`. Leer e interpretar ese informe (ReportGenerator,
SonarQube) es el Módulo 7; aquí solo lo **generas**.

## Cobertura por reglas (BR)

Esta rama mide la cobertura de lo ya cubierto en M2.2: BR-01..BR-05 (descuento por volumen y tipo +
tope), BR-07, BR-08 y las transiciones del ciclo de vida del pedido. No añade reglas de negocio
nuevas: el código de producción no cambia entre ramas, solo crecen los tests.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-02.2/tecnicas-diseno` → **`module-02.3/cobertura`** ← estás aquí (cierra el Módulo 2) → `module-03.1/patron-aaa` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Convención: `Assert` nativo de xUnit (las aserciones fluidas llegan en M5.3; el mocking, en M5.2).
- Cobertura vía `Microsoft.Testing.Extensions.CodeCoverage` (MTP). Coverlet es de VSTest y NO integra
  con MTP: no se usa aquí. ReportGenerator y el análisis a fondo, en M7.
