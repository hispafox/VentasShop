# Manual del alumno — M3.3 · Fixtures, Builders y Object Mother

Esto **no** es el [`README.md`](README.md). Este manual te cuenta el *porqué*: por qué la preparación de
datos ahoga los tests, y cómo el atrezzo de test devuelve el foco a lo que de verdad se comprueba.

Tiempo de lectura: ~12 min. Submódulo M3.3 (Patrón AAA y Estructura de Tests). Cierra el Módulo 3 y la
base conceptual del curso.

---

## 1. La idea en una frase

Si el 80% de un test es preparar datos, no estás probando: estás amueblando. Los patrones de construcción
(Object Mother, Builder) devuelven el Arrange a lo mínimo relevante.

---

## 2. El problema: cinco líneas de mobiliario para una de prueba

Sin atrezzo, dejar un pedido confirmado de verdad se ve así:

```csharp
var cliente = new Cliente { Nombre = "Ana", Tipo = TipoCliente.Estandar };
var producto = new Producto { Nombre = "Teclado", PrecioUnitario = new Dinero(50m, "EUR"), UnidadesStock = 100 };
var pedido = new Pedido(cliente);
pedido.AgregarLinea(producto, new Cantidad(2));
pedido.Confirmar();
```

Cinco líneas de montaje para una de prueba. Y lo peor no es la longitud: ese bloque lo copias en cada
test que necesite un pedido. El día que cambie el constructor de `Pedido`, tocas veinte tests.

---

## 3. El atrezzo del teatro

En una obra de teatro nadie funde una copa de cristal de verdad cada noche: usan **atrezzo**, utilería
que parece lo que tiene que parecer para lo que la escena necesita. La copa no lleva vino de verdad; se
ve como una copa con vino desde la butaca.

Tus datos de test son atrezzo. No necesitas un `Cliente` "real"; necesitas uno que sirva para *esa*
escena. La regla de fondo: **en el Arrange, lo relevante se ve; el andamiaje se esconde.** Si pruebas el
descuento de un VIP, el "VIP" a la vista; que se llame Ana, no.

---

## 4. Object Mother: utilería lista

Una clase con métodos estáticos que devuelven arquetipos ya montados.

```csharp
public static class ClienteMother
{
    public static Cliente Estandar() => new() { Nombre = "Cliente Estandar", Tipo = TipoCliente.Estandar };
    public static Cliente Vip()      => new() { Nombre = "Cliente VIP",      Tipo = TipoCliente.Vip };
}
```

En el test, el ruido desaparece: `var cliente = ClienteMother.Vip();`. Una línea que se lee, y un único
sitio que tocar cuando cambie el constructor. Se queda corto con la combinatoria: si acabas escribiendo
`VipFrancesBloqueadoSinPedidos()`, es la señal de pasar al Builder.

---

## 5. Test Data Builder: el taller a medida

En vez de un método por combinación, un objeto que encadena decisiones y construye al llamar `Build()`.
Mira `tests/.../Builders/PedidoBuilder.cs`: cada método devuelve `this` (por eso encadena) y rellena con
valores por defecto razonables lo que no especifiques.

```csharp
var pedido = new PedidoBuilder().ParaVip().ConLinea(precio: 300m, cantidad: 2).Pagado().Build();
```

Se lee como una frase: "un pedido de un VIP, con una línea de 300 € por 2 unidades, ya pagado". Cada
test pone a la vista solo lo suyo: el del descuento VIP escribe `.ParaVip()` y nada más; el de pagar sin
líneas, `.SinLineas()`. Y muy a menudo conviven: un Object Mother que por dentro usa un Builder.

---

## 6. Setup, TearDown y la independencia

xUnit no usa atributos especiales: el **constructor de la clase de test hace de setup** (antes de cada
test) y, si implementas **`IDisposable`**, su `Dispose` hace de teardown (después).

El detalle clave: **xUnit crea una instancia nueva de la clase de test por cada test**. No hay estado que
se arrastre de un test a otro. Es una decisión de diseño deliberada que te da la independencia de FIRST
(M1.3) por defecto: cada test arranca de cero, como cada medición del experimento con material limpio.

---

## 7. El peligro del estado compartido

Cuando algo es caro de crear (una conexión a base de datos), xUnit ofrece `IClassFixture<T>` para
compartirlo entre los tests de una clase. Es legítimo, pero abre una puerta peligrosa: si ese recurso
tiene estado mutable y un test lo modifica, el siguiente lo hereda sucio, y vuelves a romper la
independencia de M1.3 — tests que fallan según el orden, bugs que no existen, tardes perdidas.

La regla: **comparte lo caro que no cambia** (una conexión, una configuración); **nunca estado que los
tests modifican.** Lo barato, créalo por test. Esto se vuelve protagonista en el Módulo 6, con bases de
datos de integración de verdad; aquí queda sembrado.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M3.3-object-mother-builder.md`](material/labs/M3.3-object-mother-builder.md))
te hace construir el `PedidoBuilder`/`ClienteMother`/`PedidoMother` de VentasShop y reescribir un Arrange
enredado con ellos. La tarjeta ([`material/tarjetas/M3.3-builders.md`](material/tarjetas/M3.3-builders.md))
lo resume. Ese atrezzo se queda como plantilla y lo usaremos sin re-explicarlo en M4, M5 y M6.

Con esto cierras el Módulo 3 y toda la base conceptual: sabes *por qué* testear (M1), *qué* y cómo medirlo
sin engañarte (M2), y cómo escribir un test que se lee y se mantiene —AAA, buen nombre, datos limpios—
(M3). Lo que falta es dominar la herramienta: el Módulo 4 entra en xUnit a fondo, ya con la decisión de
stack aterrizada.
