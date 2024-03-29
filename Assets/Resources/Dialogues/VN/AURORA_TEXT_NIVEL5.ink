->NIVEL_5_INTRO
EXTERNAL SetNewSpeaker(string speakerName)
EXTERNAL NextLevel(string none)

===NIVEL_5_INTRO===
No me lo puedo creer.#speaker:0
He gastado en dos meses en Nueva York más que en Granada en toda una vida. 
Espero que mi padre me haya enviado el bote salvavidas que le pedí en mi última carta… 
Quizá mi viejo amigo Colin pueda ayudarme. Betty me dijo que trabajaba en este banco…
~SetNewSpeaker("Patty")
Buenos días, caballero. Bienvenido a Central Bank. ¿Qué desea?#speaker:1
*¡Manos arriba! ¡Esto es un atraco! #speaker:0
->NIVEL_5_INTRO_01
*Buenos días, señorita. Tengo un problema de vida o muerte.#speaker:0
->NIVEL_5_INTRO_01

===NIVEL_5_INTRO_01===
¡Qué simpático es usted! Tenga cuidado con lo que dice en un sitio como este…#speaker:1 
Dígame, ¿qué desea? 
Pero, ¡si usted es Betty!#speaker:0
Creo que me confunde, caballero. Mi nombre es Patty.#speaker:1
*¿No le ayudé a conseguir un periódico?#speaker:0
->NIVEL_5_INTRO_02
*¿No me dio una moneda que necesitaba?#speaker:0
->NIVEL_5_INTRO_02

===NIVEL_5_INTRO_02===
Creo que me confunde con mi compañera Betty. #speaker:1
*¡Pero si sois idénticas!#speaker:0
->NIVEL_5_INTRO_03
*Se está riendo de mí, ¿verdad?#speaker:0
->NIVEL_5_INTRO_03

===NIVEL_5_INTRO_03===
¡Para nada! Betty y yo somos muy diferentes. ¡Ella tiene las gafas de color rojo! ¡Yo nunca haría eso!#speaker:1 
*Pues me gustaría hablar con Betty, por favor.#speaker:0
->NIVEL_5_INTRO_04
*Pues a Betty le quedan mucho mejor las gafas.#speaker:0
->NIVEL_5_INTRO_04_1

===NIVEL_5_INTRO_04_1===
No va a conseguir que le ayude si me habla de esa manera… #speaker:1
Disculpe, tiene razón. Solo quiero hablar con Betty.#speaker:0
 ->NIVEL_5_INTRO_04

===NIVEL_5_INTRO_04===
Me temo que eso no va a ser posible. Betty está muy ocupada.#speaker:1
De hecho, yo estoy muy ocupada con unas operaciones muy complicadas. 
¿Sabe cuánto es 24 x 12? 
-No tengo ni idea…#speaker:0
->NIVEL_5_INTRO_05
-Use una calculadora.#speaker:0 
->NIVEL_5_INTRO_05

===NIVEL_5_INTRO_05===
Vamos, ayúdeme. Si me ayuda, quizá pueda ayudarle. #speaker:1
¿Sabe cuánto es 24 x 12? 
*Creo que es… ¿264?.#speaker:0
->NIVEL_5_INTRO_06_01
*Por favor, necesito mi dinero. Quiero hablar con…#speaker:0
->NIVEL_5_INTRO_06

===NIVEL_5_INTRO_06_01===
Usted y yo sabemos que esa no es la respuesta.#speaker:1 
Necesito que se esfuerza un poco más.
->NIVEL_5_INTRO_06

=== NIVEL_5_INTRO_06===
Vamos, gentil caballero… Ayúdeme… ¿Sabe qué hay debajo de una multiplicación?#speaker:1  
*Debajo de las multiplicaciones hay una gota de sangre de pato.#speaker:0 
->NIVEL_5_INTRO_07
*Una multiplicación más complicada aún.#speaker:0
->NIVEL_5_INTRO_07


===NIVEL_5_INTRO_07===
Debajo de una multiplicación hay un universo por descubrir. #speaker:1
Ehm, creo que usted no me va a servir de ayuda…
Dígame, ¿qué necesita? 
¡Por fin! #speaker:0
Necesito hablar con la señorita Betty. Tengo una cita con Mr. Campbell. 
¿Con el señor Hackworth-Jones? ¡Debería haberlo dicho antes!#speaker:1
*No me ha dejado ni hablar…#speaker:0
->NIVEL_5_INTRO_08
*Si me hubiera escuchado…#speaker:0
->NIVEL_5_INTRO_08


