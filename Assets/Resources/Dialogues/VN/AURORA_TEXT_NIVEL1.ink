->INTRO
EXTERNAL SetNewSpeaker(string speakerName)
EXTERNAL PlayAudio(string audioName)
EXTERNAL NextLevel(string none)
===INTRO===
~SetNewSpeaker("Fernando")
¡Federico! ¿Estás bien? Te oí gritar desde el pasillo y me asusté. #speaker:1
*¡Déjame dormir! ¡No me molestes!#speaker:0
->NIVEL_1_INTRO_01
*Sí, todo en orden. #speaker:0
->NIVEL_1_INTRO_01

=== NIVEL_1_INTRO_01===
¿Seguro? ¿Te encuentras bien? #speaker:1
Sí, creo que sí… ¿Dónde estoy? #speaker:0
¡Ja, ja, ja! Sí que te sentó mal la copa de anoche. ¿De veras no sabes dónde estás? #speaker:1
*¿En un sueño dentro de un sueño?#speaker:0
->NIVEL_1_INTRO_02
*¿En una horrible pesadilla?#speaker:0
->NIVEL_1_INTRO_02

===NIVEL_1_INTRO_02===
Dime, Fernando, ¿por qué se mueve todo tanto? ¿Tan mareado estoy? #speaker:0
El capitán ha advertido que estamos atravesando una zona de nieblas y parece que el mar anda contento. #speaker:1
¿El mar? ¡Yo soy de secano! ¿Dónde me llevas, don Fernando? #speaker:0
¡Al centro del Universo! ¡A Nueva York!#speaker:1
[Ah, creo que comienzo a recordar…]#speaker:0
Pero, don Fernando, 
*¿Qué se me ha perdido en Nueva York? 
->NIVEL_1_INTRO_03
*¿No había otro sitio más cerca para perderme? 
->NIVEL_1_INTRO_03

===NIVEL_1_INTRO_03===
¿Por qué hablas de perderse? ¡Federico, usa tus alas para volar lejos y encontrarte! #speaker:1
*Federico tiene alas para volar, pero no para nadar.#speaker:0
->NIVEL_1_INTRO_04
*Federico le tiene miedo a las alturas. #speaker:0
->NIVEL_1_INTRO_04

===NIVEL_1_INTRO_04===
Federico solo necesita valor para llegar alto, ya sea por mar o por aire. #speaker:1
Federico solo necesita su máquina de escribir para volar.
Cierto es que esas teclas son las únicas que me permiten ser quien soy… Pero, ¡NO! ¡No puede ser! #speaker:0
¿Qué ocurre? ¿Por qué gritas?#speaker:1
¡Mi máquina de escribir! Mire, don Fernando. ¡Está rota! ¿Cómo ha podido suceder?#speaker:0//vision_maquina
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
->NIVEL_1_INTRO_07

===NIVEL_1_INTRO_07===
No he bajado de este barco y ya me da usted deberes. #speaker:0
Buen amigo, prometí ser tu guía en este viaje y así lo cumpliré.#speaker:1
No tengas miedo de lo desconocido, Federico. 
*No es miedo, es ansia de futuro.#speaker:0
->NIVEL_1_INTRO_08
*No es miedo, es terror de mí mismo.#speaker:0
->NIVEL_1_INTRO_08

=== NIVEL_1_INTRO_08===
¡El gran poeta de España no puede temerle a nada ni a nadie! No sabes cuántas aventuras te esperan en Nueva York…#speaker:1
Solo quiero perderme en sus jardines y sus selvas. ¡El paraíso prometido!#speaker:0
¡Ja, ja, ja!#speaker:1
Más bien encontrarás el infierno. Pero, créeme, terminarás sintiéndote muy cómodo entre sus llamas. 
No son llamas… Es…#speaker:0
Es esta luz, este fuego que devora.
Este paisaje gris que me rodea.
Este dolor por una sola idea.
Esta angustia de cielo, mundo y hora.
¡Bravo! ¡Federico, promesa del futuro de la poesía!#speaker:1
Me estima usted en demasía.#speaker:0
Como el resto del mundo. En Nueva York no te conocen y ya te quieren, Federico. ¡Menuda fiesta sorpresa te tienen preparada!#speaker:1
*Si es sorpresa, ¿no debería de no saberlo?#speaker:0
->NIVEL_1_INTRO_09
*¿Una fiesta? ¿Para mí? ¿Por qué?#speaker:0
->NIVEL_1_INTRO_09


===NIVEL_1_INTRO_09===
¡Tiempo al tiempo, Federico!#speaker:1
Queda poco para atracar en Nueva York.
Toma, aquí tienes tu pasaporte, sellado por el capitán. Así no tendrás ningún problema cuando lleguemos al puerto. #give_item:obj_pasaporte_key
->NIVEL_1_INTRO_10

===NIVEL_1_INTRO_10===
¡Pero qué retrato! Bordea la luz del asesinato y la esquina nocturna donde el carterista guarda el bajo de billetes.#speaker:0
¡Magnífica descripción, Federico! Pero no hables de asesinos ni carteristas cuando pasemos el control en el puerto…
~PlayAudio("Boat")
¡Federico! Ya estamos llegando a Nueva York. Recuerda: encuentra las teclas de tu máquina de escribir y explora cada rincón de la ciudad de Nueva York. Toma, aquí tienes un mapa, para que no te pierdas.#speaker:1
Ahí podrás ver todos los rincones que te quedan por descubrir en la ciudad. ¡Vamos, Federico! Recoge tus cosas. ¡Te veo en el puerto!
Ya las oigo. Es su voz.#speaker:0
Adiós, campanas de la catedral. Ya me llaman. Son las sirenas. Son las sirenas de la ciudad. Comienzo este viaje sin saber quién soy. ¿Qué me deparará el destino en Nueva York?  
¡Allá voy, Nueva York! 
~NextLevel("none")
->END
