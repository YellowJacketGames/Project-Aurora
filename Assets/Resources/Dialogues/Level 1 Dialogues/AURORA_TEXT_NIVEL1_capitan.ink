//NIVEL 1: BARCO
//INTERACCIÓN CON CAPITAN
->NIVEL_1_INTRO

EXTERNAL CheckIfHasItem(string itemName)
EXTERNAL NextLevel(string none)

===NIVEL_1_INTRO===
No… ¡Lo sabía! Noche sin Luna… ¿Dónde estás, mi Luna de plata? ¿No me acompañas en este viaje? #speaker:0 
Noche sin Luna, noche de fortuna. #speaker:1
*No se meta en conversaciones ajenas. #speaker:0
->NIVEL_1_06
*Disculpe, no estaba hablando con usted. #speaker:0
->NIVEL_1_06


===NIVEL_1_06===
Bueno, en caso de que no haya subido hasta aquí para hablar conmigo, debería detenerle… #speaker:1
*¿Detenerme? ¿Por? ¡Ayuda! #speaker:0
->NIVEL_1_07
*Aquí tiene mis manos. Adelante. #speaker:0
->NIVEL_1_07


===NIVEL_1_07===
Los pasajeros no deben subir a la torre vigía… a no ser que sea por extrema necesidad… ¡o serán detenidos! #speaker:1 
¡Es extrema necesidad! He subido para buscar mi Luna de Plata. ¡Pero no me acompaña en este viaje! #speaker:0
En Nueva York no necesitará que la Luna le ilumine las noches. Las luces de los edificios hacen que Nueva York viva en un día eterno. ¡Nueva York es la ciudad que nunca duerme! #speaker:1
Quien no duerme, no sueña.#speaker:0
Quien no sueña, no ama. #speaker:0
Se equivoca, caballero. Para amar hay que estar despierto. ¿Disfrutaría usted de su Luna si estuviera dormido? #speaker:1
Pues ahora que lo dice… #speaker:0
Hago este viaje de Londres a Nueva York diez veces al año. Hay quien huye del hambre. Hay quien huye por temas políticos. Hay quien huye de su familia… Dígame, ¿de quién huye usted, joven? #speaker:1 
*Huyo de mí mismo. #speaker:0
->NIVEL_1_08
*Huyo de la vida. #speaker:0
->NIVEL_1_08


===NIVEL_1_08===
Su mirada me dice que huye por amor, pero no soy quién para juzgarle. No tenga miedo. Nueva York es el mejor lugar para escapar y comenzar de nuevo. Allí encontrará música, oportunidades, chicas hermosas… ¡Qué mas puede esperar! #speaker:1 
¿Quién le ha dicho que estoy interesado en ese tipo de cosas? #speaker:0
Nadie huye de buenas oportunidades y, la música, gusta a todo el mundo, por lo que entiendo que son las chicas las que no les interesa… #speaker:1 
*¿Y si es así, qué? #speaker:0
->NIVEL_1_09
*¡Yo no he dicho eso! #speaker:0
->NIVEL_1_09


===NIVEL_1_09===
Un consejo, joven: deje de enfadarse con el mundo. Sonría más. Abra los ojos. En Nueva York hay sitio para todo el mundo menos para el que no quiera divertirse. Hágame caso: piérdase por la ciudad y se encontrará a sí mismo. #speaker:1
No podré hacer nada de eso si no me sella mi pasaporte. #speaker:0
¡Ah! Ese es el motivo por el que subió hasta aquí, entonces. Deme el pasaporte, no hay problema. #take_item:obj_Pasaporte_key #speaker:1
¿Deseas algo más? #speaker:1
Devuélvame mi pasaporte sellado, por favor. #speaker:0
Si quiere su pasaporte, deberá responderme algunas preguntas… #speaker:1
Dice aquí que usted es… Mr. García Lorca. ¿A qué se dedica? #speaker:1
*Soy poeta. #speaker:0
->NIVEL_1_10_01
*Soy un perro andaluz. #speaker:0
->NIVEL_1_10_02


===NIVEL_1_10_01===
¿Poeta? ¿Eso es una profesión? ¿Qué hace un poeta? #speaker:1 
Ponerle nombre a las cosas que no lo tienen. #speaker:0
->NIVEL_1_11

===NIVEL_1_10_02===
¿Cómo que un perro andaluz? ¡Ja, ja! #speaker:1
Al menos eso dicen mis amigos… Soy un perro andaluz sin nombre. #speaker:0
->NIVEL_1_11


===NIVEL_1_11===
Uhm… Interesante… Hablando de nombres… #speaker:1
¿Sabe el nombre del barco en el que viaja? #speaker:1
Esta me la sé… #speaker:0 
*El… ¿Titanic? #speaker:0
->NIVEL_1_12_01
*El… ¿Olympic? #speaker:0
->NIVEL_1_12_02


===NIVEL_1_12_01===
¡Pero, muchacho! El Titanic se hundió por estas aguas hace 17 años. #speaker:1
->NIVEL_1_13

===NIVEL_1_12_02===
¡Bravo! El Olympic, el hermano gemelo del Titanic… Espero que no suframos el mismo destino. ¿Sabes que se hundió justo por estas aguas? #speaker:1
->NIVEL_1_13

===NIVEL_1_13===
Hundirse…, morirse de frío por fuera y por dentro… #speaker:0 
Ya salió a flote el poeta… Una última pregunta, joven: #speaker:1
¿Sabes qué barrio de Nueva York es la cuna del jazz? #speaker:1
Ehm… ¿el qué? #speaker:0
*¿Quién es jazz? #speaker:0
->NIVEL_1_14
*¿Jazz? ¿Se come? #speaker:0 
->NIVEL_1_14


===NIVEL_1_14===
Para encontrarse, tendrá que perderse primero. Y, si quiere perderse, Harlem es el lugar al que debe ir en Nueva York. #speaker:1
¿Harlem? #speaker:0
El paraíso de los negros. La cuna del jazz. El lugar donde encontrará lo que busca, aunque no sepa aún qué está buscando.#speaker:1  
Tome… #speaker:1 #give_item:obj_PasaporteSellado_key 

¡Mi pasaporte sellado! #speaker:0 #trigger_achievement:ACH_1

Se lo ha ganado por aguantar las preguntas de este viejo capitán. Un consejo: cambie su fotografía cuanto antes… #speaker:1
Sí… Ese retrato bordea la luz del asesinato y la esquina nocturna donde el ladrón espera para robar carteras. #speaker:0
¡Bravo, poeta! ¡Magnífica descripción! Pero no hable de asesinos ni carteristas cuando pase el control en el puerto… Hágame caso… #speaker:1
¡Vaya! ¡Hemos llegado! #speaker:1  #play_sound:Boat_sound
¡Tierra! ¡Nueva York a la vista! #speaker:1
Mire, Mr. García Lorca. Llegamos a Nueva York justo al amanecer. #speaker:1

¿Eso es Nueva York? #speaker:0
¿No oye esa voz que le llama? ¡Es América! #speaker:1
Adiós, campanas de la catedral de Granada. Ya me llaman. Son las sirenas. Son las sirenas de Nueva York. #speaker:0
Mr. Garcia Lorca, ¡bienvenido al Nuevo Mundo! 
¡Bienvenido a Nueva York! #speaker:1 
~NextLevel("none")
->END



// #take_item:obj_Pasaporte_key

// #give_item:obj_Pasaporte_sellado_key