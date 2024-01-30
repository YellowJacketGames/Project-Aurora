//NIVEL 2: PUERTO
//INTERACCIÓN CON VENDEDOR_PERIODICOS
EXTERNAL CheckIfHasItem(string itemName)
EXTERNAL CheckIfHasQuest(int questIndex)
VAR hasQuest = false

~CheckIfHasQuest(4)
{hasQuest:
    ->2DialogoHenry
    
  - else:
    ->2DialogoHenryNoPeriodico
}


===2DialogoHenryNoPeriodico===
¡Extra! ¡Extra! #speaker:1
La situación de la Bolsa comienza a complicarse.
¡Hola! #speaker:0
¡Extra! ¡Extra! #speaker:1
Se precisan trabajadores para la culminación del Chrysler!. 
Perdona, joven... #speaker:0
¿Un periódico, sir? Son diez centavos. #speaker:1
*No tengo dinero, chico. #speaker:0
->2DialogoHenryNoPeriodico01
*No quiero un periódico... #speaker:0
->2DialogoHenryNoPeriodico01

===2DialogoHenryNoPeriodico01===
Sir, no me haga perder el tiempo.Tengo mucho trabajo. #speaker:1
¡Extra! ¡Extra! 
La situación de la Bolsa comienza a complicarse.
¡Extra! ¡Extra! 
->END


//NIVEL 2: PUERTO
//INTERACCIÓN CON VENDEDOR_PERIODICOS

//DIALOGO ACTIVADA MISION #PERIODICO 


===2DialogoHenry===
¡Extra! ¡Extra! #speaker:1
El Edificio Chrysler será el edificio más alto del mundo.
¡Hola! #speaker:0
¡Extra! ¡Extra! #speaker:1
Todo preparado para el combate entre Schmeling y Pauline de esta noche.
Perdona, joven... #speaker:0
¿Un periódico, sir? #speaker:1
*[(¡Dios! ¡Qué hermosos son estos americanos...!)] #speaker:0  //Thought
Pero, chico, ¿qué ángel llevas oculto en la mejilla?
->2DialogoHenryAvance0 
*Eh, sí. Dame un ejemplar de hoy, por favor. 
->2DialogoHenryAvance1

===2DialogoHenryAvance0===
¿Sir? ¿Qué quiere decir? ¿Tengo alguna mancha en la cara? #speaker:1
*Ojalá fuera eso... Solo rubor de hombre mancha tu blanca tez. #speaker:0
->2DialogoHenryAvance2
*No, disculpa. Era solo un verso libre que te dedicaba. #speaker:0
->2DialogoHenryAvance2

===2DialogoHenryAvance2===
Ups, eh...esto...¿Debo decir...Gracias? #speaker:1

¡Ja, ja, ja! ¿Por qué titubeas? #speaker:0
Ese ligero temblor de escarcha,
la ausencia de tu boca está marcando. 

¡Auch! ¿Qué le pasa ahora a mi boca? #speaker:1

Tranquilo, joven, no hagas caso a este poeta perdido. #speaker:0

¡Así que poeta! Ahora lo entiendo... #speaker:1
¡Extra! ¡Extra! ¡Un poeta anda perdido en Nueva York!
Debe saber que en Nueva York no hay sitio para la poesía. 

*Pero, ¿en qué momento hemos comenzado esta apasionada lucha de versos? #speaker:0
->2DialogoHenryAvance3
*Amigo, no nos perdamos entre palabras... #speaker:0
->2DialogoHenryAvance3

===2DialogoHenryAvance3===
Dime, guía de este poeta perdido... ¿Tienes nombre? #speaker:0

Henry, sir. #speaker:1

Henry... ¡Enrique! Ay, mi Enrique, mi guía por el mundo de las camas, #speaker:0
Enrique, guía por el mundo de los muertos y los periódicos abandonados. 

¡Qué extraño idioma hablan los poetas en su tierra, sir! #speaker:1
Dígame, ¿puedo hacer algo por usted, señor...?

*Don Federico, a tu servicio. #speaker:0
->2DialogoHenryAvance4
*Llámame "Extra" para que grites mi nombre a cada segundo... #speaker:0
->2DialogoHenryAvance5


===2DialogoHenryAvance5===
...y para llenar de palabras mi locura. #speaker:0
O déjame vivir en mi serena noche
del alma para siempre oscura.
O también puedes llamarme simplemente Federico...
->2DialogoHenryAvance4

===2DialogoHenryAvance4===

Federico, parece que el tiempo se ha parado al hablar con usted, pero... #speaker:1

*Contigo. Por favor, tutéame. #speaker:0
->2DialogoHenryAvance6
*O será que se te ha parado el reloj, chico... #speaker:0
->2DialogoHenryAvance6

===2DialogoHenryAvance6===
Lo cierto es que aún me quedan periódicos por vender y... #speaker:1
*Precisamente por eso me acerqué a ti. #speaker:0
->2DialogoHenryAvance7
*¡Por la Alhambra! Es cierto, ¡el periódico! #speaker:0
->2DialogoHenryAvance7

