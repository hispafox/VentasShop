# Manual del alumno — M7.3 · Buenas prácticas consolidadas

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué el código de test se
cuida como el de producción, y qué hábitos lo mantienen sano con los años.

Tiempo de lectura: ~12 min. Submódulo M7.3 (cierra el Módulo 7).

---

## 1. La idea en una frase

El test es código de producción que vas a **leer** muchas más veces de las que lo escribes. Una suite se
cuida como un jardín —se poda, se limpia, se mantiene legible— o se convierte en una jungla que nadie toca y
que acaba siendo decorado en vez de red de seguridad.

---

## 2. El jardín, no el monumento

Mucha gente trata su suite como un monumento: lo levanta una vez y lo da por terminado. Pero una suite se
parece a un jardín: si no vuelves, las malas hierbas (tests duplicados, obsoletos, inestables) crecen, y en
dos veranos es una jungla en la que nadie entra y a la que nadie añade nada. Cuidarla son gestos pequeños y
constantes: arrancar la mala hierba, podar para que entre la luz, y poner un seto (el gate de CI).

---

## 3. Un assert conceptual por test

Cada test comprueba **un solo comportamiento**, una sola razón para ponerse rojo. El anti-ejemplo es el test
que lo comprueba todo:

```csharp
[Fact]
public void Pagar_DePedidoValido_FuncionaTodo()   // ❌ cuatro cosas a la vez
{
    var pedido = new PedidoBuilder().ConLinea(50m, 2).Confirmado().Build();
    pedido.Pagar();
    Assert.Equal(EstadoPedido.Pagado, pedido.Estado);
    Assert.Equal(100m, pedido.CalcularTotal().Importe);
    Assert.Single(pedido.Lineas);
    Assert.Equal(TipoCliente.Estandar, pedido.Cliente.Tipo);
}
```

Dos problemas: el nombre `FuncionaTodo` no dice qué falló, y la primera aserción que falla **corta el test**
(si se rompe el estado, las otras tres ni se ejecutan). La cura: un test por comportamiento, con su nombre
que canta (ver `BuenasPracticasTests`). Matiz: es un assert **conceptual**, no una sola línea `Assert` —si un
comportamiento se comprueba mirando dos propiedades del mismo resultado, esas dos líneas van juntas. Cuentas
comportamientos, no líneas.

---

## 4. Independencia y determinismo

Cada test monta su propio mundo, no depende de ningún otro, y da el mismo resultado lo lances cuando lo
lances. La lista de «nunca»: nunca compartas estado mutable entre tests, nunca asumas un orden de ejecución,
nunca dependas del reloj/red/fichero. Es FIRST (M1.3) y la cura de los flaky (M7.2). El `PedidoBuilder` (M3.3)
es tu aliado: cada test se fabrica sus datos limpios.

---

## 5. Simplicidad: ni lógica en los tests

Nada de `if`/`for`/cálculos en el cuerpo. Un test con lógica puede tener bugs, y caes en «¿quién testea al
test?». Si quieres varios casos, `[Theory]` con `[InlineData]` (M4.2), no un `for`. Y al revés que en
producción: un número «mágico» con significado en un test es **bueno** —`Assert.Equal(0.15m, tasa)` se
entiende solo—, no lo escondas tras una constante calculada.

---

## 6. Refactor de tests: DAMP, no DRY

Si el test es código, se refactoriza. Pero con otra regla. En producción mandas por **DRY** (no te repitas);
aplicado a rajatabla en los tests, produce jerarquías de clases base y setups enormes donde, para entender un
solo test, saltas por cinco archivos. En los tests se prefiere **DAMP** (*Descriptive And Meaningful
Phrases*): un poco de duplicación se paga si cada test se entiende solo. Se quita el **ruido** (el montaje
farragoso → un `PedidoBuilder`), se conserva la **historia** (qué datos usa y qué afirma, a la vista). Y se
**borran sin culpa** los tests de funcionalidades muertas: Git se acuerda; la suite no es un museo.

---

## 7. Los tests como red de CI/CD

El seto del jardín: una suite solo cumple su misión si se ejecuta sola, en cada cambio, y bloquea lo que
rompe. Ese es el *gate* de integración continua. Por eso importaba que los tests fueran rápidos (un gate lento
se esquiva) y deterministas (uno que falla al azar se desactiva). Montar el pipeline es la fase F3, que es
M8.2; aquí basta con tener clara la meta.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M7.3-jungla-a-jardin.md`](material/labs/M7.3-jungla-a-jardin.md)) es
jardinería en directo: una suite descuidada de VentasShop que dejas como un jardín —separar por
comportamiento, `for`→`[Theory]`, romper la dependencia de orden, renombrar, borrar lo muerto—. La misma
protección con la mitad del ruido. La tarjeta
([`material/tarjetas/M7.3-buenas-practicas.md`](material/tarjetas/M7.3-buenas-practicas.md)) lo resume.

Con esto cierra el Módulo 7, que iba de una sola cosa: **no engañarte** —leer la cobertura sin falsa
seguridad (7.1), comprobar si tus tests verdes vigilan de verdad (7.2) y mantener la suite sana con los años
(7.3)—. Lo que queda es aplicarlo a un proyecto real: el Módulo 8.
