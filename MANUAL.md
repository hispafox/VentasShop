# Manual del alumno — M5.1 · Test doubles (Dummy, Stub, Spy, Mock y Fake)

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué necesitas sustituir
las dependencias de tu código para poder testearlo, qué tipos de sustituto existen y cómo se eligen con
criterio. Abre el Módulo 5.

Tiempo de lectura: ~12 min. Submódulo M5.1 (Mocking, Aislamiento y Aserciones Fluidas). Es **conceptual**:
la sintaxis con NSubstitute es M5.2.

---

## 1. La idea en una frase

Un test double es un sustituto controlado de una dependencia, para que puedas probar tu código aislado,
rápido y sin efectos reales. Como el especialista de cine que rueda la escena peligrosa: ocupa el sitio
del actor, hace lo que la escena necesita, y nadie se quema.

---

## 2. El problema: código que depende de otros

Hasta ahora testeabas lógica pura —`CalculadoraDescuentos`, `Cantidad`, las transiciones del pedido—: le
das una entrada, te da una salida, no toca el mundo. El `ServicioPedidos` no es así. Para pagar un pedido
necesita un repositorio (base de datos), una pasarela (que *cobra dinero de verdad*), un reloj (la hora del
sistema) y un registro de auditoría. Testearlo tal cual sería lento, frágil, peligroso y no determinista.
Y aun así tiene lógica que importa: el orden de las operaciones, que un rechazo de la pasarela no marque el
pedido como pagado (BR-09), que se registre la auditoría (BR-10). Esa lógica hay que probarla.

---

## 3. Los dobles de cine

"Test double" se llama así por los *stunt doubles*. Cuando hay que saltar de un coche en llamas, no se
quema al actor: viene un especialista que lo sustituye en ese plano. Tus dependencias en un test son lo
mismo: traes un doble que ocupa el sitio de la pasarela, hace lo que la escena necesita ("el pago fue bien"
o "el pago falló"), y te deja rodar la escena rápido, sin riesgo y bajo control.

---

## 4. Por qué necesitas dobles (cuatro motivos)

- **Aislamiento** — si el test del `ServicioPedidos` falla, quieres saber que el bug es tuyo, no de la base
  de datos. Sustituyendo las dependencias, aíslas el SUT.
- **Velocidad** — una base de datos o una red tardan; multiplica por mil tests y la suite pasa de segundos
  a minutos. Un doble responde en microsegundos.
- **Determinismo** — el reloj real da una hora distinta cada vez; eso vuelve el test *flaky*. Un doble da
  siempre la misma respuesta controlada.
- **Evitar efectos reales** — la pasarela *cobra*. No quieres que tu suite, al ejecutarse mil veces al día,
  dispare mil cobros. El doble simula el efecto sin provocarlo. Este es el coche en llamas.

---

## 5. La taxonomía: cinco tipos

De menos a más sofisticado, cada uno con su papel en el rodaje:

- **Dummy** — relleno de un parámetro obligatorio que la ruta no usa (el extra de cartón al fondo).
- **Stub** — devuelve valores prefijados; no verificas nada sobre él (el doble que recita su línea). El
  `RelojFijo` que siempre da "1 de enero, 12:00".
- **Spy** — hace su papel y, además, anota cómo se le llamó, para mirarlo *después* (el que toma notas). El
  `LoggerEspia` que captura los apuntes de auditoría.
- **Mock** — configurado con expectativas; verificar la llamada *es* el objetivo del test (el especialista
  con guion estricto). La pasarela sobre la que compruebas "se cobró el importe exacto, una vez".
- **Fake** — implementación de verdad pero reducida (el que de verdad hace el salto). El
  `RepositorioPedidosEnMemoria`: guarda y recupera con un diccionario en RAM, sin base de datos.

La distinción que más cuesta es **stub vs. mock**, y es conceptual, no de sintaxis.

---

## 6. Verificación de estado vs. de interacción

- **Estado** — compruebas el *resultado*: `Assert.Equal(EstadoPedido.Pagado, pedido.Estado)`. Es lo que
  hemos hecho todo el curso. El stub solo sirve de apoyo para llegar a ese estado.
- **Interacción** — compruebas que el SUT *llamó* a una dependencia de cierta forma: "¿se intentó cobrar?",
  "¿se guardó?", "¿se registró el log?". Aquí no hay estado propio que mirar, así que verificas la llamada.

**Regla:** prefiere estado siempre que puedas; usa interacción solo cuando el comportamiento que importa es
la llamada en sí. El estado no se acopla a *cómo* el código consigue el resultado, solo a *qué* resultado
consigue (M2.1: testea comportamiento, no implementación). Abusar de la interacción acopla el test a la
implementación y lo rompe en cada refactor.

---

## 7. La regla de oro y el reparto en VentasShop

**Usa el doble más simple que satisfaga el test.** Stub si solo necesitas un valor; dummy si solo rellenas
un hueco; mock solo cuando la interacción es lo que pruebas. Cada nivel de más es acoplamiento.

En el `ServicioPedidos`, cada dependencia ejemplifica un tipo: el `IReloj` es **stub**, la `IPasarelaPago`
**mock**, el `ILogger` **spy**, y el `IRepositorioPedidos` es **stub**, **mock** o **fake** según el test.
Es la misma clase: el tipo de doble lo decide el test, no la dependencia.

---

## 8. Hechos a mano (lo que NSubstitute te ahorra)

En este repo los cuatro dobles están escritos **a mano**, sin librería, en
[`tests/.../Dobles/`](tests/VentasShop.TestsUnitarios/Dobles/). Mira
[`tests/.../DoblesArtesanalesTests.cs`](tests/VentasShop.TestsUnitarios/DoblesArtesanalesTests.cs): el
`ServicioPedidos` testeado con esos dobles y `Assert` nativo. Un fake o un stub no necesitan librería; se
escriben una vez y se reutilizan, como los Builders de M3.3. Lo verás: montar cada doble a mano es trabajo.
**Eso** es lo que NSubstitute reduce a dos líneas en M5.2 — y por eso primero lo ves por dentro.

---

## 9. Lo que te llevas

El laboratorio ([`material/labs/M5.1-elegir-doble.md`](material/labs/M5.1-elegir-doble.md)) es **de
criterio**: para tres escenarios del `ServicioPedidos`, decides el doble de cada dependencia y si verificas
estado o interacción. La tarjeta ([`material/tarjetas/M5.1-test-doubles.md`](material/tarjetas/M5.1-test-doubles.md))
lo resume.

Con el criterio claro, M5.2 baja a la sintaxis: cómo se crean estos dobles con NSubstitute
(`Substitute.For<>`, `Received()`) y cómo el montaje de hoy se queda en dos líneas. Las aserciones fluidas
de AwesomeAssertions llegan en M5.3.
