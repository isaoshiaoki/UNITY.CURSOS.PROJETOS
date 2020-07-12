using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject painelCompleto;

    public bool isPaused=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        if (isPaused)
        {
            painelCompleto.SetActive(false);
            isPaused = false;
        }
        else {
            painelCompleto.SetActive(true);
            isPaused = true;
        }
        
    }


}
