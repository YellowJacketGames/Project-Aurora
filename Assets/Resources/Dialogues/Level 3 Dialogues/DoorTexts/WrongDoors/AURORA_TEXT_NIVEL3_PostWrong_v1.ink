->NIVEL_3_SELLOS
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_SELLOS===
Buenas tardes, ¿desea enviar una carta o una postal? #speaker:1
Oh, ahora que lo dice, me encantaría… #speaker:0
¿Destino?#speaker:1
Mi Granada del alma.#speaker:0
Son treinta y cinco centavos.#speaker:1
Ehm… no tengo dinero…#speaker:0
Pues ya sabe… ¡Policía!#speaker:1
->DoorFunctions
