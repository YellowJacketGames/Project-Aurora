->NIVEL_3_CARTAS

EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_CARTAS===
Hola, he visto los naipes sobre la puerta. ¡La fiesta clandestina es aquí! ¿Verdad?#speaker:0
¿Contraseña?#speaker:1
*Ehm… Sí, me la sé: Nuestra amistad es un juego de naipes.#speaker:0
->NIVEL_3_CARTAS_01
*Ehm… ¿Salud y República?#speaker:0
->NIVEL_3_CARTAS_01

===NIVEL_3_CARTAS_01===
¡Correcto! Siéntate y juega con nosotros una partida.#speaker:1
¡Gracias, señor! Pero, ¿podré pasar a la fiesta?#speaker:0
Claro, después de pasar por comisaría. ¡Policía!#speaker:1
->DoorFunctions
