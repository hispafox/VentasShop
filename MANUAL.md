# Manual del alumno — M5.2 · Mocking con NSubstitute

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: cómo se crean y se manejan
los dobles que decidiste en M5.1, y por qué NSubstitute reduce a dos gestos todo lo que parecía complicado.

Tiempo de lectura: ~12 min. Submódulo M5.2 (Mocking, Aislamiento y Aserciones Fluidas).

---

## 1. La idea en una frase

Con un doble solo haces dos cosas: le das el guion antes de actuar (qué debe responder) y revisas la toma
después (cómo se le llamó). En NSubstitute eso es `Returns` y `Received`. Lo demás son variantes.

---

## 2. De los dobles a mano (M5.1) a la librería

En M5.1 escribiste los dobles a mano para verlos por dentro: un `RelojFijo`, un repositorio en memoria,
una pasarela falsa. Funcionaba, pero era trabajo. NSubstitute hace ese montaje por ti: `Substitute.For<T>()`
te da un doble de cualquier interfaz en una línea. El mismo `ServicioPedidos`, ahora con menos andamiaje.

---

## 3. Los dos gestos del director

Piensa en un director de cine con su especialista. Antes de rodar le da el guion ("cuando te pidan cobrar,
di que falló"); después de rodar revisa la toma ("¿hiciste el salto?, ¿una vez?"). Configurar es `Returns`;
verificar es `Received`. Si tienes claros los dos, la librería entera encaja.

---

## 4. Crear el doble

`Substitute.For<IRepositorioPedidos>()` te da un objeto que implementa la interfaz y, de fábrica, no hace
nada: cada método devuelve el valor por defecto. Doblamos **interfaces**, no clases concretas — por eso las
dependencias del `ServicioPedidos` viven detrás de `IRepositorioPedidos`, `IPasarelaPago`, `IReloj`.

---

## 5. El guion: `Returns` (stub)

`repositorio.ObtenerPorId(idPedido).Returns(pedido)` le dice al método qué devolver. Cuando el argumento no
importa, usas `Arg.Any<Dinero>()`; cuando quieres una condición, `Arg.Is<Dinero>(m => m.Importe > 0)`. Para
el caso del rechazo (BR-09), configuras `pasarela.Cobrar(...).Returns(new ResultadoPago(false))`. Y si
necesitas que un método lance, está `.Throws(...)` (en `NSubstitute.ExceptionExtensions`).

---

## 6. Revisar la toma: `Received` (mock/spy)

`repositorio.Received(1).Guardar(pedido)` comprueba que se guardó una vez; `DidNotReceive()` comprueba que
**no** se llamó. Y con los matchers verificas el argumento:
`repositorio.Received(1).Guardar(Arg.Is<Pedido>(p => p.Estado == EstadoPedido.Pagado))` confirma que se
guardó el pedido *ya pagado*, no en cualquier estado. Todo esto va en el Assert, después del Act.

---

## 7. El test del rechazo y la regla del estado

Mira [`tests/.../MockingNSubstituteTests.cs`](tests/VentasShop.TestsUnitarios/MockingNSubstituteTests.cs).
En el caso feliz compruebas el **estado** (`pedido.Estado == Pagado`) y de paso la interacción. En el del
rechazo, como `Pagar` no devuelve nada, la prueba *tiene* que ser de interacción (`DidNotReceive`). Regla:
prefiere el estado siempre que puedas; reserva `Received` para cuando no hay estado que mirar.

---

## 8. Permisivo, no estricto

NSubstitute es permisivo: un método sin configurar devuelve el valor por defecto y no se queja. Eso tiene
una trampa (el error nº 3 de abajo): si tu código espera algo y recibe `null`, revienta dentro del SUT con
un `NullReferenceException` confuso. La ventaja es que no te obliga a configurar lo que no te importa, que
es lo que hacía frágiles a los tests estrictos de Moq. Si vienes de Moq, la tabla del README te traduce todo.

---

## 9. Errores comunes

Cuatro descuidos: **verificar de más** (un `Received` por cada llamada te acopla a la implementación),
**verificar interacción donde bastaba el estado**, **olvidar que es permisivo** (el `null` que revienta el
SUT) y, en async, **no configurar bien los métodos `Task`**.

---

## 10. Lo que te llevas

El laboratorio ([`material/labs/M5.2-mockear-servicio-pedidos.md`](material/labs/M5.2-mockear-servicio-pedidos.md))
te hace testear el `ServicioPedidos` en sus tres casos: feliz, rechazo y pedido sin líneas. La tarjeta
([`material/tarjetas/M5.2-nsubstitute.md`](material/tarjetas/M5.2-nsubstitute.md)) resume la sintaxis.

Con esto ya aíslas tu código y verificas cómo se comporta. Queda la pieza que hace que los tests se lean
mejor y que, al fallar, te digan más: las aserciones fluidas de AwesomeAssertions. Es M5.3.
