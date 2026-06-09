# VentasShop · M7.1 — Análisis de cobertura

> Rama `module-07.1/analisis-cobertura`. Checkpoint del curso **TESTNET**. No añade tests de producción:
> enseña a **generar y leer** el informe de cobertura sobre la suite que ya tienes. Un porcentaje no se
> mira, se lee.

## La cobertura en este repo (sobre MTP, sin Coverlet)

El proyecto de tests ya trae el paquete **`Microsoft.Testing.Extensions.CodeCoverage`** (desde M2.3). La
cobertura se genera por la cobertura de Microsoft Code Coverage, **no con Coverlet** (Coverlet es de la
plataforma de pruebas antigua —VSTest— y no integra con el runner del curso, MTP).

```bash
# vía cómoda (la de M2.3): genera coverage.cobertura.xml
dotnet test tests/VentasShop.TestsUnitarios -- --coverage --coverage-output-format cobertura

# vía con más control (CI / SonarQube), con el tool dotnet-coverage
dotnet tool install --global dotnet-coverage
dotnet-coverage collect "dotnet test tests/VentasShop.TestsUnitarios" -f cobertura -o coverage.cobertura.xml
```

Del XML al mapa, con ReportGenerator:

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
# abre coveragereport/index.html
```

## Qué mirar en el mapa

- Pincha en una clase y mira el **color línea a línea**: verde (ejecutada), rojo (zona ciega), amarillo
  (decisión con ramas a medias). El amarillo es la información de oro.
- Mira la columna de **ramas**, no la de líneas (M2.3).
- La `CalculadoraDescuentos` está **bien cubierta a propósito** —incluido el tope BR-05— para que veas un
  mapa sano. El test que el capítulo imagina «faltando» (el VIP con pedido grande que llega justo al 15%)
  existe: es `CoberturaFalsoPositivoTests.CalcularTasaDescuento_VipPedidoGrande_AplicaElTope`.

## Verde ≠ probado (la trampa que conecta con M7.2)

`CoberturaFalsoPositivoTests` (de M2.3) lo deja a la vista: un test que **llama** al método pero **no
comprueba** el resultado pinta la línea de verde igual. El mapa te dice dónde pusiste cámara, no que esté
enfocando algo. Cómo cazar ese «verde encendido pero ciego» (mutation testing) es M7.2.

## El laboratorio

[`material/labs/M7.1-leer-el-mapa.md`](material/labs/M7.1-leer-el-mapa.md): el encargo del hombre del
tiempo —no cazar todos los rojos, sino hacer la **lista razonada** de cuáles importan (ciudad) y cuáles se
dejan (océano), con el marco de M2.1—. La entrega es la lista, no un porcentaje más alto. La tarjeta
([`material/tarjetas/M7.1-cobertura.md`](material/tarjetas/M7.1-cobertura.md)) resume los comandos.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 73/73
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11
```

## Dónde estás en el curso

… → `module-06.4/repositorios` (cierra M6) → **`module-07.1/analisis-cobertura`** ← estás aquí → `module-07.2/...` → …

## Notas

- Material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Cobertura sobre **MTP** (`Microsoft.Testing.Extensions.CodeCoverage`), no Coverlet.
