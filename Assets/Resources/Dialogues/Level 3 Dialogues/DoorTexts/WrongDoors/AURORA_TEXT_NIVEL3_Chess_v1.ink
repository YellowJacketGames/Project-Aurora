-> PUERTA_AJEDREZ
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_AJEDREZ===
¡Hola! ¿En este club hacéis eventos "especiales"? #speaker:0
No sé a qué se refiere joven, aquí solo jugamos al ajedrez. #speaker:1
Ya sabe... eventos clandestinos.#speaker:0
¿Clandestinos? ¡Pero bueno! Aquí lo único clandestino que hay es nuestra defensa siciliana. ¡Policía!#speaker:1
->DoorFunctions

