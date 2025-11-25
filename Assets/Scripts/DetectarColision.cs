using System.Linq;
using UnityEngine;

public class DetectarColision : MonoBehaviour
{
    public GameObject Jugador; //Referencia al padre
    private Jugador JugadorScript;
    private Enemigo enemigo;
    public bool Derecha; //Somos hitbox izquierda o derecha? (poner en Inspector)

    private void Awake()
    {
        //Coger referencias
        JugadorScript = Jugador.GetComponent<Jugador>();
    }

    //Destruimos todos los Enemigos del mismo color que el jugador dentro de la hitbox
    private void OnTriggerStay2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo") && JugadorScript.ColorVerde == otro.gameObject.GetComponent<Enemigo>().ColorVerde)
        {
            enemigo = otro.GetComponent<Enemigo>();
            enemigo.Morir();
        }
    }
}
