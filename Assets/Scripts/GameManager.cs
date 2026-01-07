using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject enemigoTemp;
    private int beats = 0;

    private float enemyChance = 1f / 2.5f; //40%
    private float positionChance = 1f / 2f; //50%

    public GameObject gameOverPanel;
    public TextMeshProUGUI GameOverText;
    public Button Reiniciar;

    public bool gameOverActivo = false;

    private int tipo; //Tipo de enemigo
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

	// VIDA
	[SerializeField] private int vidaMaxima = 3;
	private int vidaActual;
	[SerializeField] private TextMeshProUGUI VidaText;

	// PUNTUACIÓN
	private int puntuacion = 0;
	[SerializeField] private TextMeshProUGUI ScoreText;
    private List<int> puntuaciones = new List<int>() {100, 300, 2400};
    [SerializeField] private TextMeshProUGUI puntosText;

	// COMBO POR SUPERVIVENCIA
	[SerializeField] private float tiempoSinGolpeParaCombo = 2f;
	private float temporizadorSinGolpe = 0f;
	private int multiplicadorSupervivencia = 1;
	[SerializeField] private TextMeshProUGUI comboText;



	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        if (gameOverPanel != null)
            {  gameOverPanel.SetActive(false); }

        if (Reiniciar != null) 
            { Reiniciar.onClick.AddListener(ReiniciarEscena); }

        secondsPerBeat = 60.0 / BPM;

		vidaActual = vidaMaxima;
		ActualizarVidaUI();
		ActualizarScoreUI();

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

        //Reiniciar partida sin quieres pulsar el botón
        if (gameOverActivo)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) { ReiniciarEscena(); }
        }

        //Temporizador para el combo
		if (!gameOverActivo)
		{
			temporizadorSinGolpe += Time.unscaledDeltaTime;

			if (temporizadorSinGolpe >= tiempoSinGolpeParaCombo)
			{
				temporizadorSinGolpe = 0f;
				multiplicadorSupervivencia *= 2;

				Debug.Log("Multiplicador de supervivencia: x" + multiplicadorSupervivencia);
			}
		}

		if (comboText != null)
		{
			comboText.text = "x" + multiplicadorSupervivencia;
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
        beats++;
        //Llamar funciones secundarias
        if (beats % 2 == 0)
        {
            On2Beat();
        }
        if (beats % 4 == 0)
        {
            On4Beat();
        }
        //Avanzar enemigos
        Enemigo[] enemigos = Object.FindObjectsByType<Enemigo>(FindObjectsSortMode.None);
        foreach (Enemigo e in enemigos)
        {
            e.Avanzar();
        }
    }

    void On2Beat()
    {
        //Spawneo de enemigos
        if (Random.Range(0.0f, 1.0f) < enemyChance)
        {
            enemigoTemp = Instantiate(enemigo);
            Tipo();
            if (Random.Range(0.0f, 1.0f) < positionChance)
            {
                enemigoTemp.transform.position = new Vector3(7, transform.position.y, transform.position.z);
            }
            else
            {
                enemigoTemp.transform.position = new Vector3(-7, transform.position.y, transform.position.z);
            }
        }
    }

    void On4Beat()
    {
		if (gameOverActivo) { return; }
		//Añadir aquí código que corre cada 4 beats (un compás entero)
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

	public void QuitarVida(int cantidad = 1)
	{
		temporizadorSinGolpe = 0f;
		multiplicadorSupervivencia = 1;

		if (gameOverActivo) return;

		vidaActual -= cantidad;
		if (vidaActual < 0) vidaActual = 0;

		ActualizarVidaUI();

		if (vidaActual <= 0)
		{
			GameOver();
		}
		
	}

	public void SumarPuntos(int puntos)
	{
		puntuacion += puntos * multiplicadorSupervivencia;

		ActualizarScoreUI();
	}

	private void ActualizarVidaUI()
	{
		if (VidaText != null)
			VidaText.text = "VIDA: " + vidaActual;
	}

	private void ActualizarScoreUI()
	{
		if (ScoreText != null)
			ScoreText.text = "SCORE: " + puntuacion;
	}

	public void GameOver()
    {
		InsertarOrdenado(puntuaciones, puntuacion);

		if (gameOverActivo) { return; }
        gameOverActivo = true;

		Time.timeScale = 0f;

		if (musicSource != null && musicSource.isPlaying)
		{
			musicSource.Stop();
		}


		if (gameOverPanel != null) { gameOverPanel.SetActive(true); }

        if (GameOverText != null) 
        { GameOverText.text = "GAME OVER\n\n" + "PUNTUACIÓN: " + puntuacion;
			puntosText.text = "Récord";
            puntosText.enabled = true;
			int limite = Mathf.Min(10, puntuaciones.Count);
			for (int i = puntuaciones.Count - 1; i >= puntuaciones.Count - limite; i--)
			{
                puntosText.text += "\n" + puntuaciones[i];
            }
        }
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    int OrdenarPuntuacion(List<int> puntuaciones, int puntuacion)
    {
        int izquierda = 0;
        int derecha = puntuaciones.Count - 1;

        while (izquierda <= derecha)
        {
            int medio = (izquierda + derecha) / 2;

            if (puntuaciones[medio] == puntuacion)
            {
                return medio;
            }

            if (puntuaciones[medio] < puntuacion)
            {
                izquierda = medio + 1;
            }
            else
            {
                derecha = medio - 1;
            }
        }
		return izquierda;
	}

    void InsertarOrdenado(List<int> puntuaciones, int puntuacion)
    {
        int posicion = OrdenarPuntuacion(puntuaciones, puntuacion);
        puntuaciones.Insert(posicion, puntuacion);
    }
}
