-> PUERTA_LAVANDERIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_LAVANDERIA===
Deje sus manchas en nuestras manos. ¿Trae algo para limpiar? #speaker:1
No son las manchas lo que me preocupa... Busco algo oculto entre las telas. #speaker:0
Lo único que lavamos aquí son ropas. Ningún secreto se esconde entre las sábanas. #speaker:1
Busco un lugar donde el mundo sucio quede atrás... una fiesta, tal vez. #speaker:0
Lavamos suciedad, no rumores. ¡Policía! #speaker:1
->DoorFunctions

