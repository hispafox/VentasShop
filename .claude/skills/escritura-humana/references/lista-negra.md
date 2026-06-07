# Lista Negra — Palabras y Expresiones Prohibidas

Si alguna de estas aparece en el texto, es señal inequívoca de que suena a IA.
Elimínalo y reescribe.

---

## 1. Conectores de IA → Reemplazos humanos

### Prohibidos
- «Sin embargo, es importante destacar que...»
- «Cabe mencionar que...»
- «En este sentido...»
- «Es fundamental señalar...»
- «Resulta imprescindible...»
- «Dicho esto...»
- «No obstante lo anterior...»

### Reemplazos (consulta `lista-blanca.md` para más opciones)
- «Ahora bien...»
- «Eso sí...»
- «Lo que pasa es que...»
- «Mira...»
- «Aquí viene lo interesante:»
- «Ojo:»
- «El tema es que...»
- «Claro que...»
- «Vamos, que...»

---

## 2. Adjetivos que gritan «IA»

### Prohibidos
Revolucionario, Innovador, Transformador, Disruptivo, Robusto, Integral, Holístico,
Sinérgico, Vanguardista, De última generación, Pionero, Paradigmático, Excepcional,
Extraordinario, Fascinante, Apasionante.

### Reemplazos
Interesante, Potente, Útil, Práctico, Sólido, Que funciona bien, Serio, Maduro,
Con recorrido, Que merece la pena, Que aguanta en producción, Decente, Razonable,
Curioso, Llamativo.

---

## 3. Verbos corporativos de IA

### Prohibidos
Impulsar, Fomentar, Potenciar, Optimizar, Apalancar, Catalizar, Orquestar,
Dinamizar, Sinergia (como verbo), Articular, Vertebrar, Materializar,
Empoderar, Habilitar, Maximizar, Escalar (fuera de contexto técnico).

### Reemplazos
Mejorar, Ayudar a, Hacer que funcione, Lograr, Conseguir, Mover, Empujar,
Arrancar, Montar, Sacar adelante, Tirar de, Poner en marcha, Hacer crecer,
Meter mano, Resolver.

---

## 4. Frases hechas — la huella dactilar de la IA

### Moldes binarios encadenados

Estos son fórmulas que se cuelan al estructurar argumentos en dos pasos. Cuando se repiten en el mismo documento, la cadencia se vuelve mecánica:

- «Por dos motivos. Primero,... Segundo,...»
- «Por dos cosas. La primera,... La segunda,...»
- «Por dos razones. Una,... Otra,...»
- «X no es Y — es Z.» / «X no es Y; es Z.» (cuando se repite ≥2 veces por documento)
- «Lo bueno: ... Lo malo: ...»
- «La parte fácil: ... La parte difícil: ...»

**Regla operativa:** una sola fórmula binaria por submódulo se tolera si tiene contenido real. Dos o más en el mismo documento = reescribir.

### Otras frases hechas tradicionales

NUNCA las uses:

- «En el panorama actual...»
- «En un mundo cada vez más...»
- «Es importante tener en cuenta...»
- «En última instancia...»
- «No es de extrañar que...»
- «Cabe destacar que...»
- «A lo largo y ancho de...»
- «En definitiva...»
- «No cabe duda de que...»
- «Resulta evidente que...»
- «Es menester señalar...»
- «Vale la pena resaltar...»
- «Esto nos lleva a...»
- «Como hemos visto...»
- «Es crucial entender que...»
- «Juega un papel fundamental...»
- «Marca un antes y un después...»
- «Abre un abanico de posibilidades...»
- «En el ámbito de...»
- «Desde una perspectiva...»
- «En el contexto de...»
- «Adentrémonos en...»
- «Sumerjámonos en...»
- «Exploremos juntos...»
- «Sin lugar a dudas...»
- «Indudablemente...»
- «Es innegable que...»
- «Constituye un pilar fundamental...»
- «Representa una oportunidad única...»
- «Ha llegado para quedarse...»
- «Está transformando la forma en que...»
- «A día de hoy...» (cuando no aporta nada)
- «En este orden de ideas...»
- «Huelga decir que...»
- «No podemos pasar por alto...»
- «Merece especial atención...»

---

## 5. Patrones de apertura prohibidos

Nunca empieces un texto, capítulo, sección o post con estas estructuras:

