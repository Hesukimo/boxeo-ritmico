CARACTERÍSTICAS DEL JUEGO:

PARTICIPANTES:

Hesukimo - Héctor Suay;
LucyFRZ - Lucía Wen;
TeoNB - Teodosio Teofilo Nsue;
Mewis10 - Luis García;
SergiGnvs - Sergi Carpio;
gibalu - Gian Andres Bavaro;

{{RESUMEN}}
  Este proyecto de Unity contiene: un sistema de ritmo mediante el script GameManager, que genera enemigos al ritmo, y un objeto jugador con dos objetos hijo para las colisiones que puede destruir los objetos al acercarse.

[[SISTEMA DE RITMO]]

  El Proyecto contiene un sistema de ritmo bastante compacto que se puede usar para cualquier cosa y que está almacenado en el script del GameManager. Solo debes asignarle un BPM y automáticamente podrás usar las siguientes funciones:

  OnBeat(); Se llama cada vez que transcurre un beat de la canción. Se puede usar para hacer que cualquier cosa vaya al ritmo. Queda pendiente hacer una función más avanzada que pueda contar sin necesidad de hacerlo manualmente contadores de 2, 4 o incluso 8 beats.
  ComprobarRitmo(); Devuelve un valor entre 0 y 1. 0 significa que en ese momento se está en el contratiempo y 1 que estamos justo en pleno beat.

[[ENEMIGOS]]

  La generación de enemigos se lleva dentro de OnBeat(); desde el GameManager, siguiendo el beat de la canción. Esto es, cada vez que se llama OnBeat().
  Al hacerlo, se corre una probabilidad de 1/3 de generar un enemigo, seguida de una probabilidad de 1/2 que elige el lado (izquierda/derecha) del boxeador en el que se genera, y seguida de otra probabilidad de 1/2 para elegir el color del enemigo entre amarillo y verde.
  Los enemigos tienen una función para la muerte, que incluye un argumento para determinar si generar un pop-up de "KO!" o no.

  Morir(bool Knock = true); Instancia el pop-up de texto de "KO!" (en caso de ser Knock = true), referenciado en el Prefab del enemigo, y se destruye a si mismo.

[[GOLPES Y COLISIONES]]

  El objeto jugador tiene dos objetos hijos, que son dos GameObject con un Collider2D, y un script DetectarColision.cs, que destruye todos los objetos de tag "Enemigo" en su interior al instante si la variable ColorVerde del enemigo tiene el mismo valor que la variable ColorVerde de JugadorScript.cs. 
  Las dos hitbox (izquierda y derecha) son activadas y desactivadas por el script de su padre, JugadorScript.cs, dependiendo de si se ha presionado la tecla de golpe izquierdo o derecho. En el script se puede customizar el cooldown entre golpes y el tiempo que dura activa la hitbox con las variables DuracionPuñetazo y HitCooldownTime.
  El jugador puede cambiar su variable ColorVerde que representa el color del boxeador (true = verde, false = amarillo), que se invierte al presionar el botón espacio.

[[SHOWCASE]]

  El jugador acertando golpes (mismo color que enemigo):
  ![Stock-1764115106266](https://github.com/user-attachments/assets/7ff3d229-99f0-4ad7-ac67-e87a0152a330)

  El jugador fallando golpes (color diferente enemigo):
  ![Color-ezgif com-crop](https://github.com/user-attachments/assets/c28feac7-7fb4-48d9-ae87-881cda7453f0)


