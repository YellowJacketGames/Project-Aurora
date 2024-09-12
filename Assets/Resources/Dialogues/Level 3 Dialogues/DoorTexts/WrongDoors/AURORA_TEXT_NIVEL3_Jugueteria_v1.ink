-> PUERTA_JUGUETERIA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END

===PUERTA_JUGUETERIA===
Juguetes para todas las edades. ¿Busca algo especial? #speaker:1
No busco juguetes, sino un lugar donde la infancia nunca termine. #speaker:0
Las fiestas infantiles no son nuestro asunto. Aquí solo vendemos lo que ve. #speaker:1
Algunas fiestas no se celebran bajo la luz del día... ¿No sabrá de ninguna? #speaker:0
Eso no parece apropiado. ¡Policía! #speaker:1
->DoorFunctions

