using Unity.VisualScripting;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] int speed = 1;
    private float lerpSpeed = 0.05f;
    private Vector2 posObj;
    private Transform jugador;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posObj = transform.position;
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        direction = new Vector2(jugador.position.x - transform.position.x, jugador.position.y - transform.position.y);
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {

        if ((Vector2)transform.position != posObj)
        {
            {
                transform.position = Vector2.Lerp(transform.position, posObj, lerpSpeed);
            }

            if (Mathf.Abs(jugador.position.x - transform.position.x) < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Avanzar()
    {
        posObj += new Vector2(direction.x * speed, 0);
    }
}