- «En el mundo del desarrollo de software...»
- «En la era de la inteligencia artificial...»
- «A medida que la tecnología avanza...»
- «En los últimos años hemos sido testigos de...»
- «¿Alguna vez te has preguntado...?» (retórica barata)
- «Imagina que...» (retórica barata en el 90% de los casos)
- «Vivimos en una época en la que...»
- «No es ningún secreto que...»
- «Hoy en día...» (cuando es puro relleno sin fecha concreta)

---

## 6. Patrones de cierre prohibidos

- «En conclusión...»
- «En resumen...»
- «Para recapitular...»
- «Como hemos visto a lo largo de este capítulo...»
- «Esperamos que este artículo te haya sido de utilidad...»
- «Con todo lo anterior en mente...»
- «Queda claro que...»
- «Solo el tiempo dirá...»

### Etiquetas-índice de cierre (recap-eco encubierto)

Más sutiles porque no son frases hechas obvias, pero delatan el patrón "anuncio del recap antes del recap". Cuando un bloque de cierre arranca con una de estas, el lector ya sabe que viene un índice y desconecta:

- «Tres ideas para llevarte de este submódulo.» / «Tres ideas para llevarte.»
- «Cuatro ideas para llevarte...» / «N ideas para llevarte...» (cualquier número)
- «Tres cosas a notar.» / «Dos cosas a notar.» (etiqueta-índice al final del bloque)
- «Tres cosas para recordar...» / «Tres cosas que importan...»
- «Lo importante de este submódulo cabe en tres puntos...»
- «Para llevarte de aquí...»

**Cómo arreglarlo:** borra la etiqueta y deja que los párrafos siguientes empiecen con la afirmación directa. Si el primer párrafo necesita engancharse, reescribe su arranque, no metas etiqueta-índice.

### Cápsulas-eslogan en cierre o al final de bloque

Las "frases-cápsula como entrega por defecto" (antipatrón 14) cuando se repiten o se usan como cierre rotundo. Estos hits son **literales** detectables por grep:

- «Asimetría a tu favor.» (cápsula corta de cierre repetida en varios submódulos)
- «Esa es la pieza que multiplica.» / «Esa es la X que multiplica/cambia/define.»
- «Total: X minutos antes de Y.» (cápsula de recap operativo)
- «Por ahí va el filtro/criterio/camino.» (cápsula de remate)
- «Es bueno, no malo.» / «Es buena noticia, no mala.» (cápsula antitética corta)
- «El criterio se queda.» / «Tu criterio se queda.» (eslogan)

---

## 7. Patrones estructurales prohibidos

Además de palabras sueltas, vigila estos patrones que delatan IA por su estructura:

- **El triplete simétrico**: Tres puntos, tres beneficios, tres pasos, todos de la misma longitud.
- **La apertura definición**: Empezar cualquier tema con «X es...» seguido de una definición enciclopédica.
- **El cierre eco**: Cerrar una sección repitiendo lo que acabas de decir con sinónimos.
- **El disclaimer preventivo**: Poner «es importante recordar que cada caso es diferente» al final de cada recomendación.
- **La escalera ascendente**: «No solo X, sino también Y, e incluso Z» — la IA adora este patrón.
- **La escalera ascendente** ya está descrita arriba; lo que viene ahora es aparte y es de las cosas que más cuesta cara.

---

## 7.1. REGLA DURÍSIMA — Nada de telegramas. Gasta las palabras que hagan falta.

Esta es de las que tumban un texto por sí solas, así que va explicada con detalle y sin prisa, que es exactamente el espíritu de la regla: **cuando algo importa, se desarrolla; no se trocea en notas**.

**Qué es el telegrama.** Es el vicio de escribir a base de fragmentos cortados, casi siempre sin verbo, puestos uno detrás de otro como quien apunta cosas en un pósit para acordarse luego, o como quien redacta un parte militar donde lo único que cuenta es ahorrar tinta. En lugar de coger una idea y explicarla en una frase que respira —con su sujeto, su verbo, sus conectores y, si hace falta, su subordinada y su matiz—, el telegrama la parte en pedacitos sueltos y le deja al lector el trabajo de reensamblarlos. El que escribe se ahorra el esfuerzo de hilar; el que lee lo paga.

**Cómo se reconoce, con ejemplos reales cazados en producción:**

