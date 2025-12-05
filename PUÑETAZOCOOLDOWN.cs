using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUÑETAZOCOOLDOWN : MonoBehaviour
{
    public float cooldown;

    public float cronometro;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cronometro += Time.deltaTime;
        if (cronometro >= cooldown) 
        {
            cooldown = 0;
        }
    }
}
