//NIVEL 1: BARCO
//INTERACCIÓN CON CAPITAN
->DialogoColin

EXTERNAL CheckIfHasItem(string itemName)
EXTERNAL NextLevel(string none)

===DialogoColin===
Pero..., ¡si es mi amigo Colin!#speaker:0
*¡No lo hagas!#speaker:0
->Nivel_9_01
*¿Qué tal Colin? ¿Tomando el fresco?#speaker:0
->Nivel_9_01



===Nivel_9_01===
Aparta, hispano. No vas a conseguir impedirlo.#speaker:1
Colin, solo quiero charlar un rato contigo.#speaker:0
¡Déjame, Federico! No aguanto más.#speaker:1
*¿Y quién aguanta esta vida?#speaker:0
->Nivel_9_03
*¡Piensa en tu familia!#speaker:0
->Nivel_9_02A

===Nivel_9_02A===
¡Lo hago por ellos! ¡Por mi familia! ¡Les he fallado!#speaker:1
Hagamos lo que hagamos, siempre fallaremos a nuestra familia.Espera tanto de nosotros, que siempre resultará poco lo que consigamos#speaker:0
->Nivel_9_03

===Nivel_9_03===
¿Sientes lo mismo?#speaker:1
Creo que no hay hombre o mujer de más de veinte años que no sienta lo mismo.#speaker:0 
Lo he perdido todo. #speaker:1
*¿Alguna vez tuvimos algo?#speaker:0
->Nivel_9_04
*Mejor, así vas más ligero.#speaker:0
->Nivel_9_04

===Nivel_9_04===
Federico, todo. La Bolsa ha explotado. Todos hemos perdido todo. Volveremos a las fábricas. A la calle. Se acabó el jazz. #speaker:1
Que reine el silencio de metal sobre los tejados y azoteas.#speaker:0
*¿Sabes qué es lo que más hecho de menos de Granada?#speaker:0
->Nivel_9_05
*Nunca me gustó el jazz.#speaker:0
->Nivel_9_06

===Nivel_9_05===
¿El qué?#speaker:0
El silencio de mi huerta, de la Huerta de San Vicente. Escuchar el cri-cri de las margaritas.#speaker:0
Federico, las margaritas no hacen cri-cri.#speaker:1
Eso es porque nunca te has parado a escuchar el susurro de las margaritas o el coro de las dalias.#speaker:0
Nunca lo había pensado así.#speaker:1
->Nivel_9_07

===Nivel_9_06===
¿Por qué? Es el alma de Nueva York.#speaker:1
Porque habéis inventado el jazz para dejar de escuchar los gritos.#speaker:0
¿En serio piensas eso?#speaker:1
Nueva York se muere de dolor y habéis inventado el jazz para no escuchar ladrar a los perros que avisan de la muerte.#speaker:0
Nunca lo había pensado así.#speaker:1
->Nivel_9_07

===Nivel_9_07===
No sé, Federico... No veo salida. Me siento ya muerto. Me siento asesinado por el cielo.#speaker:1
Asesinado por el cielo...#speaker:0
Tropezando con mi rostro distinto cada día.#speaker:0
Eso es.#speaker:1
Con los animalitos de cabeza rota y el niño con el blanco rostro de huevo.#speaker:0
Lo del niño de huevo no lo entendí, pero seguro que también...#speaker:1
Da igual que no lo entiendas. No entender significa seguir buscando. Buscar significa seguir vivo.#speaker:0
Eh..., ¡guau!#speaker:1
*¡Miau!#speaker:0
->Nivel_9_08
*¡Pío, pío!#speaker:0
->Nivel_9_08

===Nivel_9_08===
¡Ja, ja, ja! Federico siempre Federico.#speaker:1
Eso pensaba hace un rato.#speaker:0
¿Y qué resolviste?#speaker:1
Te lo cuento con una copa de Jerez que sigo teniendo en mi habitación.#speaker:0
¿Y la ley seca?#speaker:1
Eso es un oxímoron. Una ley puede ser seca como un grito silencioso.#speaker:0
¡Y solo quiero gritar! ¡GRITAR!#speaker:1
Eso es, amigo. ¡GRITA!#speaker:0
¡AHHHHHHHHH!#speaker:1
¡AHHHHHHHHH!#speaker:0
Mucho mejor. ¿Bajamos?#speaker:1
Bajamos, pero espera que recupere el aliento. Las escaleras me dejaron KO...#speaker:0
¿Escaleras?  ¿Por qué no usaste el ascensor?#speaker:1
¿Hay ascensor? #speaker:0
Un momento...#speaker:0
¡AHHHHHHHHH!#speaker:0
Ya está.#speaker:0
Tienes razón: Federico siempre Federico. ¡Ja, ja, ja!#speaker:1
~NextLevel("none")
->END
// #take_item:obj_Pasaporte_key

// #give_item:obj_Pasaporte_sellado_key