El telegrama tiene **tres caras**, y las tres caen bajo esta misma regla:

1. **Fragmentos sin verbo en ráfaga.** ❌ «Decide cómo abordar la tarea. Qué ficheros tocar, en qué orden, qué comandos ejecutar, qué riesgos hay.» / ❌ «Ejecuta. Modifica ficheros, lanza comandos, escribe código.» / ❌ «Tres scopes. Personal, proyecto, plugin. Cada uno con su sitio.»
2. **Cadena de condicionales cortos** («Si…, Si…, Si…»). ❌ «Si ha lanzado pruebas, lee los resultados. Si fallan, analiza por qué. Si encuentra el error, modifica el código y vuelve a ejecutar. Si no, sigue investigando.» Cuatro «Si» seguidos disparados como un parte: aburre y suena a máquina.
3. **Andanada de imperativos seguidos** («Haz… Ejecuta… Comprueba…»). ❌ «Mira el diff. Ejecuta las pruebas. Comprueba que la solución te encaja.» Tres órdenes secas en fila, una detrás de otra.

**Regla de detección rápida:** en cuanto cuentes **tres frases cortas seguidas con la misma estructura** —tres fragmentos, tres «Si», tres imperativos—, es telegrama. Hílalas en una o dos frases que respiren, con sus conectores y su matiz.

✅ «Aquí el agente se para a decidir cómo va a abordar la tarea: piensa qué ficheros tiene que tocar y en qué orden conviene hacerlo, qué comandos va a necesitar ejecutar y qué riesgos hay por el camino antes de tocar nada.»
✅ «En esta fase el agente por fin actúa: modifica los ficheros que ha decidido tocar, lanza los comandos que necesita y va escribiendo el código, todo a la vista.»

**Por qué es regla dura y no una preferencia de estilo.** Estás escribiendo material de formación, y quien lo lee no ha venido a descifrar tus notas: ha venido a que se lo expliquen bien. Un texto formativo no se cobra por brevedad, se cobra por claridad. Si una idea necesita una subordinada, un par de conectores y diez palabras más para entenderse a la primera, esas diez palabras no sobran: son justo el trabajo por el que existe el texto. Gastar palabras en explicar no es relleno; el relleno es repetir lo ya dicho con otras palabras (eso sí está prohibido, ver densidad de valor). Desarrollar una idea por primera vez, con sus verbos y su hilo, es lo contrario del relleno: es enseñar.

**La distinción que hay que tener clara para no pasarse de frenada.** Una frase corta y rotunda, **sola**, con intención, sigue siendo un recurso buenísimo y está en la lista blanca: «Funciona.» «Exacto.» «Y punto.» Eso no es telegrama. El telegrama es la **ráfaga**: tres, cuatro fragmentos seguidos disparados sin verbo. La diferencia es la de un golpe único y seco frente a una ametralladora de trocitos. Un golpe, bien; ráfaga, nunca.

**Y que no se confunda con la densidad de valor (regla de oro 5).** Ser denso significa quitar las frases que no aportan nada nuevo, no quitar los verbos y los conectores que hacen que una frase se entienda. Puedes —y debes— ser denso y a la vez desarrollar: cada frase aporta algo nuevo, y cada frase está completa. Densidad es no repetirse; telegrama es no terminar las frases. No son lo mismo, y confundirlos lleva a escribir partes militares creyendo que estás siendo eficiente.

**El test rápido al releer:** coge cualquier párrafo y léelo en voz alta como si se lo estuvieras contando a alguien sentado a tu lado. Si suena a que estás leyendo una lista de la compra o dictando un telegrama, reescríbelo hasta que suene a que lo estás explicando. Si suena a que lo estás explicando, ya está bien.

---

## 8. Calcos del inglés y frases-golpe (cazados en producción real)

Lista viva: se amplía cada vez que el cliente caza uno. Si aparece, reescribe — y mira el antipatrón 11.

