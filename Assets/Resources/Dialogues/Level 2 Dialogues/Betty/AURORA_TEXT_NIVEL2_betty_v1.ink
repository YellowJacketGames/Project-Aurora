
//NIVEL 2: PUERTO
//INTERACCIÓN CON BETTY

EXTERNAL CheckIfHasItem(string itemName)
EXTERNAL CallEvent(int eventIndex)
EXTERNAL GoToNextObjective(string none)
EXTERNAL ChangeDialogue(string dialoguePath)
EXTERNAL CheckIfHasInteracted(string none)
EXTERNAL HasInteractedCheck(string none)
VAR hasItem = false
VAR hasInteracted = false

~CheckIfHasItem("obj_Periodico_key")
~CheckIfHasInteracted("none")

{hasItem:
    ->2DialogoBettyPeriodico
  - else:
        {hasInteracted:
            ->Revisita_Betty01
        - else:
        ~HasInteractedCheck("none")
            ->DialogoBetty
        }
}

===DialogoBetty===
~CallEvent(2)
¡Hola! #speaker:0
Hi, darling!  #speaker:1
	*	Hello, señorita! #speaker:0
	-> 2DialogoBettyAvance1
	*	¿Perdón? ¿Qué dice? #speaker:0
	-> 2DialogoBettyAvance2

===2DialogoBettyAvance1===
Hi, what do you want, sir? #speaker:1
Ah, no, no, no... Disculpe, pero... Mi inglés comenzó y acabó con ese "hello". #speaker:0
¡Ah! No se preocupe. #speaker:1
En Nueva York se hablan todas las lenguas del mundo.
->2DialogoBettyAvance3

===2DialogoBettyAvance2===
¡Aggg! ¡Otro hispano! #speaker:1
De la mismísima Hispania, ¡sí! #speaker:0
¿Por qué venís todos a Nueva York? #speaker:1 
->2DialogoBettyAvance3

===2DialogoBettyAvance3===
Nueva York, ¡Babilonia del siglo XX! #speaker:0
Perdóneme, pero estoy muy ocupada. #speaker:1
Vengo desde Wall Street buscando un periódico de hoy y no consigo encontrar uno.
*Todos tenemos un mal día hoy, ¿no? #speaker:0
->2DialogoBettyAvance4
*¿Un periódico? Debería ser algo sencillo, ¿no? #speaker:0
->2DialogoBettyAvance5

===2DialogoBettyAvance4===
Yo también tengo problemas... No sé si sabe dónde puedo conseguir un par de monedas. #speaker:0
Necesito comprar un billete para el tranvía y...
Lo siento, pero tengo lo justo para comprar el periódico. #speaker:1
*¡Esto es un atraco! Dame ese dinero. #speaker:0
->2DialogoBettyAvance402
*¿Si le consigo el periódico que necesita me daría el dinero? #speaker:0
->2DialogoBettyAvance6

===2DialogoBettyAvance5===
Últimamente todos están muy preocupados por la Bolsa y los periódicos se acaban antes de que amanezca. #speaker:1
->2DialogoBettyAvance4


===2DialogoBettyAvance402===
Pero, ¿qué estás diciendo? #speaker:1
Acabo de llegar a Nueva York, ¡necesito el maldito dinero! #speaker:0
Caballero, intente ser más educado en Nueva York o tendrá problemas... #speaker:1
*Disculpe, llevo dos semanas sin dormir bien... #speaker:0
->2DialogoBettyAvance403
*Estoy irritado... Necesito coger el tranvía. #speaker:0
->2DialogoBettyAvance403

===2DialogoBettyAvance403===
Le propongo un trato: ¿qué le parece lo siguiente? Usted me consigue un periódico y yo le daré esas monedas que necesita para el tranvía. #speaker:1
->2DialogoBettyAvance6

===2DialogoBettyAvance6===
Bueno...ehm...si es el periódico de hoy..., no tendría problema. #speaker:1
Consígame el periódico y le daré el dinero que necesita.

->2DialogoBettyAvance7

===2DialogoBettyAvance7===
Cuente con ello, señorita. #speaker:0
Por favor, llámeme Betty, señor hispano. #speaker:1
Federico, mi nombre es Federico. #speaker:0
¡¡¡El periódico!!! #speaker:1 
~GoToNextObjective("none")
->END



//DIALOGO SI YA TIENE EL PERIODICO 
->2DialogoBettyPeriodico

