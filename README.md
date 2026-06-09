# VentasShop · M7.2 — Falsos positivos y tests frágiles

> Rama `module-07.2/falsos-positivos`. Checkpoint del curso **TESTNET**. Un test solo vale si su
> resultado significa algo. Aquí se ven las dos formas de verde que no protege —aserciones débiles y
> tests inestables— y la herramienta que las caza: **mutation testing con Stryker.NET sobre MTP**.

## Qué hay en esta rama

- **`tests/.../AsercionesDebilesTests.cs`** — 3 aserciones débiles (anti-ejemplos a propósito: pasan en
  verde pero no comprueban el valor) + 1 fuerte de contraste (`Assert.Equal(0.15m, tasa)`). El mutante que
  pone el tramo de volumen a cero (`0.10m → 0.0m`) **sobrevive** a las débiles y **muere** con la fuerte.
- **`tests/.../RelojTraidorTests.cs`** — el flaky por dependencia del reloj: un test determinista con
  `RelojFijo` (IReloj, M5.1) que fija «ahora», y el anti-ejemplo frágil con `DateTimeOffset.Now` marcado
  `[Fact(Skip=...)]` para no pudrir la suite (el lab pide reproducirlo y curarlo).
- **`tests/.../stryker-config.json`** — config de Stryker (muta `CalculadoraDescuentos` y `Cupon`, umbrales).

## Mutation testing con Stryker.NET (sobre MTP, NO Coverlet)

```bash
dotnet tool install -g dotnet-stryker
cd tests/VentasShop.TestsUnitarios
dotnet stryker --test-runner mtp
# informe en StrykerOutput/.../reports/mutation-report.html
```

**Por qué `--test-runner mtp`** (a junio de 2026): el proyecto va sobre el runner moderno (MTP), sin la
plataforma de pruebas antigua (VSTest). Stryker, por defecto, busca los tests por la vía antigua y no los
encontraría; la opción `--test-runner mtp` (en *preview* desde Stryker 4.13) le dice que use el runner del
curso. Es el mismo aviso de plataforma que con la cobertura en M7.1.

**Resultado verificado en esta rama:** mutation score **87,5%** sobre las dos clases (7 mutantes matados,
**1 superviviente**) — y ese superviviente es justo el «inspector ciego»: el bug que las aserciones débiles
no cazan. Refuerza la aserción y el mutante muere.

> Stryker es **lento** (ejecuta la suite por cada mutante): una pasada de vez en cuando sobre la lógica
> crítica, o un pipeline nocturno. No va en cada commit como la cobertura.

## Tests inestables (flaky)

La causa más común es **el tiempo**. `Cupon.EsValidoEn(ahora)` (BR-06) depende de la fecha: un test que le
pasa `DateTimeOffset.Now` pasa hoy y fallará tras la caducidad («funciona en junio, peta en julio»). La cura
es fijar «ahora» con `RelojFijo` (IReloj) — el test se vuelve determinista. Ver `RelojTraidorTests`.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 78 verdes + 1 skip (el frágil a propósito)
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11
```

## Nota sobre los anti-ejemplos

`AsercionesDebilesTests` y el test `Skip` de `RelojTraidorTests` están MAL hechos **a propósito**: son el
material del submódulo, no un modelo a copiar. (El capítulo muestra también `Assert.NotNull(decimal)` como
débil; aquí se usa `<= 0.15m` en su lugar para no chocar con el analizador xUnit2002 sobre tipos por valor —
misma lección, variante ejecutable.)

## Dónde estás en el curso

… → `module-07.1/analisis-cobertura` → **`module-07.2/falsos-positivos`** ← estás aquí → `module-07.3/...` → …

## Notas

- Material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Mutation testing sobre **MTP** (`dotnet stryker --test-runner mtp`), no Coverlet ni Docker.