| Prohibido | Qué es | En su lugar |
|---|---|---|
| «con esteroides» (*X on steroids*) | calco | dilo llano: «un X un poco más espabilado», «un X que además…» |
| «estado del arte» (*state of the art*) | calco | «cómo está el patio», «cómo está la cosa (a fecha X)» |
| «modelos frontera» (*frontier models*) | calco | «modelos punteros», «los modelos de cabeza» |
| «abrir la caja / la caja negra» (*open the black box*) | calco | «ver qué hay dentro», «mirar cómo funciona por dentro» |
| «throughline» | palabra inglesa | «idea-eje», «el hilo que lo cruza todo» |
| «ideas metidas», «limpiar las ideas» | coloquialismo cutre | «llega convencido de que…», «con la idea de que…» |
| «X que aguanta toda la frase / la idea» | metáfora de escritor | «lo importante de esa frase es…», «la palabra clave es…» |
| «Vamos con X, y luego…» / «Déjame que…» | anuncio de transición | salta directo al contenido |
| Etiquetas «Encuadre / Núcleo / Desarrollo / Concreción / Destilación» en material de curso | plantilla de diseño instruccional traducida | títulos naturales + prosa; la nota de producción, aparte |
| «pregunta cerrada / abierta» (*closed/open question*) | calco, y casi siempre impreciso | «una respuesta de un repertorio ya fijado (etiqueta o número)», «preguntas de sí/no» |
| «slides», «demo», «deck», «paper», «target», «deadline», «insight» en el texto que lee o escucha el alumno | anglicismos | «diapositivas», «demostración», «versión/borrador», «artículo», «objetivo», «plazo», «idea/hallazgo». (En notas internas de producción se toleran como jerga de pipeline; en la prosa del alumno, jamás.) |
| «WebSearch», «vault», «ranking», «Gamma destilado», nombres de herramienta/pipeline crudos en prosa | jerga interna que se cuela | «búsqueda web», «el vault de Obsidian»/«el repositorio», «clasificación», «versión resumida para Gamma». Nombres de carpeta o de tool: solo en rutas/identificadores, nunca en prosa |
| «el bot» repetido | reduccionista | alternar con «el sistema», «el asistente»; reservar «bot» para el chatbot de primer nivel |
| «el ChatGPT» / «el Photoshop» (artículo + marca) en material formal | coloquial peninsular | «ChatGPT» sin artículo en texto escrito formal (en locución, el artículo puede quedarse si suena natural) |
| «iguala el terreno (hacia arriba y hacia abajo)» | calco de *level the playing field* | «cambia el reparto de capacidades», «mueve las reglas del juego» |
| «resultados de otro planeta», absolutos coloquiales en material base | demasiado coloquial/exagerado para corporativo | «resultados radicalmente distintos» |
| «lenguaje normal» (como término) | calco de *normal language*; el término real en IA es otro | **«lenguaje natural»** (es el término establecido). Coloquial: «con tus propias palabras», «con palabras de andar por casa» — pero el concepto se nombra «lenguaje natural» |
| «la que toca el trabajo de oficina» (tocar = afectar/incumbir) | uso de «tocar» que suena raro | «la que afecta al trabajo de oficina», «la que se mete en el trabajo de oficina» |
| «crear / crea información» (hablando de IA generativa) | le concede veracidad que no tiene | «generar contenido» (puede ser contenido falso) |
| «en cristiano» en material corporativo | coloquialismo castizo, según público | «dicho sin jerga», «en simple» |
| «lo hace la máquina en segundos» (promesa de resultado) | sobrepromete | «puede empezar a hacerlo», «te deja una primera versión» |
| Cierre motivacional de coach («lo decides tú», «el límite lo pones tú») en material corporativo | póster motivacional | «depende del criterio con el que la uses» |
| «creerse el bombo», «el bombo» (= hype), «hype» en prosa del alumno | coloquialismo opaco / anglicismo: no todo el mundo lo entiende | «las exageraciones», «lo que promete el titular», «el ruido», «la euforia» — di la cosa claro |
| Deíctico sin antecedente claro: «llegar a esto», «lo anterior», «eso que vemos» cuando el lector no puede saber a qué se refiere | el referente no se resuelve en el contexto | nombra la cosa: «a una formación así», «el modelo», «esa diferencia». Si dudas de a qué apunta un "esto/eso", sustitúyelo por el sustantivo |
| «radicalmente / absolutamente / completamente + adjetivo» | intensificador de relleno con olor a IA | quita el adverbio o ensúcialo: «que no tienen nada que ver», «muy distintos». **Ojo de registro:** ensuciarlo así es el default coloquial/aula; en material base sobrio la forma limpia («completamente distintos») puede ser la correcta — lo fija el autor (ver antipatrón 15) |
| «caso de uso» (*use case*) | calco de consultoría | «algo que automatizar», «una tarea donde encaja», «un sitio donde usarla» |
| Mezclar «correo basura» y «spam» en el mismo texto | inconsistencia terminológica | unifica al término lexicalizado y vivo: «spam» |
| «se rió / se frió / se lió» con tilde | la RAE computa estos como monosílabos | «se rio / se frio / se lio» (sin tilde). Ojo: muchos correctores y hablantes piden la tilde por la norma antigua — la moderna es sin tilde |
| «esta es la foto», «la foto de verdad», «la foto real» (= la situación real) | calco de *the (real) picture / snapshot* | «lo que de verdad pasa», «la situación real», «lo que de verdad cambia» |
| Metáfora rota / mezclada: (a) cambiar el sustantivo y dejar el verbo de la imagen anterior («a una balanza se le da la vuelta»); (b) sustantivo que no puede ser **agente** del verbo («la pieza que lo cambió todo» — una pieza es estática, no actúa) | imagen incoherente; suele pasar al "variar vocabulario" o al titular efectista | (a) si cambias el sustantivo, revisa que el verbo siga teniendo sentido: el **balance** se inclina o se invierte; la **proporción** se da la vuelta; el **listón** se sube. (b) un sustantivo estático/pasivo no va de sujeto de un verbo de acción → «lo que lo cambió todo», «el giro», «la circunstancia». Ojo: «balance» (equilibrio) ≠ «balanza» (instrumento). No mezcles imágenes |
| «un montón de / un porrón de» en el contenido base escrito | cuantificador oral; bien en locución, de charla en el texto base | base: «documentación extensa», «mucha documentación», «grandes volúmenes de». En locución se queda |
| Artículo indefinido espurio en enumeración de genéricos/incontables: «texto repetitivo, datos y **una** revisión humana» | rompe el paralelismo (los otros van sin artículo) y vuelve contable lo genérico | quita el artículo: «texto repetitivo, datos y revisión humana» |
| «Lo que lo resume:…» / «Lo que lo cambió todo» como sujeto-comodín o como título | relleno antes de nombrar la cosa; pariente del deíctico sin antecedente | nómbrala: «La idea que lo resume…»; en título, «El cambio clave» en vez de «Lo que lo cambió todo» |
| «va a volver / se va a ver / vamos a ver» (futuro perifrástico plano para una idea que recurre) | predicado mecánico, sin vida | verbo con vida: «rondará por ahí», «vuelve una y otra vez», «te lo vas a encontrar más de una vez» |
| «la respuesta corta es… / la larga…», «versión corta / versión larga», «spoiler:», «dicho en corto» como forma de entregar una idea | cápsula antitética por defecto (antipatrón 14); además anuncia la respuesta en vez de darla | da la respuesta desarrollada y punto; sin anunciar el formato |
| Verbo/atributo que no pega con el sustantivo (colocación rota), sobre todo en TÍTULOS: «una pregunta se queda **colgando**», «la idea que **junta** el mapa», «las fronteras se **vuelven borrosas**» | el verbo es de otra imagen; un mapa no se junta, una pregunta no cuelga, las fronteras no se vuelven borrosas (se difuminan) — reincidente y el grep no lo ve | nombra la cosa o pon el verbo que sí pega: «la pregunta que quedó en el aire»; «Multimodal: un modelo, varios formatos»; «las fronteras se difuminan / se desdibujan». Regla: lee cada título y cada metáfora aislados y comprueba que el verbo case con el sustantivo |
| «superficie / superficies» y el compuesto **«multi-superficie»** (*surfaces / multi-surface*) para los sitios/clientes donde corre una herramienta (terminal/CLI, extensión de VS Code, app de escritorio, web, cloud agent…) | calco de *surfaces*; en prosa de alumno suena raro y abstracto (cazado en GHCOPVS, reincidente en CCDEVNA) | **«entorno(s) de uso»**, «formas de usarlo», «por dónde lo usas», «según dónde lo uses», «los sitios donde se usa», o nómbralos: «la terminal, VS Code, el escritorio o la web». Para «la X que mejor se compone», di «la que mejor se engrana / donde más rinde». (Excepción: «superficie de ataque» es término de seguridad establecido — pero si suena abstracto en formación, reformula: «trata X como una dependencia», «entrada de riesgo».) |
| «refactors» / «un refactor» (sustantivo inglés, sobre todo en plural) | anglicismo crudo; el sustantivo español existe (cazado en SDDGHC, se le coló al revisor de voz) | **«refactorización / refactorizaciones»**. El verbo «refactorizar» sí es correcto. «Rojo-verde-refactor» se mantiene: es el nombre propio del ciclo TDD |
| Verbo inglés **hispanizado en «-ear/-eado»** en prosa del alumno: «commitear», «mergear», «testear», «deployar», «briefear», «debuggear», «parsear» | la conjugación a la inglesa chirría aunque el sustantivo inglés esté consolidado | usa la forma española o «hacer + sustantivo»: «hacer commit», «fusionar» (no «mergear»), «probar / tener tests» (no «testear»), «desplegar» (no «deployar»), «instruir / dar el encargo» (no «briefear»), «depurar», «analizar». **Matiz clave:** el SUSTANTIVO consolidado se queda («un commit», «un push», «un merge», «un bug», «un endpoint»); lo que se evita es **conjugar el verbo a la inglesa** |
| «scope» mezclado con «alcance» en la misma pieza | inconsistencia: usar el inglés junto al término español que tú mismo fijaste («fuera de alcance») | unifica a **«alcance»** en toda la pieza |
| «take-it-or-leave-it» | calco de idiom inglés | «no es 'lo tomas o lo dejas'», «no es un todo-o-nada» |
| **«calentamiento»** (laboratorio / práctica / ejercicio **de calentamiento**) | **doble sentido** (connotación sexual); inaceptable en material de curso (decisión Pedro 2026-06-07) | «de cabeza», «de criterio», «de arranque», «para entrar en materia», «para soltar la mano». Para herramientas (no ejercicios), «periodo de **rodaje**» en vez de «de calentamiento» |
| **«correr / corre / corren»** referido a ejecutar código/tests/suite («el test corre», «corren en segundos», «correr la suite») | **doble sentido** (vulgar en España); inaceptable en material de curso (decisión Pedro 2026-06-07) | **«ejecutar / se ejecuta / se ejecutan»**: «los tests se ejecutan en segundos», «ejecutas la suite», «al ejecutarlo». (No confundir con «recorrer», «correcto», «corregir», «correo» ni «correr el riesgo» / «a lo largo de» — esos no se tocan) |
| «case-sensitive» | calco | «distingue mayúsculas y minúsculas», «sensible a mayúsculas» |
| «placeholder» (prosa del alumno) | anglicismo (en código se queda) | «marcador de relleno», «hueco de relleno», «texto de relleno» |
| «flaky» / «test flaky» | anglicismo de testing | **«test inestable»** (glosa la 1.ª vez: «que falla de forma intermitente sin que cambie el código») |
| «edit / edits» (sustantivo en prosa) | anglicismo; `Edit` como nombre de la herramienta de Claude Code se mantiene | **«cambio(s)»** o «edición/ediciones» (cazado en CCDEVNA) |
| «test / tests» (en prosa del alumno) | lexicalizado en dev, pero en este vault se prefiere el término español | **«prueba(s)»**; «pruebas unitarias / de integración» como término técnico. Excepción: comandos y nombres de fichero (`dotnet test`, `…Tests.cs`) se mantienen (cazado en CCDEVNA) |
| «string» (en prosa) | anglicismo; en código se queda | **«cadena»** («una cadena», «un literal de cadena») |
| «outputs» (en prosa) | anglicismo | **«salidas»**, «resultados», «lo que devuelve» |
| «debugging» (en prosa) | anglicismo (el sustantivo «debug» se tolera; el verbo «debuggear» no) | **«depuración» / «depurar»** |
| «doc» / «la doc» (= documentación) | abreviatura anglosajona | **«la documentación»** |
| «repo» (en prosa) | abreviatura informal | **«repositorio»** (en prosa; «repo» se tolera en tono muy de aula) |
| «suite (de tests)» | anglicismo | **«batería de pruebas» / «conjunto de pruebas»** |
| «cross-X» (cross-fichero, cross-cutting…) | calco-prefijo inglés | **«que cruza varios…», «entre ficheros», «transversal»** |
| «razón fuerte» · «tirarle un problema/tarea a X» (= pasárselo) | colocación que chirría | «un buen motivo / un motivo de peso»; «es el modelo al que recurres / al que le pasas lo difícil» (cazado en CCDEVNA) |
| «log / logs» en prosa del alumno | anglicismo; tiene equivalente directo | **«registro / registros»** (el verbo «loguear» también fuera: «registrar») |
| «input / output» (y plurales) en prosa | anglicismo; equivalente directo | **«entrada / salida»**, «entradas/salidas». (En firmas de código o nombres de campo se quedan) |
| «working tree» / «working directory» | jerga git sin traducir | **«árbol de trabajo»** (término oficial de git en español) o «tu copia de trabajo local» |
| «sandbox» en prosa | anglicismo | **«entorno aislado»** (efímero, si aplica) |
| «stack trace» | jerga sin traducir | **«traza de pila»** |
| «plumbing» (metáfora de infraestructura) | anglicismo-metáfora | **«fontanería / cañería / tubería»** («es fontanería, no inteligencia») |
| «dashboard» | anglicismo | **«panel»**, «panel de control» |
| «brief» (sustantivo, prosa) | anglicismo de producto/diseño | **«encargo»**, «encargo inicial», «resumen de partida» |
| «feature / features» | anglicismo con equivalente directo | **«funcionalidad / funcionalidades»** |
| «bug / bugs» | anglicismo con equivalente directo | **«error / errores»** (o «fallo»). Compuesto: «bugfix» → **«corrección de error»** (cuida la concordancia: «una corrección de error», no «un bugfix»). Nota: «bug» es de los más lexicalizados; tradúcelo salvo que el cliente pida mantenerlo |
| «X rinde» con sujeto evento/abstracto («las conversaciones/sesiones rinden») | «rendir» (= dar fruto/utilidad) es CORRECTO con herramienta, método o inversión como sujeto, pero chirría con un evento | «funcionan mejor», «cunden», «dan mejor resultado». Mantén «rinde» cuando el sujeto es una herramienta o enfoque: «OpenAPI rinde», «el método rinde», «este patrón rinde» |