===NIVEL_5_INTRO_08=== 
No se preocupe, solo tendremos que rellenar un formulario y le paso a su despacho.#speaker:1 
Dígame su nombre. 
*Federico. Federico García Lorca.#speaker:0
->NIVEL_5_INTRO_09_01
*No sé quién soy. Ese es mi problema.#speaker:0
->NIVEL_5_INTRO_09_02

===NIVEL_5_INTRO_09_02===
¿No se conoce a sí mismo? Ha llegado al lugar indicado.#speaker:1 
Deme un segundo y le atenderá mi compañera. 
->NIVEL_5_2_01

===NIVEL_5_INTRO_09_01===
De acuerdo, Mr. Lorca. ¿Año de nacimiento?#speaker:1 
*1898.#speaker:0
->NIVEL_5_INTRO_10
*No sé tan siquiera si estoy vivo…#speaker:0
->NIVEL_5_INTRO_09_02

===NIVEL_5_INTRO_10===
De acuerdo. ¿Motivo de su visita? #speaker:1
*Quiero recoger el dinero que me ha enviado mi padre.#speaker:0
->NIVEL_5_INTRO_11
*Quiero mi dinero de una vez por todas.#speaker:0
->NIVEL_5_INTRO_11

===NIVEL_5_INTRO_11===
De acuerdo. Pues espere un segundo y le atiende ahora mi compañera.#speaker:1 
->NIVEL_5_2_01


===NIVEL_5_2_01===
¿Compañera? Pero si quiero hablar con Colin… #speaker:0
~SetNewSpeaker("Maddy")
Buenos días. ¿Qué desea?#speaker:1
*Mi dinero. ¡Quiero mi dinero!#speaker:0 
->NIVEL_5_2_02
*Deseo hablar con el señor Hackworth-Jones.#speaker:0 
->NIVEL_5_2_02

===NIVEL_5_2_02===
Claro, lo entiendo.#speaker:1
¿Ha rellenado el formulario? 
Sí, acabo de rellenarlo con usted, hace un momento.#speaker:0
No, lo rellenó con mi compañera Patty. Yo soy la señorita Maddy, y estaré encantada de ayudarle.#speaker:1 
¿Ha rellenado el formulario entonces? 
Sí, con su compañera, entonces.#speaker:0
No, con mi compañera rellenó el formulario para hablar conmigo.#speaker:1 
Si desea hablar con el señor Hackworth-Jones debe rellenar otro formulario. 
¿Nombre? 
*Federico, Federico García Lorca.#speaker:0
->NIVEL_5_2_03_01
*¡Quiero mi dinero! #speaker:0
->NIVEL_5_2_03_02

===NIVEL_5_2_03_02===
De acuerdo, señor Quiero mi dinero.#speaker:1 
Una pregunta más.
->NIVEL_5_2_04

===NIVEL_5_2_03_01===
De acuerdo, señor García Lorca.#speaker:1 
Una pregunta más.
->NIVEL_5_2_04

===NIVEL_5_2_04===
¿Sabe qué hay debajo de las divisiones?#speaker:1 
*Una gota de sangre de marinero.#speaker:0 
->NIVEL_5_2_05
*Un abismo en el que se pierden los encontrados.#speaker:0
->NIVEL_5_2_05

===NIVEL_5_2_05===
Excelente respuesta. ¡Qué profunda!#speaker:1
El señor Hackworth-Jones estará encantado de hablar con usted. 
Una pena que no esté hoy en la oficina. 
*¿Cómo? ¿Colin no está aquí hoy?#speaker:0 
->NIVEL_5_2_06
*¿Se está riendo de mí en mi propia cara?#speaker:0 
->NIVEL_5_2_06

=== NIVEL_5_2_06===
Entiendo su enfado, señor. Solo puedo intentar que el director del banco, Mr. Bank lo reciba.#speaker:1
*Por favor. ¿Puedo verlo ahora mismo?#speaker:0
->NIVEL_5_2_07
*Estoy deseando de conocer a ese tipo…#speaker:0
->NIVEL_5_2_07

===NIVEL_5_2_07===
De acuerdo. Solo tendrá que rellenar un formulario. Un segundo, por favor.#speaker:1 
¿Otro formulario? ¿En serio?#speaker:0 
->NIVEL_5_3_01

