using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource musica;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            musica.Play();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            musica.Stop();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            musica.Pause();
        }


    }
}
