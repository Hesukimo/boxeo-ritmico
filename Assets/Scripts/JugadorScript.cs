using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] GameObject HitboxIzq;
    [SerializeField] GameObject HitboxDcha;
    private float HitCooldown = 0f;
    private float HitCooldownTime = 1f; //En segundos
    private float DuracionPuñetazo = 0.1f;
    private float TimerPuñetazo = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HitboxDcha.SetActive(false);
        HitboxIzq.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HitCooldown > 0)
        {
            HitCooldown -= Time.deltaTime;
        } else
        {
            HitCooldown = 0f;
        }

        if (TimerPuñetazo > 0)
        {
            TimerPuñetazo -= Time.deltaTime;
        }
        else
        {
            TimerPuñetazo = 0;
            HitboxIzq.SetActive(false);
            HitboxDcha.SetActive(false);
        }

        if (HitCooldown == 0f) {
            //Activar hitbox
            if (Input.GetKeyDown(KeyCode.A))
            {
                HitCooldown = HitCooldownTime;
                HitboxIzq.SetActive(true);
                TimerPuñetazo = DuracionPuñetazo;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                HitCooldown = HitCooldownTime;
                HitboxDcha.SetActive(true);
                TimerPuñetazo = DuracionPuñetazo;
            }
        }
    }

    public void EnemigoCerca(bool derecha, Collider2D otro)
    {
        Debug.Log("Detectando");
        if (otro.CompareTag("Enemigo"))
        {
            Debug.Log("Es un enemigo");
            if (Input.GetKeyDown(KeyCode.A) && !derecha)
            {
                Destroy(otro.gameObject);
                Debug.Log("Matado enemigo izquierda");
            }

            if (Input.GetKeyDown(KeyCode.D) && derecha)
            {
                Destroy(otro.gameObject);
                Debug.Log("Matado enemigo derecha");
            }
        }
    }
}
