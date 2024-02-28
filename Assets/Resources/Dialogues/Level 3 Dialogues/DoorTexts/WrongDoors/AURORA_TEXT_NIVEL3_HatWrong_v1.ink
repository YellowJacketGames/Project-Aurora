->PUERTA_SOMBRERO

EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===PUERTA_SOMBRERO===
¡Hola! ¿Puedo pasar a la fiesta clandestina?#speaker:0
Esto es una tienda de sombreros. #speaker:1 
Deme uno, que el mío…#speaker:0
¿Qué le ocurre a su sombrero?#speaker:1
No tengo. Porque de amor me duele el aire, el corazón y el sombrero que no tengo. #speaker:0  
Si le duele la azotea, cómprese una aspirina y no moleste… ¡Policía! #speaker:1
->DoorFunctions

