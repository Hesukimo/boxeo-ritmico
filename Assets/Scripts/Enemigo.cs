using Unity.VisualScripting;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] int speed = 1;
    private float lerpSpeed = 0.05f;
    private Vector3 posObj;
    private int vida;
    private Transform jugador;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posObj = transform.position;
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        vida = 100;
        direction = new Vector2(jugador.position.x - transform.position.x, jugador.position.y - transform.position.y);
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != posObj)
        {
            transform.position = Vector3.Lerp(transform.position, posObj, lerpSpeed);
        }

        if (Mathf.Abs(jugador.position.x - transform.position.x) < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Avanzar()
    {
        posObj += new Vector3(direction.x * speed, 0, 0);
    }
}
