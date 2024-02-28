->NIVEL_3_CAFETERA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_CAFETERA===
¡Hola! ¿La fiesta es en este café? #speaker:0
Solo y en silencio se toma el café en este café.#speaker:1
En el café de Chinitas #speaker:0
dijo Paquiro a su hermano:
“Soy más valiente que tú,
más torero y más gitano”.
Le he dicho que el café lo tomamos…¡EN SILENCIO!#speaker:1
¡Policía!
->DoorFunctions

