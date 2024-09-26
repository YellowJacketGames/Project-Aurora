//NIVEL 1: BARCO
//INTERACCIÓN CON CAPITAN
->DialogoCapitanSellarPasaporte

EXTERNAL CheckIfHasItem(string itemName)
EXTERNAL NextLevel(string none)

===DialogoCapitanSellarPasaporte===
Buenos días chico! Parece que estas listo para llegar a la gran ciudad! Tienes encima tu pasaporte? Habrá que sellártelo no?  #speaker:1
*Así es, séllemelo por favor #take_item:obj_Pasaporte_key #speaker:0
->DialogoCapitanSellarPasaporte1
*Lo cierto es que no me apetece sellarlo aún #speaker:0
->DialogoCapitanSellarPasaporte2



===DialogoCapitanSellarPasaporte1===
Sin problema! aquí lo tienes. Ya puedes ir a la gran ciudad! #give_item:obj_PasaporteSellado_key #speaker:1
~NextLevel("none")
->END
===DialogoCapitanSellarPasaporte2===
Pues te aguantas, toma sello#take_item:obj_Pasaporte_key #give_item:obj_PasaporteSellado_key #speaker:1
~NextLevel("none")
->END



// #take_item:obj_Pasaporte_key

// #give_item:obj_Pasaporte_sellado_key