->PUERTA_LUNA_NO_ORDEN
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END



===PUERTA_LUNA_NO_ORDEN===
¡Hola! ¿Puedo pasar a la fiesta? #speaker:0
No, aquí solo vendemos lunas. #speaker:1
Deme una luna para ir a la fragua con su polisón de nardos. #speaker:0
¡Pero qué grosero! ¡Policía! #speaker:1
->DoorFunctions