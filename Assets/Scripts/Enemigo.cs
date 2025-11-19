using Unity.VisualScripting;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] int speed = 1;
    private int vida;
    private Transform jugador;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        vida = 100;
        direction = new Vector2(jugador.position.x - transform.position.x, jugador.position.y - transform.position.y);
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(jugador.position.x - transform.position.x) < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Avanzar()
    {
        transform.position += new Vector3(direction.x * speed, 0, 0);
    }
}
