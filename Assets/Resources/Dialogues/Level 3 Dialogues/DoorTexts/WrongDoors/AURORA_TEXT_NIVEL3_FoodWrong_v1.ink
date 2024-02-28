-> PUERTA_COMIDA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_COMIDA===
¡Hola! ¿Es aquí la fiesta clandestina? #speaker:0
No, aquí solo servimos comida. #speaker:1
¿Y tienen piononos?#speaker:0
¿Pio qué? ¡Policía!#speaker:1
->DoorFunctions