=== NIVEL_5_3_01===
~SetNewSpeaker("Kitty")
Buenos días, señor. ¿Qué desea?#speaker:1
¡Betty! Por fin puedo verla.#speaker:0
No, me confunde con otra, señor. Soy la señorita Kitty.#speaker:1 
¿No se ha fijado en mis gafas?
Por favor, apiádese de mí. Necesito mi dinero.#speaker:0 
Claro, haré lo que sea por ayudarle.#speaker:1
En Central Bank solo queremos lo mejor para nuestros clientes. 
*Necesito el dinero que me ha enviado mi padre desde España.#speaker:0
->NIVEL_5_3_02
*Necesito acabar con esto cuanto antes.#speaker:0
->NIVEL_5_3_02

===NIVEL_5_3_02===
No será un problema si usted es cliente nuestro.#speaker:1 
Lo es, ¿verdad?
Federico:
*No, solo estoy aquí de vacaciones.#speaker:0
->NIVEL_5_3_03
*No querría pertenecer a este cementerio jamás.#speaker:0
->NIVEL_5_3_03

===NIVEL_5_3_03=== 
Entonces tenemos un problema. Deberá ver al director del banco.#speaker:1
¡Eso es lo que quiero! Pero no puedo con tanta burocracia. ¡Póngase en mi lugar!#speaker:0 
No me grite, caballero.#speaker:1
¿Sabe qué? Voy a denunciarles.#speaker:0 
Me parece genial.#speaker:1 
Un segundo. Le atenderá el señor Mr. Bank.#speaker:1 
*¡Maldito Nueva York! ¡Yo os escupo en la cara!#speaker:0 
->NIVEL_5_4
*¡Nueva York, río de lodo y sangre!#speaker:0
->NIVEL_5_4

===NIVEL_5_4===
~SetNewSpeaker("Bank")
¿Qué dice, señor? ¿Cómo se atreve a…?#speaker:1
¡Por fin usted! ¿Sabe lo que le digo? ¡Le voy a denunciar!#speaker:0
¿Por qué? #speaker:1
Yo denuncio a toda la gente que ignora a la otra mitad,#speaker:0
la mitad irredimible donde laten los corazones.
Todos formamos parte de esa mitad, caballero.#speaker:1 
No. La otra mitad me escucha, devorando,#speaker:0
cantando, volando en su pureza. 
No es el infierno; es la calle. 
¿Quiere decir usted que esto es el infierno?#speaker:1
Quiero decir que denuncio la conjura de estas oficinas#speaker:0
que no radian las agonías, que borran los programas de la selva. 
Señor, usted no sabe lo que es Nueva York.#speaker:1 
Si usted muriera, sería un animal muerto más. Nada más.
Todos los días se matan en Nueva York
Cuatro millones de patos, cinco millones de cerdos y dos millones de gallos
que dejan los cielos hechos añicos. 
¿Quiere un consejo? 
*No quiero nada de este templo de níquel y papel mojado.#speaker:0
->NIVEL_5_4_01
*No quiero nada más que mi dinero.#speaker:0
->NIVEL_5_4_01

===NIVEL_5_4_01===
Se lo daré de todos modos: No se resista a la madrugada.#speaker:1 
¿Qué quiere decir?#speaker:0 
Dígase la verdad. Usted no ha venido a ver el cielo de Nueva York.#speaker:1
¿A qué he venido entonces?#speaker:0 
Usted a venido a ver la turbia sangre que lleva las máquinas a las cataratas.#speaker:0 
*¡Para nada! El cielo es mi voz.#speaker:0 
->NIVEL_5_4_02
*Creo que es la primera verdad que oigo aquí.#speaker:0
->NIVEL_5_4_02

===NIVEL_5_4_02===
Hágase un favor: ríndase. Ríndase a Nueva York. #speaker:1
Está bien. Me rindo. #speaker:0
Me ofrezco a ser comido por las vacas estrujadas.
Me ofrezco a gritar en el valle donde el Hudson se emborracha con aceite. 
Es lo único que nos importa, señor. Que se rinda. #speaker:1
Que deje de ser usted para ser Nueva York. 
¿Entonces?#speaker:0
Entonces cumple con los requisitos para recibir su dinero. #speaker:1
Es lo que quería escuchar. Enhorabuena, señor. Recibirá su dinero. 
Espero que disfrute de su estancia en Nueva York. 
~NextLevel("none")
->END 
