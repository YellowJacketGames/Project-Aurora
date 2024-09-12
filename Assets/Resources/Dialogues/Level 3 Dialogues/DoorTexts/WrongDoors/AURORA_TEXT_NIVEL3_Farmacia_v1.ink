-> PUERTA_FARMACIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_FARMACIA===
Bienvenido a la farmacia. ¿Busca algún remedio? #speaker:1
Remedios para el alma, quizás. Aunque ahora busco algo más... ¿Sabe de alguna fiesta? #speaker:0
No tenemos medicinas para esos males. Aquí solo hay calmantes para el cuerpo. #speaker:1
Lo que busco no se cura con pastillas. Tal vez alguien haya mencionado algo… secreto. #speaker:0
Los secretos son peligrosos. Aquí no los vendemos. ¡Policía! #speaker:1
->DoorFunctions

