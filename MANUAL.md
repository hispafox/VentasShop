# Manual del alumno — M7.1 · Análisis de cobertura

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué un porcentaje de
cobertura, solo, no sirve, y cómo se lee el informe para salir de ahí con decisiones, no con un número.

Tiempo de lectura: ~12 min. Submódulo M7.1 (abre el Módulo 7).

---

## 1. La idea en una frase

Un porcentaje de cobertura no se mira, se lee. El número (un «78%») es la temperatura media del país: cierto
y casi inútil. El informe coloreado es el mapa que te dice en qué clase y en qué rama falta señal, y cuáles
de esos huecos importan de verdad.

---

## 2. El mapa del tiempo, no la temperatura media

Si el hombre del tiempo dijera solo «hoy, 16 grados de media» y se fuera, no te serviría: esa media es
compatible con un día de primavera en todas partes y con que nieve en los Pirineos mientras Sevilla se asa.
Por eso el informe de verdad es un mapa pintado por zonas. La cobertura igual: el porcentaje es la media; el
informe de ReportGenerator es el mapa, con cada clase y cada rama en su color.

---

## 3. Generar el dato (sobre MTP, sin Coverlet)

La cobertura de este repo va con `Microsoft.Testing.Extensions.CodeCoverage`, la extensión que ya trae el
proyecto de tests desde M2.3. **No usamos Coverlet**: es de la plataforma de pruebas antigua (VSTest) y no
integra con el runner del curso (MTP). Dos vías:

```bash
# cómoda (la de M2.3)
dotnet test tests/VentasShop.TestsUnitarios -- --coverage --coverage-output-format cobertura

# con más control (CI / Sonar), con el tool dotnet-coverage
dotnet-coverage collect "dotnet test tests/VentasShop.TestsUnitarios" -f cobertura -o coverage.cobertura.xml
```

El formato lo eliges según quién lo lee: **`cobertura`** para ReportGenerator (y trae la cobertura de ramas
completa), **`xml`** (el de Visual Studio) para SonarQube. El umbral que rompe la build, si lo quieres, se
configura por un fichero de settings de cobertura, como red de seguridad baja, no como vara alta que empuje
a inflar el número.

---

## 4. Del XML ilegible al mapa: ReportGenerator

El `coverage.cobertura.xml` crudo es para máquinas. ReportGenerator lo convierte en HTML navegable:

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

Abres `coveragereport/index.html` y, por fin, tienes el mapa.

---

## 5. Leer el mapa

Pincha en una clase y verás su código coloreado: **verde** (ejecutada), **rojo** (zona ciega), **amarillo**
(decisión con ramas a medias — un `if`, un `switch`, un `Math.Min`). El amarillo es el oro: la información
que no se ve en ningún porcentaje. Y mira la columna de **ramas**, no la de líneas (M2.3).

En este repo la `CalculadoraDescuentos` está bien cubierta a propósito —incluido el tope BR-05— para que
veas un mapa sano. El capítulo del curso imagina que ese tope se quedó en amarillo (cobertura de condición
50%, 1 de 2) y que escribes el test que faltaba: el VIP con pedido grande que **llega justo al 15%**
(0,10 de volumen + 0,05 de VIP). Aquí ese test ya existe
(`CoberturaFalsoPositivoTests.CalcularTasaDescuento_VipPedidoGrande_AplicaElTope`); abre la clase en el
informe y comprueba que el `Math.Min` está en verde, no en amarillo.

---

## 6. Qué rojo importa y qué rojo es océano

No todo rojo hay que apresurarse a cubrirlo. Con el marco de M2.1 (riesgo por consecuencia): una rama roja
en la `CalculadoraDescuentos`, en la validación de un descuento o en una transición de estado del pedido —la
cocina del negocio— es la borrasca sobre la ciudad: se cubre. Una rama roja en un getter sin lógica, en un
`ToString()` de depuración o en un `catch` defensivo imposible —la borrasca sobre el océano vacío— puedes
dejarla roja y dormir igual. El mapa te enseña dónde está el rojo; tu criterio decide cuál es ciudad.

---

## 7. Verde ≠ probado

`CoberturaFalsoPositivoTests` lo deja a la vista: un test que **llama** al método pero **no comprueba** el
resultado pinta la línea de verde igual. El informe te dice dónde pusiste cámara, no que esté enfocando algo.
Cómo cazar ese verde ciego —el mutation testing— y los tests inestables es M7.2.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M7.1-leer-el-mapa.md`](material/labs/M7.1-leer-el-mapa.md)) no te pide subir
el porcentaje: te pide la **lista razonada** de qué rojo cubrir y qué rojo dejar, con el marco de M2.1.
Porque el porcentaje lo sube cualquiera; leerlo con criterio es lo que cuesta y lo que de verdad vale.
