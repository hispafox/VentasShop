# VentasShop · M4.1 — Introducción a xUnit.net

> Rama `module-04.1/introduccion-xunit`. Checkpoint del curso **TESTNET**. Aquí montas el proyecto de
> tests con xUnit v3 y escribes tus primeros `[Fact]` con la clase `Assert` nativa. Abre el Módulo 4
> (la herramienta a fondo) y aterriza el stack cerrado del curso.

## Qué hay en esta rama

- **`tests/.../PrimerasPruebasXunitTests.cs`** — los `[Fact]` de M4.1: el primer test del capítulo y los
  tres tramos de descuento, **uno por test**, más un par de ejemplos del repertorio de `Assert`
  (`Assert.Single`, `Assert.Empty`, `Assert.Equal`). Es el "antes" de los tests parametrizados.
- **[`MANUAL.md`](MANUAL.md)** — el banco de trabajo, montar el proyecto (estructura, comandos, el `.csproj`
  con `OutputType=Exe` sobre Microsoft Testing Platform), `[Fact]`, la clase `Assert` y leer un fallo.
- **[`material/tarjetas/M4.1-xunit.md`](material/tarjetas/M4.1-xunit.md)** — tarjeta de 1 página.
- **[`material/labs/M4.1-montar-proyecto-xunit.md`](material/labs/M4.1-montar-proyecto-xunit.md)** — lab:
  montar el proyecto de cero, primeros `[Fact]`, y romper el código a propósito para leer la salida.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (39/39). `PrimerasPruebasXunitTests.cs` añade 7 `[Fact]`; el código de
producción no cambia.

## El setup de xUnit v3 (lo que conviene mirar)

El proyecto `tests/VentasShop.TestsUnitarios` está montado con la variante **MTP** de xUnit v3:

- `<OutputType>Exe</OutputType>` — el proyecto de test es ejecutable (novedad de la v3).
- `<TestingPlatformDotnetTestSupport>true</...>` + paquete `xunit.v3.mtp-v2` — se apoya en la
  **Microsoft Testing Platform**, no en la variante VSTest (`xunit.runner.visualstudio`).
- `xunit.runner.json` — configuración del runner.

Es la base sobre la que el Módulo 7 monta la medición de cobertura.

## Qué cubre (BR)

No añade reglas de negocio: reusa la `CalculadoraDescuentos` (descuento por volumen + tipo de cliente con
tope del 15%) para enseñar **la mecánica de xUnit**: `[Fact]`, el repertorio de `Assert` y la lectura de un
fallo. El SUT no cambia.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `tests/Builders/` y `tests/Mothers/` → el atrezzo de test (M3.3,
  reutilizado aquí sin re-explicar).
- `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-03.3/builders-object-mother` (cierra el Módulo 3) → **`module-04.1/introduccion-xunit`** ← estás aquí (abre el Módulo 4) → `module-04.2/tests-parametrizados` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Los tres tramos van como `[Fact]` separados **a propósito**: es el tedio que `[Theory]` resuelve en M4.2.
  Compara `PrimerasPruebasXunitTests.cs` (el "antes") con `CalculadoraDescuentosTests.cs` (el "después").
- Las versiones concretas de los paquetes las pone la plantilla y caducan: no se fijan en el material.