> **Nota — nombres de producto y términos técnicos consolidados NO son calcos.** No "corrijas" lo que es el nombre real de lo que enseñas ni la jerga lexicalizada: se mantienen en inglés *agent mode, prompt files, chat modes, coding agent, branch protection, secret scanning, workflow* (objeto de GitHub Actions), *pull request, commit, push, endpoint, deploy, build, lint, MCP, Spec Kit, spec*. La regla de arriba ataca el **verbo hispanizado** y el **calco de prosa**, no el sustantivo técnico establecido. En la duda: ¿tiene equivalente español natural y de uso corriente? → tradúcelo. ¿Es el nombre propio de una herramienta/feature o jerga universal del gremio? → se queda.

> **Regla del coloquialismo transparente**: una expresión coloquial solo vale si la entiende toda la audiencia sin explicación. Si hay que glosar qué significa («el bombo», o sea el hype…), no es color: es ruido. Lo castizo que no es universal (España vs. LatAm, generacional, de gremio) se sustituye por lo claro.

> **Palabras con doble sentido vulgar/ofensivo — JAMÁS.** Aunque la acepción que tú usas sea inocente, si la palabra tiene **otra acepción vulgar u ofensiva** en España, fuera del material de curso. El lector la lee, no lee tu intención. Caso cazado: **«paja»** (= relleno/broza) tiene acepción sexual vulgar en España → nunca «paja inventada», «mucha paja»; di «relleno», «broza», «se lo está inventando», «invención sin fundamento». Mismo criterio con cualquier término de doble lectura escatológica/sexual: si dudas, cámbialo por el sinónimo neutro.

