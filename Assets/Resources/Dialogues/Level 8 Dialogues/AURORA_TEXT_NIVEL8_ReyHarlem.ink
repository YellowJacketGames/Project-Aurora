//NIVEL 1: BARCO
//INTERACCIÓN CON CAPITAN
->DialogoReyHarlem

EXTERNAL NextLevel(string none)

===DialogoReyHarlem===
¡Rey de Harlem! ¡Por fin te encontré!#speaker:0
Solo tenías que buscar dentro de ti.#speaker:1

*¿Puede haber lugar más profundo?#speaker:0
->NIVEL_8_02
*Justo ahí es donde me perdí.#speaker:0
->NIVEL_8_02



===NIVEL_8_02===
Tranquilo, has cruzado los puentes.#speaker:1
Hace falta valor para cruzar los puentes.#speaker:1
*¿Qué puente? ¡Si estamos bajo tierra!#speaker:0
->NIVEL_8_03
*No, Federico tiene miedo a las alturas.#speaker:0
->NIVEL_8_03

===NIVEL_8_03===
Veo en ti un ansia por cruzar el puente pero, a veces, te pierdes en el miedo de tu propia alma.#speaker:1
¿Dónde está el puente que debo cruzar?#speaker:0 
Está en ti. Cruzar el puente implica cruzar por encima del río de la vida.#speaker:1
Las vidas son ríos que van a dar al mar…#speaker:0
Y los puentes permiten conectar unas vidas con otras. Estás viviendo una vida que no es tuya. Acéptate.#speaker:1
¿Cómo?#speaker:0
Deja de ser el Lorca que todos quieren. Sé el Federico que eres.#speaker:1
Otra vez, el miedo me hace galopar…#speaker:0
La búsqueda implica riesgos. Para resucitar es necesario morir.#speaker:1

*¿Cómo? ¿Insinúas que debo…?#speaker:0
->Nivel_8_04
*¡Federico nunca morirá!#speaker:0
->Nivel_8_04

===Nivel_8_04===
La muerte llegará. Para encontrarte, debes aceptar todas las partes de ti, incluso la que más temes.#speaker:1
¿Incluso la más profunda?#speaker:0
La que hace que te tiemblen los labios al sonreír a los muchachos.#speaker:1
*¿Cómo es posible que sepas…?#speaker:0
->Nivel_8_05
*¿Yo? ¿Muchachos? ¡Nunca!#speaker:0
->Nivel_8_05

===Nivel_8_05===
Tu poesía fluye de dentro de ti. Para encontrar tu poesía, debes amar tu alma tal y como es.#speaker:1
¿Cómo amar el que nunca ha sido amado?#speaker:0
¡Falso! Te han amado, Federico. Salvador, Emilio, Rafael, Eduardo… Todos te han amado.#speaker:1
*¡Y todos han dejado de amarme!#speaker:0
->NIVEL_8_06
*¡Mentira! ¡Todo aquello acabó!#speaker:0
->NIVEL_8_06


===NIVEL_8_06===
¿Acaso no acaba una poesía? El último verso de un poema no es más que el primero del siguiente.#speaker:1
Oh… Tienes razón. Todo tiene un tiempo.#speaker:0
Pero tú no, Federico. Tú serás eterno.#speaker:1
Pero siento que la bruma y el sueño y la muerte me están buscando.#speaker:0
A ti la muerte te encontrará y te llevará en hombros. Ahora, debes volver a la vida. A tu nueva vida.#speaker:1
*¿Por dónde empiezo?#speaker:0 
->NIVEL_8_07
*¿Por quién lo hago?#speaker:0 
->NIVEL_8_07


===NIVEL_8_07===
Por ti mismo. Acepta. Quiere. Ama. Ama sin límites. Ni miedo.#speaker:1
¿Y qué hago con mi soledad?#speaker:0
Hazla poesía.#speaker:1
Mira a tu alrededor.#speaker:1
Escribe Nueva York en ti. Escribe Nueva York en un poeta.#speaker:1
*Qué necesidad tengo de escribir, oh, Rey de Harlem.#speaker:0
->NIVEL_8_08
*¿Cómo vuelvo a la ciudad?#speaker:0 
->NIVEL_8_08


===NIVEL_8_08===
Que tu pluma sea tu puente. 
Y, cuando tengas miedo, mira tu Luna de plata. 
Ella te dará la respuesta.#speaker:1
Mi Luna de plata…#speaker:0
Será lo último que veas con tu último respiro.#speaker:1
¿Cuándo sucederá?#speaker:0
Federico es presente. Federico nunca será muerte.#speaker:1
Que así sea.#speaker:0
Federico es Nueva York. 
Nueva York es Federico.#speaker:1
~NextLevel("none")
->END