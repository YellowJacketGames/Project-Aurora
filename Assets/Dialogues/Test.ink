EXTERNAL CheckIfHasItem(string itemID)
VAR hasItem = false

-> main

== main ==

#speaker:caja
¡Hola! Esto es una prueba de dialogo.
Prueba a hablar tú, creo que ya deberías poder 
#speaker:player
Anda ahora puedo hablar yo también.
#speaker:caja
Bien, entonces funciona 
Debería ir cambiando de línea en linea para comprobar que esto funciona.
Ahora si que tengo algo más que decir
Voy a proponerte una serie de decisiones para probar como se implementa este sistema
Dime, ¿cuál de estos dos colores te gusta más?

~CheckIfHasItem("obj_Lilac Flower_key")

{hasItem:
    * [Lila]
    Creo que el lila me gusta más  #speaker:player
    A mí también me gusta mucho ese color.#speaker:caja
    Como has elegido la opción que me gusta te voy a dar un objeto
    Espero que te guste
    Aquí tienes #give_item:obj_Lilac Flower_key
    Bien eso era todo, más adelante vamos a ver si implementamos variables en ink
    -> END
    
    * [Azul]
    Creo que el azul me gusta más #speaker:player
    Meh, podría ser mejor. #speaker:caja
    Bien eso era todo, más adelante vamos a ver si implementamos variables en ink
    -> END
    
    * [Oye esto lo he vivido ya]
    Oye de esto ya hemos hablado antes #speaker:player
    Así es, era para comprobar si tenías el objeto que había usado antes #speaker:caja
    Ahora la voy a coger de vuelta si no te importa #take_item:obj_Lilac Flower_key
    ¿Un saludo!
    -> END
    
  - else:
    * [Lila]
    Creo que el lila me gusta más  #speaker:player
    A mí también me gusta mucho ese color.#speaker:caja
    Como has elegido la opción que me gusta te voy a dar un objeto
    Espero que te guste
    Aquí tienes #give_item:obj_Lilac Flower_key
    Bien eso era todo, más adelante vamos a ver si implementamos variables en ink
    -> END
    
    * [Azul]
    Creo que el azul me gusta más #speaker:player
    Meh, podría ser mejor. #speaker:caja
    Bien eso era todo, más adelante vamos a ver si implementamos variables en ink
    -> END

}

 