===2DialogoBettyPeriodico
#speaker:0
*¡Buenos días de nuevo, Miss Betty! #speaker:0
->2DialogoBettyPeriodico1
*Tome su maldito periódico. #speaker:0
->2DialogoBettyPeriodico2

===2DialogoBettyPeriodico2
Ahora deme mi maldito dinero... #speaker:0  #take_item:obj_Periodico_key
Aquí tiene sus malditos dólares. #speaker:1  #give_item:obj_Dolar_key
¡Vaya con el diablo! #speaker:0
No va conmigo quien con usted queda. #speaker:1 
~GoToNextObjective("none")
->END


===2DialogoBettyPeriodico1===
¡Fernando! ¡Ha vuelto! #speaker:1
(Qué alegría que nadie me conozca aquí...) #speaker:0
Federico, mi nombre es Federico.
¡Ups! Perdone, Federico, estoy algo nerviosa. Debo volver al banco o mi jefe me amonestará. #speaker:1
¿Ha conseguido mi periódico? 
*¡Por supuesto! #speaker:0
->2DialogoBettyPeriodicoOk
*¿Qué periódico? ¡Ah! ¡El periódico! ¡Ya sabía que algo se me olvidaba! #speaker:0
->2DialogoBettyPeriodico3

===2DialogoBettyPeriodico3===
¿En serio? ¿No lo ha traído? #speaker:1
¡Es broma! Un granadino siempre cumple sus promesas. #speaker:0
->2DialogoBettyPeriodicoOk

===2DialogoBettyPeriodicoOk
Aquí tiene su periódico. #take_item:obj_Periodico_key #speaker:0
¡Oh! ¡Mil gracias! Al menos, hoy, Mr. Hackworth no me gritará. #speaker:1
Tome, aquí tiene el dinero que le prometí. #give_item:obj_Dolar_key
*Muchas gracias! ¡Me voy corriendo! #speaker:0
~GoToNextObjective("none")
->END
*¿Ha dicho dicho Mr. Hackworth? #speaker:0
->2DialogoBettyPeriodico4

===2DialogoBettyPeriodico4
Sí, mi jefe. Mister Campbell Hackworth-Jones. #speaker:1
¡No puedo creerlo! ¡Colin! #speaker:0
Así solo lo llama su esposa... #speaker:1 
¡Qué granuja! ¿Colin se ha casado? Lo conocí en Granada la primavera pasada. #speaker:0
Disculpe, pero tengo que irme o Mr. Hackworth... #speaker:1
*No se preocupe, dígale que ha estado con García Lorca... #speaker:0
->2DialogoBettyPeriodico5
*Bah, ya sabe que perro ladrador, poco mordedor. #speaker:0
->2DialogoBettyPeriodico6


===2DialogoBettyPeriodico5===
No está de buen humor... Últimamente, todos andan muy preocupados con la Bolsa...). Será mejor que me vaya cuanto antes. Pero venga a verlo cuando quiera. #speaker:1
->2DialogoBettyPeriodicoFIN


===2DialogoBettyPeriodico6===
No entiendo a los hispanos cuando hablan de esa forma... Será mejor que me vaya...#speaker:1
->2DialogoBettyPeriodicoFIN


===2DialogoBettyPeriodicoFIN===
¡Por favor! Dígame dónde puedo encontrarlo.#speaker:0
En Central Bank, en la oficina de Beverly Bogarty & Co. #speaker:1
Tome, si viene a verlo, entregue esta tarjeta de visita en la entrada del banco. 
#give_item:obj_Tarjeta_key
¡Gracias, Mr. Federico!

~ChangeDialogue("Dialogues/Level 2 Dialogues/Betty/AURORA_TEXT_NIVEL2_BettyFinal_v1")
~GoToNextObjective("none")
->END


===Revisita_Betty01===
Hola, señor Hispano.#speaker:1
¿Ha conseguido el periódico que necesito? 
*Mmm…aún no, pero estoy en ello.#speaker:0
->Revisita_Betty02
*Resulta imposible. Nada es gratis en Nueva York.#speaker:0
->Revisita_Betty03

===Revisita_Betty02===
¡Vamos, ánimo! Ayúdeme y le ayudaré.#speaker:1
->END

===Revisita_Betty03===
Nada es gratis en Nueva York…, pero siempre hay una puerta abierta en la ciudad que nunca duerme. ¡Vamos, vuelva con el periódico!#speaker:1
->END

