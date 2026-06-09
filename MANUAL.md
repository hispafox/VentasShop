# Manual del alumno — M8.1 · Diagnóstico del proyecto

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué el primer trabajo en un
proyecto real no es escribir tests, sino diagnosticar y abrir el código para que se deje testear.

Tiempo de lectura: ~12 min. Submódulo M8.1 (abre el Módulo 8).

---

## 1. Se acabó la autoescuela

Siete módulos sobre VentasShop, que es un proyecto de laboratorio: limpio, en capas, con DI desde el primer
día. El proyecto de tu trabajo no se parece: sin tests tras años de vida, una clase de dos mil líneas, métodos
que crean por dentro su base de datos y leen la hora del sistema. Si abres el editor y empiezas a escribir
`[Fact]`, te estrellas: no se deja testear. Antes hay un trabajo de **criterio**: diagnosticar.

---

## 2. Urgencias y triaje

Eres el médico de guardia con quince pacientes. No los atiendes por orden de llegada ni empiezas por el que
más grita: haces **triaje**, una pasada que no cura, solo clasifica (crítico / aguanta / puede esperar). Y al
paciente agitado, antes de tratarlo, le pones una vía para *poder* trabajar con él. Diagnosticar un proyecto
es igual: triaje primero, vía (refactor mínimo) después, tratamiento (el test) al final.

---

## 3. El triaje: qué es crítico

La regla es la de M2.1: riesgo por consecuencia. *Si esto se rompe, ¿qué pasa?* y *¿cada cuánto se toca?* El
cálculo de facturación que mueve dinero y cambia cada sprint es la cocina con la chimenea encendida; el
importador que se tocó hace tres años puede esperar. Una señal gratis: el **historial de Git**. Un archivo que
cambia cada semana es donde se meten bugs cada semana. Empieza por la cocina, no por el recibidor.

---

## 4. Por qué un código no se deja testear

Mira el legacy `NotificadorPedidos` (neutro, inventado). Tiene cuatro **clavos** que impiden el test:

```csharp
var repo = new RepositorioPedidosSql("Server=prod-db;...");   // 1. acceso a datos clavado a producción
...
if (DateTime.Now.Hour < 22) { ... }                          // 2. depende del reloj del sistema
    var smtp = new SmtpClient("smtp.empresa.local");
    smtp.Send(...);                                            // 3. manda un correo de verdad
Logger.Instance.Log(...);                                      // 4. singleton estático global
```

Cada clavo ata la clase a una pieza del mundo real, de forma que no puedes interponer un doble. Y un test
unitario es ejecutar tu lógica con las dependencias dobladas (M5). Sin sitio para el doble, no hay test.

---

## 5. Costura (seam)

Una **costura** (Feathers, *Working Effectively with Legacy Code*) es un sitio donde puedes cambiar el
comportamiento sin editar ese código, normalmente sustituyendo una dependencia. VentasShop está lleno de
costuras (cada interfaz inyectada). El legacy no tiene ninguna: todo soldado. Diagnosticar la testabilidad es
buscar costuras: si las hay, testeas hoy; si no, abres una.

---

## 6. La intervención mínima

El cambio más pequeño que abre una costura, sin más. Inyección de dependencias (M5) aplicada al revés: la
*introduces* en código que no la tenía. Las cuatro dependencias clavadas pasan a cuatro parámetros del
constructor (`IAccesoPedidos`, `IReloj`, `IEnviadorCorreo`, `ILogger<>`). La lógica de dentro es idéntica
línea por línea: **cambias la estructura, no el comportamiento**. Mira la versión refactorizada en
[`Legacy/NotificadorPedidos.cs`](tests/VentasShop.TestsUnitarios/Legacy/NotificadorPedidos.cs) y su test en
[`Legacy/NotificadorPedidosTests.cs`](tests/VentasShop.TestsUnitarios/Legacy/NotificadorPedidosTests.cs):
la clase que era intestable ahora se prueba con `RelojFijo`, un acceso falso, un enviador espía y `LoggerEspia`.

---

## 7. El huevo y la gallina

«Para refactorizar con seguridad necesito tests, pero el código no se deja testear hasta que lo refactorizo.»
Se rompe por dos sitios: los refactors que abren costuras (extraer interfaz, introducir parámetro) son
mecánicos y de bajísimo riesgo —tu IDE los hace—, así que te los permites antes de tener tests; y para cuando
el cambio da respeto, el **test de caracterización**: fija lo que el código *hace ahora* (no lo que debería),
como una foto que se pone roja si al refactorizar cambias el comportamiento sin querer.

---

## 8. Errores comunes

Empezar por donde no toca (la primera clase, no la crítica). Querer dejarlo bonito (reescribir la clase entera
cuando solo había que abrir una costura). Refactor sin red en código que da respeto (sin un test de
caracterización antes). Y diagnosticar para siempre (semanas de análisis sin un test). El triaje es rápido a
propósito.

---

## 9. Lo que te llevas

El laboratorio ([`material/labs/M8.1-diagnostico-legacy.md`](material/labs/M8.1-diagnostico-legacy.md)): un
legacy con sus clavos que diagnosticas (triaje + costuras) y abres con el refactor mínimo, sin tocar la
lógica. La entrega no es una suite de tests: es un proyecto que antes no se podía testear y ahora sí. En M8.2,
la estrategia por fases para meterle red a todo el sistema sin pararlo.
