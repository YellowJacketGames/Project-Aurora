->NIVEL_3_CINE

EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===NIVEL_3_CINE===
Hola, ¿es aquí…? #speaker:0
Shist…la película ya ha comenzado. #speaker:1
Ah, perdone… ¿Qué proyectan? #speaker:0
"Un perro andaluz”.#speaker:1
¡Maldito Dalí!¡Salvador, deja de reírte de mí! #speaker:0
¡A Dalí no me lo toque usted, que es muy universal! #speaker:1
¡Policía! 
->DoorFunctions

