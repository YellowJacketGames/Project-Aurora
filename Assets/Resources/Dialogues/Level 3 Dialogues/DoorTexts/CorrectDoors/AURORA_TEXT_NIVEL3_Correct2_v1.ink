->PUERTA_COCHE

EXTERNAL AdvanceDoors(string none)
EXTERNAL BeginTransition(string none)

==DoorFunctions==
~BeginTransition("none")
~AdvanceDoors("none")
->END

===PUERTA_COCHE===
¡Hola! ¿Es aquí la fiesta clandestina? #speaker:0
No, aquí solo arreglamos coches.#speaker:1
¡Vaya, pensé que había entendido cómo llegar! Adiós…#speaker:0
Ey, muchacho, disimula… Pero vas bien. No es aquí, pero vas por buen camino.#speaker:1
¡Anda con cuidado!
->DoorFunctions