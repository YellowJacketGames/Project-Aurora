->NIVEL_3_TACONES
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END



===NIVEL_3_TACONES===
Buenas tardes, ¿qué desea? #speaker:1
Ehm… imaginará qué es lo que quiero… #speaker:0
¿Un par de zapatos? #speaker:1
No necesito zapatos para andar. Ya mi corazón tendría la forma de un zapato si cada aldea tuviera una sirena.#speaker:0
¿Ha dicho usted “sirena”? ¡Policía! #speaker:1
->DoorFunctions
