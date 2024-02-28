EXTERNAL GoToNextObjective(string none)
EXTERNAL CallEvent(int eventIndex)

//NIVEL 2: PUERTO
-> 2puerto

=== 2puerto ===
//Federico y Fernando de los Ríos salen del barco.
//En la entrada del puerto, se paran a hablar.
Federico, amigo, ¡bienvenido al centro del Universo! ¡Bienvenido a Nueva York! #speaker:1

+ ¿Esto es Nueva York? ¡Huele a caimán y a tabaco! #speaker:0
    No me gusta lo que veo. 
    ->2NYmal
+ ¡No me lo puedo creer! Si pensé que esto era América entera... #speaker:0
    Ya me enamoré de la ciudad. 
    ->2NYbien

=== 2NYmal ===
 ¡Ja! ¡Todavía estás a tiempo de coger el barco de vuelta! #speaker:0
 * No me lo diga dos veces, don Fernando... #speaker:1
 Si esto es Nueva York, bien se parece a la ciudad que aparece en mis pesadillas... #speaker:0 
 -> 2NYmal2
 * Tengo un mal pálpito... #speaker:0
 Siento nauseas, don Fernando. 
 ->2NYmal2
 
 ===2NYmal2===
 Es momento de dejar todo eso atrás, Federico. ¿Qué día es hoy? #speaker:1
 * No sé ni el año en el que vivo... #speaker:0
 ->2avance2
 * Creo que estamos a 26. #speaker:0
 ->2avance2

===2NYbien===
¡Amor a primera vista, Federico! #speaker:1

¡Si toda Granada cabe en una de estas calles! #speaker:0
¡Oh, sirenas de Manhattan, hacia vosotras voy! 

No son sirenas, es la voz de tu nueva vida. ¿Qué día es hoy? #speaker:1
 * El día más feliz de mi vida. #speaker:0
 ->2avance2
 * Creo que estamos a 26. #speaker:0
 ->2avance2


===2avance2===
Pues deberías marcar este día en tu diario como el día de tu nuevo nacimiento. Tu nueva vida acaba de comenzar. #speaker:1

 * Lléveme a bautizarme entonces, don Fernando. #speaker:0
 No quiero tener una segunda vida en pecado. 
 ->2avance3
* Si con mi vida anterior a mí me bastaba. #speaker:0
No sé por qué se empeña en cambiar a Federico si Federico es lo único que tengo. 
->2avance3

===2avance3===
Mucho más tendrás en esta urbe, Federico. Si le das una oportunidad a Nueva York, Nueva York te devolverá cientos de ellas. #speaker:1

Vamos allá entonces, don Fernando. Lléveme lejos del mar, que quiero quitarme este olor a salado. #speaker:0

¿Por dónde quieres comenzar? #speaker:1

* ¡Lléveme al teatro! #speaker:0
Sé que allí me sentiré como en casa. 
->2iralteatro
* ¡Lléveme a tomar una copa! #speaker:0
Muero por probar los licores del Nuevo Mundo. 
->2iralbar

===2iralteatro
Has venido al sitio indicado entonces. ¡Nueva York es la capital de la escena!  ¡Te encantará conocer a los actores y actrices del momento! #speaker:1 

* [(Mmm... Si supiera que el teatro no me interesa...)] #speaker:0 //Thought
(No quiero actores, sino hombres que lloren de verdad y rían de verdad). #speaker:0 //Thought
¡Oh, sí! ¡Lléveme a un teatro, que me muero! #speaker:0
->2avance4
* [(Mmm... Solo en un teatro yo volveré a ser yo...)] #speaker:0 //Thought
La realidad ha matado a Federico y el teatro le devolverá la vida. #speaker:0 //thought
¡Oh, sí! Lléveme a un teatro, que me muero! #speaker:0
->2avance4

===2iralbar===
Aquí no encontrarás vinos de Jerez ni coñac, Federico. #speaker:1
Nueva York lleva 3 años con la Ley Seca y beber cada vez es más difícil.

* [(Mmm... No es el alcohol lo que me importa)] #speaker:0
(Al Federico ebrio y desinhibido es al que echo de menos). #speaker:0 //thought
Encontraremos la forma de llenar nuestro vaso, ¿no? 
->2avance4
* [(¿Y me dice esto ahora?)] #speaker:0
No me imagino a Colón atravesando el Océano para beber agua...  //thought
Don Fernando, acaba usted de quitarme dos años de vida. 
->2avance4


 ===2avance4===
 ¡Ja! Tengo una idea mejor, Federico. ¿Qué tal si vas a darte una ducha y luego nos encontramos con Juan, Lucas y Pepín? #speaker:1

 ¿En serio? ¿Están aquí? #speaker:0
 
 ¡Pues claro! ¡Y tu amigo Maroto! Y andan preparándote una fiesta sorpresa de bienvenida. #speaker:1

* ¡No! ¡Quiero perderme y conocer nuevos rostros! #speaker:0
No vine hasta aquí para ver a los de siempre. 
->2avance5
* ¡Sí! ¡Muero por ver caras amigas! #speaker:0
¡Qué ansia de fraternidad! 
->2avance5
 
 
 ===2avance5===
 Hay tiempo para todo, Federico. #speaker:1
 Por ahora, lo mejor será que vayas a la Residencia de Estudiantes y te des una buena ducha. 
 Al final de la calle, encontrarás la estación del tranvía. Coge la línea 3 y baja en la última parada. ¡No tiene pérdida! 
 Ah, se me olvidaba... Debes comprar un billete para el tranvía. ¡Será sencillo para un hombre como tú! 
 ~GoToNextObjective("None")
Te veo esta noche en el Small Paradise. 
¡Salud y República, Federico! 
~CallEvent(0)
 //Fernando de los Ríos se va. 

 Vamos allá, Federico. ¿No querías nuevas aventuras? Pues aquí tienes un nuevo continente por descubrir... #speaker:0
 ->END
 

