# Manual del alumno — M3.1 · El patrón Arrange-Act-Assert

Esto **no** es el [`README.md`](README.md). Este manual te cuenta el *porqué*: por qué un test se tiene
que leer de un vistazo, qué estructura lo consigue, y cómo reconocer cuándo la estás rompiendo.

Tiempo de lectura: ~12 min. Submódulo M3.1 (Patrón AAA y Estructura de Tests). **Aquí no añades
comportamiento al SUT**: aprendes a *escribir* los tests para que se lean y se mantengan. Abre el Módulo 3.

---

## 1. La idea en una frase

Un test se lee en tres golpes de vista —**preparar, actuar, comprobar**—. Si no se lee así, el problema
está en el test, no en tu vista.

---

## 2. El gancho: el test de hace seis meses

Coge un test que escribiste hace medio año, o mejor, uno de un compañero que ya no está. Intenta
entender en diez segundos qué comprueba. Si lo consigues, está bien estructurado. Si te toca leerlo tres
veces y seguir variables arriba y abajo, no es un problema de comprensión: es un test mal escrito.

Y esto importa porque un test no es solo código que se ejecuta. Es código que se **lee**, y se lee mucho
más de lo que se escribe: cuando falla en el pipeline, cuando cambias una regla y buscas qué tocar,
cuando entra alguien nuevo. Un test ilegible es deuda que pagas una y otra vez.

---

## 3. Un test es un experimento

Un test es un experimento científico en miniatura, y los experimentos llevan tres siglos con la misma
estructura porque funciona. El científico **prepara las condiciones** (un estado conocido), **aplica una
sola variable** (si cambia tres cosas a la vez, no sabe cuál causó el efecto) y **mide el resultado**
contra lo que predecía. Preparar, aplicar, medir. Eso es Arrange-Act-Assert con bata de laboratorio.

Guárdate la imagen: cuando un test sea confuso, casi siempre será porque viola uno de los tres.

---

## 4. Las tres fases

Todo test hace por dentro las mismas tres cosas, en el mismo orden. Sepáralas visualmente y el test se
vuelve transparente.

- **Arrange** (preparar): montas el escenario, los objetos, los datos de entrada. El *"dadas estas condiciones"*.
- **Act** (actuar): ejecutas la acción que pruebas. Una sola. El *"cuando ocurre esto"*.
- **Assert** (comprobar): verificas el resultado esperado. El *"entonces debería pasar esto"*.

Mira el contraste. Un test apelmazado, todo en una línea:

```csharp
[Fact]
public void TestDescuento()
{
    var calc = new CalculadoraDescuentos();
    Assert.Equal(0.10m, calc.CalcularTasaDescuento(500m, TipoCliente.Estandar));
}
```

Y el mismo en AAA: arriba qué tienes, en medio qué haces, abajo qué esperas.

```csharp
[Fact]
public void CalcularTasaDescuento_PedidoDe500ClienteEstandar_Aplica10Porciento()
{
    // Arrange
    var calculadora = new CalculadoraDescuentos();
    decimal totalPedido = 500m;
    var tipo = TipoCliente.Estandar;

    // Act
    decimal tasa = calculadora.CalcularTasaDescuento(totalPedido, tipo);

    // Assert
    Assert.Equal(0.10m, tasa);
}
```

No hay que reconstruir nada: la estructura te lleva de la mano. Y eso se multiplica por cada test de la
suite y por cada vez que alguien los lee.

---

## 5. El SUT: a quién estás probando

El **SUT** —*System Under Test*— es la cosa que estás probando en ese test. En el ejemplo de arriba es
la `CalculadoraDescuentos`; el importe y el tipo de cliente son datos de apoyo.

Tener claro quién es el SUT te ordena el test. El Act debería ser una llamada al SUT, una sola. Si en el
Act llamas a tres objetos distintos, párate: o no tienes claro qué pruebas, o estás probando demasiadas
cosas a la vez. La pregunta que desenreda un test difícil es "¿quién es aquí el SUT?".

