# Manual del alumno — M4.3 · Gestión de excepciones en tests

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué que tu código falle
bien es una funcionalidad como cualquier otra, y cómo se prueba un mecanismo de seguridad que solo se
activa cuando algo va mal.

Tiempo de lectura: ~12 min. Submódulo M4.3 (Tests Unitarios con xUnit.net). Cierra el Módulo 4.

---

## 1. La idea en una frase

Que el código falle bien —rechazar la operación, lanzar la excepción correcta, dejar el sistema coherente—
es una funcionalidad que diseñas a propósito. Y como toda funcionalidad, se testea.

---

## 2. No se puede con `Assert.Equal`

Si un método lanza una excepción, no hay valor de retorno que comparar: la excepción interrumpe la
ejecución antes de devolver nada. Y si salta sin que la esperes, el test falla por la razón equivocada,
como un error inesperado. Necesitas decirle a xUnit "espero que esto lance una excepción, y si NO la lanza,
*eso* es el fallo". Aquí el éxito es que algo explote.

---

## 3. Probar el airbag

¿Cómo se prueba el airbag de un coche? No esperando a tener un accidente: con un *crash test*, provocando
el choque en condiciones controladas para comprobar que el mecanismo salta. Las excepciones de dominio
—"no se puede pagar un pedido sin líneas"— son los airbags de tu código, y se prueban igual: le pasas la
entrada inválida a propósito y verificas que lanza la excepción correcta, con el mensaje correcto.

---

## 4. `Assert.Throws<T>`: provocar el choque

Le pasas el tipo de excepción esperado y un trozo de código; pasa si lanza *exactamente* esa excepción.
Mira [`tests/.../ExcepcionesTests.cs`](tests/VentasShop.TestsUnitarios/ExcepcionesTests.cs).

Dos detalles importan. **La acción va en una lambda *dentro* del `Throws`**, porque xUnit necesita
ejecutarla él mismo rodeada de un `try/catch` para cazar la excepción; si la llamas fuera, el choque ocurre
fuera de la pista. Y **exige el tipo exacto**: esperar la clase padre cuando se lanza la hija falla, lo que
te obliga a ser preciso. Para aceptar el tipo y sus subtipos existe `Assert.ThrowsAny<T>`, a conciencia.

---

## 5. Verificar el mensaje y las propiedades

`Assert.Throws<T>` **devuelve la excepción capturada**, así que la guardas y la interrogas: el `ParamName`
del parámetro inválido, un código de error, el mensaje. Con la invariante de `Cantidad`, compruebas que
`ParamName` es `"valor"` y que el mensaje contiene "mayor que cero".

El mensaje, siempre con `Assert.Contains` (un fragmento), nunca con `Assert.Equal` (el texto exacto): si
afirmas el texto entero, el día que alguien corrija una tilde tu test se rompe sin que haya bug, una falsa
alarma del airbag. Y prioriza las propiedades estructuradas (`ParamName`, un código) sobre el texto: las
propiedades son contrato estable, el mensaje es presentación que cambia.

---

## 6. Excepciones en código asíncrono

Si el método es `async` y lanza dentro de un `await`, la excepción viaja en un `Task` y `Assert.Throws` no
sirve: usas `Assert.ThrowsAsync<T>` y el test pasa a `async Task`. El dominio de VentasShop es síncrono, así
que el ejemplo del repo es una operación async **autocontenida** que ilustra la forma; el servicio real con
pasarela se monta en M5.

Y aquí está el error más caro del módulo: **el `await` olvidado**. Sin el `await` delante del `ThrowsAsync`,
el `Task` se descarta sin esperarlo, la aserción nunca se evalúa, y el test pasa siempre, aunque el código
no lance nada. Es un falso positivo silencioso: parece que tienes el camino de error cubierto y no tienes
nada. La regla: `await` siempre, `async Task` nunca `async void`, y ten activado el analizador (xUnit2021).
El laboratorio te hace vivirlo: quitas el `await`, ves el verde mentiroso, lo vuelves a poner, ves el rojo.

---

## 7. `Record.Exception` y qué excepciones testear

Para afirmar que un código **no** lanza nada, `Record.Exception` ejecuta el código y devuelve la excepción
o `null`. Sirve para comprobar que un pago válido no dispara ningún airbag.

Y un criterio que cierra el círculo con M2.1: solo se testean las excepciones **de dominio**, las que tu
código lanza a propósito porque representan una regla de negocio (`PedidoSinLineasException`,
`TransicionPedidoInvalidaException`, la `ArgumentException` de una invariante). Las que saltarían por un bug
interno —un `NullReferenceException` por no validar la entrada— no se testean: se arreglan.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M4.3-testear-excepciones.md`](material/labs/M4.3-testear-excepciones.md))
cubre los caminos de error de VentasShop con `Assert.Throws` y te hace vivir el susto del `await` olvidado.
La tarjeta ([`material/tarjetas/M4.3-excepciones.md`](material/tarjetas/M4.3-excepciones.md)) lo resume.

Con esto cierras el Módulo 4: sabes montar el proyecto, parametrizar y testear los caminos de error. Sobre
lógica pura ya eres autónomo. Lo que falta es testear código que depende de otros —el `ServicioPedidos` con
su repositorio y su pasarela—, y ese es el salto al Módulo 5: test doubles, mocking con NSubstitute y las
aserciones fluidas de AwesomeAssertions, las dos piezas del stack aparcadas desde M4.1.
