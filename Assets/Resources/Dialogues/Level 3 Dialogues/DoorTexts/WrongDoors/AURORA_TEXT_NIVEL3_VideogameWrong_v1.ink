->NIVEL_3_VIDEOGAME
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_VIDEOGAME===
Ey, bro, ¿qué tal? #speaker:1
Ehm… ¿qué se vende aquí?#speaker:0
No lo sabría describir, porque la palabra aún no se ha inventado. #speaker:1
Intente explicarlo.#speaker:0
Algo así como “regocijos ociosos para el alma que conectan la realidad con un mundo inexistente”. #speaker:1
¡Vende libros!#speaker:0
No, tome esto, que se lo ha ganado por llegar hasta lo más alto.#speaker:1//Give tecla B
¡Oh, gracias! #speaker:0
Y ahora, ya sabe lo que le espera, ¿no? #speaker:1
Sí, déjeme a mí decirlo… ¡Policía!#speaker:0
->DoorFunctions