===2DialogoHenryAvance7===
Necesito un periódico de hoy, mi querido Enrique... #speaker:0
Pero acabo de llegar a Nueva York y no tengo monedas...
*¿Podrías regalarme uno a cambio de escribirte un poema?
->2DialogoHenryAvance8
*Dame un periódico y te invito a una copa esta noche.#speaker:0
->2DialogoHenryAvance8

===2DialogoHenryAvance8===
¿Qué te parece el trato? #speaker:0
->2DialogoHenryAvance9


===2DialogoHenryAvance9===
Espero que los poetas cumplan su palabra... #give_Item:obj_Periodico_key #speaker:1
¡Voz que despierta a Nueva York de su mañana fría, #speaker:0
si volvemos a vernos, darás a mi oído gozo y alegría!
Adiós, poeta perdido. #speaker:1
¡Extra, extra!
El Edificio Chrysler será el edificio más alto del mundo.
¡Extra, extra!
Mmm... Nueva York, ¡qué me deparará esta ciudad! #speaker:0
->END


===2DialogoHenryAvance1===
Son diez centavos, señor. #speaker:1
*Ehm... Lo cierto es que no tengo dinero... #speaker:0
->2DialogoHenryAvance101
*¿Diez centavos? ¡Es muy caro! #speaker:0
->2DialogoHenryAvance102

===2DialogoHenryAvance101===
Es fácil, señor. Si no hay dólares, no hay periódicos. #speaker:1
Esa es la ley de Nueva York. 
¿Y qué le parece si cambiamos juntos esa ley? #speaker:0
¿A qué se refiere? #speaker:1
Si fuera pescadero, le pagaría con pescado. #speaker:0
Si fuera panadero, podría darle un panecillo a cambio.
Pero soy poeta... 
¿No pensará pagarme con poemas, verdad? #speaker:1
Dígame una palabra, cualquiera, y prometo regalarle una ristra de versos a cambio. Si queda satisfecho, creo que mereceré uno de sus periódicos. #speaker:0
No hay trato, sir. Entiéndame: póngase en mis zapatos... #speaker:1
*¿Zapato? ¡De acuerdo! Mi corazón tendría la forma de un zapato... #speaker:0
->2DialogoHenryAvance1010
*¿Zapato? Mmm... ¿Quién te compra zapatera el paño de tus vestidos...? #speaker:0

->2DialogoHenryAvance1011


===2DialogoHenryAvance1010===
...si cada aldea tuviera una sirena.
¿Y para qué quiere una aldea tener una sirena? #speaker:1
*¿Acaso no eres tú sirena que atrae a los viandantes para vender tus periódicos? #speaker:0
->2DialogoHenryAvance1012
*¿Para que los hombres no se pierdan camino a casa? #speaker:0
->2DialogoHenryAvance1012

==2DialogoHenryAvance1011===
...¿y esas chambas de batista con encaje de bolillos? #speaker:0
¿Quién es esa zapatera? #speaker:1
Una mujer prodigiosa, de la que tendré el gusto de hablarte...si me das ese periódico que necesito... #speaker:0
->2DialogoHenryAvance1012

===2DialogoHenryAvance1012===
Mmm... está a punto de convencerme... #speaker:1
Pero nada es gratis en Nueva York, amigo.
*Prometo devolverte esa moneda cuando volvamos a vernos. #speaker:0
Porque nos veremos, te lo aseguro. 
->2DialogoHenryAvance1013
*Enrique, dame esa moneda y haré que seas eterno. #speaker:0
Ayúdame y serás recordado para siempre.
->2DialogoHenryAvance1013

===2DialogoHenryAvance1013===

Espero que los poetas cumplan su palabra... #give_Item:obj_Periodico_key #speaker:1
¡Voz que despierta a Nueva York de su mañana fría, #speaker:0
si volvemos a vernos, darás a mi oído gozo y alegría!
Adiós, poeta perdido. #speaker:1
¡Extra, extra!
El Edificio Chrysler será el edificio más alto del mundo.
¡Extra, extra!
Mmm... Nueva York, ¡qué me deparará esta ciudad! #speaker:0
->END


===2DialogoHenryAvance102===
Todo es caro si no tiene ni una sola moneda, sir. #speaker:1
¿Que no tengo monedas? ¡Cómo se atreve a...! #speaker:0
Sir, si no tiene monedas, al menos, debería ser amable. #speaker:1
*¿Me está amenazando? #speaker:0
->2DialogoHenryAvance103
*¿Me está diciendo cómo debo ser? #speaker:0
->2DialogoHenryAvance103

===2DialogoHenryAvance103===
No; es solo un consejo de un desconocido. #speaker:1
Ahora, déjeme.
¡Extra, extra!
->2DialogoHenry



 