> **Regla general detrás de las dos últimas**: cuando un campo tiene un **término establecido** (lenguaje natural, alucinación, ventana de contexto…), úsalo — no inventes un sinónimo coloquial que suene a traducción. Lo coloquial va en la *glosa* ("o sea, le hablas con tus palabras"), no en sustituir el término.

Regla de oro de esta sección: **si suena a charla TED traducida o a manual de Silicon Valley en español, está mal.** El patrón es siempre el mismo — adornar en vez de decir.

---

## 9. Nombres de fichero en prosa teórica (cursos técnicos)

En la **explicación conceptual** —cuando todavía NO hay ficheros delante del alumno: estás contando *cómo funciona algo* o *cómo se siente* usarlo, no haciendo una demo paso a paso— **no se usan nombres literales de fichero ni símbolos del proyecto-ejemplo inventado**. Se habla por su nombre natural en español. Si el alumno aún no tiene esos archivos, nombrarlos es falso y distrae.

| En prosa teórica, NO | Di mejor |
|---|---|
| `OrdersController.cs` | «el controlador de pedidos» |
| `Order.cs` | «la clase de pedidos», «la entidad pedido» |
| `OrderDto` | «el DTO de pedido / de respuesta» |
| «Lee `OrdersController.cs`, ojea `Order.cs`» | «abre el controlador de pedidos, ojea la entidad pedido» |
| «`/Contracts/Orders/`» | «la carpeta de contratos» |
| «la prueba con `WebApplicationFactory`» | «la prueba de integración» |
| `OrderMapper`, `OrderService` y demás clases con nombre propio que el alumno aún no conoce | descríbelas por su función: «la conversión a DTO», «la lógica de pedidos» |
| `Program.cs` | «el arranque de la aplicación» |

