//NIVEL 2: PUERTO
//INTERACCIÓN CON TAQUILLERO

EXTERNAL CheckIfHasItem(string item)
EXTERNAL CallEvent(int eventIndex)
EXTERNAL GoToNextObjective(string none)
VAR hasItem = false


->TAQUILLA
== TAQUILLA ==
¡Hola! Quiero comprar un billete. #speaker:0
-> 2TaquilleroPregunta

===2TaquilleroPregunta===
¿Qué línea quieres coger? #speaker:1
	*	Quiero coger la línea 5 #speaker:0
	-> 2LineaError
	*	Quiero coger la línea 3 #speaker:0
	-> 2LineaOk

===2LineaError===
¿A dónde quieres ir? #speaker:1
A la Residencia de Estudiantes. #speaker:0
Entonces no tienes que tomar esa línea...#speaker:1
-> 2TaquilleroPregunta

===2LineaOk===
¿A dónde quieres ir? #speaker:1
A la Residencia de Estudiantes.#speaker:0
La línea 3 te llevará directamente allí. #speaker:1
Un billete cuesta un dolar. 
~CheckIfHasItem("obj_Dolar_key")
{hasItem:
    Aquí tienes. #speaker:0 #take_item:obj_Dolar_key
    Aquí tienes tu ticket. #speaker:1 #give_item:obj_Ticket_key
    Tu tranvía está a punto de salir. 
    Corre o lo perderás. ¡Y no pasa otro hasta dentro de una hora!
    ~CallEvent(1)
    ~GoToNextObjective("none")
    -> END 
  - else:
    No tengo dinero...#speaker:0 
    -> 2TaquilleroSinDinero
}

===2TaquilleroSinDinero=== 
¿Cómo que no tienes dinero? #speaker:1
¿Por qué me molestas entonces?
¡Estoy harto de estos hispanos...! ¡Fuera! 
-> END 