->PUERTA_COCHE_NO_ORDEN
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===PUERTA_COCHE_NO_ORDEN===
¡Hola, soy Federico! Estoy invitado a la fiesta por…#speaker:0
¡Shissst! Las fiestas están prohibidas. Váyase, amigo… #speaker:1
Gracias por no llamar a la policía. #speaker:0
Ah, es verdad… ¡Policía!#speaker:1
->DoorFunctions
