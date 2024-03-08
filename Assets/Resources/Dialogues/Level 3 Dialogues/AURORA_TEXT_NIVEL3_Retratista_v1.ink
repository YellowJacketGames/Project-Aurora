->PUERTA_RETRATO

EXTERNAL PlayAudio(string audioName)
EXTERNAL SetNewSpeaker(string speakerName)
EXTERNAL ChangeDoorDialogue(string dialogueFilePath)
EXTERNAL BeginTransition(string none)
EXTERNAL ResetDoors(string none)
EXTERNAL CallEvent(int eventIndex)

==DoorFunctions==
~ResetDoors("none")
~PlayAudio("PoliceSiren")
~BeginTransition("none")
~ChangeDoorDialogue("Dialogues/Level 3 Dialogues/DoorTexts/WrongDoors/AURORA_TEXT_NIVEL3_BookWrong_v1")
~CallEvent(0)
->END

===PUERTA_RETRATO===
~SetNewSpeaker("Retratista")
¡Hola! ¿Es aquí la fiesta clandestina?#speaker:0
¡Buen amigo! ¡Me ha encontrado!#speaker:1 
¿Qué imagen tiene en la mente ahora?#speaker:0
No es en mi mente. Está en mi pincel, extensión viva de mi cuerpo inerte.#speaker:1 
¿Cuál de mis amigos le ha visitado en las sombras? ¿De cuál me revelará su destino?#speaker:0
Tome, seguro que lo reconoce.#give_item:obj_Retrato_key
¡Que me aspen! Si es Dalí.#speaker:0
¡Parece todo un marqués!
Gala será su única compañía#speaker:1 
y galante pintor será marqués.
Loco incomprendido en el mundo,
luz del alba en Cadaqués. 
¿Me dirás ya cuál es mi destino?#speaker:0 
Reúne los diez retratos de tus amigos y prometo revelarte tu verdad.#speaker:1  
Guarda esa imagen en el museo de la escarcha, amigo. 
Y ahora, ¡a correr! ¡Policía!
->DoorFunctions
