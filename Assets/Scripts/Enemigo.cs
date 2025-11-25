using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] int speed = 1;
    private float lerpSpeed = 0.05f;
    private Vector2 posObj;
    private Transform jugador;
    private Vector2 direction;
    public bool ColorVerde;
    private SpriteRenderer spriteRenderer;
    private GameObject KO;

    //KO al morir los enemigos
    private GameObject KOTemp;
    [SerializeField] private GameObject KOEnemigo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ColorVerde = Random.value > 0.5f;
        if (ColorVerde)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.yellow;
        }
        posObj = transform.position;
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        direction = new Vector2(jugador.position.x - transform.position.x, jugador.position.y - transform.position.y);
        direction.Normalize();
        KO = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "KOJugador");
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
                Morir(false);
                KO.SetActive(true);
            }
        }
    }

    public void Avanzar()
    {
        posObj += new Vector2(direction.x * speed, 0);
    }

    public void Morir(bool Knock = true)
    {
        if (Knock)
        {
            KOTemp = Instantiate(KOEnemigo);
            KOTemp.transform.position = new Vector2(transform.position.x, 2.5f);
        }
        Destroy(this.gameObject);
    }
}
