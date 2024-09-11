->NIVEL_3_CATS_WRONG

EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_CATS_WRONG===
Hola, ¿es aquí el evento?#speaker:0
¡Como no se refiera a adoptar mininos no, no es aquí! #speaker:1
Lo lamento, me habré confundido.#speaker:0
¡Y tanto que sí, Policía!#speaker:1
->DoorFunctions