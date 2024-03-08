->PUERTA

EXTERNAL AdvanceDoors(string none)
EXTERNAL BeginTransition(string none)
EXTERNAL SetNewSpeaker(string newSpeaker)
EXTERNAL NextLevel(string none)
==DoorFunctions==
~BeginTransition("none")
~AdvanceDoors("none")
->END

===PUERTA===
~SetNewSpeaker("Henry")
¡Hola! ¿Es aquí la fiesta clandestina?#speaker:0
¡Federico! ¡Has conseguido llegar!#speaker:1
Pero, Henry, ¿qué haces aquí?#speaker:0
¿Qué esperabas? ¿Que estuviera todo el día vendiendo periódicos?#speaker:1
¡Toda Nueva York está en esta fiesta! ¡Ven!
Tú me arrastras y voy. Tü me dices que me vuelva y te sigo por el aire como una brizna de hierba.#speaker:0
¡No es momento de palabras, Federico! ¡Es momento de bailar!#speaker:1
¡Oye cómo la música se apodera de tu cuerpo! 
¡Pasa a la fiesta! ¡Te lo has ganado!
~NextLevel("none")
->DoorFunctions