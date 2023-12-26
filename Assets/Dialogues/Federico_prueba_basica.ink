//Federico prueba basica 

//NIVEL 2: PUERTO
-> puerto

=== puerto ===
//Federico y Fernando de los Ríos salen del barco.
//En la entrada del puerto, se paran a hablar.
¿Esto es Nueva York? ¡No lo puedo creer! ¡Si toda Granada cabe en una calle! #speaker:Federico 
+ Mejor me vuelvo a España, don Fernando #speaker:Federico 
    Esta ciudad me da miedo. #speaker:Federico 
    -> miedo
+ [¡Quiero perderme por sus calles!] #speaker:Federico -> avance 


=== miedo ===
¿Cómo que miedo? ¡Vamos, Federico! ¡Esto es el ombligo del mundo! #speaker:Fernando 

+ Mejor me cojo el billete de vuelta #speaker:Federico 
    -> end


=== avance ===
¡Claro que sí! ¡Corre! ¡La ciudad es tuya! #speaker:Fernando
//Federico se despide de don Fernando y continúa el juego hablando con otro personaje. 
-> END

=== end
//Federico coge el barco de vuelta. GAME OVER.
-> END