**Regla más fina:** en teoría no introduzcas **una clase con nombre propio que el alumno todavía no ha visto** (`OrderMapper`). Aunque la traduzcas a «el mapper de pedidos», sigue siendo jerga sin contexto. Di lo que **hace** —«la conversión de la entidad a su DTO»— no cómo se llama. El nombre propio se presenta el día que el alumno lo tiene delante.

**Se mantienen** (no son nombres de archivo inventados): los conceptos y la tecnología real que se enseña (endpoint, DTO, repositorio, controlador, prueba de integración, inyección de dependencias), los nombres propios de la herramienta/framework (`CLAUDE.md`, `SKILL.md`, `settings.json`, tipos reales del SDK), y los nombres de las herramientas de Claude Code (`Read`, `Edit`, `Grep`).

**Dónde sí van los nombres literales de fichero:** en demos, prácticas guiadas y pasos sobre ficheros reales, donde el alumno los tiene abiertos y los va a tocar. Ahí nombrarlos es lo correcto.

Tell de proceso: si un párrafo es una explicación de *cómo es / cómo se siente / qué hace* y aparece un `Algo.cs` con CamelCase de proyecto, casi seguro es nombre de archivo colado en teoría → sustitúyelo por el nombre natural.

---

## 10. Plural dirigido al alumno — PROHIBIDO en TODO el material (base, slides, locución)

