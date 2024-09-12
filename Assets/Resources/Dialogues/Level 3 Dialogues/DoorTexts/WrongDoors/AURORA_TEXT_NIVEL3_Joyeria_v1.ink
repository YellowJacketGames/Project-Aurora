-> PUERTA_JOYERIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_JOYERIA===
¡Joyas relucientes para adornar su vida! ¿Qué busca hoy? #speaker:1
Busco más que diamantes. Quizás un pasaje a algo... escondido. #speaker:0
Aquí solo hay joyas, nada más. Los verdaderos tesoros son difíciles de hallar. #speaker:1
Los verdaderos tesoros brillan más allá de las vitrinas. ¿Sabe de alguno... secreto? #speaker:0
No vendemos secretos. Esto es un lugar honrado. ¡Policía! #speaker:1
->DoorFunctions

