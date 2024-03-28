->NIVEL4_INTRO
EXTERNAL SetNewSpeaker(string speakerName)
EXTERNAL GoToNextObjective(string none)
EXTERNAL BeginRace(string none)
EXTERNAL CallEvent(int eventIndex)

===TestFunctions===
~BeginRace("none")
->END
===NIVEL4_INTRO===
~SetNewSpeaker("Fernando")
¡Federico! Menos mal que has conseguido subir hasta la azotea.#speaker:1
¿Por qué? ¿Qué ocurre, don Fernando?#speaker:0
¡La Ley seca! ¡Ha llegado la policía de Nueva York!#speaker:1 
*¿Cómo? ¿La policía? ¡Socorro!#speaker:0
->NIVEL4_01
*¡Me da igual! ¡Yo soy Federico García Lorca!#speaker:0 
->NIVEL4_01

===NIVEL4_01===
¡No grites, Federico!#speaker:1 
*El grito deja en el viento una sombra de ciprés.#speaker:0 
->NIVEL4_02
*No grito. Todo se ha roto en el mundo; solo queda silencio.#speaker:0 
->NIVEL4_02

===NIVEL4_02===
Parece que la noche te ha alegrado, Federico…#speaker:1 
Mi corazón está vacío. No soy un poeta alegre.#speaker:0 
Soy un poeta perdido en Nueva York. 
No hay tiempo para lamentos. La policía va a subir de un momento a otro.#speaker:1 
No deben encontrarte aquí. 
*¿Huir? ¿Por qué? ¡No hice nada!#speaker:0
->NIVEL4_03
*¿Por las azoteas? ¡Tengo vértigo!#speaker:0 
->NIVEL4_03

===NIVEL4_03===
¡Están subiendo, Federico!#speaker:1 
Date prisa. Intenta llegar al final antes de que te detenga la policía. 
~CallEvent(1)
->NIVEL4_04

===NIVEL4_04===
Lorca perseguido por las azoteas de Nueva York…#speaker:0 
La noche llama temblando
al cristal de los balcones,
perseguido por los mil 
perros que no lo conocen.
Y un olor de vino y ámbar
viene de los corredores. 
Vamos, Federico, no es momento de recitar. ¡Corre!
->TestFunctions
