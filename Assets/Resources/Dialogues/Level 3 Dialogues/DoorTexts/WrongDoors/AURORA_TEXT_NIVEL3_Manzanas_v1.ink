-> PUERTA_MANZANA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_MANZANA===
¡Bienvenido a nuestra frutería! ¿Desea alguna cosa? #speaker:1
Lo cierto es que sí, aunque no frutas... ¿No sabrá de ninguna fiesta secreta, verdad? #speaker:0
Lo lamento, aquí lo más parecido a una fiesta es cuando nos llegan manzanas frescas...#speaker:1
Manzanas de caramelo, con guardias civiles de plomo...#speaker:0
Ahora que lo dice... ¡Policía!#speaker:1
->DoorFunctions