El material de curso se dirige SIEMPRE a **UNA persona** (un lector del libro, un espectador del vídeo, cada asistente por separado). **Nunca te diriges al alumno en 2ª del plural.** Aplica a las TRES piezas, no solo a la locución. Es **bloqueante**: cada hit se corrige a 2ª del singular.

**Prohibido** (dirigido al alumno):

| Forma plural | → singular |
|---|---|
| vosotros / os / vuestro(a/s) | tú / te / tu(s) |
| fijaos, daos, mirad, pensad, ved (imperativos plurales) | fíjate, date, mira, piensa, ve |
| habláis, tenéis, queréis, sabéis, podéis (presente `-áis/-éis`) | hablas, tienes, quieres, sabes, puedes |
| comentasteis, vinisteis, tomasteis (pretérito `-asteis/-isteis`) | comentaste, viniste, tomaste |
| teníais, habíais, queríais (imperfecto `-abais/-íais`) | tenías, habías, querías |
| vais, dais, sois, veis (monosílabos sin tilde) | vas, das, eres, ves |

**SÍ vale** (no es plural dirigido al alumno, no se toca): el **«nosotros» inclusivo del formador** («vamos a ver», «empecemos»), y hablar de **«tu equipo» en 3ª persona** («tu equipo hace X»). El veto es dirigirse al alumno **como grupo**.

**Grep de detección** (FLAG, no reemplazo automático — revisa cada hit; falsos positivos: «país», «jamás», «más», «os» dentro de «todos/dos/nos»):
`grep -inE "\b(vosotros|os|vuestr|fijaos|daos|mirad|pensad)\b|\w*(áis|éis|asteis|isteis|abais|íais)\b|\b(vais|dais|sois|veis)\b"`

(El registro de la locución, además, lo refuerza `locucion-curso`; la concordancia del trío lo verifica `modulo-integral`. La regla **nace aquí**, en escritura-humana, para las tres piezas.)
