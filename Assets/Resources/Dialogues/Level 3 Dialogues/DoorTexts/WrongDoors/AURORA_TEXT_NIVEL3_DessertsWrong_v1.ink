->PUERTA_PASTELERIA

EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_PASTELERIA===
¡Hola! ¿La entrada a la fiesta clandestina?#speaker:0
Esto es un negocio honrado de pasteles.#speaker:1
En ese caso, me gustaría un torta por favor.#speaker:0
¿Una torta dice? Ahora mismo se la dan. ¡Policía!#speaker:1
->DoorFunctions