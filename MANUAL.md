# Manual del alumno — M4.1 · Introducción a xUnit.net

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué se dedica un rato a
montar bien el proyecto de tests antes de escribir el primero, y por qué eso decide que de verdad ejecutes
la suite o no.

Tiempo de lectura: ~12 min. Submódulo M4.1 (Tests Unitarios con xUnit.net). Abre el Módulo 4 y aterriza el
stack cerrado del curso.

---

## 1. La idea en una frase

Montar bien el banco de trabajo —estructura correcta, paquetes correctos, ejecutar en un solo comando— es
lo que hace que ejecutar la suite sea un gesto de un segundo. Y un gesto de un segundo se repite cientos de
veces; un ritual de varios pasos, no.

---

## 2. El terreno cambió, y conviene saberlo

Usamos **xUnit**, el framework de tests más extendido en .NET y el que trae de fábrica la plantilla de
Microsoft. En concreto **xUnit v3**, la versión que se estabilizó en 2025 y la recomendada para proyectos
nuevos. El aviso importa: buena parte de los tutoriales que encuentres por ahí siguen siendo de la v2, y el
montaje del proyecto cambió. Lo que de verdad escribes —los `[Fact]`, las aserciones— es casi igual en las
dos versiones; lo que cambia es cómo se arma la casa.

Y el resto del stack también se movió. La librería de aserciones FluentAssertions pasó a ser de pago en su
versión 8, y la de mocking Moq tuvo un episodio de privacidad en 2023. En este curso trabajamos con el stack
vivo y gratuito a mayo de 2026 —xUnit v3, NSubstitute, AwesomeAssertions—, pero esas dos piezas no las
necesitas hasta el Módulo 5. Hoy te basta con xUnit y sus aserciones de toda la vida.

---

## 3. El banco de trabajo

Un buen carpintero, antes de empezar, monta su banco: cada herramienta en su sitio, la madera a mano, la luz
bien puesta. Diez minutos al principio para que luego, durante horas, coger la herramienta sea un gesto
instantáneo. Un banco mal montado se paga al revés: cada operación cuesta un poco más, y al cabo del día has
perdido una hora en fricción.

Montar el proyecto de tests es eso. Los diez minutos que inviertes al principio se te devuelven multiplicados,
porque vas a ejecutar la suite cientos de veces. Si ejecutar es un gesto de un segundo, lo haces constantemente
y te enteras de los fallos al instante. Si es un ritual, no lo haces, y entonces los tests no protegen nada
(vuelve FIRST, M1.3: un test que no se ejecuta no sirve).

---

## 4. Montar el proyecto

La convención en .NET es tener producción y tests en proyectos separados, dentro de la misma solución. En
VentasShop, el dominio en `src/VentasShop.Dominio` y los tests en `tests/VentasShop.TestsUnitarios`. Dos
motivos: los tests no se despliegan a producción, y la dependencia va en un solo sentido —el proyecto de test
conoce al de producción, nunca al revés—.

Para crearlo desde cero: instalas las plantillas de xUnit v3 (una vez por máquina), generas el proyecto con
`dotnet new xunit3` y añades la referencia al dominio. Tres comandos, banco montado. Los tienes paso a paso en
el laboratorio.

Si abres el `.csproj`, ves la novedad grande de la v3: `<OutputType>Exe</OutputType>`. En la v2 el proyecto de
test era una librería que cargaba un *runner* externo; en la v3 es un programa que se ejecuta solo. Eso le
permite apoyarse en la **Microsoft Testing Platform** —por eso el paquete es `xunit.v3.mtp-v2`, no la variante
clásica—, y es la base sobre la que montaremos la cobertura en el Módulo 7. Para tu día a día es transparente:
sigues lanzando `dotnet test`.

---

## 5. `[Fact]` y la clase `Assert`

Un test en xUnit es un método público marcado con `[Fact]` —un hecho que siempre debe ser cierto—. Mira
[`tests/.../PrimerasPruebasXunitTests.cs`](tests/VentasShop.TestsUnitarios/PrimerasPruebasXunitTests.cs):
reconoces todo de los módulos anteriores; lo nuevo es solo el `[Fact]` y el `Assert.Equal`.

La clase `Assert` es el corazón de xUnit, con un método por tipo de comprobación. No los memorices —el
autocompletado los ofrece—, pero conoce el repertorio, y sobre todo dos hábitos:

- **Usa la aserción más específica que encaje.** `Assert.Empty(lista)` cuando falla dice "esperaba vacía,
  tenía 3"; `Assert.True(lista.Count == 0)` solo dice "esperaba True". La específica describe mejor el fallo.
- **`Assert.Equal(esperado, real)`: esperado primero, real después.** Si los inviertes, el test funciona
  igual, pero al fallar el mensaje sale del revés y te hace buscar un bug donde no está. Esperado primero,
  siempre.

---

## 6. Ejecutar y leer el resultado

Dos formas, y usas las dos. Desde la terminal, `dotnet test` compila, descubre los `[Fact]`, los ejecuta y
resume; es lo que usa la integración continua (M7). Desde el editor, el **Test Explorer**: el árbol de tests,
ejecutar uno o todos, verdes y rojos de un vistazo, y un breakpoint dentro de un test para depurarlo paso a
paso. ¿Cada cuánto? Constantemente: como los unitarios son rápidos, los ejecutas tras cada cambio.

El momento que importa es cuando un test se pone rojo. La salida de xUnit te da el nombre (qué se rompió),
el mensaje (qué esperaba frente a qué obtuvo) y la traza (dónde). Léela de arriba abajo: el test no solo
grita que algo va mal, te lleva de la mano al sitio. En el laboratorio rompes el cálculo del tramo alto a
propósito (un `>=` por un `>`) y practicas leer ese fallo.

---

## 7. Lo que te llevas

El laboratorio
([`material/labs/M4.1-montar-proyecto-xunit.md`](material/labs/M4.1-montar-proyecto-xunit.md)) te hace montar
el proyecto de cero, escribir tus primeros `[Fact]` sobre la `CalculadoraDescuentos` y leer un fallo de
verdad. La tarjeta ([`material/tarjetas/M4.1-xunit.md`](material/tarjetas/M4.1-xunit.md)) lo resume en una
página.

Y enseguida notas algo: para cubrir bien la calculadora necesitas muchos tests casi iguales, y escribirlos
como `[Fact]` separados —copiar y pegar cambiando dos números— es justo el tedio que detectamos en M2.2.
Compara `PrimerasPruebasXunitTests.cs` (los tres tramos, un `[Fact]` por caso) con
`CalculadoraDescuentosTests.cs` (los mismos casos en `[Theory]`): es el mismo trabajo sin la repetición. Esa
es la puerta a los tests parametrizados, y es lo siguiente: M4.2.
