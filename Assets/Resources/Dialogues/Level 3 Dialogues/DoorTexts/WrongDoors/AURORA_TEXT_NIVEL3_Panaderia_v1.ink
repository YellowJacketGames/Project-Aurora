-> PUERTA_PANADERIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_PANADERIA===
El pan del día está recién horneado. ¿Le interesa algo? #speaker:1
Busco algo más que pan. Tal vez un lugar donde la noche sea más... viva. #speaker:0
Aquí solo se sirven hogazas, nada más. No sé de qué habla. #speaker:1
Busco una fiesta donde el pan se reparta con su vino, bajo luces clandestinas. #speaker:0
Eso suena peligroso... ¡Policía! #speaker:1
->DoorFunctions

