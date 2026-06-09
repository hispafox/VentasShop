# Manual del alumno — M7.2 · Falsos positivos y tests frágiles

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué un test verde puede
no proteger nada, y cómo lo cazas.

Tiempo de lectura: ~12 min. Submódulo M7.2.

---

## 1. La idea en una frase

Un test solo vale si su resultado significa algo. Hay dos formas de que el verde de un test no signifique
nada: el que pasa **siempre** (porque no comprueba lo que debería) y el que pasa **a veces** (porque depende
de algo que cambia por su cuenta). Verde que no protege y verde inestable.

---

## 2. El inspector que cuela una pieza defectuosa

Una línea de fábrica con su control de calidad. Te encanta un número: el 99,8% de las piezas pasa. Pero ese
número mide el trabajo de la línea, no la vista del inspector: uno que estampa «OK» sin mirar también daría
99,8%. ¿Cómo compruebas que vigila? Cuelas una pieza defectuosa a propósito y miras si la aparta. Y la clave:
cuando la pieza mala pasa, el problema no es la pieza, es el inspector. Eso es el **mutation testing**: metes
bugs deliberados en tu código y miras si tus tests (el inspector) se quejan.

---

## 3. Aserciones débiles: el test que pasa y no comprueba nada

Mira `AsercionesDebilesTests`. Tres tests sobre la `CalculadoraDescuentos` que pasan en verde y no sirven:

- `Assert.True(tasa >= 0)` — se cumple para 0%, 15% y 99%: deja pasar casi cualquier cálculo erróneo.
- `Assert.Null(ex)` (no lanza) — solo dice «no petó», no «calculó bien».
- `Assert.True(tasa <= 0.15m)` — se cumple también para 0%, 5%, 10%: no fija el valor.

La pieza que falta en los tres es la misma: ninguno afirma cuánto tiene que valer el descuento. El test bueno
es el aburrido: `Assert.Equal(0.15m, tasa)` (10% volumen + 5% VIP). La regla: **afirma el resultado concreto
que esperas, no una propiedad vaga que casi cualquier resultado cumpliría.**

---

## 4. ¿Quién vigila a los tests? Mutation testing con Stryker

La cobertura te dice qué código tocas; no si tus tests detectan un fallo. El **mutation testing** lo
comprueba: planta bugs deliberados (un `0.10m` por un `0.0m`, un `>=` por un `>`) y, por cada mutante, lanza
tu suite. Si algún test falla, el mutante está **matado**; si todos pasan, **sobrevive** (metiste un bug y
nadie se enteró). El **mutation score** es el porcentaje de mutantes matados: eso sí mide la calidad de tus
tests.

En .NET la herramienta es **Stryker.NET**. Como el proyecto va sobre MTP (sin VSTest), se ejecuta con
`--test-runner mtp` (en *preview* desde Stryker 4.13):

```bash
dotnet tool install -g dotnet-stryker
cd tests/VentasShop.TestsUnitarios
dotnet stryker --test-runner mtp
```

En esta rama el score es **87,5%** con **1 superviviente**: el mutante que las aserciones débiles no cazan.
Refuerzas la aserción (de `>= 0` a `Equal 0.15m`) y ese mutante muere. Stryker es **lento**: úsalo de vez en
cuando sobre la lógica crítica, no en cada commit.

---

## 5. Código muerto que la cobertura pinta de verde

El reverso del «rojo sano» de M7.1: una rama defensiva que no puede ser falsa nunca (un `if (descuento > 1m)`
donde el descuento ya viene topado al 15% antes). Tus tests pasan por la línea, así que cuenta como cubierta,
pero la rama de dentro es inalcanzable. Cuando lo detectes (y Stryker es bueno en eso), casi siempre lo
correcto es borrarlo, no escribirle un test.

---

## 6. Tests inestables (flaky): el verde que va y viene

Un test que pasa unas veces sí y otras no sin que toques una línea. Son veneno: como el cuento del pastor y
el lobo, cuando el equipo aprende que «ese falla a veces, dale a relanzar», deja de mirarlo — y el día que se
pone rojo por un bug real, nadie acude. La «cura» de relanzar hasta que pase solo esconde el problema.

La causa más común es **el tiempo**. `Cupon.EsValidoEn(ahora)` depende de la fecha; un test que le pasa
`DateTimeOffset.Now` pasa hoy y fallará tras la caducidad. La cura es fijar «ahora» con `RelojFijo` (IReloj,
M5.1) — ver `RelojTraidorTests`. Otras causas: estado compartido entre tests (cura: independencia, FIRST de
M1.3), concurrencia/async mal esperado, y dependencias externas reales en un unitario (cura: dobles, M5).

Matiz honesto: muy de vez en cuando un inestable destapa una *race condition* real en producción. Por eso se
investiga antes de tocar, no se borra de primeras.

---

## 7. Errores comunes

**Reintentar los inestables hasta que pasen** (esconde el problema). **Perseguir el 100% de mutation score**
(hay mutantes equivalentes que ningún test puede matar). **Confundir cobertura con mutation score** (miden
cosas distintas). Y **matar al mensajero**: borrar el test molesto o debilitar la aserción para que el mutante
deje de sobrevivir.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M7.2-inspector-ciego-y-reloj-traidor.md`](material/labs/M7.2-inspector-ciego-y-reloj-traidor.md))
tiene dos ejercicios: pasar Stryker y matar el mutante superviviente reforzando la aserción, y curar el flaky
del cupón pasándolo a `RelojFijo`. La tarjeta
([`material/tarjetas/M7.2-falsos-positivos.md`](material/tarjetas/M7.2-falsos-positivos.md)) lo resume. La
entrega no es un número: es una suite donde cada verde significa algo. En M7.3, la higiene que evita todo esto.
