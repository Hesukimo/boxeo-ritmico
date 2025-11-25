using System.Linq;
using UnityEngine;

public class DetectarColision : MonoBehaviour
{
    public GameObject Jugador; //Referencia al padre
    private Jugador JugadorScript;
    private GameManager GM;
    public bool Derecha; //Somos hitbox izquierda o derecha? (poner en Inspector)

    //KO al morir los enemigos
    private GameObject KOTemp;
    [SerializeField] private GameObject KO;

    private void Awake()
    {
        JugadorScript = Jugador.GetComponent<Jugador>();
        GM = Jugador.GetComponent<GameManager>();

    }
    private void OnTriggerStay2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo") && JugadorScript.ColorVerde == otro.gameObject.GetComponent<Enemigo>().ColorVerde)
        {
            Destroy(otro.gameObject);
            KOTemp =  Instantiate(KO);
            KOTemp.transform.position = GM.enemigoTemp.transform.position;
        }
    }
}
