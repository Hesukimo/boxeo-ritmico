using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Sistema de ritmo
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public float BPM;

    private double secondsPerBeat;
    private double dspStartTime;
    private double songTime;
    private int lastBeat = -1;
    private double lastBeatTime = 0.0;
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
        //Meter cosas aquí que vayan al ritmo de la música
        transform.position += Vector3.up; //(Prueba)
    }
}
