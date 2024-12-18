//NIVEL 1: BARCO
//INTERACCIÓN CON CAPITAN
->NIVEL_9_01

EXTERNAL CheckIfHasItem(string itemName)
EXTERNAL NextLevel(string none)

===NIVEL_9_01===
La terraza del Chrysler Building… 
Al fin un poco de aire puro. Es justo lo que necesitaba. #speaker:0
¡Déjeme! ¡No intente convencerme! #speaker:1
*¿Cómo? ¿Qué le ocurre? #speaker:0
->Nivel_9_02
*¡No! ¡Hablemos, por favor!#speaker:0
->Nivel_9_02



===Nivel_9_02===
Nada tiene sentido. El aire me asfixia. #speaker:1
Usted es… ¡Pero si es el director de Central Bank! #speaker:0
Déjeme. Central Bank ya no existe… Todo son cenizas. #speaker:1
Mire la ciudad. Respire. Nueva York es un poema que se abre al horizonte. #speaker:0
¡Nueva York es una cuerda al cuello! #speaker:1

*¿Qué le ocurre? Póngale nombre a su pena. #speaker:0
->Nivel_9_03
*¿Pero por qué todo el mundo está hoy triste? #speaker:0
->Nivel_9_03

===Nivel_9_03===
¿No se ha enterado? ¡Hoy es un día oscuro!
¡La Bolsa se ha desplomado! 
¡Todo ha saltado por los aires! #speaker:1

La riqueza es un sueño. Laberinto verde de papel mojado. #speaker:0 
¡No hay lugar para la poesía! La realidad supera sus versos. En Nueva York no hay sitio para nosotros. #speaker:1
Las calles están ocupadas por los fantasmas. Mi poesía es una luz que los espanta. #speaker:0
No hay luz que pueda iluminar mi oscuridad. #speaker:1
Déjeme. Debo poner fin a este sufrimiento. #speaker:1
Amigo, ¿y el sufrimiento que va a provocar? #speaker:0 
¿Cómo? #speaker:1
Su hijo, su esposa, su perro… ¿Cuánto dolor va a provocar si lo hace? #speaker:0 
¡Cuánto dolor he provocado! Soy un animal. Mi mente estaba llena de musgo verde de Central Bank. #speaker:1
No es que tuviera buenas formas en su oficina, todo sea dicho… #speaker:0
Estaba cegado por el níquel de Nueva York. Disculpe si fui grosero en su visita. Fui injusto con usted. #speaker:1
La injusticia nos mantiene despiertos. #speaker:0 
En mi mundo solo hay pesadillas. #speaker:1

*También en el mío. Pero uno se acostumbra a ellas. #speaker:0 
->Nivel_9_04
*Pesadilla o sueño, son todo mentiras. #speaker:0 
->Nivel_9_04


===Nivel_9_04===

¿Qué vamos a hacer ahora? #speaker:1
No comprendo esta ciudad. Habla un idioma que no entiendo. #speaker:1
La ciudad es un poema. El idioma es el de siempre. Solo ha cambiado el ritmo de sus versos. #speaker:0
No entiendo yo de versos.  #speaker:1
¡Lo mismo me dijo su hijo en Granada! #speaker:0
Es cierto, conocía a mi hijo. Pobre Phil… No le diga nada de esto, por favor. #speaker:1
Se entiende de versos si se entiende de respirar, de andar o pestañear. #speaker:0
No quiero ni andar ni respirar… #speaker:1
¡Pues pestañee! ¡Con ritmo! ¡Con gracia! ¡Con poesía! La poesía es el único lugar donde encontramos respuesta a las grietas del alma. #speaker:0
Una brújula para navegar en las tinieblas… #speaker:1
*Un abrazo cuando uno no quiere vida. #speaker:0 
->Nivel_9_05
*Va entendiéndome… #speaker:0
->Nivel_9_05


===Nivel_9_05===
Me he quedado absolutamente sin nada. ¿Para qué seguir viviendo? #speaker:1
Vivir es un acto de resistencia. Vivir es echarle un pulso a la muerte. Vivir es bailar en medio de la lluvia. #speaker:0
¿Qué debo hacer entonces? #speaker:1
Vivir, aunque solo sea por importunar a la muerte. Le digo esto pero, realmente, me lo digo a mí mismo… #speaker:0
Óigase de vez ven cuando. Suena bien lo que dice. #speaker:1
Hoy no es el día de tu partida, amigo. Y tampoco el mío. #speaker:0
Sigo sin saber qué debo hacer entonces. #speaker:1
*Sé poesía. ¡Siente la poesía dentro de ti! #speaker:0
->Nivel_9_06
*¡Sé un arma con la que apuntar al cielo! #speaker:0
->Nivel_9_06


===Nivel_9_06===
¡Ahhhhhhhhh! #speaker:1
Eso es. Grite más fuerte. #speaker:0 
¡AHHHHHHHHHHHH! #speaker:1
¡AHHHHHHHHHHHH! #speaker:0
Me siento mucho mejor. Pero me siento vacío. #speaker:1
Pinte en su lienzo. Coja un lápiz y un papel. Escriba. Que esa palabra sea la brújula que necesita. #speaker:0
¿Y después? #speaker:1
*Después de la primera palabra, vendrá la segunda. #speaker:0
->Nivel_9_07
*Después del primer día, venga el siguiente. #speaker:0
->Nivel_9_07

===Nivel_9_07===
Y así un poema… #speaker:1
Y así una vida… ¿Qué le parece si bajamos y brindamos por esta nueva vida? #speaker:0
Una idea estupenda. ¡Venga a casa! ¡Phil se alegrará de verlo! #speaker:1
Phil, americano perdido en Granada… #speaker:0
Ahora que caigo, creo que me contó que conoció a un extraño poeta en España… #speaker:1
¡Ese soy yo! #speaker:0
¡Que viva la gente extraña! #speaker:1
¡Que viva la vida! #speaker:0 

~NextLevel("none")
->END
// #take_item:obj_Pasaporte_key

// #give_item:obj_Pasaporte_sellado_key