---

## 6. Por qué funciona tan bien

No es cosmética. Te da un **molde mental compartido**: cuando todos los tests tienen la misma forma, tu
cerebro deja de descifrar la estructura y va al contenido. En una suite de mil tests, esa uniformidad es
la diferencia entre una base mantenible y una que da pereza abrir.

Y es un **diagnóstico**: si te sale un Act con tres acciones, o dos bloques de Assert separados por más
acciones, el patrón te avisa de que ese test quiere ser dos. Cuando la forma AAA se te rompe, no la
fuerces: parte el test.

---

## 7. "Un assert por test", con su matiz

La regla famosa no significa una sola línea `Assert`: significa un solo **concepto** comprobado por
test, una sola razón de fallo.

```csharp
[Fact]
public void CalcularTotal_PedidoConDosUnidadesDe50_Da100Euros()
{
    // Arrange
    var pedido = new Pedido(new Cliente { Nombre = "Ana" });
    pedido.AgregarLinea(
        new Producto { Nombre = "Teclado", PrecioUnitario = new Dinero(50m, "EUR") },
        new Cantidad(2));

    // Act
    Dinero total = pedido.CalcularTotal();

    // Assert — un concepto, dos comprobaciones que lo forman
    Assert.Equal(100m, total.Importe);
    Assert.Equal("EUR", total.Moneda);
}
```

Esos dos asserts comprueban facetas de un mismo concepto ("el total salió bien"): perfecto. Lo prohibido
es meter comprobaciones de cosas distintas en un test —que pagar funcione Y que enviar funcione—, porque
entonces, cuando falle, no sabrás cuál se rompió.

---

## 8. Los cuatro errores típicos

- **Varios Act en un test:** el reporte no te dice cuál falló, y si el primero revienta los demás ni se
  ejecutan. Un comportamiento, un test.
- **Fases mezcladas:** asserts en medio del arrange, o seguir preparando después de actuar. Sepáralas.
- **Lógica en el test:** un `if` o un cálculo le dan al test su propia complejidad (¿quién testea el
  test?). La variante traicionera es calcular el esperado con la *misma fórmula* del código: no prueba
  nada. El valor esperado se escribe a mano, literal (`0.10m`).
- **El Arrange gigante:** no rompe AAA, pero ahoga el test. Solución: Builders y Object Mother (M3.3).

Si un test tiene alguno de estos cuatro, no está terminado, por mucho que pase en verde.

---

## 9. En este repo

- `tests/VentasShop.TestsUnitarios/EstructuraAaaTests.cs` — el AAA canónico (con los comentarios de las
  tres fases) y el caso de "un concepto, dos asserts" (`CalcularTotal`).
- `tests/VentasShop.TestsUnitarios/PedidoEstadosTests.cs` — el ciclo de un pedido ya partido en tests
  AAA, uno por transición, en vez de un `TestPedido` de cuatro-en-uno. Es el modelo del refactor del lab.

`Assert` nativo de xUnit: las aserciones fluidas y el mocking entran en M5.

---

## 10. Lo que te llevas

El laboratorio ([`material/labs/M3.1-refactor-a-aaa.md`](material/labs/M3.1-refactor-a-aaa.md)) te da
tests mal estructurados —el `TestPedido` de cuatro-en-uno y uno con la fórmula en el Assert— para que
los refactorices a AAA. La tarjeta ([`material/tarjetas/M3.1-aaa.md`](material/tarjetas/M3.1-aaa.md)) lo
resume para el día a día.

Con AAA ya tienes el esqueleto de cualquier test. Habrás notado dos cosas pendientes: por qué el nombre
del test es tan largo y de dónde sale ese Builder que limpia el Arrange. Son los dos siguientes
submódulos: el **nombrado** en M3.2 y la **construcción de datos** en M3.3.
