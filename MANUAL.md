# Manual del alumno — M4.2 · Tests parametrizados con `[Theory]`

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué quince tests casi
idénticos son una mala señal, y cómo un `[Theory]` los convierte en un test con muchos casos sin perder
—al revés, ganando— diagnóstico cuando algo falla.

Tiempo de lectura: ~12 min. Submódulo M4.2 (Tests Unitarios con xUnit.net).

---

## 1. La idea en una frase

Un mismo comportamiento con muchos datos no son muchos tests: es un test con muchos casos. Parametrizar es
separar la lógica del test (el molde) de los datos que lo ejercitan (las láminas).

---

## 2. El muro de copia-pega

Para cubrir bien la `CalculadoraDescuentos` hacen falta unos quince casos: los tramos de importe, los tipos
de cliente, los valores límite, el tope. Si los escribes como `[Fact]` separados, te queda un muro de
métodos idénticos salvo dos números. Huele mal: si el método cambia de firma tocas quince sitios, el
archivo se vuelve ilegible, y añadir un caso invita a olvidarse. El problema de fondo es que mezclas la
lógica (siempre igual) con los datos (lo único que cambia).

---

## 3. La prensa y las láminas

Una prensa de estampar se fabrica una vez, con cuidado. Por ella pasan láminas, una tras otra, y cada una
sale igual. Nadie fabrica una prensa por cada pieza. Un test parametrizado es eso: el método es la prensa
—la lógica escrita una sola vez—, y los datos son las láminas. Los quince `[Fact]` eran quince prensas
hechas a mano para estampar una pieza cada una.

---

## 4. `[Theory]` + `[InlineData]` y la ventaja del diagnóstico

`[Theory]` marca un test que se ejecuta varias veces; cada `[InlineData]` aporta un juego de datos. El
ejemplo limpio del repo es la invariante de `Cantidad` con enteros (mira
[`tests/.../CantidadTests.cs`](tests/VentasShop.TestsUnitarios/CantidadTests.cs)).

La ventaja que se subestima: **xUnit trata cada `[InlineData]` como un test independiente**. En el Test
Explorer ves una fila por caso. Si uno falla y los demás pasan, el reporte te dice cuál, con su valor. Un
`foreach` dentro de un `[Fact]` se pararía en el primer fallo y te ocultaría el resto. Es parametrización
con diagnóstico, no solo ahorro de líneas.

---

## 5. La limitación del `decimal` (el punto del curso)

`[InlineData]` solo admite constantes de compilación, y `decimal` **no** lo es: `[InlineData(0.10m, ...)]`
no compila. Y resulta que los importes de la `CalculadoraDescuentos` son `decimal`. Por eso, en este curso,
los importes van por las otras fuentes de datos, no por `[InlineData]`. Lo ves resuelto en
[`tests/.../CalculadoraDescuentosTests.cs`](tests/VentasShop.TestsUnitarios/CalculadoraDescuentosTests.cs),
que usa `TheoryData<decimal, ...>` justo por esto.

---

## 6. Las cuatro fuentes de datos

Una sola idea —separar el molde de las láminas—, cuatro sitios donde guardar las láminas. Las tienes una al
lado de otra en
[`tests/.../ParametrizacionTests.cs`](tests/VentasShop.TestsUnitarios/ParametrizacionTests.cs):

- **`[InlineData]`** para constantes simples (enteros, enums).
- **`[MemberData]`** toma los casos de un método estático (ahí sí caben decimales y objetos). Usa
  `nameof(...)`, no la cadena: si renombras, el compilador te avisa.
- **`TheoryData<>`** es `[MemberData]` tipado: si te equivocas de tipo, no compila. La opción por defecto.
- **`[ClassData]`** pone los datos en su propia clase (reutilizable entre clases de test, o para casos
  complejos que merecen su archivo).

La regla por defecto: constantes simples, `[InlineData]`; todo lo demás, `TheoryData<>` tipado.

---

## 7. El criterio: cuándo parametrizar y cuándo no

La parametrización es tan cómoda que se abusa de ella. La regla fina: parametriza cuando es el mismo
comportamiento con datos distintos, y separa cuando son comportamientos distintos. El olfato, si dudas:
¿solo cambian los valores, o cambia lo que el test verifica?

El antipatrón clásico es un `[Theory]` con un parámetro `bool debeLanzar` y un `if` que, según el caso,
comprueba un cálculo o una excepción. Eso mete lógica en el test (M3.1) y junta dos comportamientos en un
método ilegible. Si aparece un `if` para tratar unos casos distinto de otros, tienes dos tests peleándose
por un cuerpo. Sepáralos.

---

## 8. Lo que te llevas

El laboratorio
([`material/labs/M4.2-parametrizar-descuento.md`](material/labs/M4.2-parametrizar-descuento.md)) te hace
convertir los `[Fact]` del descuento en un `[Theory]`, eligiendo la fuente de datos adecuada. La tarjeta
([`material/tarjetas/M4.2-theory.md`](material/tarjetas/M4.2-theory.md)) lo resume.

Y queda algo pendiente que abre el siguiente submódulo: entre los casos del descuento hay algunos que no
devuelven un número, sino que *lanzan una excepción* —un importe negativo, una cantidad de cero—. Esos no
se comprueban con `Assert.Equal`, y mezclarlos en el `[Theory]` con un `bool` es justo el antipatrón que
acabas de ver. Cómo se testea que un código lanza la excepción correcta es M4.3, y cierra el módulo.
