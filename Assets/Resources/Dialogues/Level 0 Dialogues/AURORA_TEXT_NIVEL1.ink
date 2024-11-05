->INTRO
EXTERNAL SetNewSpeaker(string speakerName)
EXTERNAL PlayAudio(string audioName)
EXTERNAL NextLevel(string none)
===INTRO===
~SetNewSpeaker("Fernando")
El tiempo va sobre el viento flotando como un velero...
y el sueño va sobre el tiempo hundido hasta los cabellos. 
Uhm... esto me lo apunto para un futuro poema. #speaker:0  
¡Vaya sueño extraño que has tenido, Federico! 
Para una vez que consigues dormir en quince días...
Creo que el ruido de algo rompiéndose me despertó. #speaker:0

¡Federico! ¿Estás bien? Te oí gritar desde el pasillo y me asusté. #speaker:1
*¡Déjame dormir! ¡No me molestes!#speaker:0
->NIVEL_1_INTRO_01
*Sí, todo en orden. #speaker:0
->NIVEL_1_INTRO_01

=== NIVEL_1_INTRO_01===
¿Seguro? ¿Te encuentras bien? #speaker:1
Sí, creo que sí… ¿Dónde estoy? #speaker:0
¡Ja, ja, ja! Sí que te sentó mal la fiesta de anoche. ¿De veras nosabes dónde estás?  #speaker:1
*¿En un sueño dentro de un sueño?#speaker:0
->NIVEL_1_INTRO_02
*¿En una horrible pesadilla?#speaker:0
->NIVEL_1_INTRO_02

===NIVEL_1_INTRO_02===
Dime, Fernando, ¿por qué se mueve todo tanto? ¿Tan mareado estoy? #speaker:0
El capitán ha advertido que estamos atravesando una zona de nieblas y parece que el mar anda contento. #speaker:1
¿El mar? ¡Yo soy de secano! ¿Dónde me llevas, don Fernando? #speaker:0
¡Al centro del Universo! ¡A Nueva York!#speaker:1
Ah, creo que comienzo a recordar…#speaker:0
Pero, don Fernando, 
*¿Qué se me ha perdido en Nueva York? 
->NIVEL_1_INTRO_03
*¿No había otro sitio más cerca para perderme? 
->NIVEL_1_INTRO_03

===NIVEL_1_INTRO_03===
¿Por qué hablas de perderse? ¡Federico, usa tus alas para volar lejos y encontrarte!#speaker:1
*Federico tiene alas para volar, pero no para nadar.#speaker:0
->NIVEL_1_INTRO_04
*Federico le tiene miedo a las alturas. #speaker:0
->NIVEL_1_INTRO_04

===NIVEL_1_INTRO_04===
Federico solo necesita su máquina de escribir para volar. #speaker:1
Cierto es que esas teclas son las únicas que me permiten ser quien soy… Pero, ¡NO! ¡No puede ser! #speaker:0  

¿Qué ocurre? ¿Por qué gritas? #speaker:1 
¡Mi máquina de escribir! Mire, don Fernando. ¡Está rota! ¿Cómo ha podido suceder? #speaker:0 #invoke_event_ZoomInCameraTimelapseToTypewriter
->NIVEL_1_INTRO_05

===NIVEL_1_INTRO_05===
¡Cielos! Se ha debido romper con alguno de los vaivenes del barco.#speaker:1
¡Mire, don Fernando! ¡Han desaparecido todas las teclas!#speaker:0
No te preocupes, Federico. Seguro que en Nueva York encontrarás las teclas de tu máquina de escribir.#speaker:1
*¿En Nueva York? ¿Dónde?#speaker:0
->NIVEL_1_INTRO_06
*Imposible, seguro que no encuentro lo que necesito. #speaker:0
->NIVEL_1_INTRO_06

===NIVEL_1_INTRO_06===
Nueva York es el gran mercado del mundo.#speaker:1
En sus calles encontrarás todo lo que deseas. 
Federico, encuentra las 28 teclas de tu máquina de escribir. 
Cuando encuentres una tecla, colócala en su sitio hasta tenerlas todas. Échale un ojo de vez en cuando a tu máquina para ver cuáles te faltan por encontrar.
#invoke_event_ZoomOutCameraTimelapseToTypewriter

->NIVEL_1_INTRO_07

===NIVEL_1_INTRO_07===
No he bajado de este barco y ya me da usted deberes. #speaker:0
Buen amigo, prometí ser tu guía en este viaje y así lo cumpliré. Nueva York es el pasaporte a tu nueva felicidad, ya verás.
Aunque, hablando de pasaportes..., ¿Tienes ya tu pasaporte sellado? #speaker:1

*¿Cómo? ¿Sellado? ¿Pasaporte? ¿Qué?#speaker:0
->NIVEL_1_INTRO_08
*¡No sé ni dónde está! ¡Ahhh!#speaker:0
->NIVEL_1_INTRO_08

=== NIVEL_1_INTRO_08===
¡La promesa del futuro de la poesía española en peligro! ¡Ja, ja, ja!#speaker:1
No te preocupes, Federico. Aquí tengo tu pasaporte.#give_item:obj_pasaporte_key 

Para no tener problemas en la aduana del puerto, debes sellarlo antes de que atraquemos.#speaker:1
Federico,  busca al CAPITÁN del barco y pide que te lo selle. #speaker:1

Más deberes... ¡Pero si yo venía de vacaciones! #speaker:0

¡De eso nada! Ya verás como este viaje te cambia la vida, amigo mío. #speaker:1
Vamos, busca al CAPITÁN del barco y supera tu primer reto en esta gran aventura.#speaker:1
->END
