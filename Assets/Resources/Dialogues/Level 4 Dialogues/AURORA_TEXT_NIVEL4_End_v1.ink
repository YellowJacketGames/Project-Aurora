->4Harlem
EXTERNAL SetNewSpeaker(string speakerName)
EXTERNAL NextLevel(string none)
===4Harlem===
~SetNewSpeaker("ReyDeHarlem")
¡Negros! ¡Negros! ¡Es la hora de los negros!#speaker:1
*¿Quién es usted?#speaker:0
->4Harlem0101
*Ehm... ¿Por qué lleva usted una cuchara colgada de la oreja?#speaker:0 
Federico
->4Harlem0102

===4Harlem0101===
El Rey de Harlem no llora.#speaker:1 
El Rey de Harlem desemboca el mar por sus mejillas.
->4Harlem02

===4Harlem0102===
El Rey de Harlem lleva una cuchara para arrancar los ojos a los cocodrilos de la ciudad.#speaker:1
->4Harlem02

===4Harlem02===
*¡Así que usted es un rey! ¡Salve! ¡Larga vida al Rey!#speaker:0 
->4Harlem02001 
*No pensé que hubiera reyes vivos en el Nuevo Mundo...#speaker:0
->4Harlem02001

===4Harlem02001===
El Rey de Harlem está vivo porque está muerto.#speaker:1
*¿Y dónde se encuentra su reinado, oh, Rey de Harlem?#speaker:0
->4Harlem0201
*Pero la cuchara colgando de la oreja...#speaker:0
->4Harlem0202

===4Harlem0201===
Harlem es la capital negra del mundo.#speaker:1
Todos los negros llevan a Harlem en su piel.
->4Harlem03

===4Harlem0202===
La cuchara es la muerte. La cuchara es el símbolo de la opresión.#speaker:1 
Somos negros. Somos comida. 
Solo somos una cuchara en la gran máquina que mueve el mundo. 
->4Harlem03

===4Harlem03===
*Perdone, Su Majestad, pero debo huir de la policía…#speaker:0
->4Harlem0301
*No me entero de nada, ¡Hasta otra, Su Majestad!#speaker:0
->4Harlem0301

===4Harlem0301===
¡Es preciso cruzar los puentes!#speaker:1 
¡Más saltos no, por favor!#speaker:0
Los puentes te llevan más allá.#speaker:1
¿Cómo? ¿Más allá de dónde?#speaker:0 
Más allá de ti. Donde tú eres lo que eres y no lo que crees ser.#speaker:1
No tengas miedo. Cruza el puente. 
*No necesito cruzar ningún puente.#speaker:0
¡Mírame! Federico sabe quién es.#speaker:0
->4Harlem04
*Federico no tiene miedo.#speaker:0
->4Harlem04

===4Harlem04===
Federico no es Federico.#speaker:1 
Federico se mira en el espejo y no se ve.
*¿Qué sabrás tú? ¡No me conoces!#speaker:0
->4Harlem05
*Es verdad. Soy un hombre sin reflejo.#speaker:0 
->4Harlem05

===4Harlem05===
¿Dónde está tu Luna de plata, Federico?#speaker:1 
¿Cómo? ¡Eso mismo creí leer en una valla publicitaria…!#speaker:0
Harlem te habla pero no lo escuchas.#speaker:1
La ciudad te habla pero no la ves.
Soy un Poeta perdido en Nueva York.#speaker:0 
Pero si cruzas los puentes, Harlem te espera.#speaker:1
No dudes en venir a verte a Harlem.
Harlem es la voz de los oprimidos. 
Harlem será la voz de Federico. 
Qué extraña conversación... No entendí nada, pero ahora siento que la ciudad me habla.#speaker:0 
Ahora siento que he aprendido el idioma de esta ciudad. 
Pero, sobre todo, siento que esta noche ha dado a luz un poema: “Ciudad sin sueño”.
~NextLevel("none")
 ->END
