# Manual del alumno — M3.2 · Convenciones de nombrado de tests

Esto **no** es el [`README.md`](README.md). Este manual te cuenta el *porqué*: por qué el nombre de un
test importa tanto como su código, qué formato lo convierte en documentación, y cómo reconocer un mal
nombre.

Tiempo de lectura: ~11 min. Submódulo M3.2 (Patrón AAA y Estructura de Tests). **Aquí no añades
comportamiento al SUT**: aprendes a *nombrar* los tests para que, al fallar, te digan qué se rompió.

---

## 1. La idea en una frase

El nombre de un test es su documentación: si al fallar no te dice qué se rompió sin abrir el código,
está mal puesto.

---

## 2. El gancho: el pipeline en rojo

Lunes por la mañana, la build en rojo. Despliegas el panel de tests y ves `Test1`, `TestDescuento2`,
`Prueba_final_OK`. ¿Qué se ha roto? Ni idea: toca abrir cada test, leerlo entero y reconstruir qué
probaba. Has convertido un diagnóstico de tres segundos en una arqueología de veinte minutos.

En un test, el nombre es tan importante como el código. Quizá más: el código lo lees cuando vas a
tocarlo; el nombre, cada vez que la suite se ejecuta. Es la cara pública del test.

---

## 3. Un buen nombre es un titular

Piensa en los titulares de un periódico. "Sube el precio de la luz un 12% en marzo por el frío" te
cuenta la noticia sin leer el artículo. "Novedades en el sector energético" te obliga a leerlo todo.

Los nombres de tus tests son los titulares de tu suite. Cuando el pipeline está en rojo, lees los
titulares: si cuentan la noticia, sabes qué se rompió sin abrir un archivo. La pregunta que juzga
cualquier nombre: ¿me cuenta la noticia, o me obliga a leer el artículo?

---

## 4. El formato que sí funciona

La convención más extendida en .NET tiene tres partes separadas por guion bajo:

```
Metodo_Escenario_ResultadoEsperado
```

Qué método pruebas, bajo qué escenario, y qué debería pasar. Son las tres fases de AAA convertidas en
nombre: el escenario es el Arrange, el método es el Act, el resultado esperado es el Assert. Puesto en
práctica, se leen como frases:

```csharp
CalcularTasaDescuento_PedidoSupera500_Aplica10Porciento
Pagar_PedidoSinLineas_LanzaPedidoSinLineas
Enviar_PedidoSinPagar_LanzaTransicionInvalida
```

Vuelve ahora al panel del pipeline, pero con estos nombres: el diagnóstico es instantáneo. Sabes qué
mirar antes de abrir un solo archivo.

Dos detalles. El idioma: el nombre arranca con el nombre real del método (`CalcularTasaDescuento`,
porque así se llama en el código) y el escenario y el resultado van en el idioma del equipo; aquí, en
castellano. La longitud: salen largos, y no pasa nada — se leen mucho más de lo que se teclean, y el IDE
autocompleta. Optimiza la claridad, nunca la longitud.

---

## 5. El test como documentación que no puede mentir

Cuando los nombres son frases que describen comportamientos, la lista de tests de una clase se convierte
en su especificación. Pero una especificación que, a diferencia del wiki o el Word de requisitos, no se
queda obsoleta en silencio: si alguien cambia el código para que un test deje de cumplir su nombre, el
test se pone rojo. Te avisa, en rojo, el día mismo que deja de ser cierto.

Haz la prueba: lee de corrido solo los nombres de tests de una clase bien testeada. Deberías obtener su
especificación en lenguaje natural ("calcula 10% si supera 500; nunca pasa del 15%; no se puede pagar
sin líneas"). Si suena a la spec, los nombres son buenos. Para quien entra nuevo al equipo, esa lectura
es la forma más rápida y fiable de entender el sistema.

---

## 6. Otras convenciones (y lo que importa)

`Metodo_Escenario_Resultado` es la apuesta más común en .NET, pero no la única. Está **Given-When-Then**
(del BDD): `Given_PedidoSinPagar_When_Enviar_Then_LanzaExcepcion`. Y la **frase con `Should`**, fluida en
inglés: `Should_Apply10Percent_When_OrderExceeds500`. Todas dan buenos titulares.

Importa menos cuál elijas que elegir una y ser consistente: mezclar formatos obliga a tu cerebro a
cambiar de idioma a cada test. Por encima del formato hay una prueba de fuego: al leer el nombre, sin
abrir el código, ¿sabes qué comprueba y qué significaría que fallara? Si sí, el nombre es bueno.

---

## 7. Los antipatrones de nombre

- **Vago** (`Test1`, `TestDescuento`): no dice escenario ni resultado; acabas en `TestDescuento2`,
  `TestDescuento3` y perdido.
- **Describe la implementación** (`CalcularTasaDescuento_UsaSwitch_DevuelveDecimal`): miente en cuanto
  cambias el `switch` sin cambiar el comportamiento. Nombra lo que hace, no cómo está hecho.
- **El que miente**: el nombre dice una cosa y el cuerpo prueba otra. Peor que no tener nombre, porque
  te da información falsa cuando confías en ella.
- **El que no se actualiza**: cambias qué prueba el test y dejas el nombre viejo. La documentación viva
  miente en verde, en silencio. Cuando cambias lo que un test prueba, el nombre cambia con él.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M3.2-renombrar-tests.md`](material/labs/M3.2-renombrar-tests.md)) te da
una batería de tests mal nombrados para renombrar al formato y leer la lista como especificación. La
tarjeta ([`material/tarjetas/M3.2-nombrado.md`](material/tarjetas/M3.2-nombrado.md)) lo resume. Los
tests reales del repo (`CalculadoraDescuentosTests.cs`, `PedidoEstadosTests.cs`, `EstructuraAaaTests.cs`)
ya siguen el formato: son tu modelo.

Con AAA dándote la estructura (M3.1) y un buen nombre dándole voz (este), tus tests ya se leen. Queda el
ingrediente que más código ahorra: construir los datos de prueba sin que el Arrange se coma el test
entero. Eso es M3.3, y cierra el módulo.
