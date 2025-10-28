using UnityEngine;

public class Juego : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Personaje heroe;
    public Enemigo goblin;
    public JefeFinal JefeFinal;
    void Start()
    {
        heroe.MostrarInfo();
        goblin.MostrarInfo();
        //JefeFinal.MostrarInfo();

        goblin.Atacar(heroe);
        heroe.Atacar(goblin);
        //JefeFinal.Atacar(heroe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
