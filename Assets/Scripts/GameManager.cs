using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Sistema de ritmo
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public float BPM;
    [SerializeField] public GameObject jugador;
    [SerializeField] public GameObject enemigo;

    private double secondsPerBeat;
    private double dspStartTime;
    private double songTime;
    private int lastBeat = -1;
    private double lastBeatTime = 0.0;
    private bool playerUp = true;
    private GameObject enemigoTemp;

    private float enemyChance = 1f / 3f;
    private float positionChance = 1f / 2f;

    private int tipo; //Tipo de enemigo
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsPerBeat = 60.0 / BPM;

        // Timear inicio de la canción un pelín más tarde
        dspStartTime = AudioSettings.dspTime + 1f;
        musicSource.loop = true; // Poner canción en bucle
        musicSource.PlayScheduled(dspStartTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Sistema de ritmo
        songTime = AudioSettings.dspTime - dspStartTime;
        if (songTime < 0)
            return;
        int currentBeat = (int)(songTime / secondsPerBeat);
        if (currentBeat > lastBeat)
        {
            lastBeat = currentBeat;
            lastBeatTime = songTime;
            OnBeat();
        }
    }

    //Función que comprueba lo cerca que se está de un beat y lo devuelve de 0 a 1, 0 = lo más alejado del beat posible (contratiempo) y 1 exacto
    public double ComprobarRitmo()
    {
        double recentTime = songTime - lastBeatTime; //Tiempo que ha pasado desde el último beat
        return System.Math.Abs(1 - System.Math.Min(recentTime, (lastBeatTime + secondsPerBeat) - recentTime) / (secondsPerBeat / 2d));
    }
    void OnBeat()
    {
        if (Random.Range(0.0f, 1.0f) < enemyChance)
        {
            enemigoTemp = Instantiate(enemigo);
            Tipo();
            if (Random.Range(0.0f, 1.0f) < positionChance)
            {
                enemigoTemp.transform.position = new Vector3(7, transform.position.y, transform.position.z);
            } else
            {
                enemigoTemp.transform.position = new Vector3(-7, transform.position.y, transform.position.z);
            }
        }

        Enemigo[] enemigos = Object.FindObjectsByType<Enemigo>(FindObjectsSortMode.None);
        foreach (Enemigo e in enemigos)
        {
            e.Avanzar();
        }
    }
    //Función para definir el tipo del enemigo al aparecer
    private void Tipo()
    {
        tipo = Random.Range(1, 3);
            if(tipo == 1)
            {
                enemigoTemp.GetComponent<SpriteRenderer>().sprite = sprite1;
            }
            if (tipo == 2)
            {
                enemigoTemp.GetComponent<SpriteRenderer>().sprite = sprite2;
            }
    }
}
