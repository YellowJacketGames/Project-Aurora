-> PUERTA_PESCADERIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_PESCADERIA===
Pescado fresco, recién traído del océano. ¿Le interesa algo? #speaker:1
No busco lo que se pesca, sino lo que queda oculto bajo las aguas. #speaker:0
Aquí no hay secretos, solo peces que salen a la luz. #speaker:1
Algunos secretos flotan a la deriva, esperando ser encontrados en las sombras de la noche... #speaker:0
No jugamos con sombras ni secretos. ¡Policía! #speaker:1
->DoorFunctions

