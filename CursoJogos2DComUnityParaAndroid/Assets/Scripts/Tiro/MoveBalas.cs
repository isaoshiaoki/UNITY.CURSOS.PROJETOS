using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBalas : MonoBehaviour
{
    public float velocidade = 2.5f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(velocidade *Time.deltaTime,0));  
    }
}
