->NIVEL_3_PIPA
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_PIPA===
Buenas tardes, bienvenido a la Casa de la Pipa. ¿En qué puedo ayudarle? #speaker:1
Cojos perros fumaban sus pipas y un olor de cuero caliente#speaker:0
Ponía grises los labios redondos de los que vomitaban en las esquinas. 
Entiendo. Eso significa que desea ir a una fiesta clandestina, ¿verdad?#speaker:1
¡Sí! ¡Sí! ¡Sí! ¿Sabría decirme por dónde se va?#speaker:0
Por ahí… ¡Policía!#speaker:1
->DoorFunctions

