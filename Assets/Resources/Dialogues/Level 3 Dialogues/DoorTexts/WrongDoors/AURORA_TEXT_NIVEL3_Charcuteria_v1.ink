-> PUERTA_CHARCUTERIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_CHARCUTERIA===
Tenemos los mejores embutidos de la ciudad. ¿Qué le apetece? #speaker:1
Busco más bien un lugar donde el festín sea de otro tipo. #speaker:0
Aquí solo hay carnes curadas, no fiestas. No se equivoque de lugar. #speaker:1
Busco un sitio donde el festín sea en silencio, en las sombras... #speaker:0
No nos metemos en esos asuntos. ¡Policía! #speaker:1
->DoorFunctions

