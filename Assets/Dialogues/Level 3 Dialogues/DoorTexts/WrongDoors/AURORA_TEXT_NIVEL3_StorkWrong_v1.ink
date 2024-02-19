->NIVEL_3_CIGUENA_NO_ORDEN
EXTERNAL ResetDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL PlayAudio(string audioName)
~PlayAudio("PoliceSiren")
==DoorFunctions==
~ResetDoors("none")
~BeginTransition("none")
~PlayAudio("PoliceSiren")
->END


===NIVEL_3_CIGUENA_NO_ORDEN===
Bienvenido a la tienda de ropa de bebé Miss Watson.#speaker:1
Hola, señorita. ¿Podría usted indicarme dónde se celebra una fiesta? #speaker:0
En Coney Island todos los días es una fiesta. ¡Y puede vestir a sus hijos con nuestros vestidos!#speaker:1
Déjelo, ya lo digo yo… ¡Policía!#speaker:0
->DoorFunctions

