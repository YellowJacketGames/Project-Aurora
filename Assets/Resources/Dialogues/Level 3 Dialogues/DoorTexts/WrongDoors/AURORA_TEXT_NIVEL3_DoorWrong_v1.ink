EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_LIBRO===
¡Hola! ¿Es aquí la fiesta clandestina? #speaker:0
No, aquí no es. Cuidado, joven, si la policía te pilla… ¡Auch! ¡Policía!#speaker:1
-> END

->DoorFunctions

