using UnityEngine;

public class Jugador : MonoBehaviour
{
    //Definiciones
    private float HitCooldown = 0f;
    private float HitCooldownTime = 1f; //En segundos
    private float DuracionPuñetazo = 0.1f;
    private float TimerPuñetazo = 0f;

    public bool ColorVerde = true; //Falso representa el amarillo

    //Combos (mecánica experimental no implementada)
    private int combo = 0;
    private float combotimer = 0f;
    private float comboMaxDuration = 1.2f;

    //Referencias
    private SpriteRenderer Sr;
    [SerializeField] GameObject HitboxIzq;
    [SerializeField] GameObject HitboxDcha;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        HitboxDcha.SetActive(false);
        HitboxIzq.SetActive(false);
        Sr = GetComponent<SpriteRenderer>();
        Sr.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        //Cambio de colores al presionar espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ColorVerde)
            {
                Sr.color = Color.yellow;
            }
            else
            {
                Sr.color = Color.green;
            }
            ColorVerde = !ColorVerde;
        }

        //Cooldown entre pu�etazos
        if (HitCooldown > 0)
        {
            HitCooldown -= Time.deltaTime;
        } else
        {
            HitCooldown = 0f;
        }

        //Tiempo que se mantiene activa hitbox de puñetazo
        if (TimerPuñetazo > 0)
        {
            TimerPuñetazo -= Time.deltaTime;
        }
        else
        {
            //Al terminar la duración del puñetazo, desactivamos de nuevo las 2 hitbox de daño
            TimerPuñetazo = 0;
            HitboxIzq.SetActive(false);
            HitboxDcha.SetActive(false);
        }


        // --- COMBOS: por enemigos consecutivos derrotados

        if (combo > 0)
        {
            combotimer -= Time.deltaTime;
            if (combotimer <= 0)
            {
                combo = 0;
                Debug.Log("Combo perdido");
            }
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

                // Le añadido los comandos para detectar los combos por la izquierda
                combo++;
                combotimer = comboMaxDuration;
                Debug.Log("Combo actual: " + combo);
            }

            if (Input.GetKeyDown(KeyCode.D) && derecha)
            {
                Destroy(otro.gameObject);
                Debug.Log("Matado enemigo derecha");

				// Le añadido los comandos para detectar los combos por la derecha
				combo++;
				combotimer = comboMaxDuration;
				Debug.Log("Combo actual: " + combo);
			}
        }
    }
}
