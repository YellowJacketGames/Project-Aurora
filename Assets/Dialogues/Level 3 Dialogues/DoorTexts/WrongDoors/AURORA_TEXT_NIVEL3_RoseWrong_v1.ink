->PUERTA_ROSA_NO_ORDEN
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===PUERTA_ROSA_NO_ORDEN===
¡Hola! ¿Es aquí la fiesta? Me he perdido… #speaker:0
Esto es una floristería. Estamos especializados en rosas. #speaker:1
Federico:
Deme una rosa que no busque ni ciencia ni sombra.#speaker:0
Una rosa confín de carne y sueño que busque otra cosa.
No he entendido nada de lo que ha dicho… ¡Policía! #speaker:1
->DoorFunctions