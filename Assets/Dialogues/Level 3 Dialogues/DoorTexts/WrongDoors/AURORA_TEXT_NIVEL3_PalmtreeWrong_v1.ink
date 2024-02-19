->NIVEL_3_PALMERA_NO_ORDEN
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_PALMERA_NO_ORDEN===
Estoy cansado de subir y bajar… Dígame que es aquí la fiesta, por favor. #speaker:0
Es aquí la fiesta. #speaker:1
¿De veras? ¡Gracias! ¿Puedo pasar?#speaker:0
¿A mi tienda? ¡Claro que no puede!#speaker:1
Pero si me ha dicho que es aquí la fiesta.#speaker:0
Yo solo le he dicho lo que usted me ha pedido que le dijera. ¡Policía!#speaker:1
->DoorFunctions

