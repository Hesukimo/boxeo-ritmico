using UnityEngine;

public class Jugador : MonoBehaviour
{
    //Definiciones
    private float HitCooldown = 0f;
    private float HitCooldownTime = 1f; //En segundos
    private float DuracionPuñetazo = 0.5f;
    private float TimerPuñetazo = 0f;

    public bool ColorVerde = true; //Falso representa el amarillo

    //Movimiento
    private float lerpSpeed = 0.25f; //Velocidad de la interpolación
    private Vector3 posObj; //Posición hacia la que nos queremos mover
    public Vector3 iniPos;
    private Vector3 posIzq;
    private Vector3 posDch;

    //Detecciones
    Enemigo enemigo;

    //Combos (mecánica experimental no implementada)
    private int combo = 0;
    private float combotimer = 0f;
    private float comboMaxDuration = 1.2f;

    ///[Referencias]
    private SpriteRenderer Sr;
    //[SerializeField] GameObject HitboxIzq;
    //[SerializeField] GameObject HitboxDcha;
    //Sprites
    [SerializeField] Sprite spriteFrente;
    [SerializeField] Sprite spriteAtaqueIzq;
    [SerializeField] Sprite spriteAtaqueDcha;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        //Guardar pos inicial
        iniPos = transform.position;
        posIzq = new Vector2(transform.position.x - 1f, transform.position.y);
        posDch = new Vector2(transform.position.x + 1f, transform.position.y);

        //Desactivamos hitboxes
        //HitboxDcha.SetActive(false);
        //HitboxIzq.SetActive(false);
        Sr = GetComponent<SpriteRenderer>();
        Sr.color = Color.green;

        // Este codigo es para que inicie de frente siempre
        Sr.sprite = spriteFrente;
    }

    // Update is called once per frame
    void Update()
    {
        //Mover a posición objetivo en todo momento
        if (transform.position != posObj)
        {
            {
                transform.position = Vector2.Lerp(transform.position, posObj, lerpSpeed);
            }
        }

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
            //Actualizamos contador de tiempo de puñetazo
            TimerPuñetazo -= Time.deltaTime;
        }
        else
        {
            //Al terminar la duración del puñetazo, desactivamos de nuevo las 2 hitbox de daño
            TimerPuñetazo = 0;
            //HitboxIzq.SetActive(false);
            //HitboxDcha.SetActive(false);
            posObj = iniPos;
            Sr.sprite = spriteFrente;
        }

        #region Experimental
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

        #endregion

        //Golpear y actualizar posición
        if (HitCooldown == 0f) { //Si el cooldown entre golpes ha terminado
            //Activar hitbox correspondiente
            if (Input.GetKeyDown(KeyCode.A))
            {
                HitCooldown = HitCooldownTime;
                //HitboxIzq.SetActive(true);
                TimerPuñetazo = DuracionPuñetazo;
                Sr.sprite = spriteAtaqueIzq;
                posObj = posIzq;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                HitCooldown = HitCooldownTime;
                //HitboxDcha.SetActive(true);
                TimerPuñetazo = DuracionPuñetazo;
				Sr.sprite = spriteAtaqueDcha;
                posObj = posDch;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo") && TimerPuñetazo > 0f)
        {
            enemigo = otro.GetComponent<Enemigo>();
            if (ColorVerde == enemigo.ColorVerde)
            {
                enemigo.Morir(true);
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
