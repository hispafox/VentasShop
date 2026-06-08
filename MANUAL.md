# Manual del alumno — M2.3 · Métricas de cobertura de código

Esto **no** es el [`README.md`](README.md). Este manual te cuenta el *porqué*: qué mide de verdad la
cobertura, por qué un 100% puede fallar en producción, y cómo usarla sin que te engañe.

Tiempo de lectura: ~12 min. Submódulo M2.3 (Estrategia y Cobertura). **Aquí mides la cobertura de los
tests reales** que ya tienes en `tests/VentasShop.TestsUnitarios/`. Cierra el Módulo 2.

---

## 1. La idea en una frase

La cobertura mide qué líneas de tu código **ejecutan** los tests, no si están **bien probadas**: es un
**detector de huecos**, no un certificado de calidad.

---

## 2. El gancho: un 100% que falla en producción

Un equipo presume de tener un 100% de cobertura. Badge verde, reuniones, da gusto. Y un martes
cualquiera, en producción, el cálculo de un descuento se dispara y se factura mal a media cartera de
clientes. ¿El método que falló? Tenía un 100% de cobertura, claro.

¿Cómo es posible? Esa pregunta es todo el submódulo.

---

## 3. Cámaras que apuntan a la pared

Montas cámaras de seguridad en un edificio. La cobertura de tus cámaras es el porcentaje de superficie
que tienen a la vista: si enfocan el 80%, tienes un 80% de "cobertura de cámaras". Y saberlo es útil,
porque el 20% que ninguna mira es zona ciega.

Pero que una cámara apunte a una zona no significa que esa zona esté vigilada. Puede estar encendida,
contar para tu porcentaje y estar grabando una pared. Esa es la diferencia entre que una línea se
*ejecute* en un test y que esté *comprobada*. La cobertura cuenta cámaras que apuntan, no cámaras que
vigilan.

---

## 4. Qué es, exactamente, y para qué sirve

Es el porcentaje de tu código de producción que se ejecuta cuando lanzas los tests. La herramienta
instrumenta el código, ejecuta la suite y apunta qué líneas se pisan. Mil líneas, ochocientas pisadas:
80%; las otras doscientas son zonas ciegas.

Su valor real es ese: **detectar huecos**. Una zona crítica al 0% —la `CalculadoraDescuentos` sin un
solo test— es información de oro. Lo que la cobertura no te dice es si lo cubierto está *bien* cubierto.

---

## 5. Líneas, ramas y caminos

Cuando una herramienta te da "un 80%", la pregunta es: ¿de qué?

- **Cobertura de líneas:** qué porcentaje de líneas se ejecutó. La más simple y la más engañosa: una
  línea puede esconder una decisión. Un `if` con dos condiciones, probado con un solo caso que entra,
  da 100% de líneas y no ha probado casi nada.
- **Cobertura de ramas (*branch coverage*):** cuenta los caminos de cada decisión. Es la que debes
  mirar. Ese `if` tiene dos ramas; con un solo test estás al 50%, no al 100%. Si alguien cambia un
  `&&` por un `||`, la de ramas lo caza y la de líneas no.
- **Cobertura de caminos (*path coverage*):** todas las combinaciones de ramas. Exhaustiva e
  impracticable en código real (explota combinatoriamente). Resérvala para la lógica más crítica.

→ En el repo: la `CalculadoraDescuentos` tiene ramas de verdad (los `switch` de volumen y de tipo).
Genera la cobertura y mira el porcentaje **de ramas**, no solo el de líneas.

---

## 6. ¿Cuánto es suficiente? El 80%

El 80% es una referencia sana, no un objetivo sagrado. La cobertura tiene rendimientos decrecientes:
del 0% al 60% es barato y te quita los agujeros gordos; hasta el 80% sigue mereciendo la pena; y el
último empujón hasta el 100% es el peor negocio, porque acabas escribiendo tests artificiales para el
`catch` de una excepción que no puede ocurrir o para el getter más tonto.

Y no debe ser uniforme: la lógica crítica, cerca del 100%; el andamiaje, al 40% o sin medir. Un 80%
global puede ser excelente o un desastre según *dónde* esté ese 80%. Leer el *dónde* —clase por
clase— es el Módulo 7.

---

## 7. El falso positivo de cobertura

Aquí está la trampa que explica al equipo del 100%. Que una línea se ejecute no significa que se
compruebe nada: la cobertura mide ejecución, no verificación.

```csharp
[Fact]
public void CalcularTasaDescuento_NoRevienta()   // ❌ falso positivo
{
    var calculadora = new CalculadoraDescuentos();
    calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip);
    // ... y aquí no hay ninguna aserción
}
```

Ese test ejecuta el método entero, la herramienta lo da por cubierto, y no comprueba nada: pasa
siempre mientras no salte una excepción. La versión sutil es aún peor, porque parece seria: una
aserción débil como `Assert.True(tasa >= 0)` se cumple para 0%, 15% y 99%, y deja pasar cualquier bug.

Esto enlaza con FIRST (M1.3): un test que no termina en una aserción de verdad no se valida solo.
Cómo se cazan estos tests —y el *mutation testing*, que mide si tus aserciones valen algo— es el M7.2.

→ En el repo: `CoberturaFalsoPositivoTests.cs` tiene los dos anti-ejemplos (a propósito, en verde) y
el contraste con una aserción de verdad. Genera la cobertura con y sin ellos y míralo con tus ojos.

---

## 8. Medir la cobertura en este repo

La cobertura la da una extensión oficial y gratuita, `Microsoft.Testing.Extensions.CodeCoverage`, ya
añadida al proyecto de tests. Se recoge en la misma ejecución de la suite:

```bash
dotnet test tests/VentasShop.TestsUnitarios -- --coverage --coverage-output-format cobertura
```

El formato *cobertura* es el que trae la información de ramas. Te deja un `.cobertura.xml` en
`TestResults/`: un XML para máquinas, no para leer a ojo. Convertirlo en un informe HTML navegable
(ReportGenerator), mandarlo a SonarQube e *interpretarlo* es el Módulo 7. Aquí generas el dato; allí
aprenderás a leerlo sin engañarte.

> Si vienes del Coverlet de toda la vida: esa herramienta es del motor de testing clásico (VSTest) y
> no se engancha a la plataforma nueva. El concepto no cambia; solo el comando.

---

## 9. Lo que te llevas

El laboratorio ([`material/labs/M2.3-generar-cobertura.md`](material/labs/M2.3-generar-cobertura.md))
te hace generar la cobertura, leer el porcentaje de ramas y vivir el momento del falso positivo. La
tarjeta [`material/tarjetas/M2.3-cobertura.md`](material/tarjetas/M2.3-cobertura.md) lo resume para el
día a día.

Con esto cierras el Módulo 2: ya sabes qué testear (2.1), cómo elegir los casos (2.2) y cómo medir sin
dejarte engañar (2.3). Lo que viene es la mecánica fina de escribir tests que se lean y se mantengan,
empezando por el patrón AAA. Es el **Módulo 3**.
